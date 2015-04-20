using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class Particles
    {
        public Texture2D image;
        public bool remove;
        private int itterationLeft;
        public Vector2 position;
        public Vector2 speed;
        public int id;
        public int rndPosition;
        public int rndSpeed;
        public float gravity;
        public bool stuck;
        private Rectangle rec;

        public Particles(Texture2D image, int itterationLeft, Vector2 position, Vector2 speed, int id, float gravity, Rectangle rec)
        {
            this.image = image;
            this.itterationLeft = itterationLeft;
            this.position = position;
            this.speed = speed;
            this.id = id;
            this.gravity = gravity;
            stuck = false;
            this.rec = rec;
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }

        public void Draw(SpriteBatch sb, Camera camera)
        {
            Vector2 vect = DecaleXY(position, camera);
            rec.X = (int)vect.X;
            rec.Y = (int)vect.Y;
            sb.Draw(image, rec, Color.White);
        }

        private void IsOnMapY(Map map)
        {
            if (position.Y >= (((map.height) * 75) - 30))
            {
                remove = true;
            }
            if (position.Y < 0)
            {
                remove = true;
            }
        }

        private void IsOnMapX(Map map)
        {
            if (position.X >= (((map.width) * 75) - 30))
            {
                remove = true;
            }
            if (position.X < 0)
            {
                remove = true;
            }
        }

        private bool TestAllPixelObjet(List<Particles> particleList)
        {
            bool test = false;
            for (int i = 0; i < particleList.Count; i++)
            {
                if (particleList[i].id != id)
                    test = test || (position.X == particleList[i].position.X && position.Y == particleList[i].position.Y);
            }
            return test;
        }

        private int Ma(float num)
        {
            return ((int)(num - (num % 75)) / 75);
        }

        private bool TestPixel(Vector2 pixel, Map map)
        {
            return (map.world[(int)Ma(pixel.Y), (int)Ma(pixel.X)].bloque);
        }

        public void Update(Map map, List<Particles> particleList)
        {
            itterationLeft--;
            if (itterationLeft <= 0)
                remove = true;
            else
            {
                IsOnMapY(map);
                IsOnMapX(map);
                {
                    if (!remove)
                    if (!((map.world[(int)Ma(position.Y + image.Height / 2), (int)Ma(position.X + image.Width / 2)].bloque)))
                    {
                        position += speed;
                        if (speed.Y < 20)
                            speed.Y += gravity;
                        speed.X *= 0.99f;
                    }
                    else
                    {
                        speed.Y = 0;
                        speed.X = 0;
                    }
                }

                /*
                if (!stuck)
                {
                    position.Y += speed.Y;
                    IsOnMapY(map);
                    //
                    while (TestAllPixelObjet(particleList))
                    {
                        if (speed.Y >= 0)
                            position.Y++;
                        else
                            position.Y--;
                    }

                    IsOnMapY(map);
                    if (TestPixel(position, map))
                        stuck = true;

                    ////////

                    position.X += speed.X;
                    IsOnMapX(map);
                    //
                    while (TestAllPixelObjet(particleList))
                    {
                        if (speed.X >= 0)
                            position.X++;
                        else
                            position.X--;
                    }

                    IsOnMapY(map);
                    if (TestPixel(position, map))
                        stuck = true;

                    if (speed.Y < 20)
                        speed.Y += 0.5f;
                    speed.X *= 0.99f;
                }*/
            }
        }
    }
}