namespace Ftx.Client.Websocket.Messages
{
    public class MessageBase
    {
        /// <summary>
        /// Unique operation, is serialized as "op": "command"
        /// </summary>
        public virtual MessageType Op { get; set; }
    }
}