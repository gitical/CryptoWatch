using System.Collections.Generic;

namespace CryptoWatchAPI.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public string Last { get; set; }
        public string High { get; set; }
        public string Change { get; set; }
        public string Low { get; set; }
        public string Volume { get; set; }
    }

}