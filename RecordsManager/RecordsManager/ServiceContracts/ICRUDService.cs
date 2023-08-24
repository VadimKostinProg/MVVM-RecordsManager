using RecordsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManager.ServiceContracts
{
    public interface ICRUDService<TModel, TGroupedModel>
    {
        Task<TModel> GetByIdAsync(Guid id);
        Task<List<TModel>> GetByDateAsync(string date);
        Task<List<TGroupedModel>> GetAllGroupedAsync();
        Task CreateAsync(TModel model);
        Task UpdateAsync(TModel model);
        Task DeleteAsync(Guid id);
    }
}
