using System;

namespace Ftx.Client.Websocket.Exceptions
{
    public class FtxException : Exception
    {
        public FtxException()
        {
        }

        public FtxException(string message)
            : base(message)
        {
        }

        public FtxException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
