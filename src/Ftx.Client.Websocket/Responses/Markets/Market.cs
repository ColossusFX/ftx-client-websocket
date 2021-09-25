using Newtonsoft.Json;

namespace Ftx.Client.Websocket.Responses.Markets
{
    public class Market
    {
        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("enabled")] public bool Enabled { get; set; }

        [JsonProperty("postOnly")] public bool PostOnly { get; set; }

        [JsonProperty("priceIncrement")] public double PriceIncrement { get; set; }

        [JsonProperty("sizeIncrement")] public double SizeIncrement { get; set; }

        [JsonProperty("type")] public MarketType Type { get; set; }

        [JsonProperty("baseCurrency")] public string BaseCurrency { get; set; }

        [JsonProperty("quoteCurrency")] public string QuoteCurrency { get; set; }

        [JsonProperty("restricted")] public bool Restricted { get; set; }

        [JsonProperty("underlying")] public string Underlying { get; set; }

        [JsonProperty("future")] public Future Future { get; set; }

        [JsonProperty("highLeverageFeeExempt")]
        public bool HighLeverageFeeExempt { get; set; }

        [JsonProperty("minProvideSize")] public double? MinProvideSize { get; set; }
    }
}