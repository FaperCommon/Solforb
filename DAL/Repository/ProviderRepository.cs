using DAL.Common;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace DAL.Repository
{
    public class ProviderRepository : BaseReadonlyRepository<Provider>, IProviderRepository
    {
        public ProviderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
