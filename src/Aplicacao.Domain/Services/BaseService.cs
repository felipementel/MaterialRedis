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

            var validated = await _validator.ValidateAsync(entity, strategy =>
            {
                strategy.IncludeRuleSets("new");
            });

            entity.ValidationResult = validated;

            if (!validated.IsValid)
            {
                return entity;
            }

            _uow.BeginTransaction();

            var entityTemp = await _sqlServerRepository.Create(entity);

            _uow.Commit();

            await _redisRepository.Set(entityTemp);

            return entityTemp;
        }

        public virtual async Task<bool> Delete(Tid tid)
        {
            await _sqlServerRepository.Delete(tid);

            _redisRepository.Remove(tid);

            return _uow.Commit();
        }

        public virtual async Task<T> Get(Tid tid)
        {
            //TODO: Cache A-Side Pattern
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

            if (tempEntity is not null)
                await _redisRepository.Setm(tempEntity);

            return tempEntity;
        }

        public virtual async Task<T> Update(T entity)
        {
            if (_validator == null)
                throw new ArgumentException($"Não foi informado o validador da classe {nameof(entity)}");

            var validated = await _validator.ValidateAsync(entity, strategy =>
            {
                strategy.IncludeRuleSets("update");
            });

            entity.ValidationResult = validated;

            if (!validated.IsValid)
            {
                return entity;
            }

            _uow.BeginTransaction();

            var entityTemp = await _sqlServerRepository.Update(entity);

            _uow.Commit();

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