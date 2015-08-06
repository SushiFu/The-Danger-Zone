using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace ProjetSup_Win_SLMQ
{
    public class Arm
    {
        private Texture2D texture;
        public Rectangle rectangle;
        public Vector2 position;
        public int width;
        public int height = 8;
        private Color color = new Color(240, 200, 131, 255);
        public double angle;

        public Arm(GraphicsDevice graphics)
        {
            this.width = 0;
            texture = new Texture2D(graphics, 1, 1);
            texture.SetData(new Color[]{ color }); 
        }

        public void Update(Perso player, double angle, Vector2 dir)
        {
            if (player.direction == Direction.left)
            {
                position = new Vector2(player.position.X + 18, player.position.Y + player.currentWeapon.image[(int)Direction.left].Height / 2);
            }
            else if (player.direction == Direction.right)
            {
                position = new Vector2(player.position.X + player.image[0].Width - 18, player.position.Y + player.currentWeapon.image[(int)Direction.right].Height / 2);
            }

            width = (int)Vector2.Distance(position, player.currentWeapon.position + dir * (player.currentWeapon.image[0].Width / 2));
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);

            this.angle = angle;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 ori = new Vector2(3, height / 2);
            spriteBatch.Draw(texture, DecaleXY(position, camera), rectangle, Color.White, (float)angle, ori, 1.0f, SpriteEffects.None, 0.0f);
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }
    }
}

