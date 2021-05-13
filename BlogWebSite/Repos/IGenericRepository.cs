using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogWebSite.Repos
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Query(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAll();
        T GetModelById(int modelId);
        Task<bool> InsertModel(T model);
        Task<bool> UpdateModel(T model);
        Task<bool> DeleteModel(int modelId);
    }
}
