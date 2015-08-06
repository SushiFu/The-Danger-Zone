using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public class Camera
    {
        public float Xcentered;
        public float Ycentered;
        private float Xmax;
        private float Ymax;
        private float Xdistance;
        private float Ydistance;
        private Vector2 oldPosition = Vector2.Zero;

        public float Xspecial { get; private set; }

        public float Yspecial { get; private set; }

        public float Xcurrent { get; private set; }

        public float Ycurrent { get; private set; }

        public Camera(List<Player> players, int hostPlayer, float offsetCamera, GraphicsDevice graphics, Map map)
        {
            Xcentered = players[hostPlayer].position.X - graphics.Viewport.Width / (2 * offsetCamera) + players[hostPlayer].image[0].Width / 2;
            Ycentered = players[hostPlayer].position.Y - graphics.Viewport.Height / (2 * offsetCamera) + players[hostPlayer].image[0].Height / 2;
            Xmax = map.width * 75;
            Ymax = map.height * 75;
            FirstCentred(graphics, offsetCamera, players, hostPlayer);
            Xcurrent = Xcentered;
            Ycurrent = Ycentered;
            Xspecial = 0;
            Yspecial = 0;
        }

        private void FirstCentred(GraphicsDevice graphics, float offsetCamera, List<Player> players, int hostPlayer)
        {
            Xdistance = graphics.Viewport.Width / (2 * offsetCamera);
            Ydistance = graphics.Viewport.Height / (2 * offsetCamera) + players[hostPlayer].image[0].Height / 2;

            if (Xcentered + graphics.Viewport.Width > Xmax)
            {
                while (Xcentered + graphics.Viewport.Width > Xmax)
                {
                    Xcentered--;
                }
            }
            else if (Xcentered < 0)
            {
                while (Xcentered < 0)
                {
                    Xcentered++;
                }
            }

            if (Ycentered + graphics.Viewport.Height > Ymax)
            {
                while (Ycentered + graphics.Viewport.Height > Ymax)
                {
                    Ycentered--;
                }
            }
            else if (Ycentered < 0)
            {
                while (Ycentered < 0)
                {
                    Ycentered++;
                }
            }
        }

        public void UpdateCentered(List<Player> players, int hostPlayer, float offsetCamera, GraphicsDevice graphics, Controles controles)
        {
            Xcentered = players[hostPlayer].position.X - graphics.Viewport.Width / (2 * offsetCamera) + players[hostPlayer].image[0].Width / 2;
            Ycentered = players[hostPlayer].position.Y - graphics.Viewport.Height / (2 * offsetCamera) + players[hostPlayer].image[0].Height / 2;
            Vector2 deplacement = players[hostPlayer].position - oldPosition;

            if (deplacement.X > graphics.Viewport.Width / 4 || deplacement.X < -graphics.Viewport.Width / 4)
            {
                FirstCentred(graphics, offsetCamera, players, hostPlayer);
                Xcurrent = Xcentered;
                Ycurrent = Ycentered;
            }

            Xdistance = graphics.Viewport.Width / (4 * offsetCamera);
            Ydistance = graphics.Viewport.Height / (4 * offsetCamera);

            if (Xcentered - Xcurrent > Xdistance && deplacement.X > 0 && Xcentered + Xdistance * 3 + 10 < Xmax)
            {
                Xcurrent += deplacement.X;
            }
            else if (Xcurrent - Xcentered > Xdistance && deplacement.X < 0 && Xcentered + Xdistance - 10 > 0)
            {
                Xcurrent += deplacement.X;
            }

            if (Ycentered - Ycurrent > Ydistance && deplacement.Y > 0 && Ycentered + Ydistance * 3 + 10 < Ymax)
            {
                Ycurrent += deplacement.Y;
            }
            else if (Ycurrent - Ycentered > Ydistance && deplacement.Y < 0 && Ycentered + Ydistance - 10 > 0)
            {
                Ycurrent += deplacement.Y;
            }

            if (players[hostPlayer].currentWeapon.specialAttackEnabled)
            {
                if (controles.cursorPosition.X > graphics.Viewport.Width - 100 && Xcurrent + Xspecial < Xmax - graphics.Viewport.Width - 20)
                {
                    Xspecial += 20;
                }
                else if (controles.cursorPosition.X < 100 && Xcurrent + Xspecial > 0 + 20)
                {
                    Xspecial -= 20;
                }

                if (controles.cursorPosition.Y > graphics.Viewport.Height - 100 && Ycurrent + Yspecial < Ymax - graphics.Viewport.Height - 20)
                {
                    Yspecial += 20;
                }
                else if (controles.cursorPosition.Y < 100 && Ycurrent + Yspecial > 0 + 20)
                {
                    Yspecial -= 20;
                }
            }
            else
            {
                Xspecial = 0;
                Yspecial = 0;
            }

            oldPosition = players[hostPlayer].position;
        }

        public void UpdateExplosion(Map mapWorld)
        {
            for (int i = 0; i < mapWorld.explosionList.Count; i++)
            {
                if (mapWorld.explosionList[i].iteration < 5)
                    Xcurrent += 3;
                else if (mapWorld.explosionList[i].iteration < 10)
                    Xcurrent -= 3;
                else if (mapWorld.explosionList[i].iteration < 15)
                    Xcurrent += 3;
                else if (mapWorld.explosionList[i].iteration < 20)
                    Xcurrent -= 3;
                else if (mapWorld.explosionList[i].iteration < 25)
                    Xcurrent += 3;
                else if (mapWorld.explosionList[i].iteration < 30)
                    Xcurrent -= 3;
                else if (mapWorld.explosionList[i].iteration < 35)
                    Xcurrent += 3;
                else
                    Xcurrent -= 2;
            }
        }
    }
}