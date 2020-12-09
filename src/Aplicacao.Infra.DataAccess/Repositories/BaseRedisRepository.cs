using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Shared.Model;
using Aplicacao.Infra.DataAccess.Repositories.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aplicacao.Infra.DataAccess.Repositories
{
    public class BaseRedisRepository<T, Tid> : IRedisRepository<T, Tid> where T : TEntity<Tid>
    {
        private readonly IDistributedCache _cacheRedis;

        public BaseRedisRepository(IDistributedCache cacheRedis)
        {
            _cacheRedis = cacheRedis;
        }

        public virtual async Task<T> Get(Tid key)
        {
            var dadosCache = await _cacheRedis.GetStringAsync(
                $"{nameof(CustomerRedisRepository)}:{key}");

            if (!string.IsNullOrEmpty(dadosCache))
            {
                T temp = JsonConvert.DeserializeObject<T>(dadosCache);
                return JsonConvert.DeserializeObject<T>(dadosCache);
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

            if (!string.IsNullOrEmpty(dadosCache))
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(dadosCache);
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
            var dadosJson = JsonConvert.SerializeObject(dadosCache, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            await _cacheRedis.SetStringAsync(
                $"{nameof(CustomerRedisRepository)}:{dadosCache.Id}", dadosJson);
        }

        public async Task Setm(IEnumerable<T> dadosCache)
        {
            var dadosJson = JsonConvert.SerializeObject(dadosCache, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            await _cacheRedis.SetStringAsync(
                $"{nameof(CustomerRedisRepository)}", dadosJson);
        }
    }
}