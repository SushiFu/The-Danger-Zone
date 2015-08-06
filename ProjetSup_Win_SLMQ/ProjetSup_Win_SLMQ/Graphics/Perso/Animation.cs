using Microsoft.Xna.Framework.Input;
using System;

namespace ProjetSup_Win_SLMQ
{
    public class Animation
    {
        private int count;

        public Animation()
        {
            count = 0;
        }

        public void SetCorrectSprite(Entity player)
        {
            /* Player play = (Player)player;
             if (play.currentWeapon.weaponType == cadenceType.none)
             {
                 if (play.direction == Direction.left && play.currentWeapon.count2 < 4)
                 {
                     play.currentWeapon.imagename = "knife/left/" + play.currentWeapon.count2;
                     count2++;
                 }
                 else
                     if (play.direction == Direction.right && count2 < 4)
                     {
                         play.currentWeapon.imagename = "knife/right/" + count2;
                         count2++;
                     }
                     else
                         count2 = 0;
             }*/
            if (player.speed.X > 0)
            {
                count++;
                if (count % 4 == 0 && count < 40)
                {
                    player.activeSprite = (AnimDir)AnimDir.beginRight + count / 4;
                }
                else if (count == 40)
                {
                    count = 0;
                }
            }
            else if (player.speed.X < 0)
            {
                count++;
                if (count % 4 == 0 && count < 40)
                {
                    player.activeSprite = (AnimDir)AnimDir.beginLeft + count / 4;
                }
                else if (count == 40)
                {
                    count = 0;
                }
            }
            else
            {
                count = 0;
                if (player.activeSprite >= AnimDir.beginLeft && player.activeSprite <= AnimDir.EndLeft)
                {
                    player.activeSprite = AnimDir.stayLeft;
                }
                else if (player.activeSprite >= AnimDir.beginRight && player.activeSprite <= AnimDir.EndRight)
                {
                    player.activeSprite = AnimDir.stayRight;
                }
            }
        }
    }
}