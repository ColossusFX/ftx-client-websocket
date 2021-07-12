using System.Runtime.Serialization;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Validations;

namespace Ftx.Client.Websocket.Requests
{
    public class MarketsSubscribeRequest: SubscribeRequestBase
    {
        /// <summary>
        /// Orders topic
        /// </summary>
        //[IgnoreDataMember]
        public override string Channel => "markets";

    }
}
