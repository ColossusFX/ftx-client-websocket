using System;

namespace Ftx.Client.Websocket.Responses.Trades
{
    public class Trade
    {
        public string Market { get; set; }
        public long Id { get; set; }
        public double Price { get; set; }
        public double Size { get; set; }
        public FtxSide Side { get; set; }
        public bool Liquidation { get; set; }
        public DateTimeOffset Time { get; set; }
    }
}