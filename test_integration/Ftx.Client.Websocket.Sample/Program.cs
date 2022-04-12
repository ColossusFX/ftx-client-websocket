using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using System.Threading.Tasks;
using Ftx.Client.Websocket.Client;
using Ftx.Client.Websocket.Requests;
using Ftx.Client.Websocket.Websockets;
using MoreLinq;
using Serilog;
using Serilog.Events;

namespace Ftx.Client.Websocket.Sample
{
    class Program
    {
        private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
        private static readonly string API_KEY = "xxx";
        private static readonly string API_SECRET = "xxx";

        static void Main(string[] args)
        {
            InitLogging();

            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnProcessExit;
            AssemblyLoadContext.Default.Unloading += DefaultOnUnloading;
            Console.CancelKeyPress += ConsoleOnCancelKeyPress;

            Console.WriteLine("|=======================|");
            Console.WriteLine("|     FTX CLIENT        |");
            Console.WriteLine("|=======================|");
            Console.WriteLine();

            Log.Debug("====================================");
            Log.Debug("              STARTING              ");
            Log.Debug("====================================");

            var url = FtxValues.ApiWebsocketUrl;
            using (var communicator = new FtxWebsocketCommunicator(url))
            {
                communicator.Name = "Ftx-1";
                communicator.ReconnectTimeout = TimeSpan.FromMinutes(10);
                communicator.ReconnectionHappened.Subscribe(type =>
                    Log.Information($"Reconnection happened, type: {type.Type}"));

                using (var client = new FtxWebsocketClient(communicator))
                {
                    _ = StartPinging(client);

                    SendSubscriptionRequests(client).Wait();

                    SubscribeToStreams(client);

                    communicator.Start();

                    ExitEvent.WaitOne();
                }
            }

            Log.Debug("====================================");
            Log.Debug("              STOPPING              ");
            Log.Debug("====================================");
            Log.CloseAndFlush();
        }

        private static async Task StartPinging(FtxWebsocketClient client)
        {
            var ping = Observable
                .Interval(TimeSpan.FromSeconds(5))
                .Subscribe(_ => client.Send(new PingRequest()));
        }

        private static async Task SendSubscriptionRequests(FtxWebsocketClient client)
        {
            // client.Send(new AuthenticationRequest(API_KEY, API_SECRET, null));
            // client.Send(new TickerSubscribeRequest("BTC-PERP"));
            // client.Send(new TradesSubscribeRequest("BTC-PERP"));
            // client.Send(new TradesSubscribeRequest("ETH-PERP"));
            // client.Send(new TradesSubscribeRequest("DOT-PERP"));
            // client.Send(new OrdersSubscribeRequest());
            // client.Send(new FillsSubscribeRequest());
            // client.Send(new MarketsSubscribeRequest());
            // client.Send(new BookSubscribeRequest("BTC-PERP"));
            client.Send(new GroupedBookSubscribeRequest("SPELL-PERP", 0.0005));
        }

        private static void SubscribeToStreams(FtxWebsocketClient client)
        {
            client.Streams.SubscribedStream.Subscribe(x =>
                Log.Warning($"Subscribed: {x.Market}, channel: {x.Channel}"));

            client.Streams.ErrorStream.Subscribe(x =>
                Log.Warning($"Error received, message: {x.Msg}, status: {x.Code}"));

            client.Streams.PongStream.Subscribe(x =>
                Log.Information($"Pong received"));

            client.Streams.TradesStream.Subscribe(trades => trades.Data.ToList().ForEach(x =>
            {
                //Log.Information($"Trades {trades.Market} [{x.Side}] {x.Price}:{x.Size}");
                if (x.Liquidation)
                {
                    Log.Information($"Liquidated {trades.Market} [{x.Side}] {x.Price} amount:{x.Size}");
                }
            }));

            client.Streams.MarketsStream.Subscribe(markets => markets.Data.MarketsData.ForEach(x =>
            {
                Log.Information(
                    $"Market {x.Key} type:{x.Value?.Type} group:{x.Value?.Future?.Group} underlying:{x.Value?.Underlying} expiry:{x.Value?.Future?.Expiry}");
            }));

            client.Streams.TickerStream.Subscribe(x =>
                Log.Information($"Ticker {x.Market} {x.Data.Last}"));

            client.Streams.OrdersStream.Subscribe(x =>
                Log.Information($"Orders {x.Data.Market} {x.Data.Price} {x.Data.Id} {x.Data.Status}"));

            client.Streams.FillsStream.Subscribe(x =>
                Log.Information($"Fills {x.Data.Future} {x.Data.Price} {x.Data.Size} {x.Data.Side}"));
            
            client.Streams.OrderBookUpdateStream.Subscribe(ob =>
            {
                ob.Data.Asks.ToList().ForEach(x =>
                    Log.Information($"Book update {ob.Market} [{x.Side}] {x.Price}:{x.Amount}"));

                ob.Data.Bids.ToList().ForEach(x =>
                    Log.Information($"Book update {ob.Market} [{x.Side}] {x.Price}:{x.Amount}"));
            });
            
            client.Streams.GroupedOrderBookUpdateStream.Subscribe(ob =>
            {
                ob.Data.Asks.ToList().ForEach(x =>
                    Log.Information($"Book update {ob.Market} [{x.Side}] {x.Price}:{x.Amount} Grouping:{ob.Grouping}"));

                ob.Data.Bids.ToList().ForEach(x =>
                    Log.Information($"Book update {ob.Market} [{x.Side}] {x.Price}:{x.Amount} Grouping:{ob.Grouping}"));
            });

            client.Streams.OrderBookUpdateStream.Subscribe(ob =>
            {
                ob.Data.Asks.ToList().ForEach(x =>
                    Log.Information($"Book update {ob.Market} [{x.Side}] {x.Price}:{x.Amount}"));
            
                ob.Data.Bids.ToList().ForEach(x =>
                    Log.Information($"Book update {ob.Market} [{x.Side}] {x.Price}:{x.Amount}"));
            });
            
            client.Streams.OrderBookSnapshotStream.Subscribe(snapshot =>
                Log.Information(
                    $"{snapshot.Market} snapshot loaded count:{snapshot.Data.Asks.Length + snapshot.Data.Bids.Length}"));
        }

        private static void InitLogging()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var logPath = Path.Combine(executingDir, "logs", "verbose.log");
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.File(logPath, rollingInterval: RollingInterval.Day)
                .WriteTo.ColoredConsole(LogEventLevel.Information)
                .CreateLogger();
        }

        private static void CurrentDomainOnProcessExit(object sender, EventArgs eventArgs)
        {
            Log.Warning("Exiting process");
            ExitEvent.Set();
        }

        private static void DefaultOnUnloading(AssemblyLoadContext assemblyLoadContext)
        {
            Log.Warning("Unloading process");
            ExitEvent.Set();
        }

        private static void ConsoleOnCancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            Log.Warning("Canceling process");
            e.Cancel = true;
            ExitEvent.Set();
        }
    }
}