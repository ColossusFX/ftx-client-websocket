using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses
{
    public class ResponseBase
    {
        public virtual MessageType Type { get; set; }

        public virtual MessageType Channel { get; set; }
    }
}