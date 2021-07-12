using System;

namespace Ftx.Client.Websocket.Exceptions
{
    public class FtxBadInputException : FtxException
    {
        public FtxBadInputException()
        {
        }

        public FtxBadInputException(string message) : base(message)
        {
        }

        public FtxBadInputException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
