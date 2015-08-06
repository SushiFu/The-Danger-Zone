using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public enum Direction
    {
        right,
        left,
        stop
    }

    public abstract class Entity
    {
        public objectType type;
        public int id;
        public Vector2 position;
        public Vector2 size;
        public Texture2D[] image;
        public Direction direction;
        public AnimDir activeSprite;
        public Vector2 speed;
        public bool IsAlive;
        public int pV;
        public int pVMax;
        public float defense;
        public List<objectType> objetTouched;
        public List<Bonus> bonusList;
        //
        public float accelerate;
        public float decelerate;
        public float fall;
        public float jump;
        public float vitesseMax;
        public float fallMax;
        //
        public float accelerateInit;
        public float decelerateInit;
        public float fallInit;
        public float jumpInit;
        public float vitesseMaxInit;
        public float fallMaxInit;
        //
        public Animation animation;

        public void IsAlived()
        {
            if (this.pV > 0)
                this.IsAlive = true;
            else
                this.IsAlive = false;
        }

        public void Update(List<Animate> animationList)
        {
            if (this.GetType() == typeof(Player))
            {
                for (int i = 0; i < bonusList.Count; i++)
                {
                    bonusList[i].Update((Player)this, animationList);
                    if (bonusList[i].remove)
                    {
                        bonusList.RemoveAt(i);
                    }
                }
            }
            // animationList.Add(new Animate(new Texture2D[1] { image[(int)activeSprite] }, position, new Vector2(0, 0), 100, 101, false));
        }

        public Entity(Texture2D[] persoImage, Vector2 persoPos, int id, objectType type, Vector2 size, Vector2 speed)
        {
            objetTouched = new List<objectType>();
            this.image = persoImage;
            this.position = persoPos;
            this.size = size;
            this.speed = speed;
            this.activeSprite = AnimDir.stayRight;
            animation = new Animation();
            this.id = id;
            this.type = type;
            bonusList = new List<Bonus>();
            IsAlive = true;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(image[(int)activeSprite], DecaleXY(position, camera), Color.White);
        }

        public abstract void Die(Map map, Sound sound);

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }
    }
}