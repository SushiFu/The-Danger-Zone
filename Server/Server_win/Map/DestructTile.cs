using System;

namespace Server_win
{
    public class DestructTile
    {
        public float X { get; set; }

        public float Y { get; set; }

        public int hit { get; set; }

        public DestructTile(float X, float Y, int hit)
        {
            this.X = X;
            this.Y = Y;
            this.hit = hit;
        }
    }
}

