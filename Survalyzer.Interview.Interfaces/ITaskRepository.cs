using Survalyzer.Interview.Interfaces.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survalyzer.Interview.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<TaskRecord>> GetAllAsync(int skip, int take);

        Task<int> CreateAsync(TaskRecord model);

        Task<TaskRecord> GetByIdAsync(int id);

        Task<bool> IsExistsAsync(int id);

        Task UpdateAsync(TaskRecord model);

        Task DeleteAsync(int id);

        Task<int> GetCountAsync();
    }
}
