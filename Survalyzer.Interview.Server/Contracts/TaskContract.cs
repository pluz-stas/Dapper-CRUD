namespace Survalyzer.Interview.Server.Contracts
{
    public class TaskContract
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }

        public long ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}