using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System.Collections.Generic;

namespace BLL.Services
{
    public abstract class BaseService<T, D> : BaseReadonlyService<T, D>, IBaseService<T> where T : BaseDTO where D : BaseEntity
    {
        protected new readonly IBaseRepository<D> _repository;
        public BaseService(IBaseRepository<D> repository) : base(repository)
        {
            _repository = repository;
        }

        public virtual void Add(T item)
        {
            _repository.Add(_mapper.Map<T, D>(item));
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            _repository.AddRange(_mapper
                .Map<IEnumerable<T>, IEnumerable<D>>(items));
        }

        public virtual void Delete(T item)
        {
            _repository.Delete(_mapper.Map<T, D>(item));
        }

        public virtual void Update(T item)
        {
            _repository.Update(_mapper.Map<T, D>(item));
        }
    }
}
