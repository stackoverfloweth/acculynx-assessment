//using Api.Contract;
//using AutoMapper;
//using Data.Entities;
//using FluentAssertions;
//using Moq;
//using Ploeh.AutoFixture;
//using System.Collections.Generic;
//using System.Linq;
//using UnitTest.Framework;
//using Xunit;

//namespace Api.Core.UnitTests {
//    public class AttemptedQuestionDtoAssemblerUnitTests : UnitTestBase {
//        [Fact]
//        public void AssembleAttemptedQuestions_Always_CallsIMapperMapAttemptDtoOncePerAttempt() {
//            // arrange
//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

//            var mapperMock = AutoFixture.Freeze<Mock<IMapper>>();

//            // act 
//            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
//            assembler.AssembleAttemptedQuestions(attempts, questionDtos);

//            // assert 
//            foreach (var attempt in attempts) {
//                mapperMock.Verify(x => x.Map<AttemptDto>(attempt), Times.Once);
//            }
//        }

//        [Fact]
//        public void AssembleAttemptedQuestions_Always_CallsIQuestionFilterGetQuestionDtoByIdOncePerAttempt() {
//            // arrange
//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

//            var filterMock = AutoFixture.Freeze<Mock<IQuestionFilter>>();

//            // act 
//            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
//            assembler.AssembleAttemptedQuestions(attempts, questionDtos);

//            // assert 
//            foreach (var attempt in attempts) {
//                filterMock.Verify(x => x.GetQuestionDtoById(questionDtos, attempt.QuestionId), Times.Once);
//            }
//        }

//        [Fact]
//        public void AssembleAttemptedQuestions_WhenMapperReturnsNull_ReturnsEmptyList() {
//            // arrange
//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

//            AutoFixture.Freeze<Mock<IMapper>>()
//                .Setup(x => x.Map<AttemptDto>(It.IsAny<Attempt>()))
//                .Returns((AttemptDto)null);

//            // act 
//            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
//            var response = assembler.AssembleAttemptedQuestions(attempts, questionDtos);

//            // assert 
//            response.Should().BeEmpty();
//        }

//        [Fact]
//        public void AssembleAttemptedQuestions_WhenQuestionFilterReturnsNull_ReturnsEmptyList() {
//            // arrange
//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

//            AutoFixture.Freeze<Mock<IQuestionFilter>>()
//                .Setup(x => x.GetQuestionDtoById(It.IsAny<IEnumerable<QuestionDto>>(), It.IsAny<int>()))
//                .Returns((QuestionDto)null);

//            // act 
//            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
//            var response = assembler.AssembleAttemptedQuestions(attempts, questionDtos);

//            // assert 
//            response.Should().BeEmpty();
//        }

//        [Fact]
//        public void AssembleAttemptedQuestions_WhenMapperAndQuestionFilterReturnNotNull_ReturnsAttemptedQuestionDtos() {
//            // arrange
//            var attempts = AutoFixture.CreateMany<Attempt>().ToList();
//            var questionDtos = AutoFixture.CreateMany<QuestionDto>().ToList();

//            var attemptDto = AutoFixture.Create<AttemptDto>();
//            AutoFixture.Freeze<Mock<IMapper>>()
//                .Setup(x => x.Map<AttemptDto>(It.IsAny<Attempt>()))
//                .Returns(attemptDto);

//            var questionDto = AutoFixture.Create<QuestionDto>();
//            AutoFixture.Freeze<Mock<IQuestionFilter>>()
//                .Setup(x => x.GetQuestionDtoById(It.IsAny<IEnumerable<QuestionDto>>(), It.IsAny<int>()))
//                .Returns(questionDto);

//            // act 
//            var assembler = AutoFixture.Create<AttemptedQuestionDtoAssembler>();
//            var response = assembler.AssembleAttemptedQuestions(attempts, questionDtos);

//            // assert 
//            response.Should().HaveCount(attempts.Count);
//            response.Should().OnlyContain(x => x.AttemptDto == attemptDto && x.QuestionDto == questionDto);
//        }
//    }
//}
