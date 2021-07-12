using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Ftx.Client.Websocket.Responses;
using Ftx.Client.Websocket.Responses.Books;
using Ftx.Client.Websocket.Responses.Fills;
using Ftx.Client.Websocket.Responses.Markets;
using Ftx.Client.Websocket.Responses.Orders;
using Ftx.Client.Websocket.Responses.Trades;
using Ftx.Client.Websocket.Ticker;

namespace Ftx.Client.Websocket.Client
{
    public class FtxClientStreams
    {
        internal readonly Subject<ErrorResponse> ErrorSubject = new Subject<ErrorResponse>();
        internal readonly Subject<PongResponse> PongSubject = new Subject<PongResponse>();
        internal readonly Subject<TradeResponse> TradesSubject = new Subject<TradeResponse>();

        internal readonly Subject<OrderBookSnapshotResponse> OrderBookSnapshotSubject =
            new Subject<OrderBookSnapshotResponse>();

        internal readonly Subject<OrderBookUpdateResponse> OrderBookUpdateSubject =
            new Subject<OrderBookUpdateResponse>();

        internal readonly Subject<MarketsResponse> MarketsSubject = new Subject<MarketsResponse>();

        internal readonly Subject<MarketsSnapshotResponse> MarketsSnapshotSubject =
            new Subject<MarketsSnapshotResponse>();

        internal readonly Subject<OrdersResponse> OrdersSubject = new Subject<OrdersResponse>();
        internal readonly Subject<FillsResponse> FillsSubject = new Subject<FillsResponse>();
        internal readonly Subject<TickerResponse> TickerSubject = new Subject<TickerResponse>();
        internal readonly Subject<SubscribedResponse> SubscribedSubject = new Subject<SubscribedResponse>();
        internal readonly Subject<string> UnhandledMessageSubject = new Subject<string>();

        // PUBLIC

        /// <summary>
        /// Server errors stream
        /// </summary>
        public IObservable<SubscribedResponse> SubscribedStream => SubscribedSubject.AsObservable();

        /// <summary>
        /// Server errors stream
        /// </summary>
        public IObservable<ErrorResponse> ErrorStream => ErrorSubject.AsObservable();

        /// <summary>
        /// Response stream to every ping request
        /// </summary>
        public IObservable<PongResponse> PongStream => PongSubject.AsObservable();

        /// <summary>
        /// Trades stream - emits every executed trade on Bitmex
        /// </summary>
        public IObservable<TradeResponse> TradesStream => TradesSubject.AsObservable();

        public IObservable<OrderBookSnapshotResponse> OrderBookSnapshotStream =>
            OrderBookSnapshotSubject.AsObservable();

        public IObservable<OrderBookUpdateResponse> OrderBookUpdateStream => OrderBookUpdateSubject.AsObservable();

        /// <summary>
        /// Stream of all Trade-able Contracts, Indices, and History
        /// </summary>
        public IObservable<MarketsResponse> MarketsStream => MarketsSubject.AsObservable();

        public IObservable<MarketsSnapshotResponse> MarketsShapshotStream => MarketsSnapshotSubject.AsObservable();

        /// <summary>
        /// Stream of all Trade-able Contracts, Indices, and History
        /// </summary>
        public IObservable<TickerResponse> TickerStream => TickerSubject.AsObservable();

        // PRIVATE

        /// <summary>
        /// Stream of all your active orders
        /// </summary>
        public IObservable<OrdersResponse> OrdersStream => OrdersSubject.AsObservable();

        /// <summary>
        /// Stream of all your active orders
        /// </summary>
        public IObservable<FillsResponse> FillsStream => FillsSubject.AsObservable();


        /// <summary>
        /// Stream of all raw unhandled messages (that are not yet implemented)
        /// </summary>
        public IObservable<string> UnhandledMessageStream => UnhandledMessageSubject.AsObservable();
    }
}