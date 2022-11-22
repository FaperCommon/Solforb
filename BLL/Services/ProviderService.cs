using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;

namespace BLL.Services
{
    public class ProviderService : BaseReadonlyService<ProviderDTO, Provider>, IProviderService
    {
        public ProviderService(IProviderRepository repository) : base(repository)
        {
        }
    }
}
