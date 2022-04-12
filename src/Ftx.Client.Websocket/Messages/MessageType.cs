using System.Net.NetworkInformation;
using System.Runtime.Serialization;

namespace Ftx.Client.Websocket.Messages
{
    public enum MessageType
    {
        // Do not rename, used in requests
        [DataMember(Name = "ping")] Ping,
        [DataMember(Name = "login")] Login,
        [DataMember(Name = "error")] Error,
        [DataMember(Name = "subscribe")] Subscribe,
        [DataMember(Name = "unsubscribe")] Unsubscribe,
        [DataMember(Name = "subscribed")] Subscribed,
        [DataMember(Name = "pong")] Pong,
        [DataMember(Name = "markets")] Markets,
        [DataMember(Name = "fills")] Fills,
        [DataMember(Name = "orders")] Orders,
        [DataMember(Name = "orderbook")] OrderBook,
        [DataMember(Name = "orderbookGrouped")] OrderBookGrouped,
        [DataMember(Name = "ticker")] Ticker,
        [DataMember(Name = "trades")] Trades,
        [DataMember(Name = "partial")] Partial,
        [DataMember(Name = "update")] Update

        // Can be renamed, only for responses
        //Error,
        //Pong,
        //Info,
        //Trade,
        //Orderbook,
        //Ticker,
        //Order,
        //Position,
        //Execution,
        //Partial,
        //Update,
        //Subscribed,
        //Unsubscribed,
        //Markets
    }
}