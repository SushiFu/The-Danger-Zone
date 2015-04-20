using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class PlatForm : Entity
    {
        public Vector2 positionFinal;
        public Vector2 positionInit;

        public Vector2 oldspeed;

        public PlatForm(Vector2 positionInit, Vector2 positionFinal, Vector2 speed, objectType type, int id)
            : base(TexturesGame.platForm, positionInit, id, type, new Vector2(250, 75), speed)
        {
            this.positionFinal = positionFinal;
            this.positionInit = positionInit;
        }

        public void Update()
        {
            oldspeed.X = speed.X;
            oldspeed.Y = speed.Y;
            position.X += speed.X;
            position.Y += speed.Y;
            if (position.X == positionInit.X)
            {
                speed.X = -speed.X;
            }
            else if (position.X == positionFinal.X)
            {
                speed.X = -speed.X;
            }

            if (position.Y == positionInit.Y)
            {
                speed.Y = -speed.Y;
            }
            else if (position.Y == positionFinal.Y)
            {
                speed.Y = -speed.Y;
            }
        }

        public override void Die(Map map, Sound sound)
        {
            throw new NotImplementedException();
        }
    }
}