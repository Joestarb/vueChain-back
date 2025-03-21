namespace vueChain.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string? Details { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }

}
