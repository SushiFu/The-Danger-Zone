using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProjetSup_Win_SLMQ
{
    public class Cursor
    {
        private Texture2D texture;
        private Rectangle rectangle;
        private SpriteFont font;
        private Vector2 initialPos;
        private Vector2 position;
        private Color colour = new Color(255, 255, 255, 255);
        private float size;

        public Cursor(SpriteFont font, GraphicsDevice graphics)
        {
            this.font = font;
            this.size = font.MeasureString("o").Y;
            texture = new Texture2D(graphics, 1, 1);
        }

        public void SetPositionAndColor(Vector2 position, Color color)
        {
            this.initialPos = new Vector2(position.X + 10, position.Y);
            texture.SetData(new Color[]{ color });
        }

        int count = 0;

        public void Update(String text, bool IsActive)
        {
            position = initialPos;
            position.X += font.MeasureString(text).X;
            rectangle = new Rectangle((int)position.X, (int)position.Y, 10, (int)size);

            if (!IsActive)
            {
                colour.A = 0;
            }
            else if (count == 0)
            {
                colour.A = 255;
                count++;
            }
            else if (count == 20)
            {
                colour.A = 0;
                count++;
            }
            else if (count == 40)
            {
                count = 0;
            }
            else
            {
                count++;
            }

        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(texture, rectangle, colour);
        }
    }
}

