namespace vueChain.Models
{
    public class Wallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; } // Ejemplo: "BTC"
        public decimal Quantity { get; set; } // Ejemplo: 0.5 BTC
        public DateTime LastUpdated { get; set; } // Fecha y hora de actualización
    }
}
