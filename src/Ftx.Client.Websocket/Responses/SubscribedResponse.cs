using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses
{
    public class SubscribedResponse : ResponseBase
    {
        public override MessageType Type => MessageType.Subscribed;

        public string Market { get; set; }

        internal static bool TryHandle(string response, ISubject<SubscribedResponse> subject)
        {
            if (!FtxJsonSerializer.ContainsValue(response, "subscribed") &&
                !FtxJsonSerializer.ContainsValue(response, "unsubscribed"))
                return false;

            var parsed = FtxJsonSerializer.Deserialize<SubscribedResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}