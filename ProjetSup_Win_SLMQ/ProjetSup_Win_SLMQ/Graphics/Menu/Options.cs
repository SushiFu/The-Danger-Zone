using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace ProjetSup_Win_SLMQ
{
    public enum SubMenuOpt
    {
        general,
        keys,
    }

    public class Options
    {
        private Button keys;
        private Button general;
        private Button soundOFF;
        private Button soundON;
        private Button fullsSreenOFF;
        private Button fullsSreenON;
        private Button English;
        private Button Francais;
        public SubMenuOpt submenu;
        private int offsetButton = 0;
        private int offsetButtonX = 0;
        public bool sound = true;
        public bool fullsSreen = false;
        public bool toFullScreen = false;
        public bool toWindowed = false;
        private int changedKeys;
        private string touche = "";
        //
        public TextView serverAdress;
        private SpriteFont font;
        private SpriteFont fontkey;

        public Options(ContentManager Content, GraphicsDevice graphics)
        {
            if (graphics.Viewport.Height < 1080)
            {/*
                offsetButton = 320;
                offsetButtonX = 20;*/
            }
            submenu = SubMenuOpt.general;
            font = Tools.LoadFont("Fonts/Inversionz_Italic/64", Content);
            fontkey = Tools.LoadFont("Fonts/SergoeKeycaps/64", Content);
            soundON = new Button(Tools.LoadTexture("Menu/checked", Content));
            soundOFF = new Button(Tools.LoadTexture("Menu/unchecked", Content));
            fullsSreenON = new Button(Tools.LoadTexture("Menu/checked", Content));
            fullsSreenOFF = new Button(Tools.LoadTexture("Menu/unchecked", Content));
            keys = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 36) }, new int[] { 80 }, 5, font);
            general = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { "general" }, new int[] { 80 }, 5, font);
            Francais = new Button(Tools.LoadTexture("Menu/fr_flag", Content));
            English = new Button(Tools.LoadTexture("Menu/gb_flag", Content));

            soundON.setPositionAndColor(new Vector2(750 + offsetButtonX, 500 + offsetButton + 140), Color.Yellow);
            soundOFF.setPositionAndColor(new Vector2(750 + offsetButtonX, 500 + offsetButton + 140), Color.Yellow);
            fullsSreenON.setPositionAndColor(new Vector2(750 + offsetButtonX, 600 + offsetButton + 140), Color.Yellow);
            fullsSreenOFF.setPositionAndColor(new Vector2(750 + offsetButtonX, 600 + offsetButton + 140), Color.Yellow);
            Francais.setPositionAndColor(new Vector2(750 + offsetButtonX, 400 + offsetButton + 140), Color.Yellow);
            English.setPositionAndColor(new Vector2(900 + offsetButtonX, 400 + offsetButton + 140), Color.Yellow);
            keys.setPositionAndColor(new Vector2(20 + offsetButtonX, 700 + offsetButton + 160), Color.Yellow);
            general.setPositionAndColor(new Vector2(20 + offsetButtonX, 700 + offsetButton + 160), Color.Yellow);
            changedKeys = 0;

            serverAdress = new TextView(graphics, font, ConnectMaster.MasterIPAdress, true, true);
            serverAdress.SetPositionAndColor(new Vector2(20 + offsetButtonX + font.MeasureString(Langage.getString(Langage.langueactuelle, 44)).X, 450 + offsetButton), Color.Black);
        }

        public void Update(ref MenuState state, double mouseCoef, ref bool full, ref bool soun, Sound play, Controles controles)
        {
            sound = soun;
            fullsSreen = full;
            switch (submenu)
            {
                case SubMenuOpt.general:
                    keys.Update(mouseCoef, controles);
                    Francais.Update(mouseCoef, controles);
                    English.Update(mouseCoef, controles);

                    serverAdress.Update(controles);
                    if (serverAdress.IsFinish)
                    {
                        serverAdress.IsFinish = false;
                        ConnectMaster.MasterIPAdress = serverAdress.text;
                    }

                    if (Francais.isCliked)
                    {
                        Langage.langueactuelle = Langue.Francais;
                        keys.SetText(new string[] { Langage.getString(Langage.langueactuelle, 36) });
                        English.isCliked = false;
                    }
                    if (English.isCliked)
                    {
                        Langage.langueactuelle = Langue.English;
                        keys.SetText(new string[] { Langage.getString(Langage.langueactuelle, 36) });
                        Francais.isCliked = false;
                    }
                    if (sound)
                        soundON.Update(mouseCoef, controles);
                    else
                        soundOFF.Update(mouseCoef, controles);
                    if (fullsSreen)
                        fullsSreenON.Update(mouseCoef, controles);
                    else
                        fullsSreenOFF.Update(mouseCoef, controles);

                    if (keys.isCliked)
                    {
                        submenu = SubMenuOpt.keys;
                        keys.isCliked = false;
                    }
                    if (soundON.isCliked)
                    {
                        soun = false;
                        soundON.isCliked = false;
                        play.soundPlayer.Stop();
                    }
                    if (soundOFF.isCliked)
                    {
                        play.sounds[(int)SoundsName.fusil].Play();
                        soun = true;
                        soundOFF.isCliked = false;
                        play.soundPlayer.Play();
                    }
                    if (fullsSreenON.isCliked)
                    {
                        toWindowed = true;
                        full = false;
                        fullsSreenON.isCliked = false;
                    }
                    if (fullsSreenOFF.isCliked)
                    {
                        play.Play(SoundsName.fusil);
                        toFullScreen = true;
                        full = true;
                        fullsSreenOFF.isCliked = false;
                    }
                    break;

                case SubMenuOpt.keys:
                    general.Update(mouseCoef, controles);
                    if (general.isCliked)
                    {
                        submenu = SubMenuOpt.general;
                        general.isCliked = false;
                    }
                    if (controles.keyboard.GetPressedKeys().Length == 1 && controles.oldkeyboard.GetPressedKeys().Length == 0 && controles.keyboard.GetPressedKeys()[0] != Keys.Enter)
                    {
                        controles.controleTab[changedKeys] = controles.keyboard.GetPressedKeys()[0];
                        touche = controles.keyboard.GetPressedKeys()[0].ToString();
                    }
                    if (controles.keyboard.GetPressedKeys().Length == 1 && controles.oldkeyboard.GetPressedKeys().Length == 0 && controles.keyboard.GetPressedKeys()[0] == Keys.Enter)
                    {
                        changedKeys++;
                        touche = "";
                    }
                    if (changedKeys == controles.controleTab.Length)
                    {
                        changedKeys = 0;
                        submenu = SubMenuOpt.general;
                    }
                    break;

                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (submenu)
            {
                case SubMenuOpt.general:
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 44), new Vector2(20 + offsetButtonX, 450 + offsetButton), Color.Black);
                    serverAdress.Draw(spriteBatch);
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 27), new Vector2(20 + offsetButtonX, 550 + offsetButton), Color.Black);
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 24), new Vector2(20 + offsetButtonX, 650 + offsetButton), Color.Black);
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 26), new Vector2(20 + offsetButtonX, 750 + offsetButton), Color.Black);
                    Francais.Draw(spriteBatch);
                    English.Draw(spriteBatch);
                    keys.Draw(spriteBatch);
                    if (sound)
                        soundON.Draw(spriteBatch);
                    else
                        soundOFF.Draw(spriteBatch);

                    if (fullsSreen)
                        fullsSreenON.Draw(spriteBatch);
                    else
                        fullsSreenOFF.Draw(spriteBatch);

                    break;

                case SubMenuOpt.keys:
                    general.Draw(spriteBatch);
                    spriteBatch.DrawString(font, SetControles(), new Vector2(20 + offsetButtonX, 550 + offsetButton), Color.Black);
                    spriteBatch.DrawString(fontkey, touche, new Vector2(SetControles().Length * (font.LineSpacing - 20), 520 + offsetButton), Color.Black);
                    break;

                default:
                    break;
            }
        }

        private string SetControles()
        {
            if (changedKeys == 0)
                return Langage.getString(Langage.langueactuelle, 28);
            else if (changedKeys == 1)
                return Langage.getString(Langage.langueactuelle, 29);
            else if (changedKeys == 2)
                return Langage.getString(Langage.langueactuelle, 30);
            else if (changedKeys == 3)
                return Langage.getString(Langage.langueactuelle, 31);
            else if (changedKeys == 4)
                return Langage.getString(Langage.langueactuelle, 35);
            else if (changedKeys == 5)
                return Langage.getString(Langage.langueactuelle, 32);
            else if (changedKeys == 6)
                return Langage.getString(Langage.langueactuelle, 33);
            else if (changedKeys == 7)
                return Langage.getString(Langage.langueactuelle, 34);
            else
                return "Done";
        }
    }
}