using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Requests
{
    public class PingRequest: RequestBase
    {
        /// <inheritdoc />
        public override MessageType Operation => MessageType.Ping;
    }
}
