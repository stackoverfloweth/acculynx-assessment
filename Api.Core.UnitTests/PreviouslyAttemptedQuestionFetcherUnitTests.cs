using Api.Contract;
using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class PreviouslyAttemptedQuestionFetcherUnitTests : UnitTestBase {
        [Fact]
        public void FetchQuestions_Always_CallsIAttemptRepositoryGetAttemptsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            repositoryMock.Verify(x => x.GetAttempts(userId), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIStackExchangeClientGetQuestionsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempts(It.IsAny<string>()))
                .Returns(attempts);
            var attemptIds = attempts.Select(attempt => attempt.QuestionId).ToList();

            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            clientMock.Verify(x => x.GetQuestions(attemptIds), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIAttemptedQuestionDtoAssemblerAssembleAttemptedQuestionsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempts(It.IsAny<string>()))
                .Returns(attempts);

            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetQuestions(It.IsAny<List<int>>()))
                .Returns(questionDtos);

            var assemblerMock = AutoFixture.Freeze<Mock<IAttemptedQuestionDtoAssembler>>();

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            assemblerMock.Verify(x => x.AssembleAttemptedQuestions(attempts, questionDtos), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_ReturnsResponseFromIAttemptedQuestionDtoAssembler() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var attemptedQuestionDtos = AutoFixture.CreateMany<AttemptedQuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IAttemptedQuestionDtoAssembler>>()
                .Setup(x => x.AssembleAttemptedQuestions(It.IsAny<IEnumerable<Attempt>>(), It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(attemptedQuestionDtos);

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            var response = fetcher.FetchQuestions(userId);

            // assert
            response.Should().BeEquivalentTo(attemptedQuestionDtos);
        }
    }

}
