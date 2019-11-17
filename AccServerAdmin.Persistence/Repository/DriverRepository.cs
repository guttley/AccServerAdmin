using AccServerAdmin.Domain.AccConfig;
using AccServerAdmin.Persistence.Common;
using AccServerAdmin.Persistence.DbContext;


namespace AccServerAdmin.Persistence.Repository
{
    public class DriverRepository : DataRepository<Driver>, IDriverRepository
    {
        public DriverRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }
    }
}
