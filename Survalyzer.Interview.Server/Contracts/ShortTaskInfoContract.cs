namespace Survalyzer.Interview.Server.Contracts
{
    public class ShortTaskInfoContract
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }
    }
}