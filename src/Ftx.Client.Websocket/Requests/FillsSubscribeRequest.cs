using System.Runtime.Serialization;
using Ftx.Client.Websocket.Validations;

namespace Ftx.Client.Websocket.Requests
{
    public class FillsSubscribeRequest: SubscribeRequestBase
    {
        /// <summary>
        /// Fills topic
        /// </summary>
        //[IgnoreDataMember]
        public override string Channel => "fills";

    }
}
