using Api.Contract;
using Api.Controllers;
using Api.Core;
using Moq;
using Ploeh.AutoFixture;
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
    }
}
