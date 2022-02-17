using System.Collections.Generic;
using Survalyzer.Interview.Interfaces.Entity;

namespace Survalyzer.Interview.Interfaces.Response
{
    public class GetTaskListResponse
    {
        public List<TaskRecord> Tasks { get; set; }
    }
}
