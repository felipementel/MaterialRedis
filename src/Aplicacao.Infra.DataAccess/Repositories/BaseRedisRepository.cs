using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Shared.Model;
using Aplicacao.Infra.DataAccess.Repositories.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;
using System.Threading.Tasks;

namespace Aplicacao.Infra.DataAccess.Repositories
{
    public class BaseRedisRepository<T, Tid> : IRedisRepository<T, Tid> where T : TEntity<Tid>
    {
        private readonly IDistributedCache _cacheRedis;
        private DistributedCacheEntryOptions _distributedCacheEntryOptions;
        public BaseRedisRepository(IDistributedCache cacheRedis)
        {
            _cacheRedis = cacheRedis;

            _distributedCacheEntryOptions = new DistributedCacheEntryOptions();
            _distributedCacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(1));
        }

        public virtual async Task<T> Get(Tid key)
        {
            var dadosCache = await _cacheRedis.GetStringAsync(
                $"{nameof(CustomerRedisRepository)}:{key}");

            if (!string.IsNullOrEmpty(dadosCache))
            {
                return System.Text.Json.JsonSerializer.Deserialize<T>(dadosCache);
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
                PropertyNameCaseInsensitive = false,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            if (!string.IsNullOrEmpty(dadosCache))
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(dadosCache);
                //return JsonSerializer.Deserialize<IEnumerable<T>>(dadosCache, options);
            }
            else
            {
                return null;
            }
        }

        public async Task Remove(Tid key)
        {
            _cacheRedis.Remove($"{nameof(CustomerRedisRepository)}:{key}");
        }

        public async Task Set(T dadosCache)
        {
            //var dadosJson = JsonConvert.SerializeObject(
            //    dadosCache,
            //    Formatting.Indented,
            //    new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = false,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            var dadosJson = System.Text.Json.JsonSerializer.Serialize(
                dadosCache, options);

            await _cacheRedis.SetStringAsync(
                $"{nameof(CustomerRedisRepository)}:{dadosCache.Id}", 
                dadosJson,
                _distributedCacheEntryOptions);
        }

        public async Task Setm(IEnumerable<T> dadosCache)
        {
            //var dadosJson = JsonConvert
            //    .SerializeObject(
            //    dadosCache,
            //    Formatting.Indented, 
            //    new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = false,
                IgnoreNullValues = false,
                IgnoreReadOnlyProperties = false,
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve
            };

            var dadosJson = System.Text.Json.JsonSerializer.Serialize(
                dadosCache,
                options);

            await _cacheRedis.SetStringAsync(
                $"{nameof(CustomerRedisRepository)}",
                dadosJson,
                _distributedCacheEntryOptions);
        }
    }
}