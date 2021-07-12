using System.Runtime.Serialization;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Validations;

namespace Ftx.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to order book L2 stream
    /// </summary>
    public class BookSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to order book from all pairs
        /// </summary>
        public BookSubscribeRequest()
        {
            Market = string.Empty;
        }

        /// <summary>
        /// Subscribe to order book from selected pair ('BTC-PERP', etc)
        /// </summary>
        public BookSubscribeRequest(string pair)
        {
            FtxValidations.ValidateInput(pair, nameof(pair));

            Market = pair;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        //[IgnoreDataMember]
        public override string Channel => "orderbook";


        /// <inheritdoc />
        //[IgnoreDataMember]
        public override string Market { get; }       
    }
}
