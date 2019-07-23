using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;
using Ploeh.AutoFixture.Kernel;

namespace UnitTest.Framework {
    public class UnitTestBase
    {
        protected Fixture AutoFixture { get; set; } = new Fixture();
        protected Random RandomGenerator { get; set; } = new Random();

        public UnitTestBase() {
            TurnOnAutoMoq();
            SetupForWebApi();
        }

        protected void TurnOnAutoMoq() {
            AutoFixture.Customize(new AutoMoqCustomization());
        }

        public void SetupForWebApi() {
            //https://stackoverflow.com/questions/19908385/automocking-web-api-2-controller
            AutoFixture.Customizations.Add(
                new FilteringSpecimenBuilder(
                    new Postprocessor(
                        new MethodInvoker(
                            new ModestConstructorQuery()),
                        new ApiControllerFiller()),
                    new ApiControllerSpecification()));
        }

        private class ApiControllerFiller : ISpecimenCommand {
            public void Execute(object specimen, ISpecimenContext context) {
                if (specimen == null)
                    throw new ArgumentNullException("specimen");
                if (context == null)
                    throw new ArgumentNullException("context");

                var target = specimen as ApiController;
                if (target == null)
                    throw new ArgumentException(
                        "The specimen must be an instance of ApiController.",
                        "specimen");

                target.Request =
                    (HttpRequestMessage)context.Resolve(
                        typeof(HttpRequestMessage));
            }
        }

        private class ApiControllerSpecification : IRequestSpecification {
            public bool IsSatisfiedBy(object request) {
                var requestType = request as Type;
                if (requestType == null)
                    return false;
                return typeof(ApiController).IsAssignableFrom(requestType);
            }
        }

        protected Mock<DbSet<T>> GetMockDbSet<T>(IEnumerable<T> expectedEntities) where T : class {
            var entities = expectedEntities.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(entities.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(entities.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(entities.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(entities.GetEnumerator);

            return mockSet;
        }
    }
}
