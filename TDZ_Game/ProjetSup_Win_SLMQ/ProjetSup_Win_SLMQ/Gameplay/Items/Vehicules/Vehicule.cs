using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public enum vehiculeType
    {
        Hornet,
        Banshee,
        Warthog
    }

    public class Vehicule : Item
    {
        public vehiculeType vehiculeType;
        private int damage;

        public Vehicule(vehiculeType type, Vector2 pos, int passengers, int damage, Vector2 speed, Direction dir, bool isOnMap, Vector2 size, Texture2D[] image, int id)
            : base(image, pos, speed, isOnMap, dir, size, objectType.vehicule, id)
        {
            this.type = objectType.vehicule;
            this.vehiculeType = type;
            this.damage = damage;
        }

        public void quitVehicule(Perso perso, Vehicule vehicule)
        {
            Physics.Fall(vehicule);
            //vehicule.position = perso.position + (new Vector2(10, -20));
            if (perso.GetType() == typeof(Player))
            {
                perso.image = TexturesGame.PlayerTab[0];
            }
            if (perso.GetType() == typeof(Enemy))
            {
                perso.image = TexturesGame.IATab[0];
            }
            perso.position += new Vector2(0, -20);
            perso.size = new Vector2(TexturesGame.PlayerTab[0][0].Width, TexturesGame.PlayerTab[0][0].Height);
            perso.currentItem = null;
            vehicule.isOnMap = true;
            for (int i = 0; i < perso.ItemList.Count; i++)
            {
                if (perso.ItemList[i].type == objectType.weapon)
                {
                    perso.ItemList[i].isOnMap = true;
                    perso.currentWeapon = (Weapon)perso.ItemList[i];
                }
            }

            perso.pV = perso.pVMax;
            perso.usingVehicle = false;
            perso.nearitem.Clear();
        }

        public void useHornet(Perso Perso, Vehicule vehicule, Controles controles)
        {
            Perso.currentWeapon.isOnMap = false;
            Perso.image = TexturesGame.itemTab[0];
            Perso.vitesseMax = 20;
            Perso.currentItem.name = "Hornet";
            for (int i = 0; i < Perso.objetTouched.Count; i++)
            {
                if (Perso.objetTouched[i].GetType() == typeof(Enemy))
                {
                    //Enemy eny =(Enemy)Perso.objetTouched[i];
                    //  ((Enemy)(Perso.objetTouched[i])).pV = 0;
                }
            }
            if (controles.StayRight())
            {
                Perso.speed.X = 13;
            }
            else if (controles.StayLeft())
            {
                Perso.speed.X = -13;
            }
            else
            {
                Physics.Deccelarate(Perso);
            }

            if (controles.StayUp() && Perso.speed.Y >= -10)
            {
                Perso.speed.Y = (-1f + Perso.speed.Y) - 1.2f;
            }

            if (controles.StayDown() && !controles.Up() && Perso.speed.Y >= -20)
            {
                Perso.speed.Y = -1f;
                //if(Perso.speed.X<0)
            }
            if (controles.Action())
            {
                int i = Perso.currentItem.VehicleGun.IndexOf(Perso.currentWeapon);
                if (Perso.currentItem.VehicleGun.Count <= i + 1)
                    Perso.currentWeapon = Perso.currentItem.VehicleGun[0];
                else
                    Perso.currentWeapon = Perso.currentItem.VehicleGun[i + 1];
            }
        }

        public void useWarthog(Perso perso, Vehicule vehicule, Controles controles)
        {
            perso.image = TexturesGame.itemTab[2];
            perso.vitesseMax = 10;
            perso.currentItem.name = "Hornet";

            if (controles.StayRight())
            {
                Physics.MoveRight(perso);
                Physics.MoveRight(perso);
                Physics.MoveRight(perso);
            }
            else if (controles.StayLeft())
            {
                Physics.MoveLeft(perso);
                Physics.MoveLeft(perso);
                Physics.MoveLeft(perso);
            }
            else
            {
                Physics.Deccelarate(perso);
            }

            if (controles.StayUp() && perso.speed.Y >= -10)
            {
                perso.speed.Y = (-1f + perso.speed.Y) - 1.2f;
            }

            if (controles.StayDown() && !controles.Up() && perso.speed.Y >= -20)
            {
                perso.speed.Y = -1f;
                //if(Perso.speed.X<0)
            }
        }

        public void useBanshee(Perso Perso, Vehicule vehicule, Controles controles)
        {
            Perso.image = TexturesGame.itemTab[1];
            Perso.vitesseMax = 15;
            Perso.currentWeapon.isOnMap = false;
            //Perso.currentWeapon = Perso.currentItem.VehicleGun[0];
            Perso.currentItem.name = "Banshee";

            if (controles.StayRight())
            {
                Perso.speed.X = 9;
            }
            else if (controles.StayLeft())
            {
                Perso.speed.X = -9;
            }
            else
            {
                if (Perso.direction == Direction.left)
                    Perso.speed.X = -3;
                if (Perso.direction == Direction.right)
                    Perso.speed.X = 3;
            }

            if (controles.StayUp() && Perso.speed.Y >= -10)
            {
                Perso.speed.Y = (-1f + Perso.speed.Y) - 1.2f;
            }

            if (controles.StayDown() && !controles.Up() && Perso.speed.Y >= -20)
            {
                Perso.speed.Y = -0.8f;
                //if(Perso.speed.X<0)
            }
            if (controles.Action())
            {
                int i = Perso.currentItem.VehicleGun.IndexOf(Perso.currentWeapon);
                if (Perso.currentItem.VehicleGun.Count <= i + 1)
                    Perso.currentWeapon = Perso.currentItem.VehicleGun[0];
                else
                    Perso.currentWeapon = Perso.currentItem.VehicleGun[i + 1];
            }
        }
    }
}