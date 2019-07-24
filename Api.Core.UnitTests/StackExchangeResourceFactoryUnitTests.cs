using Api.Contract.Enums;
using FluentAssertions;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class StackExchangeResourceFactoryUnitTests : UnitTestBase {
        [Theory]
        [InlineData(StackExchangeResourceEnum.GetQuestions, "questions")]
        [InlineData(StackExchangeResourceEnum.LookupQuestions, "questions/argumentValue")]
        [InlineData(StackExchangeResourceEnum.GetQuestionAnswers, "questions/argumentValue/answers")]
        [InlineData(StackExchangeResourceEnum.CreateFilter, "filters/create")]
        public void FetchResource_Always_ReturnsQuestionsWhereAnswerCountGreaterThan1AndAcceptedAnswerIdHasValue(StackExchangeResourceEnum resourceEnum, string expectedResource) {
            // arrange

            // act 
            var fetcher = AutoFixture.Create<StackExchangeResourceFactory>();
            var response = fetcher.FetchResource(resourceEnum, new List<object> { "argumentValue" });

            // assert
            response.Should().Be(expectedResource);
        }
    }
}