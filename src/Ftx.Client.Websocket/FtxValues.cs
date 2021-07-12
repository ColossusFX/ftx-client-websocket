﻿using System;

namespace Ftx.Client.Websocket
{
    /// <summary>
    /// Ftx static urls
    /// </summary>
    public static class FtxValues
    {
        /// <summary>
        /// Main Ftx url to websocket API
        /// </summary>
        public static readonly Uri ApiWebsocketUrl = new Uri("wss://ftx.com/ws/");
    }
}
