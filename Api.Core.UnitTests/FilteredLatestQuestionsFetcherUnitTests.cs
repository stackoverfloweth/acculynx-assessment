using Api.Contract;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using Data.Entities;
using Data.Repositories;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class FilteredLatestQuestionsFetcherUnitTests : UnitTestBase {
        [Fact]
        public void FetchQuestions_Always_CallsIAttemptRepositoryGetAttemptsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(20).ToList();
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);


            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            repositoryMock.Verify(x => x.GetAttempts(userId), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIStackExchangeClientGetLatestQuestionsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(20).ToList();
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);


            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            clientMock.Verify(x => x.GetLatestQuestions(1), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIQuestionFilterFilterQuestionsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetLatestQuestions(It.IsAny<int>()))
                .Returns(questionDtos);

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(20).ToList();
            var filterMock = AutoFixture.Freeze<Mock<IQuestionFilter>>();
            filterMock
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);

            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            filterMock.Verify(x => x.FilterQuestions(questionDtos), Times.Once);
        }

        [Fact]
        public void FetchQuestions_WhenQuestionDtosReturnedIsGreaterThan20_ReturnsOnly20() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(RandomGenerator.Next(20, 100)).ToList();
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);

            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            var response = fetcher.FetchQuestions(userId);

            // assert
            response.Should().HaveCount(20);
        }

        [Fact]
        public void FetchQuestions_WhenAttemptRepositoryReturnsQuestionIds_ExcludesAttemptedQuestionIdsFromResult() {
            // arrange
            var userId = AutoFixture.Create<string>();
            var questionId = AutoFixture.Create<int>();

            var attempts = AutoFixture.Build<Attempt>()
                .With(x => x.QuestionId, questionId)
                .CreateMany()
                .ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempts(It.IsAny<string>()))
                .Returns(attempts);
            
            var previouslyAttemptedQuestions = AutoFixture.Build<QuestionDto>()
                .With(x => x.QuestionId, questionId)
                .CreateMany()
                .ToList();
            var notPreviouslyAttemptedQuestions = AutoFixture.CreateMany<QuestionDto>(20).ToList();
            previouslyAttemptedQuestions.InsertRange(RandomGenerator.Next(previouslyAttemptedQuestions.Count), notPreviouslyAttemptedQuestions);
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(previouslyAttemptedQuestions);

            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            var response = fetcher.FetchQuestions(userId);

            // assert
            response.Should().BeEquivalentTo(notPreviouslyAttemptedQuestions);
        }

        [Fact]
        public void FetchQuestions_WhenQuestionDtosReturnedIsLessThan20_RepeatsCallsUntil20IsReached() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();
            var filterMock = AutoFixture.Freeze<Mock<IQuestionFilter>>();

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(5).ToList();
            filterMock
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);

            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            fetcher.FetchQuestions(userId);

            // assert
            clientMock.Verify(x => x.GetLatestQuestions(It.IsAny<int>()), Times.Exactly(4));
            filterMock.Verify(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()), Times.Exactly(4));
        }
    }

}
