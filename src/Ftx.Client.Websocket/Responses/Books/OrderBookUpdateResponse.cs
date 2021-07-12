﻿using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
 using Ftx.Client.Websocket.Messages;
 using Newtonsoft.Json;
 using Newtonsoft.Json.Linq;

 namespace Ftx.Client.Websocket.Responses.Books
{
    /// <summary>
    /// Order book update/diff
    /// </summary>
    public class OrderBookUpdateResponse : ResponseBase
    {
        public override MessageType Type => MessageType.Update;
        /// <summary>
        /// Target product id
        /// </summary>
        public string Market { get; set; }

        /// <summary>
        /// Order book changes.
        /// Please note that size is the updated size at that price level, not a delta.
        /// A size of "0" indicates the price level can be removed.
        /// </summary>
        public OrderBookLevels Data { get; set; }

        internal static bool TryHandle(string response, ISubject<OrderBookUpdateResponse> subject, string topicName)
        {
            if (!FtxJsonSerializer.ContainsValue(response, topicName) ||
                !FtxJsonSerializer.ContainsValue(response, "orderbook"))
                return false;
            
            var parsed = FtxJsonSerializer.Deserialize<OrderBookUpdateResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}