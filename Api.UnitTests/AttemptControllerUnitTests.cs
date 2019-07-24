using Api.Contract;
using Api.Controllers;
using Api.Core;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.UnitTests {
    public class AttemptControllerUnitTests : UnitTestBase {
        [Fact]
        public void CreateAttempt_Always_CallsIAttemptSubmissionManagerSubmitAttemptOnce() {
            // arrange
            var attemptDto = AutoFixture.Create<AttemptDto>();

            var managerMock = AutoFixture.Freeze<Mock<IAttemptSubmissionManager>>();

            // act
            var controller = AutoFixture.Create<AttemptController>();
            controller.CreateAttempt(attemptDto);

            //  assert
            managerMock.Verify(x => x.SubmitAttempt(attemptDto, It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void FetchAttempt_Always_CallsIPreviouslyAttemptedQuestionFetcherFetchAttemptedQuestionOnce() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var attemptedQuestionFetcherMock = AutoFixture.Freeze<Mock<IPreviouslyAttemptedQuestionFetcher>>();

            // act
            var controller = AutoFixture.Create<AttemptController>();
            controller.FetchAttempt(questionId);

            // assert
            attemptedQuestionFetcherMock.Verify(x => x.FetchAttemptedQuestion(It.IsAny<string>(), questionId), Times.Once);
        }

        [Fact]
        public void FetchPreviousQuestions_Always_CallsIPreviouslyAttemptedQuestionFetcherFetchQuestionsOnce() {
            // arrange
            var questionFetcherMock = AutoFixture.Freeze<Mock<IPreviouslyAttemptedQuestionFetcher>>();

            // act
            var controller = AutoFixture.Create<AttemptController>();
            controller.FetchPreviousQuestions();

            //  assert
            questionFetcherMock.Verify(x => x.FetchAttemptedQuestions(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void FetchPreviousQuestions_Always_ReturnsResponseFromIPreviouslyAttemptedQuestionFetcher() {
            // arrange
            var attemptQuestionDtos = AutoFixture.CreateMany<AttemptedQuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IPreviouslyAttemptedQuestionFetcher>>()
                .Setup(x => x.FetchAttemptedQuestions(It.IsAny<string>()))
                .Returns(attemptQuestionDtos);

            // act
            var controller = AutoFixture.Create<AttemptController>();
            var response = controller.FetchPreviousQuestions();

            //  assert
            response.Should().BeEquivalentTo(attemptQuestionDtos);
        }
    }
}