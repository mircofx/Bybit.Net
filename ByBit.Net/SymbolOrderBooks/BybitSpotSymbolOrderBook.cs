﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bybit.Net.Objects.Models.Socket;
using Bybit.Net.Objects;
using Bybit.Net.Objects.Models;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using Bybit.Net.Interfaces.Clients;
using Bybit.Net.Clients;
using Bybit.Net.Objects.Models.Socket.Spot;

namespace Bybit.Net.SymbolOrderBooks
{
    /// <summary>
    /// Live order book implementation
    /// </summary>
    public class BybitSpotSymbolOrderBook : SymbolOrderBook
    {
        private readonly IBybitSocketClient socketClient;
        private readonly bool _socketOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">Options for the order book</param>
        public BybitSpotSymbolOrderBook(string symbol, BybitFuturesSymbolOrderBookOptions? options = null) : base("Bybit", symbol, options ?? new BybitFuturesSymbolOrderBookOptions())
        {
            socketClient = options?.SocketClient ?? new BybitSocketClient(new BybitSocketClientOptions
            {
                LogLevel = options?.LogLevel ?? LogLevel.Information
            });
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            // TODO are these all the same? ie. do we need seperate books for each api?
            var result = await socketClient.SpotStreams.SubscribeToOrderBookUpdatesAsync(Symbol, ProcessUpdate).ConfigureAwait(false);
            if (!result)
                return result;

            Status = OrderBookStatus.Syncing;
            
            var setResult = await WaitForSetOrderBookAsync(30000).ConfigureAwait(false);
            return setResult ? result : new CallResult<UpdateSubscription>(null, setResult.Error);
        }

        /// <inheritdoc />
        protected override void DoReset()
        {
        }

        private void ProcessUpdate(DataEvent<BybitSpotOrderBookUpdate> data)
        {
            SetInitialOrderBook(DateTime.UtcNow.Ticks, data.Data.Bids, data.Data.Asks);            
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync()
        {
            return await WaitForSetOrderBookAsync(30000).ConfigureAwait(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            if(_socketOwner)
                socketClient?.Dispose();
        }
    }
}
