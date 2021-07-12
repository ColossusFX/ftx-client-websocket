using System;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ftx.Client.Websocket.Responses.Books
{
    public class OrderBookLevels
    {
        /// <summary>
        /// Checksum of response.
        /// </summary>
        public long Checksum { get; set; }

        /// <summary>
        /// Server timestamp. 
        /// </summary>
        //[JsonConverter(typeof(DateTimeOffsetConverter))]
        public double Time { get; set; }

        public FtxAction Action { get; set; }

        /// <summary>
        /// Order book bid levels
        /// </summary>
        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Buy)]
        public OrderBookLevel[] Bids { get; set; }

        /// <summary>
        /// Order book ask levels
        /// </summary>
        [JsonConverter(typeof(OrderBookLevelConverter), OrderBookSide.Sell)]
        public OrderBookLevel[] Asks { get; set; }
    }
}