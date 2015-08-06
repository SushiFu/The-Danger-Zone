using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ProjetSup_Win_SLMQ
{
    public class TextView
    {
        private SpriteFont font;
        private Color color;

        public String text { get; private set; }

        public Vector2 position { get; private set; }

        private Rectangle rectangle;
        private Cursor cursor;
        private bool IsEditable;
        public bool IsEdited;
        public bool IsFinish;

        public TextView(GraphicsDevice graphics, SpriteFont font, String text, bool IsEditable, bool IsEdited)
        {
            this.text = text;
            this.font = font;
            this.IsEditable = IsEditable;
            this.IsEdited = IsEdited;
            this.IsFinish = false;
            cursor = new Cursor(font, graphics);
        }

        public void SetPositionAndColor(Vector2 position, Color color)
        {
            this.position = new Vector2(position.X, position.Y);
            cursor.SetPositionAndColor(this.position, color);
            this.color = color;
        }

        public void SetText(String text)
        {
            this.text = text;
        }

        public void Update(Controles controles)
        {
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)font.MeasureString(text).X, (int)font.MeasureString(text).Y);
            Rectangle mouse = new Rectangle((int)controles.cursorPosition.X - 2, (int)controles.cursorPosition.Y - 2, (int)controles.cursorPosition.X + 2, (int)controles.cursorPosition.Y + 2);
            if (mouse.Intersects(rectangle) && controles.Click() && IsEditable)
            {
                IsEdited = true;
            }
            if (IsEdited)
            {
                if (controles.Enter())
                {
                    IsEdited = false;
                    IsFinish = true;
                }
                else
                {
                    String tmp = text;
                    InputEvents.UpdateText(controles, ref tmp);
                    text = tmp;
                    cursor.Update(text, true);
                }
            }
            else
            {
                cursor.Update(text, false);
            }
            if (controles.Enter() && IsEdited)
            {
                IsEdited = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color);
            cursor.Draw(spriteBatch);
        }
    }
}

