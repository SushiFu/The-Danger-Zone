using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProjetSup_Win_SLMQ
{
    public class Bite: Entity
    {
        public Bite()
            : base(new Texture2D[2], Vector2.Zero, 0, objectType.explosion, Vector2.Zero, Vector2.Zero)
        {

        }

        public override void Die(Map map, Sound sound)
        {
            throw new NotImplementedException();
        }
    }
}

