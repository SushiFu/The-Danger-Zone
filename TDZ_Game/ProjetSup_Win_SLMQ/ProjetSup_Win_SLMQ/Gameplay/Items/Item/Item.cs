using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Item : Entity
    {
        public bool isOnMap;
        public string name;
        public List<Weapon> VehicleGun;
        public bool remove;

        public Item(Texture2D[] itemImage, Vector2 position, Vector2 speed, bool isOnMap, Direction dir, Vector2 size, objectType type, int id)
            : base(itemImage, position, id, type, size, speed)
        {
            this.VehicleGun = new List<Weapon>();
            this.isOnMap = isOnMap;
            direction = dir;
        }

        public override void Die(Map map, Sound sound)
        {
            remove = true;
        }

        public void update(Map map, Sound sound, Shot shots, List<Entity> entityList, List<Animate> animationList)
        {
            base.accelerate = 1;
            base.decelerate = 2;
            base.fall = 1f;
            base.jump = 20;
            base.vitesseMax = 3;
            base.fallMax = 20;

            //
            base.accelerateInit = accelerate;
            base.decelerateInit = decelerate;
            base.fallInit = fall;
            base.jumpInit = jump;
            base.vitesseMaxInit = vitesseMax;
            base.fallMaxInit = fallMax;
            Collisions collision = new Collisions(this, map, sound, shots, map.itemList, entityList, animationList);
            Physics.Fall(this);
            if (direction != Direction.stop)
            {
                if (speed.X == 0 && direction == Direction.left)
                {
                    Physics.MoveRight(this);
                    direction = Direction.right;
                }
                if (speed.X == 0 && direction == Direction.right)
                {
                    Physics.MoveLeft(this);
                    direction = Direction.left;
                }
            }
            String nul = "";
            collision.DoMove(ref nul);
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Draw(image[0], DecaleXY(position, camera), Color.White);
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }
    }
}