using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IBaseReadonlyService<T> where T : BaseDTO
    {
        IEnumerable<T> GetAll();
        T Find(int id);
    }
}
