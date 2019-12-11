using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using ServerData;

namespace tcpChatServer
{
    class ClientData
    {
        public Socket clientSocket;
        public Thread clientThread;
        public string id;

        public ClientData()
        {
            id = Guid.NewGuid().ToString();
            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);
            sendRegistrationPacketToClient();
        }

        public ClientData(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            id = Guid.NewGuid().ToString();

            clientThread = new Thread(Server.Data_IN);
            clientThread.Start(clientSocket);

            sendRegistrationPacketToClient();
        }

        public void sendRegistrationPacketToClient()
        {
            Packet p = new Packet(PacketType.Registration, "server");
            p.data.Add(id);
            clientSocket.Send(p.ToBytes());
        }
    }
}
