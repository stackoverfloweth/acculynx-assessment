using Api.Contract;
using Api.Controllers;
using Api.Core;
using AutoMapper;
using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.UnitTests {
    public class QuestionControllerUnitTests : UnitTestBase {
        [Fact]
        public void FetchLatestQuestions_Always_CallsIFilteredLatestQuestionsFetcherFetchQuestionsOnce() {
            // arrange
            var questionFetcherMock = AutoFixture.Freeze<Mock<IFilteredLatestQuestionsFetcher>>();

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.FetchLatestQuestions();

            //  assert
            questionFetcherMock.Verify(x => x.FetchQuestions(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void FetchLatestQuestions_Always_ReturnsResponseFromIFilteredLatestQuestionsFetcher() {
            // arrange
            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IFilteredLatestQuestionsFetcher>>()
                .Setup(x => x.FetchQuestions(It.IsAny<string>()))
                .Returns(questionDtos);

            // act
            var controller = AutoFixture.Create<QuestionController>();
            var response = controller.FetchLatestQuestions();

            //  assert
            response.Should().BeEquivalentTo(questionDtos);
        }

        [Fact]
        public void FetchAnswersForQuestion_Always_CallsIStackExchangeClientFetchAnswersForQuestionOnce() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.FetchAnswersForQuestion(questionId);

            //  assert
            clientMock.Verify(x => x.GetAnswers(questionId), Times.Once);
        }

        [Fact]
        public void FetchAnswersForQuestion_Always_ReturnsResponseFromIStackExchangeClient() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var answerDtos = AutoFixture.CreateMany<AnswerDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetAnswers(It.IsAny<int>()))
                .Returns(answerDtos);

            // act
            var controller = AutoFixture.Create<QuestionController>();
            var response = controller.FetchAnswersForQuestion(questionId);

            //  assert
            response.Should().BeEquivalentTo(answerDtos);
        }

        [Fact]
        public void GetAttemptsForQuestion_Always_CallsIAttemptRepositoryGetAttemptsForQuestion() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.GetAttemptsForQuestion(questionId);

            //  assert
            repositoryMock.Verify(x => x.GetAttemptsForQuestion(questionId), Times.Once);
        }

        [Fact]
        public void GetAttemptsForQuestion_Always_CallsIMapperMapAttemptDtoOnce() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttemptsForQuestion(It.IsAny<int>()))
                .Returns(attempts);

            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.GetAttemptsForQuestion(questionId);

            //  assert
            mapperMock.Verify(x => x.Map<IEnumerable<AttemptDto>>(attempts), Times.Once);
        }

        [Fact]
        public void GetAttemptsForQuestion_Always_ReturnsResponseFromIMapper() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttemptsForQuestion(It.IsAny<int>()))
                .Returns(attempts);

            var attemptDtos = AutoFixture.CreateMany<AttemptDto>().ToList();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<IEnumerable<AttemptDto>>(It.IsAny<IEnumerable<Attempt>>()))
                .Returns(attemptDtos);

            // act
            var controller = AutoFixture.Create<QuestionController>();
            var response = controller.GetAttemptsForQuestion(questionId);

            //  assert
            response.Should().BeEquivalentTo(attemptDtos);
        }

        [Fact]
        public void FetchAnswersForQuestion_Always_CallsIStackExchangeClientGetAnswersOnce() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.FetchAnswersForQuestion(questionId);

            // assert
            clientMock.Verify(x => x.GetAnswers(questionId), Times.Once);
        }

        [Fact]
        public void FetchAnswersForQuestions_Always_CallsIAttemptRepositoryGetAttemptsForAnswerOncePerAnswerDto() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var answerDtos = AutoFixture.CreateMany<AnswerDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetAnswers(It.IsAny<int>()))
                .Returns(answerDtos);

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.FetchAnswersForQuestion(questionId);

            // assert
            foreach (var answerDto in answerDtos) {
                repositoryMock.Verify(x => x.GetAttemptsForAnswer(answerDto.AnswerId), Times.Once);
            }
        }

        [Fact]
        public void FetchAnswersForQuestions_Always_SetsAttemptCountEqualToCountFromIAttemptRepositoryGetAttemptsForAnswer() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var answerDtos = AutoFixture.CreateMany<AnswerDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetAnswers(It.IsAny<int>()))
                .Returns(answerDtos);

            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttemptsForAnswer(It.IsAny<int>()))
                .Returns(attempts);

            // act
            var controller = AutoFixture.Create<QuestionController>();
            controller.FetchAnswersForQuestion(questionId);

            // assert
            foreach (var answerDto in answerDtos) {
                answerDto.AttemptCount.Should().Be(attempts.Count);
            }
        }

        [Fact]
        public void FetchAnswersForQuestions_Always_ReturnsAnswerDtos() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var answerDtos = AutoFixture.CreateMany<AnswerDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetAnswers(It.IsAny<int>()))
                .Returns(answerDtos);

            // act
            var controller = AutoFixture.Create<QuestionController>();
            var response = controller.FetchAnswersForQuestion(questionId);

            // assert
            response.Should().BeEquivalentTo(answerDtos);
        }
    }
}