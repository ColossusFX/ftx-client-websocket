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
    public class GroupedOrderBookSnapshotResponse : ResponseBase
    {
        public override MessageType Type => MessageType.Partial;
        
        public string Market { get; set; }
        
        public double Grouping { get; set; }
        
        public OrderBookLevels Data { get; set; }
        
        internal static bool TryHandle(string response, ISubject<GroupedOrderBookSnapshotResponse> subject, string topicName)
        {
            if (!FtxJsonSerializer.ContainsValue(response, topicName) ||
                !FtxJsonSerializer.ContainsValue(response, "orderbookGrouped"))
                return false;
            
            var parsed = FtxJsonSerializer.Deserialize<GroupedOrderBookSnapshotResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}