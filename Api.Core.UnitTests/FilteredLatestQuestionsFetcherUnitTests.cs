﻿using Api.Contract;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class FilteredLatestQuestionsFetcherUnitTests : UnitTestBase {
        [Fact]
        public void FetchQuestions_Always_CallsIStackExchangeClientGetLatestQuestionsOnce() {
            // arrange
            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(20).ToList();
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);


            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            fetcher.FetchQuestions();

            // assert
            clientMock.Verify(x => x.GetLatestQuestions(1), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIQuestionFilterFilterQuestionsOnce() {
            // arrange
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
            fetcher.FetchQuestions();

            // assert
            filterMock.Verify(x => x.FilterQuestions(questionDtos), Times.Once);
        }

        [Fact]
        public void FetchQuestions_WhenQuestionDtosReturnedIsGreaterThan20_ReturnsOnly20() {
            // arrange
            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(RandomGenerator.Next(20, 100)).ToList();
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);

            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            var response = fetcher.FetchQuestions();

            // assert
            response.Should().HaveCount(20);
        }

        [Fact]
        public void FetchQuestions_WhenQuestionDtosReturnedIsLessThan20_RepeatsCallsUntil20IsReached() {
            // arrange
            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();
            var filterMock = AutoFixture.Freeze<Mock<IQuestionFilter>>();

            var filteredQuestions = AutoFixture.CreateMany<QuestionDto>(5).ToList();
            filterMock
                .Setup(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(filteredQuestions);

            // act 
            var fetcher = AutoFixture.Create<FilteredLatestQuestionsFetcher>();
            fetcher.FetchQuestions();

            // assert
            clientMock.Verify(x => x.GetLatestQuestions(It.IsAny<int>()), Times.Exactly(4));
            filterMock.Verify(x => x.FilterQuestions(It.IsAny<IEnumerable<QuestionDto>>()), Times.Exactly(4));
        }
    }

}
