using BLL.DTOs;
using System.Collections.Generic;

namespace BLL.Services.Interfaces
{
    public interface IBaseService<T> : IBaseReadonlyService<T> where T : BaseDTO
    {
        void Add(T item);
        void AddRange(IEnumerable<T> item);
        void Update(T item);
        void Delete(T item);
    }
}
