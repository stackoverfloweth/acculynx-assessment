using Api.Contract;
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

namespace Api.Core.UnitTests {
    public class PreviouslyAttemptedQuestionFetcherUnitTests : UnitTestBase {
        [Fact]
        public void FetchQuestions_Always_CallsIAttemptRepositoryGetAttemptsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchAttemptedQuestions(userId);

            // assert
            repositoryMock.Verify(x => x.GetAttempts(userId), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIMapperMapAttemptDtoOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempts(It.IsAny<string>()))
                .Returns(attempts);

            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchAttemptedQuestions(userId);

            // assert
            mapperMock.Verify(x => x.Map<IEnumerable<AttemptDto>>(attempts), Times.Once);
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
            fetcher.FetchAttemptedQuestions(userId);

            // assert
            clientMock.Verify(x => x.GetQuestions(attemptIds), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_CallsIAttemptedQuestionDtoAssemblerAssembleAttemptedQuestionsOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetQuestions(It.IsAny<List<int>>()))
                .Returns(questionDtos);

            var attemptDtos = AutoFixture.CreateMany<AttemptDto>().ToList();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<IEnumerable<AttemptDto>>(It.IsAny<IEnumerable<Attempt>>()))
                .Returns(attemptDtos);

            var assemblerMock = AutoFixture.Freeze<Mock<IAttemptedQuestionDtoAssembler>>();

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchAttemptedQuestions(userId);

            // assert
            assemblerMock.Verify(x => x.AssembleAttemptedQuestions(attemptDtos, questionDtos), Times.Once);
        }

        [Fact]
        public void FetchQuestions_Always_ReturnsResponseFromIAttemptedQuestionDtoAssembler() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var attemptedQuestionDtos = AutoFixture.CreateMany<AttemptedQuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IAttemptedQuestionDtoAssembler>>()
                .Setup(x => x.AssembleAttemptedQuestions(It.IsAny<IEnumerable<AttemptDto>>(), It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(attemptedQuestionDtos);

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            var response = fetcher.FetchAttemptedQuestions(userId);

            // assert
            response.Should().BeEquivalentTo(attemptedQuestionDtos);
        }

        [Fact]
        public void FetchQuestions_Always_ReturnsResponseFromIAttemptedQuestionDtoAssemblerOrderedByAttemptDate() {
            // arrange
            var userId = AutoFixture.Create<string>();

            var attemptedQuestionDtos = AutoFixture.CreateMany<AttemptedQuestionDto>().ToList();
            AutoFixture.Freeze<Mock<IAttemptedQuestionDtoAssembler>>()
                .Setup(x => x.AssembleAttemptedQuestions(It.IsAny<IEnumerable<AttemptDto>>(), It.IsAny<IEnumerable<QuestionDto>>()))
                .Returns(attemptedQuestionDtos);

            // act 
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            var response = fetcher.FetchAttemptedQuestions(userId);

            // assert
            response.Should().BeInAscendingOrder(attemptedQuestion => attemptedQuestion.AttemptDto.AttemptDate);
        }

        [Fact]
        public void FetchAttemptedQuestion_Always_CallsIAttemptRepositoryGetAttemptOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();
            var questionId = AutoFixture.Create<int>();

            var repositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchAttemptedQuestion(userId, questionId);

            // assert
            repositoryMock.Verify(x => x.GetAttempt(questionId, userId), Times.Once);
        }

        [Fact]
        public void FetchAttemptedQuestion_Always_CallsIMapperMapAttemptDtoOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();
            var questionId = AutoFixture.Create<int>();

            var attempt = AutoFixture.Create<Attempt>();
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttempt(It.IsAny<int>(), It.IsAny<string>()))
                .Returns(attempt);

            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

            // act
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchAttemptedQuestion(userId, questionId);

            // assert
            mapperMock.Verify(x => x.Map<AttemptDto>(attempt), Times.Once);
        }

        [Fact]
        public void FetchAttemptedQuestion_Always_CallsIStackExchangeClientGetQuestionOnce() {
            // arrange
            var userId = AutoFixture.Create<string>();
            var questionId = AutoFixture.Create<int>();

            var clientMock = AutoFixture.Freeze<Mock<IStackExchangeClient>>();

            // act
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            fetcher.FetchAttemptedQuestion(userId, questionId);

            // assert
            clientMock.Verify(x => x.GetQuestion(questionId), Times.Once);
        }

        [Fact]
        public void FetchAttemptedQuestion_Always_ReturnsAttemptedQuestion() {
            // arrange
            var userId = AutoFixture.Create<string>();
            var questionId = AutoFixture.Create<int>();

            var attemptDto = AutoFixture.Create<AttemptDto>();
            AutoFixture.Freeze<Mock<IMapper>>()
                .Setup(x => x.Map<AttemptDto>(It.IsAny<Attempt>()))
                .Returns(attemptDto);

            var questionDto = AutoFixture.Create<QuestionDto>();
            AutoFixture.Freeze<Mock<IStackExchangeClient>>()
                .Setup(x => x.GetQuestion(It.IsAny<int>()))
                .Returns(questionDto);

            // act
            var fetcher = AutoFixture.Create<PreviouslyAttemptedQuestionFetcher>();
            var response = fetcher.FetchAttemptedQuestion(userId, questionId);

            // assert
            response.AttemptDto.Should().Be(attemptDto);
            response.QuestionDto.Should().Be(questionDto);
        }
    }

}