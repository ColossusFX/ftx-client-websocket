using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Ftx.Client.Websocket.Responses.Orders
{
    /// <summary>
    /// Order type - limit, market, etc.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderType
    {
        Undefined,
        [EnumMember(Value = "limit")]
        Limit,
        [EnumMember(Value = "market")]
        Market,
        [EnumMember(Value = "stop")]
        Stop,
        [EnumMember(Value = "trailingStop")]
        TrailingStop,
        [EnumMember(Value = "fok")]
        Fok,
        [EnumMember(Value = "stopLimit")]
        StopLimit,
        [EnumMember(Value = "takeProfitLimit")]
        TakeProfitLimit,
        [EnumMember(Value = "takeProfitMarket")]
        TakeProfitMarket
    }
}