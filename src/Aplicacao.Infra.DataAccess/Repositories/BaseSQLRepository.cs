using Aplicacao.Domain.Interfaces.Repositories;
using Aplicacao.Domain.Shared.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aplicacao.Infra.DataAccess.Repositories
{
    public abstract class BaseSQLRepository<T, Tid>
        : ISQLRepository<T, Tid> where T : TEntity<Tid>
    {
        private readonly DbContext _context;

        public BaseSQLRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            //_context.Entry(entity).State = EntityState.Added;

            return entity;
        }

        public async Task Delete(Tid id)
        {
            var entity = await this.ReadById(id);

            _context.Set<T>().Remove(entity);

            //_context.Entry(entity).State = EntityState.Deleted;
        }

        //TODO: criar paginacao
        public async Task<IEnumerable<T>> ReadAll()
        {
            IQueryable<T> result = _context.Set<T>();

            return await result.ToListAsync();
        }

        public async Task<T> ReadById(Tid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> Update(T entity)
        {
            return _context.Update(entity).Entity;

            //_context.Entry(entity).State = EntityState.Modified;
        }
    }
}