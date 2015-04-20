using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class Animate
    {
        public Texture2D[] imageTab;
        public Vector2 position;
        public Vector2 speed;
        public bool remove;
        public int itterationLeft;
        public int changementFrequency;
        private int freq;
        public bool centerOnScreen;
        private int currentImage;
        private bool firstItt;

        public Animate(Texture2D[] imageTab, Vector2 position, Vector2 speed, int itterationLeft, int changementFrequency, bool centerOnScreen)
        {
            this.changementFrequency = changementFrequency;
            this.imageTab = imageTab;
            this.position = position;
            this.speed = speed;
            this.itterationLeft = itterationLeft;
            remove = false;
            freq = 0;
            currentImage = 0;
            this.centerOnScreen = centerOnScreen;
            firstItt = true;
        }

        public void Update()
        {
            if (itterationLeft <= 0)
            {
                remove = true;
            }
            else
            {
                position += speed;
                freq++;
                itterationLeft--;
                if (freq > changementFrequency)
                {
                    currentImage++;
                    if (currentImage > imageTab.Length - 1)
                        currentImage = 0;
                    freq = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (firstItt && centerOnScreen)
            {
                position = DecaleXY(position, camera);
                firstItt = false;
            }

            if (centerOnScreen)
                spriteBatch.Draw(imageTab[currentImage], position, Color.White);
            else
                spriteBatch.Draw(imageTab[currentImage], DecaleXY(position, camera), Color.White);
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }
    }
}