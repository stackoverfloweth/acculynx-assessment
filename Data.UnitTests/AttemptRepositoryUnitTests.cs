using Data.Entities;
using Data.Repositories;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Data.UnitTests {
    public class AttemptRepositoryUnitTests : UnitTestBase {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void GetAttempts_WhenUserIdIsNullOrEmpty_ReturnsEmptyList(string userId) {
            // arrange


            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            var response = repository.GetAttempts(userId);

            //  assert
            response.Should().BeEmpty();
        }

        [Fact]
        public void GetAttempts_WhenUserIdIsNotNullOrEmpty_ReturnsAnyAttemptsWithMatchingUserId() {
            // arrange
            var userId = AutoFixture.Create<string>();
            var matchingAttempts = AutoFixture.Build<Attempt>()
                .With(x => x.UserId, userId)
                .CreateMany();
            var nonMatchingAttempts = AutoFixture.CreateMany<Attempt>().ToList();
            nonMatchingAttempts.InsertRange(RandomGenerator.Next(nonMatchingAttempts.Count), matchingAttempts);

            AutoFixture.Freeze<Mock<IStackOverflowethContext>>()
                .SetupGet(x => x.Attempts)
                .Returns(GetMockDbSet(nonMatchingAttempts).Object);

            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            var response = repository.GetAttempts(userId);

            //  assert
            response.Should().BeEquivalentTo(matchingAttempts);
        }

        [Fact]
        public void InsertAttempt_GivenNullAttempt_Returns() {
            // arrange
            Attempt attempt = null;
            var dbContextMock = AutoFixture.Freeze<Mock<IStackOverflowethContext>>();

            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            repository.InsertAttempt(attempt);

            // assert
            dbContextMock.Verify(x=>x.SaveChanges(), Times.Never);
        }

        [Fact]
        public void InsertAttempt_GivenNotNullAttempt_CallsAttemptsAddOnce() {
            // arrange
            var attempt = AutoFixture.Create<Attempt>();

            var attempts = AutoFixture.CreateMany<Attempt>();
            var dbMock = GetMockDbSet(attempts);
            AutoFixture.Freeze<Mock<IStackOverflowethContext>>()
                .SetupGet(x => x.Attempts)
                .Returns(dbMock.Object);

            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            repository.InsertAttempt(attempt);

            // assert
            dbMock.Verify(x => x.Add(attempt), Times.Once);
        }

        [Fact]
        public void InsertAttempt_GivenNotNullAttempt_CallsIStackOverflowethContextSaveChangesOnce() {
            // arrange
            var attempt = AutoFixture.Create<Attempt>();

            var dbContextMock = AutoFixture.Freeze<Mock<IStackOverflowethContext>>();

            var attempts = AutoFixture.CreateMany<Attempt>();
            dbContextMock
                .SetupGet(x => x.Attempts)
                .Returns(GetMockDbSet(attempts).Object);

            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            repository.InsertAttempt(attempt);

            // assert
            dbContextMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void GetAttemptsForQuestion_Always_ReturnsAnyAttemptsWhereQuestionIdMatches() {
            // arrange
            var questionId = AutoFixture.Create<int>();

            var matchingAttempts = AutoFixture.Build<Attempt>()
                .With(x => x.QuestionId, questionId)
                .CreateMany()
                .ToList();
            var nonMatchingAttempts = AutoFixture.CreateMany<Attempt>().ToList();
            nonMatchingAttempts.InsertRange(RandomGenerator.Next(nonMatchingAttempts.Count), matchingAttempts);

            AutoFixture.Freeze<Mock<IStackOverflowethContext>>()
                .SetupGet(x => x.Attempts)
                .Returns(GetMockDbSet(nonMatchingAttempts).Object);

            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            var response = repository.GetAttemptsForQuestion(questionId);

            // assert
            response.Should().BeEquivalentTo(matchingAttempts);
        }

        [Fact]
        public void GetAttempt_Always_ReturnsSingleOrDefaultWhereUserIdAndQuestionIdMatch() {
            // arrange
            var questionId = AutoFixture.Create<int>();
            var userId = AutoFixture.Create<string>();

            var matchingAttempt = AutoFixture.Build<Attempt>()
                .With(x => x.QuestionId, questionId)
                .With(x => x.UserId, userId)
                .Create();
            var nonMatchingAttempts = AutoFixture.CreateMany<Attempt>().ToList();
            nonMatchingAttempts.Insert(RandomGenerator.Next(nonMatchingAttempts.Count), matchingAttempt);

            AutoFixture.Freeze<Mock<IStackOverflowethContext>>()
                .SetupGet(x => x.Attempts)
                .Returns(GetMockDbSet(nonMatchingAttempts).Object);

            // act
            var repository = AutoFixture.Create<AttemptRepository>();
            var response = repository.GetAttempt(questionId, userId);

            // assert
            response.Should().Be(matchingAttempt);
        }
    }
}
