using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses.Orders
{
    public class OrdersResponse: ResponseBase
    {
        public override MessageType Channel => MessageType.Orders;

        public OrderResponse Data { get; set; }
        
        public static bool TryHandle(string response, Subject<OrdersResponse> subject)
        {
            if (!FtxJsonSerializer.ContainsValue(response, "orders"))
                return false;

            var parsed = FtxJsonSerializer.Deserialize<OrdersResponse>(response);
            subject.OnNext(parsed);
            return true;
        }
    }
}