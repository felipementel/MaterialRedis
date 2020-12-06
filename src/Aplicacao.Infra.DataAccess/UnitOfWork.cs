using Aplicacao.Domain.UoW;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Aplicacao.Infra.DataAccess
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DbContext _context;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task<bool> Commit()
        {
            if (await _context.SaveChangesAsync() > 0)
                return true; //Successful
            return false; //Not successful
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}