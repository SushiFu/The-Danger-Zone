using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
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

    public class ObjetDyna
    {
        public Vector2 posUpLeft;
        public Vector2 posDownRight;
        public objectType type;
        public int index;
        public Rectangle rectangle;

        public ObjetDyna(Vector2 pos, Vector2 siz, objectType type, int index)
        {
            posUpLeft = new Vector2(pos.X, pos.Y);
            posDownRight = new Vector2(siz.X + pos.X, siz.Y + pos.Y);
            this.type = type;
            this.index = index;
        }
    }
}