using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public enum BonusType
    {
        speedUp,
        Etoile,
        attaqueUp,
        defenseUp,
        unlimitedAmmo
    }

    public class Bonus
    {
        public bool remove;
        private int itterationLeft;
        private BonusType type;

        public Bonus(Player entity, BonusType type, int itterationLeft)
        {
            remove = false;
            this.itterationLeft = itterationLeft;
            this.type = type;
            if (type == BonusType.speedUp)
            {
                entity.vitesseMaxInit *= 2;
            }
            else if (type == BonusType.Etoile)
            {
                entity.vitesseMaxInit *= 2;
                entity.defense = 0.00001f;
                entity.attaque *= 5;
            }
            else if (type == BonusType.attaqueUp)
            {
                entity.attaque *= 7;
            }
            else if (type == BonusType.defenseUp)
                entity.defense = 0.00001f;
        }

        public void Update(Player entity, List<Animate> anim)
        {
            Random rnd = new Random();
            int posX = rnd.Next((int)(entity.position.X - 10), (int)(entity.position.X + entity.size.X + 10));
            int posY = rnd.Next((int)(entity.position.Y - 10), (int)(entity.position.Y + entity.size.Y + 10));

            itterationLeft--;
            if (type == BonusType.Etoile)
            {
                anim.Add(new Animate(new Texture2D[1] { TexturesGame.bonusTab[9] }, new Microsoft.Xna.Framework.Vector2(posX, posY), new Microsoft.Xna.Framework.Vector2(0, -4), 15, 30, false));
            }
            else if (type == BonusType.unlimitedAmmo)
            {
                anim.Add(new Animate(new Texture2D[1] { TexturesGame.bonusTab[11] }, new Microsoft.Xna.Framework.Vector2(posX, posY), new Microsoft.Xna.Framework.Vector2(0, -4), 15, 30, false));
            }
            else if (type == BonusType.attaqueUp)
            {
                anim.Add(new Animate(new Texture2D[1] { TexturesGame.bonusTab[7] }, new Microsoft.Xna.Framework.Vector2(posX, posY), new Microsoft.Xna.Framework.Vector2(0, -4), 15, 30, false));
            }
            else if (type == BonusType.defenseUp)
            {
                anim.Add(new Animate(new Texture2D[1] { TexturesGame.bonusTab[8] }, new Microsoft.Xna.Framework.Vector2(posX, posY), new Microsoft.Xna.Framework.Vector2(0, -4), 15, 30, false));
            }
            else if (type == BonusType.speedUp)
            {
                anim.Add(new Animate(new Texture2D[1] { TexturesGame.bonusTab[10] }, new Microsoft.Xna.Framework.Vector2(posX, posY), new Microsoft.Xna.Framework.Vector2(0, -4), 15, 30, false));
            }

            if (type == BonusType.unlimitedAmmo || type == BonusType.Etoile)
                entity.currentWeapon.currentAmo = entity.currentWeapon.capacity;
            if (itterationLeft <= 0)
            {
                remove = true;
                if (type == BonusType.speedUp)
                {
                    entity.vitesseMaxInit /= 2;
                    entity.vitesseMax = entity.vitesseMaxInit;
                    if (entity.speed.X > 0)
                        entity.speed.X = entity.vitesseMax;
                    else
                        entity.speed.X = -entity.vitesseMax;
                }
                else if (type == BonusType.Etoile)
                {
                    entity.vitesseMaxInit /= 2;
                    entity.attaque /= 5;
                    entity.defense = entity.decelerateInit;
                    entity.vitesseMax = entity.vitesseMaxInit;
                    if (entity.speed.X > 0)
                        entity.speed.X = entity.vitesseMax;
                    else
                        entity.speed.X = -entity.vitesseMax;
                }
                else if (type == BonusType.attaqueUp)
                    entity.attaque /= 5;
                else if (type == BonusType.defenseUp)
                    entity.defense = entity.decelerateInit;
            }
        }
    }
}