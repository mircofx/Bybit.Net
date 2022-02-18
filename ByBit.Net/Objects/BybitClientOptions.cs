﻿using Bybit.Net.Interfaces.Clients;
using CryptoExchange.Net.Objects;
using System;


namespace Bybit.Net.Objects
{
    /// <summary>
    /// Options for the Bybit client
    /// </summary>
    public class BybitClientOptions : BaseRestClientOptions
    {
        /// <summary>
        /// Default options for the Bybit client
        /// </summary>
        public static BybitClientOptions Default { get; set; } = new BybitClientOptions();

        /// <summary>
        /// The default receive window for requests
        /// </summary>
        public TimeSpan ReceiveWindow { get; set; } = TimeSpan.FromSeconds(5);

        private readonly RestApiClientOptions _inverseFuturesApiOptions = new RestApiClientOptions(BybitApiAddresses.Default.InverseFuturesRestClientAddress);
        /// <summary>
        /// Inverse futures API options
        /// </summary>
        public RestApiClientOptions InverseFuturesApiOptions
        {
            get => _inverseFuturesApiOptions;
            set => _inverseFuturesApiOptions.Copy(_inverseFuturesApiOptions, value);
        }

        private readonly RestApiClientOptions _inversePerpetualApiOptions = new RestApiClientOptions(BybitApiAddresses.Default.InversePerpetualRestClientAddress);
        /// <summary>
        /// Inverse perpetual API options
        /// </summary>
        public RestApiClientOptions InversePerpetualApiOptions
        {
            get => _inversePerpetualApiOptions;
            set => _inversePerpetualApiOptions.Copy(_inversePerpetualApiOptions, value);
        }

        private readonly RestApiClientOptions _usdPerpetualApiOptions = new RestApiClientOptions(BybitApiAddresses.Default.UsdPerpetualRestClientAddress);
        /// <summary>
        /// Usd perpetual API options
        /// </summary>
        public RestApiClientOptions UsdPerpetualApiOptions
        {
            get => _usdPerpetualApiOptions;
            set => _usdPerpetualApiOptions.Copy(_usdPerpetualApiOptions, value);
        }

        private readonly RestApiClientOptions _spotApiOptions = new RestApiClientOptions(BybitApiAddresses.Default.SpotRestClientAddress);
        /// <summary>
        /// Spot API options
        /// </summary>
        public RestApiClientOptions SpotApiOptions
        {
            get => _spotApiOptions;
            set => _spotApiOptions.Copy(_spotApiOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BybitClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : BybitClientOptions
        {
            base.Copy(input, def);

            input.ReceiveWindow = def.ReceiveWindow;
            input.InverseFuturesApiOptions = new RestApiClientOptions(def.InverseFuturesApiOptions);
            input.InversePerpetualApiOptions = new RestApiClientOptions(def.InversePerpetualApiOptions);
            input.SpotApiOptions = new RestApiClientOptions(def.SpotApiOptions);
            input.UsdPerpetualApiOptions = new RestApiClientOptions(def.UsdPerpetualApiOptions);
        }
    }

    /// <summary>
    /// Options for the futures socket client
    /// </summary>
    public class BybitSocketClientOptions : BaseSocketClientOptions
    {
        /// <summary>
        /// Default options for the futures socket client
        /// </summary>
        public static BybitSocketClientOptions Default { get; set; } = new BybitSocketClientOptions()
        {
            SocketSubscriptionsCombineTarget = 10
        };

        private readonly BybitSocketApiClientOptions _inverseFuturesStreamsOptions = new BybitSocketApiClientOptions(BybitApiAddresses.Default.InverseFuturesSocketClientAddress, BybitApiAddresses.Default.InverseFuturesSocketClientAddress);
        /// <summary>
        /// Inverse futures streams options
        /// </summary>
        public BybitSocketApiClientOptions InverseFuturesStreamsOptions
        {
            get => _inverseFuturesStreamsOptions;
            set => _inverseFuturesStreamsOptions.Copy(_inverseFuturesStreamsOptions, value);
        }

        private readonly BybitSocketApiClientOptions _inversePerpetualStreamsOptions = new BybitSocketApiClientOptions(BybitApiAddresses.Default.InversePerpetualSocketClientAddress, BybitApiAddresses.Default.InversePerpetualSocketClientAddress);
        /// <summary>
        /// Inverse perpetual streams options
        /// </summary>
        public BybitSocketApiClientOptions InversePerpetualStreamsOptions
        {
            get => _inversePerpetualStreamsOptions;
            set => _inversePerpetualStreamsOptions.Copy(_inversePerpetualStreamsOptions, value);
        }

        private readonly BybitSocketApiClientOptions _usdPerpetualStreamsOptions = new BybitSocketApiClientOptions(BybitApiAddresses.Default.UsdPerpetualPublicSocketClientAddress, BybitApiAddresses.Default.UsdPerpetualPrivateSocketClientAddress);
        /// <summary>
        /// Usd perpetual streams options
        /// </summary>
        public BybitSocketApiClientOptions UsdPerpetualStreamsOptions
        {
            get => _usdPerpetualStreamsOptions;
            set => _usdPerpetualStreamsOptions.Copy(_usdPerpetualStreamsOptions, value);
        }

        private readonly BybitSocketApiClientOptions _spotStreamsOptions = new BybitSocketApiClientOptions(BybitApiAddresses.Default.SpotPublicSocketClientAddress, BybitApiAddresses.Default.SpotPrivateSocketClientAddress);
        /// <summary>
        /// Spot streams options
        /// </summary>
        public BybitSocketApiClientOptions SpotStreamsOptions
        {
            get => _spotStreamsOptions;
            set => _spotStreamsOptions.Copy(_spotStreamsOptions, value);
        }

        /// <summary>
        /// ctor
        /// </summary>
        public BybitSocketClientOptions()
        {
            if (Default == null)
                return;

            Copy(this, Default);
        }

        /// <summary>
        /// Copy the values of the def to the input
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="def"></param>
        public new void Copy<T>(T input, T def) where T : BybitSocketClientOptions
        {
            base.Copy(input, def);

            input.InverseFuturesStreamsOptions = new BybitSocketApiClientOptions(def.InverseFuturesStreamsOptions);
            input.InversePerpetualStreamsOptions = new BybitSocketApiClientOptions(def.InversePerpetualStreamsOptions);
            input.SpotStreamsOptions = new BybitSocketApiClientOptions(def.SpotStreamsOptions);
            input.UsdPerpetualStreamsOptions = new BybitSocketApiClientOptions(def.UsdPerpetualStreamsOptions);
        }
    }

    /// <summary>
    /// Bybit socket API client options
    /// </summary>
    public class BybitSocketApiClientOptions : ApiClientOptions
    {
        /// <summary>
        /// The base address for the authenticated websocket
        /// </summary>
        public string BaseAddressAuthenticated { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
#pragma warning disable 8618
        public BybitSocketApiClientOptions()
        {
        }
#pragma warning restore

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseOn"></param>
        public BybitSocketApiClientOptions(BybitSocketApiClientOptions baseOn): base(baseOn)
        {
            BaseAddressAuthenticated = baseOn.BaseAddressAuthenticated;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        /// <param name="baseAddressAuthenticated"></param>
        public BybitSocketApiClientOptions(string baseAddress, string baseAddressAuthenticated): base(baseAddress)
        {
            BaseAddressAuthenticated = baseAddressAuthenticated;
        }

        /// <inheritdoc />
        public new void Copy<T>(T input, T def) where T : BybitSocketApiClientOptions
        {
            base.Copy(input, def);

            if(def.BaseAddressAuthenticated != null)
                input.BaseAddressAuthenticated = def.BaseAddressAuthenticated;
        }
    }

    /// <summary>
    /// Options for the futures symbol order book
    /// </summary>
    public class BybitSymbolOrderBookOptions : OrderBookOptions
    {
        /// <summary>
        /// The client to use for the socket connection. When using the same client for multiple order books the connection can be shared.
        /// </summary>
        public IBybitSocketClient? SocketClient { get; set; }

        /// <summary>
        /// The limit of entries in the order book, either 25 or 200
        /// </summary>
        public int? Limit { get; set; }
    }
}
