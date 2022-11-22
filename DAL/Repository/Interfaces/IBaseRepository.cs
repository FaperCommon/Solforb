using DAL.Entities;
using System.Collections.Generic;

namespace DAL.Repository.Interfaces
{
    public interface IBaseRepository<T> : IBaseReadonlyRepository<T> where T : BaseEntity
    {
        void Add(T item);
        void AddRange(IEnumerable<T> items);
        void Update(T item);
        void Delete(T item);
    }
}
