using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses.Markets
{
    public class MarketsSnapshotResponse : ResponseBase
    {
        /// <inheritdoc />
        public override MessageType Channel => MessageType.Markets;

        public Market[] Data { get; set; }

        internal static bool TryHandle(string response, ISubject<MarketsSnapshotResponse> subject)
        {
            // if (!FtxJsonSerializer.ContainsValue(response, "topicName") ||
            //     !FtxJsonSerializer.ContainsValue(response, "markets"))
            
            if (!FtxJsonSerializer.ContainsValue(response, "partial") &&
                 !FtxJsonSerializer.ContainsValue(response, "markets"))
                return false;

            var parsed = FtxJsonSerializer.Deserialize<MarketsSnapshotResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}