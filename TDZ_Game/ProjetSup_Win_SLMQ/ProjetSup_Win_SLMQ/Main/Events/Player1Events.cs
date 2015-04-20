using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public class Player1Events
    {
        public static int killplayer0 = 0;
        public static int SurvivalLvL0 = 0;
        public static int SurvivalLvl1 = 1;

        public static void CheckEvents(Player player, Map map, List<Vector3> destructTile, int host, Shot shots, Sound sound, ref String msg, Controles controles, List<Entity> entityList, List<Animate> animationList, List<ParticleEngine> particle, ContentManager Content, List<Vector2> explosereseau)
        {
            Collisions collision = new Collisions(player, map, sound, shots, map.itemList, entityList, animationList);

            KeyEvents(player, map, controles, sound);
            if (controles.StayDown())
            {
                player.fallMax = player.fallMaxInit;
            }
            AllTimeEvents(player);

            collision.DoMove(ref msg);

            collision.IsShooted(player, shots, sound, particle);

            collision.ValideTire(map, shots, destructTile, particle, Content, explosereseau);

            player.animation.SetCorrectSprite(player);
        }

        public static void MouseEvents(Player player, Shot shots, Camera camera, Sound sound, Controles controles, List<Animate> animationList, List<ParticleEngine> particle)
        {
            if ((/*a metttre dans controles*/controles.mouse.ScrollWheelValue - controles.oldmouse.ScrollWheelValue < 0 || (controles.gamePad.DPad.Up == ButtonState.Pressed && controles.oldgamePad.DPad.Up == ButtonState.Released)) && !player.usingVehicle)
            { //mike fait une fonction
                int i = player.ItemList.IndexOf(player.currentWeapon);
                if (i - 1 < 0)
                {
                    i = player.ItemList.Count;
                }
                if ((player.ItemList[i - 1].GetType() == typeof(Weapon)))
                {
                    player.currentWeapon = (Weapon)player.ItemList[i - 1];
                    //player.currentItem = null;
                }
            }
            if ((/*a metttre dans controles*/controles.mouse.ScrollWheelValue - controles.oldmouse.ScrollWheelValue > 117 || (controles.Drop() || (controles.gamePad.DPad.Up == ButtonState.Pressed && controles.oldgamePad.DPad.Up == ButtonState.Released))) && !player.usingVehicle)
            {//ici aussi
                int i = player.ItemList.IndexOf(player.currentWeapon);
                if (i + 1 == player.ItemList.Count)
                {
                    i = -1;
                }
                if ((player.ItemList[i + 1].GetType() == typeof(Weapon)))
                {
                    player.currentWeapon = (Weapon)player.ItemList[i + 1];
                    //player.currentItem = null;
                }
            }
            //Petit truc pour l'amination avec souris
            if (controles.cursorPosition.X > player.position.X - camera.Xcurrent - camera.Xspecial)
            {//ici aussi
                player.direction = Direction.right;
                if (player.activeSprite >= AnimDir.beginLeft && player.activeSprite <= AnimDir.EndLeft)
                {
                    player.activeSprite += 11;
                }
                else if (player.speed.X == 0)
                {
                    player.activeSprite = AnimDir.stayRight;
                }
            }
            else
            {
                player.direction = Direction.left;
                if (player.activeSprite >= AnimDir.beginRight && player.activeSprite <= AnimDir.EndRight)
                {
                    player.activeSprite -= 11;
                }
                else if (player.speed.X == 0)
                {
                    player.activeSprite = AnimDir.stayLeft;
                }
            }

            player.Shoot(controles, camera, shots, sound, animationList, particle);
        }

        private static void KeyEvents(Player player, Map map, Controles controles, Sound sound)
        {
            #region Deplacements

            if (controles.StayRight() && !player.usingVehicle)
            {
                Physics.MoveRight(player);
            }
            else if (controles.StayLeft() && !player.usingVehicle)
            {
                Physics.MoveLeft(player);
            }
            else
            {
                Physics.Deccelarate(player);
            }

            if (controles.Up() && player.usingVehicle == false)
            {
                Physics.Jump(player);
            }

            if (player.climb && controles.StayUp() && player.usingVehicle == false)
            {
                Physics.Climb(player);
            }
            if (player.usingVehicle)
            {
                useVehicle(player, (Vehicule)player.currentItem, controles);
                // ControleVehicle(player, player.currentItem, controles);
            }

            #endregion Deplacements

            if (controles.Score())
            {
                player.scoreKey = true;
            }
            else
                player.scoreKey = false;
            if (controles.Reload() && player.currentWeapon.weaponType != cadenceType.none)
            {
                player.Reload();
                if (player.currentWeapon.name == "ShotGun")
                {
                    sound.Play(SoundsName.arme);
                }
                else
                {
                    sound.Play(SoundsName.reload);
                }
            }

            if ((controles.Drop() && (player.nearitem.Count > 0)) && player.usingVehicle == false)
            {
                for (int i = 0; i < player.nearitem.Count; i++)
                {
                    if (player.nearitem[i].type == objectType.vehicule)
                    {
                        player.pVMax += 400;
                        player.pV = player.pVMax;
                        player.currentItem = player.nearitem[i];

                        useVehicle(player, (Vehicule)player.nearitem[i], controles);
                        player.currentWeapon = player.currentItem.VehicleGun[0];
                    }
                    player.nearitem.Clear();
                }
            }

            if (controles.Lacher() && (player.currentItem != null))
            {
                if (player.currentItem.type == objectType.vehicule)
                {
                    Vehicule currentvehicule = (Vehicule)player.currentItem;
                    currentvehicule.quitVehicule(player, currentvehicule);
                }
                player.currentWeapon.isOnMap = true;
            }
        }

        private static void AllTimeEvents(Player player)
        {
            Physics.Fall(player);
        }

        public static void useVehicle(Player player, Vehicule item, Controles controles)
        {
            player.usingVehicle = true;
            item.position = player.position;
            item.isOnMap = false;
            player.size = item.size;

            {
            }
            for (int i = 0; i < player.ItemList.Count; i++)
            {
                if (player.ItemList[i].type == objectType.weapon)
                    player.ItemList[i].isOnMap = false;
            }
            if (item.vehiculeType == vehiculeType.Hornet)
            {
                item.position = player.position;
                item.useHornet(player, item, controles);
            }
            if (item.vehiculeType == vehiculeType.Banshee)
            {
                item.useBanshee(player, item, controles);
            }
            if (item.vehiculeType == vehiculeType.Warthog)
            {
                item.useWarthog(player, item, controles);
            }
        }
    }
}