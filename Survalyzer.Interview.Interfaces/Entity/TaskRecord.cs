namespace Survalyzer.Interview.Interfaces.Entity
{
    public class TaskRecord
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }

        public long ProjectId { get; set; }

        public string ProjectName { get; set; }
    }
}
