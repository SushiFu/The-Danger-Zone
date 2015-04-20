using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public enum IAType
    {
        SnD,
        patrouille,
        random,
        patr_Snd,
        patr_SndHigh,
        Survival
    }

    public class IA
    {
        // vas a la ligne 298!
        public static void IACheckEvents(Enemy Glandu, IAType IAType, Map map, List<Player> PlayerTab, Shot shots, List<Vector3> destructTile, int host, Sound sound, List<Entity> entityList, List<Animate> animationList, List<ParticleEngine> particle, ContentManager Content, List<Enemy> Enemys, List<Vector2> exploseReseau)
        {
            Collisions collision = new Collisions(Glandu, map, sound, shots, map.itemList, entityList, animationList);
            if (IAcollideIA(Glandu, Enemys))
            {
                if (Glandu.id % 4 == 1)
                    IAMoveRight(Glandu);
                if (Glandu.id % 4 == 2)
                    IAMoveLeft(Glandu);
                if (Glandu.id % 4 == 3)
                    IAJump(Glandu);
            }
            Physics.Fall(Glandu);
            if (IAType == IAType.Survival)
            {
                IASurvival(Glandu, (Player)PlayerTab[0], 400, shots, 0, 0, sound, Content, map, Enemys);
                Glandu.iaType = IAType.patr_Snd;
            }

            if (IAType == IAType.SnD)
            {
                IA.IAModSnDestroy(Glandu, PlayerTab[PlayerNearest(Glandu, PlayerTab, 100000, 0)], 380, shots, 0, 0, sound);
            }
            String msg = "";
            collision.DoMove(ref msg);

            IAHole(Glandu, map);

            msg = "";

            collision.IsShooted(Glandu, shots, sound, particle);
            collision.ValideTire(map, shots, destructTile, particle, Content, exploseReseau);

            if ((collision.haut))// || collision.haut || collision.uR))// && Math.Abs(Glandu.pos.X - PlayerTab[0].pos.X) < 300 && Math.Abs(Glandu.pos.Y - PlayerTab[0].pos.Y) < 300)//(Glandu.direction == Direction.Gauche || Glandu.direction == Direction.Droite))
            {
                IAShoot(Glandu, shots, 0, 0, (int)Glandu.position.X, (int)Glandu.position.Y - 100, sound);
            }
            if (collision.droite || collision.gauche)
            {
                IAJump(Glandu);
            }

            if (IAType == IAType.patr_SndHigh)
            {
                if (Math.Abs(PlayerTab[0].position.X - Glandu.position.X) < 1000)
                {
                    IA.IAModSnDestroy(Glandu, PlayerTab[0], 700, shots, 0, 0, sound);
                }
                else
                {
                    IAModPatrouille(Glandu, (int)map.IASpawnList[Glandu.id - 100].X - 450, (int)map.IASpawnList[Glandu.id - 100].X + 450);
                }
            }
            if (IAType == IAType.patr_SndHigh)
            {
                if (Math.Abs(PlayerTab[0].position.X - Glandu.position.X) < 1000)
                {
                    IA.IAModSnDestroy(Glandu, PlayerTab[0], 700, shots, 0, 0, sound);
                }
                else
                {
                    IAModPatrouille(Glandu, (int)map.IASpawnList[Glandu.id - 100].X - 200, (int)map.IASpawnList[Glandu.id - 100].X + 200);
                }
            }
            if (IAType == IAType.patr_Snd)
            {
                if (Math.Abs(PlayerTab[0].position.X - Glandu.position.X) < 1000)
                {
                    IA.IAModSnDestroy(Glandu, PlayerTab[0], 700, shots, 0, 0, sound);
                }
                else
                {
                    IAModPatrouille(Glandu, (int)map.IASpawnList[Glandu.id - 100].X - 300, (int)map.IASpawnList[Glandu.id - 100].X + 300);
                }
            }
            if (IAType == IAType.random)
            {
                IARandomMove(Glandu);
            }

            Glandu.animation.SetCorrectSprite(Glandu);

            if (Glandu.direction == Direction.left)
            {
                Glandu.currentWeapon.Update(Glandu, Math.PI, null);
            }
            else
            {
                Glandu.currentWeapon.Update(Glandu, 0, null);
            }
        }

        public static void IAFall(Enemy Glandu)
        {
            int Vspeed = (int)Glandu.speed.Y;

            if (Vspeed > -2 && Vspeed < 7)// -2<vspeed<7
            {
                Vspeed = -30;
            }
            else
            {
                Vspeed += 1;
            }

            Glandu.speed.Y = Vspeed;
        }

        public static void IAMove(Perso Glandu)
        {
            if (Glandu.direction == Direction.left)
            {
                Physics.MoveLeft(Glandu);
            }
            if (Glandu.direction == Direction.right)
                Physics.MoveRight(Glandu);
        }

        public static void IAMoveRight(Perso Glandu)
        {
            AutoJump(Glandu);

            Physics.MoveRight(Glandu);

            Glandu.direction = Direction.right;
        }

        public static void IAMoveLeft(Perso Glandu)
        {
            AutoJump(Glandu);

            Physics.MoveLeft(Glandu);
            Glandu.direction = Direction.left;
        }

        public static void IAJump(Perso Glandu)
        {
            Physics.Jump(Glandu);
        }

        public static void AutoJump(Perso Glandu)
        {
            if (Glandu.speed.X == 0 && (Glandu.direction == Direction.right || Glandu.direction == Direction.left))
            {
                Physics.Jump(Glandu);
            }
        }

        public static void Autoturn(Perso Glandu)
        {
            if (Glandu.speed.X == 0 && Glandu.direction == Direction.left)
            {
                Physics.MoveRight(Glandu);
                Glandu.direction = Direction.right;
            }
            if (Glandu.speed.X == 0 && Glandu.direction == Direction.right)
            {
                Physics.Jump(Glandu);
                Glandu.direction = Direction.right;
            }
        }

        public static void IARandomMove(Perso Glandu)
        {
            Random Rand = new Random();

            if (Rand.Next(50) <= 25)
                IAMoveRight(Glandu);
            else
            {
                IA.IAMoveLeft(Glandu);
            }
        }

        public static void IASurvival(Enemy Glandu, Player player, int dist, Shot shots, int offsetX, int offsetY, Sound sound, ContentManager Content, Map mapWorld, List<Enemy> Enemys)
        {
            IAModSnDestroy(Glandu, player, dist, shots, offsetX, offsetY, sound);
            Glandu.iaType = IAType.Survival;
            if (Player1Events.SurvivalLvL0 > 3)
            {
                Glandu.currentWeapon = LoadWeapons.LoadLanceFlamme(Content);
                Glandu.defense = 0.5f;
                Glandu.currentWeapon.damage = (int)Player1Events.SurvivalLvL0 / 10 * Glandu.currentWeapon.damage + 50;
            }
            if (Player1Events.SurvivalLvL0 > 10)
            {
                Glandu.currentWeapon = LoadWeapons.LoadNeedler(Content);
                Glandu.defense = 0.2f;
            }
            Glandu.defense = 2 / ((int)Player1Events.SurvivalLvL0 + 1);
            Glandu.currentWeapon.damage = (int)Player1Events.SurvivalLvL0 * 20 + 20;
        }

        public static int PlayerNearest(Perso Glandu, List<Player> PlayerTab, float dist, int nearest)
        {
            int i = 0;
            foreach (Player player in PlayerTab)
            {
                if (Math.Abs(Math.Abs(player.position.X + player.position.Y) - Math.Abs(Glandu.position.X + Glandu.position.Y)) < dist)
                {
                    i++;
                    dist = Math.Abs(Math.Abs(player.position.X + player.position.Y) - Math.Abs(Glandu.position.X + Glandu.position.Y));
                    nearest = i - 1;
                }
                else
                    i++;
            }
            return nearest;
        }

        public static void IAModPatrouille(Perso Glandu, int pos1, int pos2)
        {
            {
                if (Glandu.position.X >= pos1 && Glandu.position.X <= pos2)
                {
                    if (Glandu.direction == Direction.right)
                        IAMoveRight(Glandu);

                    if (Glandu.direction == Direction.left)
                        IAMoveLeft(Glandu);
                }

                if (Glandu.position.X < pos1)
                {
                    Glandu.direction = Direction.stop;
                    IAMoveRight(Glandu);
                }
                if (Glandu.position.X > pos2)
                {
                    Glandu.direction = Direction.stop;
                    IAMoveLeft(Glandu);
                }
            }
        }

        public static void IAModSnDestroy(Enemy Glandu, Perso Player, int dist, Shot shots, int offsetX, int offsetY, Sound sound)
        {
            int distX = (int)(Glandu.position.X - Player.position.X);

            if (Math.Abs(distX) > dist)
            {
                if (distX > 0)
                {
                    Glandu.direction = Direction.left;
                    IAMoveLeft(Glandu);
                }
                else
                {
                    Glandu.direction = Direction.right;
                    IAMoveRight(Glandu);
                }
                if (Math.Abs((Glandu.position.Y - Player.position.Y)) > dist && (Glandu.direction == Direction.left || Glandu.direction == Direction.right))
                {
                    // IARandomMove(Glandu);
                }
            }
            if (Math.Abs(Glandu.position.X - Player.position.X) < 700 && Math.Abs(Glandu.position.Y - Player.position.Y) < 400)
            {
                IA.IAShoot(Glandu, shots, offsetX, offsetY, (int)Player.position.X, (int)Player.position.Y, sound);
            }

            if (Glandu.speed.X == 0 && (Glandu.direction == Direction.left || Glandu.direction == Direction.right))
            {
            }
        }

        public static void IAShoot(Enemy Glandu, Shot shots, int offsetX, int offsetY, int X, int Y, Sound sound)
        {
            Random Rand2 = new Random();
            if (Rand2.Next(100) < 7)
            {
                Vector2 ori = new Vector2(Glandu.position.X + 20, Glandu.position.Y + 40);
                Vector2 dir = Vector2.Normalize(new Vector2(X + 20 - ori.X + offsetX, Y - ori.Y + offsetY + 40));
                shots.tirList.Add(new Tirs(Glandu.currentWeapon.ammoTexture, (Glandu.currentWeapon.damage), ori, dir * 20, Glandu.currentWeapon.range, Glandu.id, (Math.Atan2(Y + offsetY - ori.Y, X + offsetX - ori.X)), Glandu.currentWeapon.nTexture, Glandu.currentWeapon.tirtype));
                //sound.Play(SoundsName.fusil);
            }
        }

        public static void IAHole(Enemy Glandu, Map map)
        {
            bool hole = false;
            if (Glandu.position.Y % 75 < map.height && map.world[0, (int)Glandu.position.X % 75].type == objectType.nothing)
            {
                hole = true;
                for (int i = 0; i < Glandu.position.Y % 75; i++)
                {
                    if (map.world[map.height - 1 - i, (int)(((Glandu.position.X + Glandu.size.X - 1) - ((Glandu.position.X + Glandu.size.X - 1) % 75)) / 75)].type == objectType.floor)
                    {
                        hole = false;
                    }
                }
            }
            if (hole)
            {
                if (Glandu.njump <= 1)
                {
                    //ne modifier ni la POSITION ni l'ACCELERATION !!! JAMAIS !!!! JAMAIS JAMAIS!!! enfait t'a juste le droit de modifier la vitesse
                    //La VITESSE et c'est TOUT!!!
                    /*
                    Glandu.position.Y -= 100;
                    Glandu.speed.X -= 2;
                    Glandu.accelerate += 2;
                    Physics.Jump(Glandu);
                    Glandu.speed.X += 2;
                     * */
                }
            }
        }

        public static bool IAcollideIA(Enemy Glandu, List<Enemy> Enemys)
        {
            bool result = false;
            for (int i = 0; i < Enemys.Count; i++)
            {
                if (Math.Abs(Glandu.position.X - Enemys[i].position.X) < 100 && Glandu.position.Y == Enemys[i].position.Y && i != Glandu.id - 100)
                {
                    result = true;
                }
            }
            return result;
        }
    }
}