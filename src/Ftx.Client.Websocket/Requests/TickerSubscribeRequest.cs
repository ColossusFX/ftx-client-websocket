using System.Runtime.Serialization;
using Ftx.Client.Websocket.Validations;

namespace Ftx.Client.Websocket.Requests
{
    public class TickerSubscribeRequest: SubscribeRequestBase
    {
        /// <summary>
        /// Subscribe to ticker
        /// </summary>
        public TickerSubscribeRequest()
        {
            Market = string.Empty;
        }

        /// <summary>
        /// Subscribe to order book from selected pair ('BTC-PERP', etc)
        /// </summary>
        public TickerSubscribeRequest(string pair)
        {
            FtxValidations.ValidateInput(pair, nameof(pair));

            Market = pair;
        }

        /// <summary>
        /// Order book L2 topic
        /// </summary>
        public override string Channel => "ticker";


        /// <inheritdoc />
        public override string Market { get; }       
    }
}
