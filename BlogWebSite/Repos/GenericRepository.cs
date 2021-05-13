using BlogWebSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogWebSite.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private BlogManagementContext _context = new BlogManagementContext();
        private DbSet<T> dbEntity;


        public GenericRepository(BlogManagementContext context)
        {
            this._context = context;
            dbEntity = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> Query(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbEntity;
            try
            {
                return await query.Where(filter).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<List<T>> GetAll()
        {
            try
            {
                return await dbEntity.ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public T GetModelById(int modelId)
        {
            try
            {
                T model = dbEntity.Find(modelId);
                if (model != null)
                    return model;
                else
                    return null;

            }

            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<bool> InsertModel(T model)
        {
            try
            {
                dbEntity.Add(model);
                int x = await _context.SaveChangesAsync();
                return x == 0 ? false : true;
            }
            catch (Exception e)
            {

                throw e;

            }
        }


        public async Task<bool> UpdateModel(T model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                int x = await _context.SaveChangesAsync();
                return x == 0 ? false : true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> DeleteModel(int modelId)
        {
            try
            {
                T model = dbEntity.Find(modelId);
                dbEntity.Remove(model);
                int x = await _context.SaveChangesAsync();
                return x == 0 ? false : true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
