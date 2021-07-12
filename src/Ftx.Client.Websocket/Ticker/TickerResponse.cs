using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Responses;

namespace Ftx.Client.Websocket.Ticker
{
    public class TickerResponse : ResponseBase
    {
        public override MessageType Channel => MessageType.Ticker;

        public string Market { get; set; }
        
        public Ticker Data { get; set; }

        public static bool TryHandle(string response, Subject<TickerResponse> subject)
        {
            if (!FtxJsonSerializer.ContainsValue(response, "ticker"))
                return false;
            
            var parsed = FtxJsonSerializer.Deserialize<TickerResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}