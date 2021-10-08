using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;

namespace Test.Doubles
{
    public class MockDbContext<TEntity> : Mock<DbContext> where TEntity : class
    {
        public MockDbContext(DbSet<TEntity> dummyDbSetProduct)
        {
            Setup(c => c.Set<TEntity>()).Returns(dummyDbSetProduct);
        }

        public MockDbContext(List<TEntity> entities) : this(new MockDbSet<TEntity>(entities).Object)
        {
            
        }

        public MockDbContext() : this(new List<TEntity>())
        {

        }
        
    }
}
