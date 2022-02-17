using Survalyzer.Interview.Interfaces.Request;

namespace Survalyzer.Interview.Server.Contracts
{
    public class CreateTaskContract
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }

        public long ProjectId { get; set; }
    }
}