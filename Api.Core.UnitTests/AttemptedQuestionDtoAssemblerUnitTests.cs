using Api.Contract;
using FluentAssertions;
using Moq;
using Ploeh.AutoFixture;
using System.Collections.Generic;
using System.Linq;
using UnitTest.Framework;
using Xunit;

namespace Api.Core.UnitTests {
    public class AttemptedQuestionDtoAssemblerUnitTests : UnitTestBase {
        [Fact]
        public void AssembleAttemptedQuestions_Always_CallsIQuestionFilterGetQuestionDtoByIdOncePerAttempt() {
            // arrange
            var attemptDtos = AutoFixture.CreateMany<AttemptDto>().ToList();
            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

            var filterMock = AutoFixture.Freeze<Mock<IQuestionFilter>>();

            // act 
            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
            assembler.AssembleAttemptedQuestions(attemptDtos, questionDtos);

            // assert 
            foreach (var attempt in attemptDtos) {
                filterMock.Verify(x => x.GetQuestionDtoById(questionDtos, attempt.QuestionId), Times.Once);
            }
        }

        [Fact]
        public void AssembleAttemptedQuestions_WhenQuestionFilterReturnsNull_ReturnsEmptyList() {
            // arrange
            var attemptDtos = AutoFixture.CreateMany<AttemptDto>().ToList();
            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.GetQuestionDtoById(It.IsAny<IEnumerable<QuestionDto>>(), It.IsAny<int>()))
                .Returns((QuestionDto)null);

            // act 
            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
            var response = assembler.AssembleAttemptedQuestions(attemptDtos, questionDtos);

            // assert 
            response.Should().BeEmpty();
        }

        [Fact]
        public void AssembleAttemptedQuestions_WhenMapperAndQuestionFilterReturnNotNull_ReturnsAttemptedQuestionDtos() {
            // arrange
            var attemptDtos = AutoFixture.CreateMany<AttemptDto>().ToList();
            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

            var questionDto = AutoFixture.Create<QuestionDto>();
            AutoFixture.Freeze<Mock<IQuestionFilter>>()
                .Setup(x => x.GetQuestionDtoById(It.IsAny<IEnumerable<QuestionDto>>(), It.IsAny<int>()))
                .Returns(questionDto);

            // act 
            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
            var response = assembler.AssembleAttemptedQuestions(attemptDtos, questionDtos);

            // assert 
            foreach (var attemptDto in attemptDtos) {
                response.Should().Contain(x => x.AttemptDto == attemptDto && x.QuestionDto == questionDto);
            }
        }
    }
}
