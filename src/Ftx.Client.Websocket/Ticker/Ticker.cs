
namespace Ftx.Client.Websocket.Ticker
{
    public class Ticker
    {
        public long Bid { get; set; }
        
        public double Ask { get; set; }
        
        public double BidSize { get; set; }
        
        public double AskSize { get; set; }
        
        public long Last { get; set; }
        
        public double Time { get; set; }
    }
}