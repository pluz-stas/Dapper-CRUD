using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Survalyzer.Interview.Interfaces;
using Survalyzer.Interview.Interfaces.Entity;
using Survalyzer.Interview.Server.Contracts;
using Survalyzer.Interview.Server.Extensions;

namespace Survalyzer.Interview.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }


        /// <summary>
        /// Gets tasks asynchronously.
        /// </summary>
        /// <param name="skip">Presents data for pagination.</param>
        /// <param name="take">Presents data for pagination.</param>
        /// <returns><see cref="IEnumerable{T}"/>Collection of tasks.</returns>
        /// <response code="200">Returns tasks.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IEnumerable<ShortTaskInfoContract>> GetAsync(int skip, int take) =>
            (await _taskRepository.GetAllAsync(skip, take)).Select(x => x.ToShortInfoContract());
        
        /// <summary>
        /// Gets task by id.
        /// </summary>
        /// <param name="id">Task id.</param>
        /// <returns>Instance of <see cref="TaskContract"/>.</returns>
        /// <response code="200">Returns task.</response>       
        /// <response code="404">A task not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<TaskContract> GetAsync(int id) => (await _taskRepository.GetByIdAsync(id)).ToContract();
        
        /// <summary>
        /// Creates a task.
        /// </summary>
        /// <param name="createTaskContract">Task model.</param>
        /// <returns>A newly created task.</returns>
        /// <response code="201">Returns the newly created task.</response>
        /// <response code="400">If the item is null</response>        
        /// <response code="500">There are any server problems.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TaskContract>> Post([FromBody] CreateTaskContract createTaskContract)
        {
            var taskModel = createTaskContract.ToEntity();
        
            taskModel.Id = await _taskRepository.CreateAsync(taskModel);
        
            return CreatedAtAction("Get", new { id = taskModel.Id }, taskModel.ToContract());
        }
        
        /// <summary>
        /// Updates existent task.
        /// </summary>
        /// <param name="id">Existent task id.</param>
        /// <param name="contract">Updated task model.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="400">If the model is null or id does not match the task model.</response>  
        /// <response code="404">A task not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] EditTaskContract contract)
        {
            if (id != contract.Id)
                return BadRequest();
        
            await _taskRepository.UpdateAsync(contract.ToEntity());
        
            return NoContent();
        }
        
        /// <summary>
        /// Deletes existent task.
        /// </summary>
        /// <param name="id">Existent task id.</param>
        /// <response code="204">Returns NoContent response.</response>
        /// <response code="404">A task not found.</response>
        /// <response code="500">There are any server problems.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskRepository.DeleteAsync(id);
        
            return NoContent();
        }
    }
}
