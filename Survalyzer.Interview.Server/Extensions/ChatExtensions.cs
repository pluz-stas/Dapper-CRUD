using System;
using Survalyzer.Interview.Interfaces.Entity;
using Survalyzer.Interview.Server.Contracts;

namespace Survalyzer.Interview.Server.Extensions
{
    /// <summary>
    /// Extensions for task models.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Converts <see cref="TaskRecord"/> model to <seealso cref="TaskContract"/> contract.
        /// </summary>
        /// <param name="model">Task model.</param>
        /// <returns><see cref="TaskContract"/> contract.</returns>
        public static TaskContract ToContract(this TaskRecord model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(TaskRecord));

            return new TaskContract
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Done = model.Done,
                ProjectId = model.ProjectId,
                ProjectName = model.ProjectName
            };
        }

        /// <summary>
        /// Converts <see cref="TaskRecord"/> model to <seealso cref="ShortTaskInfoContract"/> contract.
        /// </summary>
        /// <param name="model">Task model.</param>
        /// <returns><see cref="ShortTaskInfoContract"/> contract.</returns>
        public static ShortTaskInfoContract ToShortInfoContract(this TaskRecord model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            return new ShortTaskInfoContract
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                Done = model.Done
            };
        }

        /// <summary>
        /// Converts <see cref="CreateTaskContract"/> contract to <seealso cref="TaskRecord"/> model.
        /// </summary>
        /// <param name="contract">Task contract.</param>
        /// <returns><see cref="TaskRecord"/> model.</returns>
        public static TaskRecord ToEntity(this CreateTaskContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(CreateTaskContract));

            return new TaskRecord
            {
                Name = contract.Name,
                Description = contract.Description,
                Done = contract.Done,
                ProjectId = contract.ProjectId,
            };
        }

        /// <summary>
        /// Converts <see cref="EditTaskContract"/> contract to <seealso cref="TaskRecord"/> model.
        /// </summary>
        /// <param name="contract">Task contract.</param>
        /// <returns><see cref="TaskRecord"/> model.</returns>
        public static TaskRecord ToEntity(this EditTaskContract contract)
        {
            if (contract == null)
                throw new ArgumentNullException(nameof(EditTaskContract));

            return new TaskRecord
            {
                Id = contract.Id,
                Name = contract.Name,
                Description = contract.Description,
                Done = contract.Done,
                ProjectId = contract.ProjectId,
            };
        }
    }
}
