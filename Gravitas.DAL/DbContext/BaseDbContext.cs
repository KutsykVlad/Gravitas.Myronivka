using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;

namespace Gravitas.DAL
{
    public class BaseDbContext<TContext> : DbContext where TContext : DbContext
    {
        private ObjectContext _objectContext;

        public BaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public ObjectContext ObjectContext => _objectContext ?? (_objectContext = ((IObjectContextAdapter) this).ObjectContext);
    }
}