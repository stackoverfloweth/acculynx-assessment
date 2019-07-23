using Api.Contract;
using FluentAssertions;
using Ploeh.AutoFixture;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class QuestionFilterUnitTests : UnitTestBase {
        [Fact]
        public void FilterQuestions_Always_ReturnsQuestionsWhereAnswerCountGreaterThan1AndAcceptedAnswerIdHasValue() {
            // arrange
            var matchingQuestionDtos = AutoFixture.Build<QuestionDto>()
                .With(x => x.AnswerCount, RandomGenerator.Next(2, int.MaxValue))
                .With(x => x.AcceptedAnswerId)
                .CreateMany()
                .ToList();
            var nonMatchingQuestionDtos = AutoFixture.Build<QuestionDto>()
                .With(x => x.AnswerCount, RandomGenerator.Next(0, 1))
                .Without(x => x.AcceptedAnswerId)
                .CreateMany()
                .ToList();
            nonMatchingQuestionDtos.InsertRange(RandomGenerator.Next(nonMatchingQuestionDtos.Count), matchingQuestionDtos);

            // act 
            var filter = AutoFixture.Create<QuestionFilter>();
            var response = filter.FilterQuestions(nonMatchingQuestionDtos);

            // assert
            response.Should().BeEquivalentTo(matchingQuestionDtos);
        }

        [Fact]
        public void GetQuestionDtoById_Always_ReturnsQuestionsWhereAnswerCountGreaterThan1AndAcceptedAnswerIdHasValue() {
            // arrange
            var questionId = AutoFixture.Create<int>();
            var matchingQuestionDtos = AutoFixture.Build<QuestionDto>()
                .With(x => x.QuestionId, questionId)
                .Create();
            var nonMatchingQuestionDtos = AutoFixture.CreateMany<QuestionDto>()
                .ToList();
            nonMatchingQuestionDtos.Insert(RandomGenerator.Next(nonMatchingQuestionDtos.Count), matchingQuestionDtos);

            // act 
            var filter = AutoFixture.Create<QuestionFilter>();
            var response = filter.GetQuestionDtoById(nonMatchingQuestionDtos, questionId);

            // assert
            response.Should().Be(matchingQuestionDtos);
        }
    }
}
