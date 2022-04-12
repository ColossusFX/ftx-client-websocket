using System;
using Ftx.Client.Websocket.Responses.Orders;

namespace Ftx.Client.Websocket.Responses.Fills
{
    public class FillResponse
    {
        public long Id { get; set; }
        public string Market { get; set; }
        public string Future { get; set; }
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public OrderType Type { get; set; }
        public FtxSide Side { get; set; }
        public double? Price { get; set; }
        public double? Size { get; set; }
        public long? OrderId { get; set; }
        public DateTimeOffset Time { get; set; }
        public long? TradeId { get; set; }
        public double? FeeRate { get; set; }
        public double? Fee { get; set; }
        public string FeeCurrency { get; set; }
        public string Liquidity { get; set; }
        public long ClientOrderId { get; set; }
    }
}