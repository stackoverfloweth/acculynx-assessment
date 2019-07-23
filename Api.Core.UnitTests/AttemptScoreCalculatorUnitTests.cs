using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class AttemptScoreCalculatorUnitTests : UnitTestBase {
        [Fact]
        public void CalculateScore_GivenCorrectAnswer_Returns100() {
            // arrange
            var correctAnswer = AutoFixture.Create<int>();
            var attempt = AutoFixture.Build<Attempt>()
                .With(x => x.AnswerId, correctAnswer)
                .With(x => x.AcceptedAnswerId, correctAnswer)
                .Create();

            // act
            var calculator = AutoFixture.Create<AttemptScoreCalculator>();
            var response = calculator.CalculateScore(attempt);

            //  assert
            response.Should().Be(100);
        }

        [Fact]
        public void CalculateScore_GivenIncorrectAnswer_CallsIAttemptRepositoryGetAttemptsForQuestionOnce() {
            // arrange
            var attempt = AutoFixture.Create<Attempt>();

            var attemptRepositoryMock = AutoFixture.Freeze<Mock<IAttemptRepository>>();

            // act
            var calculator = AutoFixture.Create<AttemptScoreCalculator>();
            calculator.CalculateScore(attempt);

            //  assert
            attemptRepositoryMock.Verify(x => x.GetAttemptsForQuestion(attempt.QuestionId), Times.Once);
        }

        [Fact]
        public void CalculateScore_GivenIncorrectAnswer_ReturnsRatioOfSameAttemptsDividedByTotalAttemptsOnQuestion() {
            // arrange
            var attempt = AutoFixture.Create<Attempt>();

            var matchingAttempts = AutoFixture.Build<Attempt>()
                .With(x => x.AnswerId, attempt.AnswerId)
                .CreateMany()
                .ToList();
            var nonMatchingAttempts = AutoFixture.CreateMany<Attempt>().ToList();
            nonMatchingAttempts.InsertRange(RandomGenerator.Next(nonMatchingAttempts.Count), matchingAttempts);
            AutoFixture.Freeze<Mock<IAttemptRepository>>()
                .Setup(x => x.GetAttemptsForQuestion(It.IsAny<int>()))
                .Returns(nonMatchingAttempts);

            // act
            var calculator = AutoFixture.Create<AttemptScoreCalculator>();
            var response = calculator.CalculateScore(attempt);

            //  assert
            var score = (double)matchingAttempts.Count / nonMatchingAttempts.Count * 100;
            response.Should().Be((int)Math.Round(score));
        }
    }
}
