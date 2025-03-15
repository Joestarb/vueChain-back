namespace vueChain.Dtos
{
    public class TransactionsDto
    {
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
