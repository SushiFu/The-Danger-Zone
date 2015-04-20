using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public enum tirTypes
    {
        blaster, bullet, explosive, none
    }

    public class Tirs
    {
        public int damage;
        public Vector2 position;
        public Vector2 vitesse;
        public Texture2D texture;
        public int distance;
        public int range;
        public double angle;
        public int numPerso;
        public bool suprr;
        public tirTypes type;

        public int nTexture;

        public Tirs(Texture2D texture, int dammage, Vector2 position, Vector2 vitesse, int range, int numPerso, double angle, int nTexture, tirTypes type)
        {
            this.type = type;
            this.angle = angle;
            this.damage = dammage;
            this.position = position;
            this.vitesse = vitesse;
            this.texture = texture;
            this.range = range;
            this.numPerso = numPerso;
            this.nTexture = nTexture;
            distance = 0;
            suprr = false;
        }

        public void Update()
        {
            if (distance >= range)
                suprr = true;
            else
            {
                position.X += vitesse.X;
                position.Y += vitesse.Y;
                distance++;
            }
        }
    }
}