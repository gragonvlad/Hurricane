﻿using System;
using Hurricane.Components.Logon.LogonServer.Networking;
using Hurricane.Shared.Components;
using Hurricane.Shared.Components.Logon;
using Hurricane.Shared.Logging;
using Hurricane.Shared.Networking;
using Hurricane.Shared.Objects;

namespace Hurricane.Components.Logon.LogonServer
{
    public class LogonServer : IHurricaneComponent
    {
        internal static IHurricaneObjectManager ObjectManager;
        internal static ILogonClientFactory ClientFactory;
        internal static IPacketFactory PacketFactory;
        public ILogger Log { get; private set; }
        internal static ILogonPacketHandler LogonPacketHandler;
        internal static ILogonPacketFactory LogonPacketFactory;

        private readonly INetworkInterface _network;

        public LogonServer(ILogger log, INetworkInterface network, IHurricaneObjectManager objectManager,
            ILogonClientFactory factory, IPacketFactory packetFactory, ILogonPacketFactory logonPacketFactory,
            ILogonPacketHandler logonPacketHandler)
        {
            this.ObjectGuid = Guid.NewGuid();

            this.Log = log;
            NetworkHandlers.Log = log; // Static class

            this._network = network;
            ObjectManager = objectManager;
            ClientFactory = factory;
            PacketFactory = packetFactory;
            LogonPacketFactory = logonPacketFactory;
            LogonPacketHandler = logonPacketHandler;
        }

        public void Initialise()
        {
        }

        public void Boot()
        {
            /* Register network handlers */
            this._network.OnClientConnecting += NetworkHandlers.OnClientConnecting;
            this._network.OnClientConnected += NetworkHandlers.OnClientConnected;
            this._network.OnReceiveData += NetworkHandlers.OnReceiveData;
            this._network.OnClientDisconnecting += NetworkHandlers.OnClientDisconnecting;
            this._network.OnClientDisconnected += NetworkHandlers.OnClientDisconnected;

            /* Start listening */
            this._network.Startup();
        }

        public void Shutdown()
        {
            /* Stop listening */
            this._network.Shutdown();

            /* Unregister network handlers */
            this._network.OnClientConnecting -= NetworkHandlers.OnClientConnecting;
            this._network.OnClientConnected -= NetworkHandlers.OnClientConnected;
            this._network.OnReceiveData -= NetworkHandlers.OnReceiveData;
            this._network.OnClientDisconnecting -= NetworkHandlers.OnClientDisconnecting;
            this._network.OnClientDisconnected -= NetworkHandlers.OnClientDisconnected;
        }

        public void Tick(TimeSpan timeSinceLastTick)
        {
            /* We don't actually need to do anything here yet */
        }

        public Guid ObjectGuid { get; private set; }
    }
}