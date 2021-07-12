﻿using System;
using System.Net.WebSockets;
using Ftx.Client.Websocket.Communicator;
using Websocket.Client;

namespace Ftx.Client.Websocket.Websockets
{
    /// <inheritdoc cref="WebsocketClient" />
    public class FtxWebsocketCommunicator : WebsocketClient, IFtxCommunicator
    {
        /// <inheritdoc />
        public FtxWebsocketCommunicator(Uri url, Func<ClientWebSocket> clientFactory = null) 
            : base(url, clientFactory)
        {
        }
    }
}
