using AccServerAdmin.Domain;
using AccServerAdmin.Persistence.DbContext;

namespace AccServerAdmin.Persistence.Repository
{
    public class SessionRepository : DataRepository<Session>, ISessionRepository
    {
        public SessionRepository(ApplicationDbContext dbContext) :
            base(dbContext)
        {
        }

 }
}
