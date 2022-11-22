using AutoMapper;
using BLL.DTOs;
using BLL.Services.Interfaces;
using DAL.Entities;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public abstract class BaseReadonlyService<T, D> : IBaseReadonlyService<T> where T : BaseDTO where D : BaseEntity
    {
        protected readonly IBaseReadonlyRepository<D> _repository;
        protected readonly IMapper _mapper;
        public BaseReadonlyService(IBaseReadonlyRepository<D> repository)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<D, T>();
                cfg.CreateMap<T, D>();
            });
            _mapper = config.CreateMapper();

            _repository = repository;
        }

        public virtual T Find(int id)
        {
            return _mapper.Map<D, T>(_repository.Find(id));
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _mapper
                .Map<IEnumerable<D>, IEnumerable<T>>(_repository.GetAll());
        }
    }
}
