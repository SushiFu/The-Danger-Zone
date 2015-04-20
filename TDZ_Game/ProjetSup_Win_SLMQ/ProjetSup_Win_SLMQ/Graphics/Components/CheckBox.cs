using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace ProjetSup_Win_SLMQ
{
    public class CheckBox
    {
        Texture2D textureChecked;
        Texture2D textureUnChecked;
        Vector2 position;
        Rectangle rectangle;
        Color colour = new Color(255, 255, 255, 255);
        Vector2 size;
        int coef;
        bool isChecked;

        public CheckBox(GraphicsDevice graphics, ContentManager Content, int coef, bool isChecked)
        {
            this.coef = coef;
            textureChecked = Tools.LoadTexture("Menu/checked", Content);
            textureUnChecked = Tools.LoadTexture("Menu/unchecked", Content);
            this.isChecked = isChecked;
            size = new Vector2(textureChecked.Width, textureUnChecked.Height);
        }

        public void Update(MouseState mouse, MouseState oldMouse, double mousseCoef)
        {            
            rectangle = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            Rectangle mouseRectangle = new Rectangle((int)(mouse.X * mousseCoef), (int)(mouse.Y * mousseCoef), 1, 1);

            if (mouseRectangle.Intersects(rectangle))
            {
                if (colour.A > 200) // bouton transparent
                {
                    colour.A -= 5;
                }
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    isChecked = !isChecked;
                }
            }
            else if (colour.A < 255) // s'il est pas revenu à l'état normal
            {
                colour.A += 5; // alors on augmente sa visibilité
            } 
        }

        public void setPosition(Vector2 position)
        {
            this.position.Y = position.Y + coef;
            this.position.X = position.X;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isChecked)
            {
                spriteBatch.Draw(textureChecked, rectangle, colour);   
            }
            else
            {
                spriteBatch.Draw(textureUnChecked, rectangle, colour);
            }
        }
    }
}

