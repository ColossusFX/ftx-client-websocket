using System.Collections.Generic;
using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses.Markets
{
    public class MarketsResponse : ResponseBase
    {
        /// <inheritdoc />
        public override MessageType Channel => MessageType.Markets;
        public Markets Data { get; set; }

        internal static bool TryHandle(string response, ISubject<MarketsResponse> subject)
        {
            // if (!FtxJsonSerializer.ContainsValue(response, "partial") ||
            //     !FtxJsonSerializer.ContainsValue(response, "markets"))
            if (!FtxJsonSerializer.ContainsValue(response, "markets"))
                return false;

            var parsed = FtxJsonSerializer.Deserialize<MarketsResponse>(response);
            subject.OnNext(parsed);

            return true;
        }
    }
}