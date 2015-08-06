using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Shot
    {
        public List<Tirs> tirList;
        public List<Tirs> otherTirList;

        public Shot()
        {
            tirList = new List<Tirs>();
            otherTirList = new List<Tirs>();
        }

        public void Update(List<Animate> Anim)
        {
            for (int i = 0; i < tirList.Count; i++)
            {
                tirList[i].Update();
                if (tirList[i].suprr)
                {
                    if (tirList[i].type != tirTypes.none)
                        Anim.Add(new Animate(TexturesGame.ImpactTab, tirList[i].position - new Vector2(20, 20), new Vector2(0, 0), 25, 5, false));
                    tirList.RemoveAt(i);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < tirList.Count; i++)
            {
                spriteBatch.Draw(tirList[i].texture, DecaleXY(tirList[i].position, camera), null, Color.White, (float)tirList[i].angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
            for (int i = 0; i < otherTirList.Count; i++)
            {
                spriteBatch.Draw(otherTirList[i].texture, DecaleXY(otherTirList[i].position, camera), null, Color.White, (float)otherTirList[i].angle, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
            }
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }
    }
}