using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace UnitTest.Framework {
    public class UnitTestBase
    {
        protected Fixture AutoFixture { get; set; } = new Fixture();
        protected Random RandomGenerator { get; set; } = new Random();

        public UnitTestBase() {
            TurnOnAutoMoq();
        }

        protected void TurnOnAutoMoq() {
            AutoFixture.Customize(new AutoMoqCustomization());
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
