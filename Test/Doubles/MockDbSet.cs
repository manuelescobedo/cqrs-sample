using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace Test.Doubles
{
    public class MockDbSet<TEntity>: Mock<DbSet<TEntity>> where TEntity : class
    {
        readonly List<TEntity> _dummyList;
        public MockDbSet(List<TEntity> dummyList)
        {
            _dummyList = dummyList;
            
            As<IQueryable<TEntity>>().Setup(d => d.GetEnumerator()).Returns(Data.GetEnumerator());
            As<IQueryable<TEntity>>().Setup(d => d.Expression).Returns(Data.Expression);
            As<IQueryable<TEntity>>().Setup(d => d.Provider).Returns(Data.Provider);
            As<IQueryable<TEntity>>().Setup(d => d.ElementType).Returns(Data.ElementType);
        }

        public MockDbSet() : this(new List<TEntity>())
        {

        }

        public IQueryable<TEntity> Data
        {
            get
            {
                return _dummyList.AsQueryable();
            }
        }
    }
}
