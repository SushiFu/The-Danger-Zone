using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public enum cadenceType
    {
        none,
        semiAuto,
        rafales,
        auto
    }

    public class Weapon : Item
    {
        public int damage;
        public int count2;
        public double cadence;
        public cadenceType weaponType;
        public double angle;
        public int accuracy;
        public int range;
        public int munitions;
        public String weaponName;
        public double autoTirs;
        public int rafalstirs;
        public int currentAmo;
        public int capacity;
        public Texture2D ammoTexture;
        private Texture2D scopeTexture;
        public int nTexture;
        public string ammo;
        public int prix;
        public string imagename;
        public SoundsName shotSound;
        public tirTypes tirtype;
        public bool hasSpecialAttack;
        public bool specialAttackEnabled;
        private Vector2 PosScope;
        public Texture2D weaponSprite;
        public int numArme;
        public int numBalle;

        public Weapon(int prix, string ammo, string image, int damage, double cadence, cadenceType cadenceType, int munitions, int currentAmo, int capacity, int accuracy, int range, Vector2 position, Vector2 speed, string weaponName, bool isOnMap, int id, ContentManager Content, SoundsName Sound, tirTypes tirtypes, bool hasSpecialAttack, int numBalle, int numArme)

            : base(TexturesGame.weaponTab[numArme], position, speed, isOnMap, Direction.right, new Vector2(0, 0), objectType.weapon, id)
        {
            this.shotSound = Sound;
            this.tirtype = tirtypes;
            this.hasSpecialAttack = hasSpecialAttack;
            this.prix = prix;
            specialAttackEnabled = false;
            this.ammo = ammo;
            ammoTexture = Tools.LoadTexture(ammo, Content);
            imagename = image;
            name = weaponName;
            this.damage = damage;
            this.isOnMap = true;
            this.cadence = cadence;
            this.weaponType = cadenceType;
            this.accuracy = accuracy;
            this.range = range;
            this.munitions = munitions;
            this.weaponName = weaponName;
            this.angle = 0;
            autoTirs = 0;
            this.currentAmo = currentAmo;
            this.capacity = capacity;
            this.count2 = 0;
            scopeTexture = Tools.LoadTexture("SpriteTexture/Others/Scope", Content);

            weaponSprite = Tools.LoadTexture("Weapons/WeaponSprite/" + weaponName, Content);
            this.numArme = numArme;
            this.numBalle = numBalle;

            rafalstirs = 0;

            if (weaponName == "FlameThrower")
                nTexture = 1;
            else
                nTexture = 0;
        }

        public void Update(Perso player, double angle, Controles controles)
        {
            if (player.direction == Direction.left)
            {
                position = new Vector2(player.position.X + player.image[0].Width - 18, player.position.Y + player.currentWeapon.image[(int)Direction.left].Height / 2);
            }
            else if (player.direction == Direction.right)
            {
                position = new Vector2(player.position.X + 18, player.position.Y + player.currentWeapon.image[(int)Direction.right].Height / 2);
            }
            this.angle = angle;
            //
            if (controles != null)
            {
                PosScope = new Vector2(controles.cursorPosition.X - scopeTexture.Width / 2 + 12, controles.cursorPosition.Y - scopeTexture.Height / 2 + 12);
            }
            if (hasSpecialAttack && controles.RightClick())
            {
                specialAttackEnabled = !specialAttackEnabled;
            }
        }

        public void Reload()
        {
            if (munitions > capacity)
            {
                munitions = munitions - capacity;
                currentAmo = capacity;
            }
            else
            {
                if (munitions > 0)
                {
                    currentAmo = munitions;
                    munitions = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, Direction direction)
        {
            Vector2 ori = new Vector2(3, TexturesGame.weaponTab[numArme][(int)direction].Height / 2);
            spriteBatch.Draw(TexturesGame.weaponTab[numArme][(int)direction + count2], DecaleXY(position, camera), null, Color.White, (float)angle, ori, 1.0f, SpriteEffects.None, 0.0f);
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }

        internal void animationn()
        {
            if (weaponType == cadenceType.none)
            {
                if (count2 >= 2 & count2 < 5 && direction == Direction.right)
                {
                    count2++;
                }
                else if (count2 >= 4 & count2 < 6 && direction == Direction.left)
                {
                    count2++;
                }
                else if (count2 >= 5 & count2 < 8 && direction == Direction.left)
                {
                    count2++;
                }
                else
                    count2 = 0;
            }
        }

        public void SpecialAttack()
        {
            if (weaponName == "sniper")
            {
            }
            if (weaponName == "Gun")
            {
                tirtype = tirTypes.explosive;
            }
        }

        public void DrawSpecial(SpriteBatch spritebatch, float offsetX, float offsetY)
        {
            if (specialAttackEnabled)
            {
                spritebatch.Draw(scopeTexture, PosScope, Color.White);
            }
        }
    }
}