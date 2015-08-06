using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public class Perso : Entity
    {
        public float attaque;

        //
        public List<Item> ItemList;

        public List<Weapon> WeaponsList;
        public List<Item> nearitem;
        public Item currentItem;
        public Weapon currentWeapon;
        public bool climb = false;
        public int lifes;

        //
        public int njump;

        public bool revive;
        public bool usingVehicle;

        public Perso(Texture2D[] textures, Vector2 position, int playerNum, Weapon firstWeapon, objectType type, bool revive, int lifes)
            : base(textures, position, playerNum, type, new Vector2(textures[0].Width, textures[0].Height), new Vector2(0, 0))
        {
            currentWeapon = firstWeapon;
            ItemList = new List<Item>();
            nearitem = new List<Item>();

            this.revive = revive;
            this.lifes = lifes;
        }

        public void Reload()
        {
            currentWeapon.Reload();
        }

        public override void Die(Map map, Sound sound)
        {
            throw new NotImplementedException();
        }
    }
}