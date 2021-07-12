using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses.Trades
{
    public class TradeResponse : ResponseBase
    {
        public Trade[] Data { get; set; }

        public override MessageType Channel => MessageType.Trades;

        public string Market { get; set; }

        public static bool TryHandle(string response, Subject<TradeResponse> subject)
        {
            if (!FtxJsonSerializer.ContainsValue(response, "trades"))
                return false;

            var parsed = FtxJsonSerializer.Deserialize<TradeResponse>(response);

            subject.OnNext(parsed);
            return true;
        }
    }
}