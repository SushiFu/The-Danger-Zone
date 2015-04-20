using System;
using System.Collections.Generic;
using System.Threading;
using Lidgren.Network;

namespace Server_win
{
    public enum requestType
    {
        create,
        getlist
    }

    public class Creator
    {
        public NetServer server;
        public NetPeerConfiguration config;
        List<Server> servers;
        String request;
        int currentPort;

        public Creator(int masterPort)
        {
            Thread inputListener = new Thread(new ThreadStart(Input));
            inputListener.Start();

            currentPort = masterPort;
            servers = new List<Server>();

            config = new NetPeerConfiguration("TDZmaster");
            config.Port = masterPort;
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            server = new NetServer(config);
            server.Start();

            Thread.Sleep(500);

            Thread masterListener = new Thread(new ThreadStart(Listener));
            masterListener.Start();
        }

        private void Listener()
        {
            while (request != "/quit")
            {
                NetIncomingMessage incMsg;

                while ((incMsg = server.ReadMessage()) != null)
                {
                    switch (incMsg.MessageType)
                    {
                        case NetIncomingMessageType.ConnectionApproval:
                            incMsg.SenderConnection.Approve();
                            NetOutgoingMessage outMsg = server.CreateMessage();
                            //
                            if (incMsg.ReadInt32() == (int)requestType.create)
                            {
                                String srvname = incMsg.ReadString();
                                String mapname = incMsg.ReadString();
                                ModeMulti mode = (ModeMulti)incMsg.ReadInt32();
                                //
                                currentPort++;
                                outMsg.Write(currentPort);
                                //
                                Console.WriteLine("Request for create new game server from : " + incMsg.SenderEndpoint.ToString());
                                //
                                Server srv = new Server(currentPort, mapname, srvname, mode);
                                servers.Add(srv);
                                //
                                Thread.Sleep(1000);
                            }
                            else
                            {
                                outMsg.Write(servers.Count);
                                foreach (Server srv in servers)
                                {
                                    outMsg.Write(srv.name);
                                    outMsg.Write(srv.mapName);
                                    outMsg.Write(srv.port);
                                    outMsg.Write(srv.NumOfPlayer);
                                    outMsg.Write((Int32)srv.mode.type);
                                }
                            }                           
                            server.SendMessage(outMsg, incMsg.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                            break;
                        default:
                            break;
                    }
                    server.Recycle(incMsg);
                }
                Thread.Sleep(1);
            }
        }

        private void Input()
        {
            Console.WriteLine("Server is started !");

            while (request != "/quit")
            {
                request = Console.ReadLine();
            }
        }
    }
}

