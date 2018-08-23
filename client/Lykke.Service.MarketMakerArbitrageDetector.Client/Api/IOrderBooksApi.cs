﻿using System.Collections.Generic;
using Lykke.Service.MarketMakerArbitrageDetector.Client.Models;
using Refit;

namespace Lykke.Service.MarketMakerArbitrageDetector.Client.Api
{
    public interface IOrderBooksApi
    {
        [Get("/api/orderBooks")]
        IReadOnlyCollection<OrderBook> GetAll();
    }
}
