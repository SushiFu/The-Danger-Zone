using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ProjetSup_Win_SLMQ
{
    public class Background
    {
        private Texture2D[] textures;
        private int currentBackground;
        private int nbWidth;

        public Background(ContentManager Content, Map map)
        {
            textures = TexturesGame.Backtab;
            currentBackground = new Random().Next(0, textures.Length);
            nbWidth = (int)(map.width * 75 / textures[currentBackground].Width) + 1;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            for (int i = 0; i < nbWidth; i++)
            {
                spriteBatch.Draw(textures[currentBackground], DecaleXYBack(new Vector2(i * textures[currentBackground].Width - 1, 0), camera.Xcurrent, 0), Color.White);
            }
        }

        private Vector2 DecaleXYBack(Vector2 pos, float OffsetX, float OffsetY)
        {
            pos.X -= OffsetX;
            return pos;
        }
    }
}

