namespace CryptoWatchAPI.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public decimal last { get; set; }
        public decimal high { get; set; }
        public decimal change { get; set; }
        public decimal low { get; set; }
        public decimal volume { get; set; }
    }
}