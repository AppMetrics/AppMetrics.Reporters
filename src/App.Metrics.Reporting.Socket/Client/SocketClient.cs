﻿// <copyright file="SocketClient.cs" company="App Metrics Contributors">
// Copyright (c) App Metrics Contributors. All rights reserved.
// </copyright>

using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics.Logging;

namespace App.Metrics.Reporting.Socket.Client
{
    public class SocketClient
    {
        private static readonly ILog Logger = LogProvider.For<SocketClient>();

        private readonly TcpClient _tcpClient;
        private readonly UdpClient _udpClient;
        private readonly SocketSettings _socketSettings;

        public string Endpoint
        {
            get
            {
                return _socketSettings.Endpoint;
            }
        }

        public SocketClient(SocketSettings socketSettings)
        {
            SocketSettings.Validate(socketSettings.ProtocolType, socketSettings.Address, socketSettings.Port);

            if (socketSettings.ProtocolType == ProtocolType.Tcp)
            {
                _tcpClient = new TcpClient();
            }

            if (socketSettings.ProtocolType == ProtocolType.Udp)
            {
                _udpClient = new UdpClient();
            }

            _socketSettings = socketSettings;
        }

        public async Task<SocketWriteResult> WriteAsync(
            byte[] payload,
            CancellationToken cancellationToken = default)
        {
            if (_tcpClient != null)
            {
                NetworkStream stream = _tcpClient.GetStream();
                await Task.Run(() =>
                {
                    return stream.WriteAsync(payload, 0, payload.Length, cancellationToken);
                });
                return new SocketWriteResult(true);
            }

            if (_udpClient != null)
            {
                int sended = await _udpClient.SendAsync(
                    payload, payload.Length, _socketSettings.Address, _socketSettings.Port);
                var success = sended == payload.Length;
                return new SocketWriteResult(success);
            }

            return new SocketWriteResult(false, "There is no socket instance!");
        }
    }
}