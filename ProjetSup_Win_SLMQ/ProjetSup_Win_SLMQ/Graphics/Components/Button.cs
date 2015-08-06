using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Button
    {
        private Texture2D texture;
        private SpriteFont font;
        private Color textColor;
        private int[] textXOffsets;
        private int textYOffset;

        public String[] text { get; private set; }

        public Vector2 position { get; private set; }

        private Rectangle rectangle;
        private Color colour = new Color(255, 255, 255, 255);

        public Vector2 size { get; private set; }

        public Button(Texture2D texture)
        {
            this.texture = texture;
            size = new Vector2(texture.Width, texture.Height);
            text = new string[0];
        }

        public Button(Texture2D texture, String[] text, int[] textXOffsets, int textYOffset, SpriteFont font)
        {
            this.texture = texture;
            size = new Vector2(texture.Width, texture.Height);
            this.text = text;
            this.textXOffsets = textXOffsets;
            this.textYOffset = textYOffset;
            this.font = font;
        }

        private bool down;
        public bool isCliked;

        public void Update(double mousseCoef, Controles controles)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle((int)(controles.mouse.X * mousseCoef), (int)(controles.mouse.Y * mousseCoef), 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A == 255)
                {
                    down = false;
                }
                if (colour.A == 160) // bouton transparent
                {
                    down = true;
                }
                if (down)
                {
                    colour.A += 5;
                }
                else
                {
                    colour.A -= 5;
                }
                if (controles.Click())
                {
                    isCliked = true;
                }
            }
            else if (colour.A < 255) // s'il est pas revenu à l'état norma
            {
                colour.A += 5; // alors on augmente sa visibilité
                isCliked = false;
            }
        }

        public void setPositionAndColor(Vector2 position, Color color)
        {
            this.position = new Vector2(position.X, position.Y);
            this.textColor = color;
        }

        public void SetText(String[] text)
        {
            this.text = text;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, colour);
            if (text.Length != 0)
            {
                for (int i = 0; i < text.Length; i++)
                {
                    Vector2 tmp = position;
                    tmp.X += textXOffsets[i];
                    tmp.Y += textYOffset;
                    spriteBatch.DrawString(font, text[i], tmp, textColor);
                }
            }
        }
    }
}