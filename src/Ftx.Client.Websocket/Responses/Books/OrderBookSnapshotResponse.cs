﻿using System;
 using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
 using Ftx.Client.Websocket.Messages;
 using Newtonsoft.Json;
 using Newtonsoft.Json.Linq;

 namespace Ftx.Client.Websocket.Responses.Books
{
    /// <summary>
    /// Order book snapshot
    /// </summary>
    public class OrderBookSnapshotResponse : ResponseBase
    {
        public override MessageType Type => MessageType.Partial;
        
        public string Market { get; set; }
        
        public OrderBookLevels Data { get; set; }
        
        internal static bool TryHandle(string response, ISubject<OrderBookSnapshotResponse> subject, string topicName)
        {
            if (!FtxJsonSerializer.ContainsValue(response, topicName) ||
                !FtxJsonSerializer.ContainsValue(response, "orderbook"))
                return false;
            
            var parsed = FtxJsonSerializer.Deserialize<OrderBookSnapshotResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}