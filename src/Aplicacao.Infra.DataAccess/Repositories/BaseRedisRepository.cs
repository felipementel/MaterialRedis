using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Shared.Model;
using Aplicacao.Infra.DataAccess.Repositories.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aplicacao.Infra.DataAccess.Repositories
{
    public class BaseRedisRepository<T, Tid> : IRedisRepository<T, Tid> where T : TEntity<Tid>
    {
        private readonly IDistributedCache _cacheRedis;
        private readonly DistributedCacheEntryOptions _distributedCacheEntryOptions;
        public BaseRedisRepository(
            IDistributedCache cacheRedis,
            IConfiguration configuration)
        {
            _cacheRedis = cacheRedis;

            _distributedCacheEntryOptions = new DistributedCacheEntryOptions();
            _distributedCacheEntryOptions.SetSlidingExpiration(
                TimeSpan.FromSeconds(
                    Int32.Parse(
                        configuration["cacheSecondTimeout"])));
        }

        public virtual async Task<T> Get(Tid key)
        {
            var dadosCache = await _cacheRedis.GetStringAsync(
                $"{nameof(CustomerRedisRepository)}:{key}");

            if (!string.IsNullOrEmpty(dadosCache))
            {               
                return JsonSerializer.Deserialize<T>(dadosCache);
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<T>> Getm()
        {
            var dadosCache = await _cacheRedis.GetStringAsync(
                $"{nameof(CustomerRedisRepository)}");

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            if (!string.IsNullOrEmpty(dadosCache))
            {
                return JsonSerializer.Deserialize<IEnumerable<T>>(dadosCache, options);
            }
            else
            {
                return null;
            }
        }

        public void Remove(Tid key)
        {
            _cacheRedis.Remove($"{nameof(CustomerRedisRepository)}:{key}");
        }

        public async Task Set(T dadosCache)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            var dadosJson = JsonSerializer.Serialize(
                dadosCache, options);

            await _cacheRedis.SetStringAsync(
                $"{nameof(CustomerRedisRepository)}:{dadosCache.Id}", 
                dadosJson,
                _distributedCacheEntryOptions);
        }

        public async Task Setm(IEnumerable<T> dadosCache)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            var dadosJson = JsonSerializer.Serialize(
                dadosCache, options);

            await _cacheRedis.SetStringAsync(
                $"{nameof(CustomerRedisRepository)}",
                dadosJson,
                _distributedCacheEntryOptions);
        }
    }
}