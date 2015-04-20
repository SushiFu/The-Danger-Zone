using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Enemy : Perso
    {
        public Shot shotsIA;
        public IAType iaType;
        public Arm arm;

        public Enemy(Vector2 charPos, Texture2D[] sprites, int playerNum, ContentManager Content, Weapon persoWeapon, bool revive, int lifes, GraphicsDevice graphics)
            : base(sprites, charPos, playerNum, persoWeapon, objectType.foe, revive, lifes)
        {
            base.currentWeapon = persoWeapon;
            base.pV = 100;
            base.pVMax = pV;
            base.size = new Vector2(75, 100);
            this.shotsIA = new Shot();
            this.iaType = IAType.SnD;
            //
            this.attaque = 0.5f;
            this.defense = 4;
            //
            base.accelerate = 1;
            base.decelerate = 2;
            base.fall = 1;
            base.jump = 15;
            base.vitesseMax = 6;
            base.fallMax = 30;

            //
            base.accelerateInit = accelerate;
            base.decelerateInit = decelerate;
            base.fallInit = fall;
            base.jumpInit = jump;
            base.vitesseMaxInit = vitesseMax;
            base.fallMaxInit = fallMax;
            arm = new Arm(graphics);
        }

        public override void Die(Map map, Sound sound)
        {
            if (pV <= 0)
            {
                Player1Events.killplayer0++;
                if (revive || lifes > 0)
                {
                    if (id - 100 >= map.IASpawnList.Count)
                    {
                        position = map.IASpawnList[this.id % map.IASpawnList.Count];
                    }
                    else
                        position = map.IASpawnList[this.id - 100];

                    Player1Events.SurvivalLvL0 = Player1Events.killplayer0 / 10;

                    pV = 500;
                    pVMax = pV;
                    lifes--;
                }
                else
                    IsAlive = false;
            }
        }
    }
}