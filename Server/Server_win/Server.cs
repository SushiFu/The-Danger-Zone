using System;
using Lidgren.Network;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace Server_win
{
    public enum SrvAction
    {
        data,
        disconnect
    }

    public class Server
    {
        public int NumOfPlayer;
        public NetServer server;
        public NetPeerConfiguration config;
        public List<Client> clientList;
        public String request;
        public int port;
        public Map map;
        public Thread serverListener;
        public String name;
        public String mapName;
        public Mode mode;

        public Server(int port, string mapName, string name, ModeMulti mode)
        {
            this.name = name;
            this.mapName = mapName;
            this.port = port;
            clientList = new List<Client>();

            this.mode = new Mode(mode);

            config = new NetPeerConfiguration("TDZsrv");
            config.Port = port;
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.EnableMessageType(NetIncomingMessageType.Data);

            server = new NetServer(config);
            server.Start();

            Thread.Sleep(500);

            map = new Map();

            map.Load(mapName + ".xml");

            serverListener = new Thread(new ThreadStart(Listener));
            serverListener.Start();
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

                            ApproveConnection(incMsg);

                            break;

                        case NetIncomingMessageType.Data:

                            ReadClientPacket(incMsg);

                            server.Recycle(incMsg);

                            if (AllClientUpdate())
                            {
                                map.Update();
                                SendPacket();
                                ResetClients();
                            }

                            break;
                        default:
                            break;
                    }
                }
                Thread.Sleep(1);
            }
        }

        private void ReadClientPacket(NetIncomingMessage incMsg)
        {
            int id = incMsg.ReadInt32();

            if (id >= 1000)
            {
                clientList[id % 1000].isDisabled = true;
                clientList[id % 1000].pos = new Vector2(0f, 0f);
                clientList[id % 1000].armPos = new Vector2(0f, 0f);
                clientList[id % 1000].weaponPos = new Vector2(0f, 0f);
                NumOfPlayer--;
            }
            else
            {
                mode.Update(incMsg.ReadInt32(), id);
                
                clientList[id].pos = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                clientList[id].sprite = incMsg.ReadInt32();
                clientList[id].direction = incMsg.ReadInt32();
                
                clientList[id].weaponText = incMsg.ReadInt32();
                clientList[id].weaponPos = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                clientList[id].weaponAngle = incMsg.ReadDouble();
                clientList[id].weaponShotText = incMsg.ReadInt32();
                
                clientList[id].armPos = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                clientList[id].armSize = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                clientList[id].armAngle = incMsg.ReadDouble();
                
                //Destructs Tiles
                int count = incMsg.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    int x = incMsg.ReadInt32();
                    int y = incMsg.ReadInt32();
                    int hit = incMsg.ReadInt32();
                    clientList[id].DestroyTiles.Add(new DestructTile((float)x, (float)y, hit));
                    map.world[y, x].Hit(hit);
                }
                
                //Tirs
                count = incMsg.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    Vector2 position = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                    double angle = incMsg.ReadDouble();
                    int damage = incMsg.ReadInt32();
                    clientList[id].TirsList.Add(new Tir(damage, position, 0, angle));
                }
                
                //Client is Update
                clientList[id].isUpdate = true;
            }

        }

        private void ApproveConnection(NetIncomingMessage incMsg)
        {
            incMsg.SenderConnection.Approve();
            //
            NetOutgoingMessage outMsg = PrepareFirstPackage();
            //
            Console.WriteLine("Connection from : " + incMsg.SenderEndpoint.ToString());
            server.SendMessage(outMsg, incMsg.SenderConnection, NetDeliveryMethod.ReliableOrdered);
            Console.WriteLine("Send id : " + clientList.Count);
            Console.WriteLine("Send Current State of Map");
            //
            Client client = new Client(incMsg.SenderEndpoint.Address.ToString(), incMsg.SenderConnection);
            clientList.Add(client);
            NumOfPlayer++;
        }

        private NetOutgoingMessage PrepareFirstPackage()
        {
            NetOutgoingMessage outMsg = server.CreateMessage();
            
            //Number Players
            outMsg.Write(clientList.Count);
            
            //Map Filename
            outMsg.Write(mapName);
            
            //Map Current State
            for (int i = 0; i < map.height; i++)
            {
                for (int j = 0; j < map.width; j++)
                {
                    outMsg.Write(map.world[i, j].lifePoint);
                    outMsg.Write(map.world[i, j].alive);
                    outMsg.Write(map.world[i, j].bloque);
                    outMsg.Write(map.world[i, j].draw);
                    outMsg.Write(map.world[i, j].hitable);
                }
            }
            
            return outMsg;
        }

        private void SendPacket()
        {
            foreach (Client client in clientList)
            {
                NetOutgoingMessage outMsg = PreparePacket();
                server.SendMessage(outMsg, client.connection, NetDeliveryMethod.ReliableOrdered);
            }
        }

        private NetOutgoingMessage PreparePacket()
        {
            NetOutgoingMessage outMsg = server.CreateMessage();

            outMsg.Write(clientList.Count);

            for (int i = 0; i < clientList.Count; i++)
            {
                //Perso
                outMsg.Write((Int32)clientList[i].pos.X);
                outMsg.Write((Int32)clientList[i].pos.Y);
                outMsg.Write(clientList[i].sprite);
                outMsg.Write(clientList[i].direction);

                //Weapon
                outMsg.Write(clientList[i].weaponText);
                outMsg.Write((Int32)clientList[i].weaponPos.X);
                outMsg.Write((Int32)clientList[i].weaponPos.Y);
                outMsg.Write(clientList[i].weaponAngle);
                outMsg.Write(clientList[i].weaponShotText);

                //Arm
                outMsg.Write((Int32)clientList[i].armPos.X);
                outMsg.Write((Int32)clientList[i].armPos.Y);
                outMsg.Write((Int32)clientList[i].armSize.X);
                outMsg.Write((Int32)clientList[i].armSize.Y);
                outMsg.Write(clientList[i].armAngle);

                //Map
                outMsg.Write(clientList[i].DestroyTiles.Count);
                for (int j = 0; j < clientList[i].DestroyTiles.Count; j++)
                {
                    outMsg.Write((Int32)clientList[i].DestroyTiles[j].X);
                    outMsg.Write((Int32)clientList[i].DestroyTiles[j].Y);
                    outMsg.Write(clientList[i].DestroyTiles[j].hit);
                }

                //Tirs
                outMsg.Write(clientList[i].TirsList.Count);
                for (int j = 0; j < clientList[i].TirsList.Count; j++)
                {
                    outMsg.Write((Int32)clientList[i].TirsList[j].position.X);
                    outMsg.Write((Int32)clientList[i].TirsList[j].position.Y);
                    outMsg.Write(clientList[i].TirsList[j].angle);
                    outMsg.Write(clientList[i].TirsList[j].damage);
                }
            }

            //Scores
            outMsg.Write(mode.red);
            outMsg.Write(mode.blue);

            //Platforms
            outMsg.Write(map.platFormList.Count);
            for (int i = 0; i < map.platFormList.Count; i++)
            {
                outMsg.Write((int)map.platFormList[i].position.X);
                outMsg.Write((int)map.platFormList[i].position.Y);
                outMsg.Write((int)map.platFormList[i].speed.X);
                outMsg.Write((int)map.platFormList[i].speed.Y);
                outMsg.Write((int)map.platFormList[i].oldspeed.X);
                outMsg.Write((int)map.platFormList[i].oldspeed.Y);
            }

            return outMsg;
        }

        private bool AllClientUpdate()
        {
            bool result = true;
            foreach (Client client in clientList)
            {
                if (!client.isUpdate && !client.isDisabled)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private void ResetClients()
        {
            for (int i = 0; i < clientList.Count; i++)
            {
                clientList[i].TirsList = new List<Tir>();
                clientList[i].DestroyTiles = new List<DestructTile>();
                clientList[i].isUpdate = false;
            }
        }
    }
}

