using System.Reactive.Subjects;
using Ftx.Client.Websocket.Messages;

namespace Ftx.Client.Websocket.Responses
{
    public class PongResponse : ResponseBase
    {
        public override MessageType Type => MessageType.Pong;

        public string Message { get; set; }

        internal static bool TryHandle(string response, ISubject<PongResponse> subject)
        {
            if (response == null)
                return false;

            if (!response.ToLower().Contains("pong"))
                return false;

            var parsed = new PongResponse {Message = response};
            subject.OnNext(parsed);
            return true;
        }
    }
}