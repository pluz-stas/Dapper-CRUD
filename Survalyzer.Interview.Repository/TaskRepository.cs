using Survalyzer.Interview.Interfaces;
using Survalyzer.Interview.Interfaces.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace Survalyzer.Interview.Repository
{
    public class TaskRepository : BaseDapperRepository, ITaskRepository
    {
        public TaskRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<TaskRecord>> GetAllAsync(int skip, int take)
        {
            var sql = @"select * from [Tasks] order by [Id]
                            OFFSET     @skip ROWS
                            FETCH NEXT @take ROWS ONLY";
            return await ExecuteAsync(async db => await db.QueryAsync<TaskRecord>(sql, new { skip, take }));
        }

        public async Task<int> CreateAsync(TaskRecord model)
        {
            var sql = @"insert into [Tasks]
                                        ([Name ]
                                        ,[Description]
                                        ,[Done]
                                        ,[ProjectId])
                                        output INSERTED.Id
                                    values
                                        (@name,
                                         @description,
                                         @done,
                                         @projectId)";

            return await ExecuteAsync(async db => await db.QuerySingleAsync<int>(sql,
                new
                {
                    model.Name,
                    model.Description,
                    model.Done,
                    model.ProjectId,
                }));
        }

        public async Task<TaskRecord> GetByIdAsync(int id) =>
            await ExecuteAsync(async db =>
                await db.QueryFirstOrDefaultAsync<TaskRecord>("select * from [Tasks] where [Id] = @Id", new { id }));

        public async Task<bool> IsExistsAsync(int id) =>
            await ExecuteScalarAsync(db =>
                db.ExecuteScalarAsync<bool>(@"select count(1) from [Tasks] where [Id] = @externalId", new { id }));

        public async Task UpdateAsync(TaskRecord model)
        {
            var sql = @"update [Tasks]
                            set [Name] = @name,
                                [Description] = @description,
                                [Done] = @done,
                                [ProjectId] = @projectId
                                where [Id] = @id";

            await ExecuteAsync(async db => await db.ExecuteAsync(sql,
                new
                {
                    model.Name,
                    model.Description,
                    model.Done,
                    model.ProjectId,
                    model.ProjectName,
                    model.Id,
                }));
        }

        public async Task DeleteAsync(int id) => await ExecuteAsync(async db =>
            await db.ExecuteAsync(@"delete from [Tasks] where [Id] = @id", new { id }));

        public async Task<int> GetCountAsync() => await ExecuteScalarAsync(async db =>
            await db.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM [Tasks]"));
    }
}