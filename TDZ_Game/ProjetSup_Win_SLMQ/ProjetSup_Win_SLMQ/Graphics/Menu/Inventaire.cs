using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class Inventaire
    {
        private List<Weapon> weaponList;
        private List<Weapon> AllWeapon;
        private List<Button> butAllWeap;
        private SpriteFont font;
        private SpriteFont font2;
        private Button butLifes;
        private Button butChar1;
        private Button butChar2;
        private List<Button> ButList;
        private ListView inventaryWeapons;
        private ListView shopWeapons;

        public Inventaire(ContentManager Content, Player player)
        {
            font2 = Tools.LoadFont("Fonts/AngryBirds/44", Content);
            font = Tools.LoadFont("Fonts/AngryBirds/24", Content);
            ButList = new List<Button>();
            weaponList = new List<Weapon>();
            AllWeapon = new List<Weapon>();
            butAllWeap = new List<Button>();
            butChar1 = new Button(TexturesGame.PlayerTab[0][0]);
            butChar1.setPositionAndColor(new Vector2(1400, 580), Color.White);
            butChar2 = new Button(TexturesGame.PlayerTab[1][0]);
            butChar2.setPositionAndColor(new Vector2(1500, 580), Color.White);
            inventaryWeapons = new ListView(Tools.LoadTexture("HUD/HUD_armes", Content), new Vector2(100, 300), font, Color.Black, 5);
            shopWeapons = new ListView(Tools.LoadTexture("HUD/HUD_armes", Content), new Vector2(800, 300), font, Color.Black, 5);
            List<Weapon> temp = LoadWeapons.AllWeapon(Content);

            butLifes = new Button(Tools.LoadTexture("Item/coeur", Content));
            butLifes.setPositionAndColor(new Vector2(1500, 300), Color.Black);

            for (int i = 0; i < temp.Count; i++)
            {
                bool test = false;
                for (int j = 0; j < player.ItemList.Count; j++)
                {
                    test = test || (temp[i].name == player.ItemList[j].name);
                }
                if (!test)
                {
                    AllWeapon.Add(temp[i]);
                }
            }
            for (int i = 0; i < player.ItemList.Count; i++)
            {
                if (player.ItemList[i].GetType() == typeof(Weapon))
                {
                    weaponList.Add((Weapon)player.ItemList[i]);
                }
            }
            for (int i = 0; i < weaponList.Count; i++)
            {
                inventaryWeapons.defaultCell = Tools.LoadTexture("Weapons/WeaponSprite/" + weaponList[i].weaponName, Content);
                inventaryWeapons.AddCell(new string[] { weaponList[i].weaponName + " :  " + weaponList[i].munitions + "-" + weaponList[i].prix / 10 + "J" }, new int[] { 200 }, 10);
            }
            for (int i = 0; i < AllWeapon.Count; i++)
            {
                shopWeapons.defaultCell = Tools.LoadTexture("Weapons/WeaponSprite/" + AllWeapon[i].weaponName, Content);
                shopWeapons.AddCell(new string[] { AllWeapon[i].weaponName + " :  " + AllWeapon[i].prix + Langage.getString(Langage.langueactuelle, 39) }, new int[] { 200 }, 10);
            }
        }

        public void update(Controles controles, double mouseCoef, Player player, ContentManager Content)
        {
            inventaryWeapons.Update(mouseCoef, controles);
            shopWeapons.Update(mouseCoef, controles);
            butLifes.Update(mouseCoef, controles);
            if (butLifes.isCliked)
            {
                butLifes.isCliked = false;
                if (player.money > 500)
                {
                    player.lifes++;
                    player.money -= 500;
                }
            }
            butChar1.Update(mouseCoef, controles);
            butChar2.Update(mouseCoef, controles);
            if (butChar1.isCliked)
            {
                player.spritePerso = 0;
            }
            if (butChar2.isCliked)
            {
                player.spritePerso = 1;
            }

            int index = inventaryWeapons.GetIndexClicked();
            if (index != -1 && player.money > weaponList[index].prix / 10)
            {
                weaponList[index].munitions += weaponList[index].capacity;//TODO !!
                player.money -= weaponList[index].prix / 10;
                inventaryWeapons.updateString(new string[] { weaponList[index].weaponName +
                    " :  " + weaponList[index].munitions+ "-" + weaponList[index].prix/10 + "J" }, index);
            }

            int index2 = shopWeapons.GetIndexClicked();
            if (index2 != -1 && player.money > AllWeapon[index2].prix)
            {
                player.ItemList.Add(AllWeapon[index2]);
                weaponList.Add(AllWeapon[index2]);
                player.money -= AllWeapon[index2].prix;
                AllWeapon.RemoveAt(index2);
                inventaryWeapons = new ListView(Tools.LoadTexture("HUD/HUD_armes", Content), new Vector2(100, 300), font, Color.Black, 5);
                shopWeapons = new ListView(Tools.LoadTexture("HUD/HUD_armes", Content), new Vector2(800, 300), font, Color.Black, 5);
                for (int i = 0; i < weaponList.Count; i++)
                {
                    inventaryWeapons.defaultCell = Tools.LoadTexture("Weapons/WeaponSprite/" + weaponList[i].weaponName, Content);
                    inventaryWeapons.AddCell(new string[] { weaponList[i].weaponName + " :  " + weaponList[i].munitions + "-" + weaponList[i].prix / 10 + "J" }, new int[] { 200 }, 10);
                }
                for (int i = 0; i < AllWeapon.Count; i++)
                {
                    shopWeapons.defaultCell = Tools.LoadTexture("Weapons/WeaponSprite/" + AllWeapon[i].weaponName, Content);
                    shopWeapons.AddCell(new string[] { AllWeapon[i].weaponName + " :  " + AllWeapon[i].prix + Langage.getString(Langage.langueactuelle, 39) }, new int[] { 200 }, 10);
                }
            }
        }

        public void draw(SpriteBatch sb, Player player, ContentManager content)
        {
            sb.Draw(Tools.LoadTexture("Menu/GestionPerso", content), new Vector2(0, 0), Color.White);
            sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 11) + player.money, new Vector2(100, 150), Color.Black);
            sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 38), new Vector2(100, 230), Color.Black);

            sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 40), new Vector2(1000, 230), Color.Black);

            sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 53) + Langage.getString(Langage.langueactuelle, 12) + " 500" + Langage.getString(Langage.langueactuelle, 39), new Vector2(1400, 230), Color.Black);
            sb.DrawString(font, Langage.getString(Langage.langueactuelle, 41) + player.lifes, new Vector2(1500, 300), Color.Black);
            butLifes.setPositionAndColor(new Vector2(1500 + (Langage.getString(Langage.langueactuelle, 41) + player.lifes).Length * (font.LineSpacing - 19), 300), Color.Black);
            butLifes.Draw(sb);
            sb.DrawString(font, Langage.getString(Langage.langueactuelle, 51), new Vector2(1400, 500), Color.Black);
            butChar1.Draw(sb);
            butChar2.Draw(sb);
            sb.DrawString(font, Langage.getString(Langage.langueactuelle, 52), new Vector2(600, 150), Color.Black);

            shopWeapons.Draw(sb);
            inventaryWeapons.Draw(sb);
        }
    }
}