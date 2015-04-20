using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server_win
{
    public enum objectType
    {
        weapon,
        nothing,
        player,
        caseTexte,
        flag,
        caseMusic,
        foe,
        item,
        platformH,
        platformV,
        spikes,
        caisse,
        ice,
        explosif,
        explosion,
        floor,
        Hornet,
        eau,
        echelle,
        etoile,
        attaqueUp,
        defenseUp,
        UnlimitedAmmo,
        Banshee,
        champi,
        vie,
        chargeur,
        vehicule
    }

    public class PlatForm
    {
        public Vector2 positionInit;
        public Vector2 oldspeed;
        public Vector2 positionFinal;
        public Vector2 speed;
        public Vector2 position;
        public Vector2 size;
        public objectType type;

        public PlatForm(Vector2 positionInit, Vector2 positionFinal, Vector2 speed, objectType type)
        {
            this.positionFinal = positionFinal;
            this.positionInit = positionInit;
            position = positionInit;
            this.speed = speed;
            size = new Vector2(250, 75);
            this.type = type;
        }

        public void Update()
        {
            oldspeed = new Vector2(speed.X, speed.Y);
            position = new Vector2(position.X + speed.X, position.Y + speed.Y);
            if (position.X == positionInit.X)
            {
                speed = new Vector2(-speed.X, speed.Y);
            }
            else if (position.X == positionFinal.X)
            {
                speed = new Vector2(-speed.X, speed.Y);
            }

            if (position.Y == positionInit.Y)
            {
                speed = new Vector2(speed.X, -speed.Y);
            }
            else if (position.Y == positionFinal.Y)
            {
                speed = new Vector2(speed.X, -speed.Y);
            }
        }
    }
}