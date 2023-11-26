using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace ToDo.UnitTests
{
    public class DbContextMock
    {
        public static TContext GetMock<TData, TContext>(List<TData> lstData, Expression<Func<TContext, DbSet<TData>>> dbSetSelectionExpression) where TData
            : class where TContext : DbContext
        {
            IQueryable<TData> lstDataQueryable = lstData.AsQueryable();
            Mock<DbSet<TData>> dbSetMock = new();
            Mock<TContext> dbContext = new();

            dbSetMock.As<IQueryable<TData>>().Setup(s => s.Provider).Returns(lstDataQueryable.Provider);
            dbSetMock.As<IQueryable<TData>>().Setup(s => s.Expression).Returns(lstDataQueryable.Expression);
            dbSetMock.As<IQueryable<TData>>().Setup(s => s.ElementType).Returns(lstDataQueryable.ElementType);
            dbSetMock.As<IQueryable<TData>>().Setup(s => s.GetEnumerator()).Returns(() => lstDataQueryable.GetEnumerator());

            dbSetMock.Setup(x => x.Add(It.IsAny<TData>())).Callback<TData>(lstData.Add);
            dbSetMock.Setup(x => x.AddRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(lstData.AddRange);
            dbSetMock.Setup(x => x.Remove(It.IsAny<TData>())).Callback<TData>(t => lstData.Remove(t));
            dbSetMock.Setup(x => x.RemoveRange(It.IsAny<IEnumerable<TData>>())).Callback<IEnumerable<TData>>(ts =>
            {
                foreach (var t in ts) { lstData.Remove(t); }
            });

            dbContext.Setup(dbSetSelectionExpression).Returns(dbSetMock.Object);

            return dbContext.Object;
        }
    }
}
