using AppCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Interfaces
{
    public class WooliesRepository<T> : IWooliesRepository<T> where T : class
    {
        private readonly ILogger<WooliesRepository<T>> _logger;
        private readonly AppDbContext _dbContext;
        DbSet<T> dbset;

        public WooliesRepository(AppDbContext dbContext, ILogger<WooliesRepository<T>> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
            dbset = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var result = await dbset.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        public async Task<T> GetById(int id)
        {
            try
            {
                return await dbset.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                dbset.Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        public async Task<T> Update(T entity)
        {
            try
            {
                dbset.Update(entity);
                if (_dbContext.Entry(entity).State == EntityState.Modified)
                {
                    await _dbContext.SaveChangesAsync();
                }
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return null;
            }
        }

        public async Task<bool> DeleteById(int id)
        {
            try
            {
                _dbContext.Remove(dbset.Find(id));
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return false;
            }
        }
    }
}
