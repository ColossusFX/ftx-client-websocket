namespace Ftx.Client.Websocket.Responses.Orders
{
    /// <summary>
    /// Order type - limit, market, etc.
    /// </summary>
    public enum OrderType
    {
        Undefined,
        Limit,
        Market,
        Stop,
        TrailingStop,
        Fok,
        StopLimit,
        TakeProfitLimit,
        TakeProfitMarket
    }
}