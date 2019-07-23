using Api.Contract;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class AttemptSubmissionManagerUnitTests : UnitTestBase {
        [Fact]
        public void SubmitAttempt_Always_CallsIMapperMapAttemptOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            mapperMock.Verify(x => x.Map<Attempt>(attemptDto), Times.Once);
        }

        [Fact]
        public void SubmitAttempt_Always_CallsIAttemptScoreCalculatorCalculateScoreOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var attempt = AutoFixture.Create<Attempt>();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<Attempt>(It.IsAny<AttemptDto>()))
                .Returns(attempt);

            var calculatorMock = AutoFixture.Freeze<Mock<IAttemptScoreCalculator>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            calculatorMock.Verify(x => x.CalculateScore(attempt), Times.Once);
        }

        [Fact]
        public void SubmitAttempt_Always_AssignsUserIdAndScore() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var attempt = AutoFixture.Create<Attempt>();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<Attempt>(It.IsAny<AttemptDto>()))
                .Returns(attempt);

            var score = AutoFixture.Create<int>();
            AutoFixture.Freeze<Mock<IAttemptScoreCalculator>>()
                .Setup(x => x.CalculateScore(It.IsAny<Attempt>()))
                .Returns(score);

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            repositoryMock.Verify(x => x.InsertAttempt(It.Is<Attempt>(param => param.UserId == userId && param.Score == score)));
        }

        [Fact]
        public void SubmitAttempt_Always_CallsIAttemptRepositoryInsertAttemptOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var attempt = AutoFixture.Create<Attempt>();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<Attempt>(It.IsAny<AttemptDto>()))
                .Returns(attempt);

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            repositoryMock.Verify(x => x.InsertAttempt(attempt), Times.Once);
        }

        [Fact]
        public void SubmitAttempt_Always_CallsIMapperMapAttemptDtoOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var attempt = AutoFixture.Create<Attempt>();
            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();
            mapperMock
                .Setup(x => x.Map<Attempt>(It.IsAny<AttemptDto>()))
                .Returns(attempt);

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            mapperMock.Verify(x => x.Map<AttemptDto>(attempt), Times.Once);
        }

        [Fact]
        public void SubmitAttempt_Always_ReturnsResponseFromMapperMapAttemptDto() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var returnedAttemptDto = AutoFixture.Create<AttemptDto>();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<AttemptDto>(It.IsAny<Attempt>()))
                .Returns(returnedAttemptDto);

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            var response = manager.SubmitAttempt(attemptDto, userId);

            //  assert
            response.Should().Be(returnedAttemptDto);
        }
    }
}