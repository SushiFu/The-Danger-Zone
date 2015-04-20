using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Server_win
{
    public class Tile
    {
        public String texture { get; private set; }

        public int lifePoint;
        public bool draw;
        public bool bloque;
        public bool hitable;
        public bool alive;
        public objectType type;
        public int contentIndex;

        public Vector2 position { get; private set; }

        public Tile(int lifePoint, bool draw, String texture, Vector2 position, bool bloque, bool hitable, objectType type, int contentIndex, Map map)
        {
            this.contentIndex = contentIndex;
            this.texture = texture;
            this.lifePoint = lifePoint;
            this.draw = draw;
            this.position = position;
            this.bloque = bloque;
            this.hitable = hitable;
            alive = true;
            this.type = type;
        }

        public void Hit(int dammage)
        {
            if (hitable)
            {
                lifePoint -= dammage;
                if (lifePoint <= 0)
                {
                    bloque = false;
                    draw = false;
                    alive = false;
                }
            }
        }
    }
}