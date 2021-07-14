using System;
using Ftx.Client.Websocket.Responses.Trades;

namespace Ftx.Client.Websocket.Responses.Orders
{
    public class OrderResponse
    {
        public long Id { get; set; }
        public string ClientId { get; set; }
        public string Market { get; set; }
        public OrderType Type { get; set; }
        public FtxSide Side { get; set; }
        public double? Price { get; set; }
        public long? Size { get; set; }
        public OrderStatus Status { get; set; }
        public long? FilledSize { get; set; }
        public long? RemainingSize { get; set; }
        public bool ReduceOnly { get; set; }
        public bool Liquidation { get; set; }
        public double? AvgFillPrice { get; set; }
        public bool PostOnly { get; set; }
        public bool Ioc { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}