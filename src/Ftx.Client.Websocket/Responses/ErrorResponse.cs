using System.Reactive.Subjects;
using Ftx.Client.Websocket.Json;
using Ftx.Client.Websocket.Logging;
using Ftx.Client.Websocket.Messages;
using Utf8Json;

namespace Ftx.Client.Websocket.Responses
{
    public class ErrorResponse : ResponseBase
    {
        private static readonly ILog Log = LogProvider.GetCurrentClassLogger();
        public long Code { get; set; }
        public string Msg { get; set; }

        internal static bool TryHandle(string response, Subject<ErrorResponse> subject)
        {
            if (!FtxJsonSerializer.ContainsValue(response, "error"))
                return false;

            var error = JsonSerializer.Deserialize<ErrorResponse>(response);
            Log.Error($"Error received - message: {error.Msg}, code: {error.Code}");
            subject.OnNext(error);
            return true;
        }
    }
}