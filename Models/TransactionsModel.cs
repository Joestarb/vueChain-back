namespace vueChain.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; } // "Buy", "Sell", "Alert"
        public DateTime Timestamp { get; set; }

        // Nueva propiedad para la relación con el usuario
        public int UserId { get; set; }
        public User User { get; set; }
    }
}