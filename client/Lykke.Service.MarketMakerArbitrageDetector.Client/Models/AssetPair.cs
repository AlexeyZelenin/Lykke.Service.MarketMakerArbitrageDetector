﻿namespace Lykke.Service.MarketMakerArbitrageDetector.Client.Models
{
    public class AssetPair
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
