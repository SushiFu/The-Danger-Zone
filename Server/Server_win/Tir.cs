using System;

namespace Server_win
{
    public class Tir
    {
        public Vector2 position;
        public double angle;
        public int damage;
        public int numPerso;

        public Tir(int damage, Vector2 position, int numPerso, double angle)
        {
            this.angle = angle;
            this.damage = damage;
            this.position = position;
            this.numPerso = numPerso;
        }
    }
}
