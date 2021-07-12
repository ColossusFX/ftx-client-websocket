using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses.Fills
{
    public class FillsResponse : ResponseBase
    {
        public override MessageType Channel => MessageType.Fills;
        
        public FillResponse Data { get; set; }
        public static bool TryHandle(string response, Subject<FillsResponse> subject)
        {
            if (!FtxJsonSerializer.ContainsValue(response, "fills"))
                return false;

            var parsed = FtxJsonSerializer.Deserialize<FillsResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}