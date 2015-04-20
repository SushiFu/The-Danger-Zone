using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace ProjetSup_Win_SLMQ
{
    public class Physics
    {
        public static void Jump(Perso player)
        {
            if (player.njump < 2)
            {
                player.njump++;
                player.speed.Y = -player.jump;
            }
        }

        public static void Climb(Perso player)
        {
            if (player.speed.Y > -player.jumpInit)
            {
                player.speed.Y--;
            }/*
            else if (player.speed.Y < 0)
                player.speed.Y++;
            else
                player.speed.Y = 0;*/
        }

        public static void Descent(Perso player)
        {
            if (player.speed.Y < player.jump)
            {
                player.speed.Y++;
            }/*
            else if (player.speed.Y > 0)
                player.speed.Y--;
            else
                player.speed.Y = 0;*/
        }

        public static void Fall(Entity player)
        {
            if (player.speed.Y < player.fallMax)
            {
                player.speed.Y += player.fall;
            }
            else
                player.speed.Y = player.fallMax;
        }

        public static void MoveLeft(Entity player)
        {
            if (player.speed.X >= -player.vitesseMax)
            {
                player.speed.X -= player.accelerate;
            }
            else
                player.speed.X += 2;
        }

        public static void MoveRight(Entity player)
        {
            if (player.speed.X <= player.vitesseMax)
            {
                player.speed.X += player.accelerate;
            }
            else
                player.speed.X -= 2;
        }

        public static void Deccelarate(Entity player)
        {
            if (player.speed.X > 0)
            {
                player.speed.X -= player.decelerate;
                if (player.speed.X < 0)
                    player.speed.X = 0;
            }
            else if (player.speed.X < 0)
            {
                player.speed.X += player.decelerate;
                if (player.speed.X > 0)
                    player.speed.X = 0;
            }
        }
    }
}