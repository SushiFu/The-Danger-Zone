using System;
using Lidgren.Network;
using System.Collections.Generic;

namespace Server_win
{
    public class Client
    {
        public bool isUpdate;
        public bool isDisabled;

        public String userName { get; set; }

        public String IP { get; set; }

        public NetConnection connection { get; set; }

        public Vector2 pos { get; set; }

        public int sprite { get; set; }

        public Vector2 weaponPos { get; set; }

        public double weaponAngle { get; set; }

        public int weaponText { get; set; }

        public int weaponShotText { get; set; }

        public Vector2 armPos { get; set; }

        public Vector2 armSize { get; set; }

        public double armAngle{ get; set; }

        public int direction { get; set; }

        public List<Tir> TirsList { get; set; }

        public List<DestructTile> DestroyTiles { get; set; }

        public List<Vector2> Explosions { get; set; }

        public Client(string ip, NetConnection connection)
        {
            this.IP = ip;
            this.connection = connection;
            isUpdate = false;
            isDisabled = false;
            TirsList = new List<Tir>();
            DestroyTiles = new List<DestructTile>();
            Explosions = new List<Vector2>();
        }
    }
}

