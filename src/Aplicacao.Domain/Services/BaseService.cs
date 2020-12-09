using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Interfaces.Services;
using Aplicacao.Domain.Shared.Model;
using Aplicacao.Domain.UoW;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Domain.Services
{
    public abstract class BaseService<T, Tid> : IService<T, Tid> where T : TEntity<Tid>
    {
        private IUnitOfWork _uow;

        private ISQLRepository<T, Tid> _sqlServerRepository;

        private IRedisRepository<T, Tid> _redisRepository;

        protected AbstractValidator<T> _validator;

        public BaseService(
            IUnitOfWork uow,
            ISQLRepository<T, Tid> sqlServerRepository,
            IRedisRepository<T, Tid> redisRepository, 
            AbstractValidator<T> validator)
        {
            _uow = uow;
            _sqlServerRepository = sqlServerRepository;
            _redisRepository = redisRepository;
            _validator = validator;
        }

        public virtual async Task<T> Add(T entity)
        {
            if (_validator == null)
                throw new ArgumentException($"Não foi informado o validador da classe {nameof(entity)}");

            var validated = await _validator.ValidateAsync(entity);

            entity.ValidationResult = validated;

            if (!validated.IsValid)
            {
                return entity;
            }

            //await _uow.BeginTransaction();

            var entityTemp = await _sqlServerRepository.Create(entity);

            await _uow.Commit();

            //TODO: PASSO 1
            //await _redisRepository.Set(entityTemp);

            return entityTemp;
        }

        public virtual async Task<bool> Delete(Tid tid)
        {
            await _sqlServerRepository.Delete(tid);

            await _redisRepository.Remove(tid);

            return await _uow.Commit();
        }

        public virtual async Task<T> Get(Tid tid)
        {
            //TODO: Performance
            var tempEntity = await _redisRepository.Get(tid);

            if (tempEntity != null)
            {
                return tempEntity;
            }

            tempEntity = await _sqlServerRepository.ReadById(tid);

            if (!(tempEntity is null))
                await _redisRepository.Set(tempEntity);

            return tempEntity;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var tempEntity = await _redisRepository.Getm();

            if (tempEntity != null)
            {
                return tempEntity;
            }

            tempEntity = await _sqlServerRepository.ReadAll();

            await _redisRepository.Setm(tempEntity);

            return tempEntity;
        }

        public virtual async Task<T> Update(T entity)
        {
            if (_validator == null)
                throw new ArgumentException($"Não foi informado o validador da classe {nameof(entity)}");

            var validated = _validator.Validate(entity);

            entity.ValidationResult = validated;

            if (!validated.IsValid)
            {
                return entity;
            }

            var entityTemp = await _sqlServerRepository.Update(entity);

            await _uow.Commit();

            await _redisRepository.Set(entityTemp);

            return entityTemp;
        }

        public void Dispose()
        {
            _uow = null;
            _sqlServerRepository = null;
            _redisRepository = null;
        }
    }
}
