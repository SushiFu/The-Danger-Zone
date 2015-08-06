using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class ParticleEngine
    {
        private float nParticlesByItteration;
        private Vector2 position;
        private Vector2 direction;
        private int itterationLeft;
        private Texture2D image;
        public bool remove;
        private int particleItteration;
        public int rndPositionX;
        public int rndSpeedX;
        public int rndPositionY;
        public int rndSpeedY;
        public float gravity;
        private bool infinite;
        private double nextItt;

        public ParticleEngine(float nParticleByItteration, Vector2 position, Vector2 speed, int itterationLeft, Texture2D image, int particleItteration, int rndPositionX, int rndSpeedX, int rndPositionY, int rndSpeedY, float gravity, bool infinite)
        {
            this.nParticlesByItteration = nParticleByItteration;
            this.position = position;
            this.direction = speed;
            this.itterationLeft = itterationLeft;
            this.image = image;
            remove = false;
            this.particleItteration = particleItteration;
            this.rndPositionX = rndPositionX;
            this.rndSpeedX = rndSpeedX;
            this.rndPositionY = rndPositionY;
            this.rndSpeedY = rndSpeedY;
            this.gravity = gravity;
            this.infinite = infinite;
        }

        public void Update(List<Particles> particlesList)
        {
            Random rnd = new Random();
            int x;
            int y;
            int xpos;
            int ypos;
            if (itterationLeft <= 0)
                remove = true;
            else
            {
                nextItt += nParticlesByItteration;
                if (nextItt >= 1)
                {
                    for (int i = 0; i < (int)nextItt; i++)
                    {
                        if (particlesList.Count > 10000)
                            particlesList.RemoveAt(0);
                        y = rnd.Next(-rndSpeedY, rndSpeedY);
                        x = rnd.Next(-rndSpeedX, rndSpeedX);
                        xpos = rnd.Next(-rndPositionX, rndPositionX);
                        ypos = rnd.Next(-rndPositionY, rndPositionY);

                        int num = rnd.Next(-5, 5);
                        Rectangle rec = new Rectangle((int)(position.X + xpos), (int)(position.Y + ypos), image.Width + num, image.Height + num);
                        particlesList.Add(new Particles(image, particleItteration, new Vector2(position.X + xpos, position.Y + ypos), new Vector2(direction.X + x, direction.Y + y), particlesList.Count, gravity, rec));
                    }
                    nextItt -= (int)nextItt;
                }
            }
            if (!infinite)
                itterationLeft--;
        }
    }
}