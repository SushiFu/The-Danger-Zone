using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ProjetSup_Win_SLMQ
{
    public class GestionCompetences
    {
        private Button butAttaque;
        private Button butDefense;
        private Button butDext;
        private Button butPv;
        private Button butVitesse;
        private Button butNewChar;
        private Button butLoad;
        private SpriteFont font;
        private SpriteFont font2;
        private Inventaire inventaire;
        private ListView listviewperso;
        private String[] savesPaths;
        private Button butInventaire;
        private SubGestion state;
        private TextView saveName;

        public enum SubGestion
        {
            fichePerso,
            inventaire,
            LoadChar
        }

        public GestionCompetences(ContentManager Content, GraphicsDevice graphics)
        {
            font = Tools.LoadFont("Fonts/AngryBirds/24", Content);
            font2 = Tools.LoadFont("Fonts/AngryBirds/44", Content);

            state = SubGestion.fichePerso;
            butAttaque = new Button(Tools.LoadTexture("Menu/BoutonPlus", Content));
            butDefense = new Button(Tools.LoadTexture("Menu/BoutonPlus", Content));
            butDext = new Button(Tools.LoadTexture("Menu/BoutonPlus", Content));
            butVitesse = new Button(Tools.LoadTexture("Menu/BoutonPlus", Content));
            butPv = new Button(Tools.LoadTexture("Menu/BoutonPlus", Content));
            LoadListPerso(Content);
            butLoad = new Button(Tools.LoadTexture("boutonInventaire", Content), new string[] { Langage.getString(Langage.langueactuelle, 37) }, new int[] { 45 }, 20, font);

            butInventaire = new Button(Tools.LoadTexture("boutonInventaire", Content), new string[] { Langage.getString(Langage.langueactuelle, 5) }, new int[] { 120 }, 20, font);

            butNewChar = new Button(Tools.LoadTexture("boutonInventaire", Content), new string[] { Langage.getString(Langage.langueactuelle, 18) }, new int[] { 50 }, 20, font);

            butAttaque.setPositionAndColor(new Vector2(1450, 450), Color.Black);
            butDefense.setPositionAndColor(new Vector2(1450, 550), Color.Black);
            butDext.setPositionAndColor(new Vector2(1450, 650), Color.Black);
            butVitesse.setPositionAndColor(new Vector2(1450, 750), Color.Black);
            butPv.setPositionAndColor(new Vector2(1450, 850), Color.Black);
            butInventaire.setPositionAndColor(new Vector2(300, 150), Color.Black);
            butLoad.setPositionAndColor(new Vector2(1100, 150), Color.Black);
            butNewChar.setPositionAndColor(new Vector2(200, 200), Color.Black);

            saveName = new TextView(graphics, font2, "", false, false);
            saveName.SetPositionAndColor(new Vector2(font2.MeasureString(Langage.getString(Langage.langueactuelle, 7)).X + 150, 400), Color.Black);
        }

        public void Update(ref Player player, double mousseCoef, Controles controles, ContentManager content, GraphicsDevice graphics)
        {
            switch (state)
            {
                case SubGestion.fichePerso:
                    butAttaque.Update(mousseCoef, controles);
                    butDefense.Update(mousseCoef, controles);
                    butDext.Update(mousseCoef, controles);
                    butPv.Update(mousseCoef, controles);
                    butVitesse.Update(mousseCoef, controles);
                    butInventaire.Update(mousseCoef, controles);
                    butLoad.Update(mousseCoef, controles);

                    if (butAttaque.isCliked)
                    {
                        butAttaque.isCliked = false;
                        if (player.pointRestants > 0)
                        {
                            player.attaquePoint++;
                            if (player.attaque < 3)
                                player.attaque *= 1.1f;
                            player.pointRestants--;
                        }
                    }
                    if (butDefense.isCliked)
                    {
                        butDefense.isCliked = false;
                        if (player.pointRestants > 0)
                        {
                            player.defensePoint++;
                            player.defense *= 0.9f;
                            player.pointRestants--;
                        }
                    }
                    if (butDext.isCliked)
                    {
                        butDext.isCliked = false;
                        if (player.pointRestants > 0 && player.accuracy > 0)
                        {
                            player.dexteritePoint++;
                            player.accuracy--;
                            player.pointRestants--;
                        }
                    }
                    if (butPv.isCliked)
                    {
                        butPv.isCliked = false;
                        if (player.pointRestants > 0)
                        {
                            player.pVMax = (int)(player.pVMax * 1.1f);
                            player.pV = player.pVMax;
                            player.pointRestants--;
                        }
                    }
                    if (butVitesse.isCliked)
                    {
                        butVitesse.isCliked = false;
                        if (player.pointRestants > 0)
                        {
                            player.vitessePoint++;
                            player.vitesseMaxInit += 0.5f;
                            player.vitesseMax = player.vitesseMaxInit;
                            player.pointRestants--;
                        }
                    }

                    if (butInventaire.isCliked)
                    {
                        butInventaire.isCliked = false;
                        inventaire = new Inventaire(content, player);
                        state = SubGestion.inventaire;
                    }

                    if (player.newChar)
                    {
                        if (saveName.IsFinish)
                        {
                            saveName.IsFinish = false;
                            player.name = saveName.text;
                            player.newChar = false;
                        }
                        else
                        {
                            saveName.Update(controles);
                        }
                    }
                    else
                    {
                        saveName.SetText(player.name);
                    }

                    if (butLoad.isCliked)
                    {
                        butLoad.isCliked = false;
                        state = SubGestion.LoadChar;
                    }
                    break;

                case SubGestion.inventaire:
                    inventaire.update(controles, mousseCoef, player, content);
                    break;

                case SubGestion.LoadChar:
                    butNewChar.Update(mousseCoef, controles);
                    listviewperso.Update(mousseCoef, controles);
                    Button item = listviewperso.GetClickedButton();
                    if (item != null)
                    {
                        player = new Player(new Vector2(0, 0), TexturesGame.PlayerTab[0], 0, player.graphics, LoadWeapons.LoadKnife(content), true, FlagsType.red);
                        XmlSerializer serializer2 = new XmlSerializer(typeof(SavPerso));
                        FileStream fs2 = new FileStream((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Sauvegardes/") + item.text[0], FileMode.Open);
                        XmlReader reader2 = XmlReader.Create(fs2);
                        SavPerso obj2 = (SavPerso)serializer2.Deserialize(reader2);
                        player.Deserialise(obj2, content);
                        fs2.Close();
                        state = SubGestion.fichePerso;
                    }
                    else if (butNewChar.isCliked)
                    {
                        butNewChar.isCliked = false;
                        player = new Player(new Vector2(0, 0), TexturesGame.PlayerTab[0], 0, player.graphics, LoadWeapons.LoadKnife(content), true, FlagsType.red);
                        player.ItemList.Add(LoadWeapons.LoadKnife(content));
                        saveName = new TextView(graphics, font, player.name, true, true);
                        saveName.SetPositionAndColor(new Vector2(font.MeasureString(Langage.getString(Langage.langueactuelle, 7)).X + 500, 400), Color.Black);
                        state = SubGestion.fichePerso;
                    }
                    break;

                default:
                    break;
            }
        }

        public void Draw(Player player, SpriteBatch sb, ContentManager content)
        {
            switch (state)
            {
                case SubGestion.fichePerso:

                    sb.Draw(Tools.LoadTexture("Menu/GestionPerso", content), new Vector2(0, 0), Color.White);
                    butAttaque.Draw(sb);
                    butVitesse.Draw(sb);
                    butDefense.Draw(sb);
                    butDext.Draw(sb);
                    butPv.Draw(sb);
                    butLoad.Draw(sb);
                    butInventaire.Draw(sb);
                    sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 8) + player.pointRestants, new Vector2(1150, 350), Color.Black);
                    sb.Draw(Tools.LoadTexture("barre", content), new Vector2(1110, 410), Color.White);
                    sb.Draw(Tools.LoadTexture("barre", content), new Vector2(1320, 410), Color.White);
                    sb.DrawString(font, Langage.getString(Langage.langueactuelle, 13) + player.attaquePoint, new Vector2(1200, 470), Color.Black);
                    sb.DrawString(font, Langage.getString(Langage.langueactuelle, 14) + player.defensePoint, new Vector2(1200, 570), Color.Black);
                    sb.DrawString(font, Langage.getString(Langage.langueactuelle, 15) + player.dexteritePoint, new Vector2(1200, 670), Color.Black);
                    sb.DrawString(font, Langage.getString(Langage.langueactuelle, 16) + player.vitessePoint, new Vector2(1200, 770), Color.Black);
                    sb.DrawString(font, Langage.getString(Langage.langueactuelle, 17) + player.pVMax, new Vector2(1200, 870), Color.Black);

                    sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 7), new Vector2(150, 400), Color.Black);
                    saveName.Draw(sb);
                    sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 9) + player.experience + "/" + (int)(Math.Pow(1.5, player.niveau) * Math.PI * 100), new Vector2(150, 500), Color.Black);
                    sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 10) + player.niveau, new Vector2(150, 600), Color.Black);
                    sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 11) + player.money, new Vector2(150, 700), Color.Black);
                    sb.DrawString(font2, Langage.getString(Langage.langueactuelle, 12) + player.lifes, new Vector2(150, 800), Color.Black);

                    break;

                case SubGestion.inventaire:

                    inventaire.draw(sb, player, content);
                    break;

                case SubGestion.LoadChar:
                    sb.Draw(Tools.LoadTexture("Menu/GestionPerso", content), new Vector2(0, 0), Color.White);
                    butNewChar.Draw(sb);
                    listviewperso.Draw(sb);
                    break;

                default:
                    break;
            }
        }

        private void LoadListPerso(ContentManager Content)
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Sauvegardes/";
            listviewperso = new ListView(Tools.LoadTexture("Menu/fonditem", Content), new Vector2(10, 500), font, Color.Black, 5);
            savesPaths = Directory.GetFiles(folder);
            for (int i = 0; i < savesPaths.Length; i++)
            {
                savesPaths[i] = savesPaths[i].Replace(folder, "").Replace(".xml", "");
                listviewperso.AddCell(new string[] { savesPaths[i] }, new int[] { 200 }, 10);
            }
        }
    }
}