using System;

namespace ProjetSup_Win_SLMQ
{
    public enum ModeMulti
    {
        ctf,
        tdm
    }

    public class ServerInfo
    {
        public String name;
        public String mapName;
        public int port;
        public int nbPlayers;
        public ModeMulti mode;

        public ServerInfo(String name, String mapName, int port, int nbPlayers, ModeMulti mode)
        {
            this.name = name;
            this.mapName = mapName;
            this.port = port;
            this.nbPlayers = nbPlayers;
            this.mode = mode;
        }
    }
}

