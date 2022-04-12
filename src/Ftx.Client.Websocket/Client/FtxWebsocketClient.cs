using System;
using Ftx.Client.Websocket.Communicator;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Logging;
using Ftx.Client.Websocket.Messages;
using Ftx.Client.Websocket.Requests;
using Ftx.Client.Websocket.Responses;
using Ftx.Client.Websocket.Responses.Books;
using Ftx.Client.Websocket.Responses.Fills;
using Ftx.Client.Websocket.Responses.Markets;
using Ftx.Client.Websocket.Responses.Orders;
using Ftx.Client.Websocket.Responses.Trades;
using Ftx.Client.Websocket.Ticker;
using Ftx.Client.Websocket.Validations;
using Newtonsoft.Json.Linq;
using Utf8Json;
using Utf8Json.Resolvers;
using Websocket.Client;

namespace Ftx.Client.Websocket.Client
{
    /// <summary>
    /// Bitmex websocket client.
    /// Use method `Send()` to subscribe to channels.
    /// And `Streams` to subscribe. 
    /// </summary>
    public class FtxWebsocketClient : IDisposable
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();

        private readonly IFtxCommunicator _communicator;
        private readonly IDisposable _messageReceivedSubscription;

        /// <inheritdoc />
        public FtxWebsocketClient(IFtxCommunicator communicator)
        {
            FtxValidations.ValidateInput(communicator, nameof(communicator));

            _communicator = communicator;
            _messageReceivedSubscription = _communicator.MessageReceived.Subscribe(HandleMessage);

            JsonSerializer.SetDefaultResolver(StandardResolver.CamelCase);
        }

        /// <summary>
        /// Provided message streams
        /// </summary>
        public FtxClientStreams Streams { get; } = new FtxClientStreams();

        /// <summary>
        /// Cleanup everything
        /// </summary>
        public void Dispose()
        {
            _messageReceivedSubscription?.Dispose();
        }

        /// <summary>
        /// Serializes request and sends message via websocket communicator. 
        /// It logs and re-throws every exception. 
        /// </summary>
        /// <param name="request">Request/message to be sent</param>
        public void Send<T>(T request) where T : RequestBase
        {
            try
            {
                FtxValidations.ValidateInput(request, nameof(request));

                var serialized = FtxJsonSerializer.Serialize(request);
                _communicator.Send(serialized);
            }
            catch (Exception e)
            {
                Log.Error(e, L($"Exception while sending message '{request}'. Error: {e.Message}"));
                throw;
            }
        }

        /// <summary>
        /// Sends authentication request via websocket communicator
        /// </summary>
        /// <param name="apiKey">Your API key</param>
        /// <param name="apiSecret">Your API secret</param>
        public void Authenticate(string apiKey, string apiSecret, string subAccount)
        {
            Send(new AuthenticationRequest(apiKey, apiSecret, subAccount));
        }

        private string L(string msg)
        {
            return $"[FTX WEBSOCKET CLIENT] {msg}";
        }

        private void HandleMessage(ResponseMessage message)
        {
            try
            {
                bool handled;
                var messageSafe = (message.Text ?? string.Empty).Trim();

                if (messageSafe.StartsWith("{"))
                {
                    handled = HandleObjectMessage(messageSafe);
                    if (handled)
                        return;
                }

                handled = HandleRawMessage(messageSafe);
                if (handled)
                    return;

                if (!string.IsNullOrWhiteSpace(messageSafe))
                    Streams.UnhandledMessageSubject.OnNext(messageSafe);
                Log.Warn(L($"Unhandled response:  '{messageSafe}'"));
            }
            catch (Exception e)
            {
                Log.Error(e, L("Exception while receiving message"));
            }
        }

        private bool HandleRawMessage(string msg)
        {
            // ********************
            // ADD RAW HANDLERS BELOW
            // ********************

            return true;
        }

        private bool HandleObjectMessage(string msg)
        {
            // ********************
            // ADD OBJECT HANDLERS BELOW
            // ********************
            //var response = FtxJsonSerializer.Deserialize<JObject>(msg);
            return
                SubscribedResponse.TryHandle(msg, Streams.SubscribedSubject) ||
                ErrorResponse.TryHandle(msg, Streams.ErrorSubject) ||
                PongResponse.TryHandle(msg, Streams.PongSubject) ||
                TickerResponse.TryHandle(msg, Streams.TickerSubject) ||
                OrderBookUpdateResponse.TryHandle(msg, Streams.OrderBookUpdateSubject, "update") ||
                OrderBookSnapshotResponse.TryHandle(msg, Streams.OrderBookSnapshotSubject, "partial") ||
                GroupedOrderBookUpdateResponse.TryHandle(msg, Streams.GroupedOrderBookUpdateSubject, "update") ||
                GroupedOrderBookSnapshotResponse.TryHandle(msg, Streams.GroupedOrderBookSnapshotSubject, "partial") ||
                // MarketsUpdateResponse.TryHandle(msg, Streams.MarketsUpdateSubject, "update") ||
                // MarketsSnapshotResponse.TryHandle(msg, Streams.MarketsSnapshotSubject, "partial") ||
                MarketsResponse.TryHandle(msg, Streams.MarketsSubject) ||
                TradeResponse.TryHandle(msg, Streams.TradesSubject) ||
                OrdersResponse.TryHandle(msg, Streams.OrdersSubject) ||
                FillsResponse.TryHandle(msg, Streams.FillsSubject);
        }
    }
}