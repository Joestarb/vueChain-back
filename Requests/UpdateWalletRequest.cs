namespace vueChain.Requests
{
    public class UpdateWalletRequest
    {
        public int UserId { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public string TransactionType { get; set; } // "Buy" o "Sell"
    }

}
