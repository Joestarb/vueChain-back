﻿namespace vueChain.Dtos
{
    public class CandlestickDto
    {
        public DateTime OpenTime { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal BaseVolume { get; set; }
        public DateTime CloseTime { get; set; }
    }
}
