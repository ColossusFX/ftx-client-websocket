using System.Collections.Generic;
using Newtonsoft.Json;

namespace Ftx.Client.Websocket.Responses.Markets
{
    public class Markets
    {
        [JsonProperty("data")]
        public Dictionary<string, Market> MarketsData { get; set; }
        
        public FtxAction Action { get; set; }
    }
}