using System.Runtime.Serialization;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Validations;

namespace Ftx.Client.Websocket.Requests
{
    /// <summary>
    /// Subscribe to order book L2 stream
    /// </summary>
    public class GroupedBookSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to order book from all pairs
        /// </summary>
        public GroupedBookSubscribeRequest()
        {
            Market = string.Empty;
        }

        /// <summary>
        /// Subscribe to order book from selected pair ('BTC-PERP', etc)
        /// </summary>
        public GroupedBookSubscribeRequest(string pair, double grouping)
        {
            FtxValidations.ValidateInput(pair, nameof(pair));

            Market = pair;
            Grouping = grouping;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        //[IgnoreDataMember]
        public override string Channel => "orderbookGrouped";


        /// <inheritdoc />
        //[IgnoreDataMember]
        public override string Market { get; }

        public double Grouping { get; set; }
    }
}