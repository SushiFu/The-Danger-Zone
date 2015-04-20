using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    [Serializable]
    public class SavWeapon
    {
        public string ammoTexture;

        public string itemImage;
        public int damage;

        public double cadence;
        public cadenceType cadenceType;
        public tirTypes tirs;
        public int munitions;
        public int currentAmo;
        public int capacity;
        public int accuracy;
        public int range;
        public SoundsName sound;
        public int prix;
        public Vector2 position;
        public Vector2 speed;
        public string weaponName;

        public bool isOnMap;
        public int id;
        public bool hasSpecialAttack;
        public int numBalle;
        public int numArme;
    }
}