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
    public class GroupedOrderBookUpdateResponse : ResponseBase
    {
        public override MessageType Type => MessageType.Update;
        /// <summary>
        /// Target product id
        /// </summary>
        public string Market { get; set; }
        
        public double Grouping { get; set; }

        /// <summary>
        /// Order book changes.
        /// Please note that size is the updated size at that price level, not a delta.
        /// A size of "0" indicates the price level can be removed.
        /// </summary>
        public OrderBookLevels Data { get; set; }

        internal static bool TryHandle(string response, ISubject<GroupedOrderBookUpdateResponse> subject, string topicName)
        {
            if (!FtxJsonSerializer.ContainsValue(response, topicName) ||
                !FtxJsonSerializer.ContainsValue(response, "orderbookGrouped"))
                return false;
            
            var parsed = FtxJsonSerializer.Deserialize<GroupedOrderBookUpdateResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}