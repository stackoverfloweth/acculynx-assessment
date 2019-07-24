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
        public void SubmitAttempt_Always_CallsIAttemptRepositoryGetAttemptOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            repositoryMock.Verify(x => x.GetAttempt(attemptDto.QuestionId, userId), Times.Once);
        }

        [Fact]
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNotNull_ReturnsNull() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            var previousAttempt = AutoFixture.Create<Attempt>();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(previousAttempt);

            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            var response = manager.SubmitAttempt(attemptDto, userId);

            //  assert
            response.Should().BeNull();
            mapperMock.Verify(x => x.Map<Attempt>(It.IsAny<AttemptDto>()), Times.Never);
        }

        [Fact]
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNull_CallsIMapperMapAttemptOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Attempt)null);

            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

            // act
            var manager = AutoFixture.Create<AttemptSubmissionManager>();
            manager.SubmitAttempt(attemptDto, userId);

            //  assert
            mapperMock.Verify(x => x.Map<Attempt>(attemptDto), Times.Once);
        }

        [Fact]
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNull_CallsIAttemptScoreCalculatorCalculateScoreOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Attempt)null);

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
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNull_AssignsUserIdAndScore() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Attempt)null);

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
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNull_CallsIAttemptRepositoryInsertAttemptOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Attempt)null);

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
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNull_CallsIMapperMapAttemptDtoOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Attempt)null);

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
        public void SubmitAttempt_WhenExistingAttemptCheckReturnsNull_ReturnsResponseFromMapperMapAttemptDto() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();
            var userId = AutoFixture.Create<string>();

            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns((Attempt)null);

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