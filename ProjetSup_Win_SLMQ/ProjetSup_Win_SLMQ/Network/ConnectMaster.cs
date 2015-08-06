using System;
using Lidgren.Network;
using System.Collections.Generic;
using System.Threading;

namespace ProjetSup_Win_SLMQ
{
    public enum requestType
    {
        create,
        getlist
    }

    public class ConnectMaster
    {
        public static string MasterIPAdress = "localhost";
        public int finalPort;
        private NetClient client;
        private NetPeerConfiguration config;
        public List<ServerInfo> serversConnected;
        public ServerInfo serverCreated;

        public ConnectMaster(requestType request, String name, String mapname, ModeMulti mode)
        {
            serversConnected = new List<ServerInfo>();

            config = new NetPeerConfiguration("TDZmaster");
            client = new NetClient(config);
            client.Start();

            NetOutgoingMessage requestSrv = client.CreateMessage();
            requestSrv.Write((Int32)request);

            if (request == requestType.create)
            {
                requestSrv.Write(name);
                String tmp = mapname.Replace(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Multi/", "");
                requestSrv.Write(tmp);
                requestSrv.Write((Int32)mode);
            }
            
            client.Connect(MasterIPAdress, 4242, requestSrv);
            Thread.Sleep(1000);

            NetIncomingMessage incMsg;
            bool haveDatas = false;
            while (!haveDatas)
            {
                incMsg = client.ReadMessage();
                if (incMsg != null && incMsg.MessageType == NetIncomingMessageType.Data)
                {
                    if (request == requestType.create)
                    {
                        serverCreated = new ServerInfo(name, mapname, incMsg.ReadInt32(), 0, mode);
                    }
                    else
                    {
                        int count = incMsg.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            serversConnected.Add(new ServerInfo(incMsg.ReadString(), incMsg.ReadString(), incMsg.ReadInt32(), incMsg.ReadInt32(), (ModeMulti)incMsg.ReadInt32()));
                        }
                    }
                    haveDatas = true;
                    client.Recycle(incMsg);
                }
            }
        }
    }
}

