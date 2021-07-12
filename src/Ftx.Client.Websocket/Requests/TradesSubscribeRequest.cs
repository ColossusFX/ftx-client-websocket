using System.Runtime.Serialization;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Validations;

namespace Ftx.Client.Websocket.Requests
{
    public class TradesSubscribeRequest : SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to The trades channel provides data on all trades in the market.
        /// All messages are updates of new trades (update) and the data field contains: - price: Price of the trade - size: Size of the trade - side: Side of the taker in the trade - liquidation: true if the trade involved a liquidation order, else false - time: Timestamp
        /// </summary>
        public TradesSubscribeRequest()
        {
            Market = string.Empty;
        }

        /// <summary>
        /// Subscribe to trades from selected pair ('BTC-PERP', etc)
        /// </summary>
        public TradesSubscribeRequest(string pair)
        {
            FtxValidations.ValidateInput(pair, nameof(pair));

            Market = pair;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        public override string Channel => "trades";


        /// <inheritdoc />
        public override string Market { get; }
    }
}