//using Api.Contract;
//using Api.Controllers;
//using Api.Core;
//using AutoMapper;
//using Data.Entities;
//using Data.Repositories;
//using FluentAssertions;
//using Moq;
//using Ploeh.AutoFixture;
//using System.Collections.Generic;
//using System.Linq;
//using UnitTest.Framework;
//using Xunit;

//namespace Api.UnitTests {
//    public class QuestionControllerUnitTests : UnitTestBase {
//        [Fact]
//        public void FetchLatestQuestions_Always_CallsIFilteredLatestQuestionsFetcherFetchQuestionsOnce() {
//            // arrange
//            var questionFetcherMock = AutoFixture.Freeze<Mock<IFilteredLatestQuestionsFetcher>>();

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            controller.FetchLatestQuestions();

//            //  assert
//            questionFetcherMock.Verify(x => x.FetchQuestions(), Times.Once);
//        }

//        [Fact]
//        public void FetchLatestQuestions_Always_ReturnsResponseFromIFilteredLatestQuestionsFetcher() {
//            // arrange
//            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();
//            AutoFixture.Freeze<Mock<IFilteredLatestQuestionsFetcher>>()
//                .Setup(x => x.FetchQuestions())
//                .Returns(questionDtos);

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            var response = controller.FetchLatestQuestions();

//            //  assert
//            response.Should().BeEquivalentTo(questionDtos);
//        }

//        [Fact]
//        public void FetchPreviousQuestions_Always_CallsIPreviouslyAttemptedQuestionFetcherFetchQuestionsOnce() {
//            // arrange
//            var questionFetcherMock = AutoFixture.Freeze<Mock<IPreviouslyAttemptedQuestionFetcher>>();

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            controller.FetchPreviousQuestions();

//            //  assert
//            questionFetcherMock.Verify(x => x.FetchAttemptedQuestions(It.IsAny<string>()), Times.Once);
//        }

//        [Fact]
//        public void FetchPreviousQuestions_Always_ReturnsResponseFromIPreviouslyAttemptedQuestionFetcher() {
//            // arrange
//            var attemptQuestionDtos = AutoFixture.CreateMany<AttemptedQuestionDto>().ToList();
//            AutoFixture.Freeze<Mock<IPreviouslyAttemptedQuestionFetcher>>()
//                .Setup(x => x.FetchAttemptedQuestions(It.IsAny<string>()))
//                .Returns(attemptQuestionDtos);

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            var response = controller.FetchPreviousQuestions();

//            //  assert
//            response.Should().BeEquivalentTo(attemptQuestionDtos);
//        }

//        [Fact]
//        public void FetchAnswersForQuestion_Always_CallsIStackExchangeClientFetchAnswersForQuestionOnce() {
//            // arrange
//            var questionId = AutoFixture.Create<int>();

//            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            controller.FetchAnswersForQuestion(questionId);

//            //  assert
//            clientMock.Verify(x => x.GetAnswers(questionId), Times.Once);
//        }

//        [Fact]
//        public void FetchAnswersForQuestion_Always_ReturnsResponseFromIStackExchangeClient() {
//            // arrange
//            var questionId = AutoFixture.Create<int>();

//            var answerDtos = AutoFixture.CreateMany<AnswerDto>().ToList();
//            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
//                .Setup(x => x.GetAnswers(It.IsAny<int>()))
//                .Returns(answerDtos);

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            var response = controller.FetchAnswersForQuestion(questionId);

//            //  assert
//            response.Should().BeEquivalentTo(answerDtos);
//        }

//        [Fact]
//        public void GetAttemptsForQuestion_Always_CallsIAttemptRepositoryGetAttemptsForQuestion() {
//            // arrange
//            var questionId = AutoFixture.Create<int>();

//            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            controller.GetAttemptsForQuestion(questionId);

//            //  assert
//            repositoryMock.Verify(x => x.GetAttemptsForQuestion(questionId), Times.Once);
//        }

//        [Fact]
//        public void GetAttemptsForQuestion_Always_CallsIMapperMapAttemptDtoOnce() {
//            // arrange
//            var questionId = AutoFixture.Create<int>();

//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            AutoFixture.Freeze<Mock<IAttemptRepository>>()
//                .Setup(x => x.GetAttemptsForQuestion(It.IsAny<int>()))
//                .Returns(attempts);

//            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            controller.GetAttemptsForQuestion(questionId);

//            //  assert
//            mapperMock.Verify(x => x.Map<IEnumerable<AttemptDto>>(attempts), Times.Once);
//        }

//        [Fact]
//        public void GetAttemptsForQuestion_Always_ReturnsResponseFromIMapper() {
//            // arrange
//            var questionId = AutoFixture.Create<int>();

//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            AutoFixture.Freeze<Mock<IAttemptRepository>>()
//                .Setup(x => x.GetAttemptsForQuestion(It.IsAny<int>()))
//                .Returns(attempts);

//            var attemptDtos = AutoFixture.CreateMany<AttemptDto>().ToList();
//            AutoFixture.Freeze<Mock<IMapper>>()
//                .Setup(x => x.Map<IEnumerable<AttemptDto>>(It.IsAny<IEnumerable<Attempt>>()))
//                .Returns(attemptDtos);

//            // act
//            var controller = AutoFixture.Create<QuestionController>();
//            var response = controller.GetAttemptsForQuestion(questionId);

//            //  assert
//            response.Should().BeEquivalentTo(attemptDtos);
//        }
//    }
//}
