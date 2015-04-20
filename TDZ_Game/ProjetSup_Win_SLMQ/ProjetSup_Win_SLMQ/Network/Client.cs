using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace ProjetSup_Win_SLMQ
{
    public enum SrvAction
    {
        data,
        disconnect
    }

    public class Client
    {
        public ModeMulti mode;
        private NetClient client;
        private NetPeerConfiguration config;
        public int redScore;
        public int blueScore;

        public Client(String host, int port, ref int hostPlayer, Map map, ContentManager Content, ModeMulti mode)
        {
            this.mode = mode;

            config = new NetPeerConfiguration("TDZsrv");
            client = new NetClient(config);
            client.Start();

            NetOutgoingMessage approv = client.CreateMessage();
            approv.Write(hostPlayer);
            client.Connect(host, port, approv);

            Thread.Sleep(500);

            NetIncomingMessage incMsg;

            bool haveID = false;
            while (!haveID)
            {
                incMsg = client.ReadMessage();
                if (incMsg != null && incMsg.MessageType == NetIncomingMessageType.Data)
                {
                    hostPlayer = incMsg.ReadInt32();
                    Console.WriteLine("Connection Ok");
                    Console.WriteLine("Read Map");

                    //Get Map FileName
                    String filename = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Multi/" + incMsg.ReadString();

                    //Init a new Map
                    map.Load(filename + ".xml", Content);

                    for (int i = 0; i < map.height; i++)
                    {
                        for (int j = 0; j < map.width; j++)
                        {
                            map.world[i, j].lifePoint = incMsg.ReadInt32();
                            map.world[i, j].alive = incMsg.ReadBoolean();
                            map.world[i, j].bloque = incMsg.ReadBoolean();
                            map.world[i, j].draw = incMsg.ReadBoolean();
                            map.world[i, j].hitable = incMsg.ReadBoolean();
                        }
                    }

                    Console.WriteLine("Map is loaded");
                    haveID = true;
                }
            }
        }

        public void GetPacket(List<Player> players, int hostPlayer, Map map, Shot shots, ContentManager Content, GraphicsDevice graphics, List<Explosion> explosionList, List<ParticleEngine> particles, Sound sound, List<Vector2> explosions, List<Entity> entityList)
        {
            NetIncomingMessage incMsg;
            incMsg = client.ReadMessage();
            if (incMsg != null)
            {
                switch (incMsg.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        int count;
                        //Nplayers
                        int nbPlayers = incMsg.ReadInt32();

                        if (nbPlayers > players.Count)
                        {
                            Player player;
                            if (nbPlayers % 2 == 0)
                            {
                                player = new Player(map.spawnPoint2, TexturesGame.PlayerTab[0], players.Count, graphics, LoadWeapons.LoadKnife(Content), true, FlagsType.red);
                            }
                            else
                            {
                                player = new Player(map.spawnPoint, TexturesGame.PlayerTab[1], players.Count, graphics, LoadWeapons.LoadKnife(Content), true, FlagsType.blue);
                            }
                            players.Add(player);
                            entityList.Add(player);
                        }

                        for (int i = 0; i < nbPlayers; i++)
                        {
                            //Perso
                            if (i != hostPlayer)
                            {
                                players[i].position.X = incMsg.ReadInt32();
                                players[i].position.Y = incMsg.ReadInt32();
                                players[i].activeSprite = (AnimDir)incMsg.ReadInt32();
                                players[i].direction = (Direction)incMsg.ReadInt32();

                                players[i].currentWeapon.numArme = incMsg.ReadInt32();
                                players[i].currentWeapon.position.X = incMsg.ReadInt32();
                                players[i].currentWeapon.position.Y = incMsg.ReadInt32();
                                players[i].currentWeapon.angle = incMsg.ReadDouble();
                                players[i].currentWeapon.numBalle = incMsg.ReadInt32();

                                players[i].arm.position.X = incMsg.ReadInt32();
                                players[i].arm.position.Y = incMsg.ReadInt32();
                                players[i].arm.rectangle = new Rectangle((int)players[i].arm.position.X, (int)players[i].arm.position.Y, incMsg.ReadInt32(), incMsg.ReadInt32());
                                players[i].arm.angle = incMsg.ReadDouble();

                                //Map
                                count = incMsg.ReadInt32();
                                for (int j = 0; j < count; j++)
                                {
                                    int x = incMsg.ReadInt32();
                                    int y = incMsg.ReadInt32();
                                    int hit = incMsg.ReadInt32();
                                    map.world[y, x].Hit(hit, ref  explosionList, particles, sound, map, Content, explosions);
                                }

                                //Tirs Others Pos
                                count = incMsg.ReadInt32();
                                for (int j = 0; j < count; j++)
                                {
                                    Vector2 position = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                                    double angle = incMsg.ReadDouble();
                                    int damage = incMsg.ReadInt32();
                                    shots.otherTirList.Add(new Tirs(TexturesGame.ammoTab[players[i].currentWeapon.numBalle], damage, position, Vector2.Zero, 0, i, angle, 0, tirTypes.bullet));// TODO: Implémenter tirTypes !!!
                                }
                            }
                            else
                            {
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadDouble();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadInt32();
                                incMsg.ReadDouble();
                                count = incMsg.ReadInt32();
                                for (int j = 0; j < count; j++)
                                {
                                    incMsg.ReadInt32();
                                    incMsg.ReadInt32();
                                    incMsg.ReadInt32();
                                }
                                count = incMsg.ReadInt32();
                                for (int j = 0; j < count; j++)
                                {
                                    incMsg.ReadInt32();
                                    incMsg.ReadInt32();
                                    incMsg.ReadDouble();
                                    incMsg.ReadInt32();
                                }
                            }
                        }

                        //Scores
                        redScore = incMsg.ReadInt32();
                        blueScore = incMsg.ReadInt32();

                        //Platforms
                        count = incMsg.ReadInt32();
                        for (int i = 0; i < count; i++)
                        {
                            map.platFormList[i].position = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                            map.platFormList[i].speed = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                            map.platFormList[i].oldspeed = new Vector2((float)incMsg.ReadInt32(), (float)incMsg.ReadInt32());
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        public void SendPacket(List<Player> players, int hostPlayer, List<Vector3> destroyTiles, Shot shots, bool isFlagCaptured)
        {
            NetOutgoingMessage outMsg = client.CreateMessage();

            //Identifier
            outMsg.Write(hostPlayer);

            //Score
            if (mode == ModeMulti.ctf)
            {
                if (isFlagCaptured)
                {
                    outMsg.Write(1);
                }
                else
                {
                    outMsg.Write(0);
                }
            }
            else
            {
                if (players[hostPlayer].isDie)
                {
                    outMsg.Write(1);
                    players[hostPlayer].isDie = false;
                }
                else
                {
                    outMsg.Write(0);
                }
            }

            //This HostPlayer Position
            outMsg.Write((Int32)players[hostPlayer].position.X);
            outMsg.Write((Int32)players[hostPlayer].position.Y);
            outMsg.Write((Int32)players[hostPlayer].activeSprite);
            outMsg.Write((Int32)players[hostPlayer].direction);

            //Weapon
            outMsg.Write((Int32)players[hostPlayer].currentWeapon.numArme);
            outMsg.Write((Int32)players[hostPlayer].currentWeapon.position.X);
            outMsg.Write((Int32)players[hostPlayer].currentWeapon.position.Y);
            outMsg.Write(players[hostPlayer].currentWeapon.angle);
            outMsg.Write((Int32)players[hostPlayer].currentWeapon.numBalle);

            //Arm
            outMsg.Write((Int32)players[hostPlayer].arm.position.X);
            outMsg.Write((Int32)players[hostPlayer].arm.position.Y);
            outMsg.Write((Int32)players[hostPlayer].arm.rectangle.Width);
            outMsg.Write((Int32)players[hostPlayer].arm.rectangle.Height);
            outMsg.Write(players[hostPlayer].arm.angle);

            //Destruct Tile
            outMsg.Write(destroyTiles.Count);
            for (int i = 0; i < destroyTiles.Count; i++)
            {
                outMsg.Write((Int32)destroyTiles[i].X);
                outMsg.Write((Int32)destroyTiles[i].Y);
                outMsg.Write((Int32)destroyTiles[i].Z);
            }

            //Send Tirs Positions
            outMsg.Write(shots.tirList.Count);
            for (int i = 0; i < shots.tirList.Count; i++)
            {
                outMsg.Write((Int32)shots.tirList[i].position.X);
                outMsg.Write((Int32)shots.tirList[i].position.Y);
                outMsg.Write(shots.tirList[i].angle);
                outMsg.Write(shots.tirList[i].damage);
            }
            client.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        public void SendExit(int hostPlayer)
        {
            NetOutgoingMessage outMsg = client.CreateMessage();
            outMsg.Write(1000 + hostPlayer);

            client.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}