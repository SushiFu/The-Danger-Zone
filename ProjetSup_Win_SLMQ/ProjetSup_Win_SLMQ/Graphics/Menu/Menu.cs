using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetSup_Win_SLMQ
{
    public enum MenuState
    {
        main,
        selection,
        solo,
        editor,
        multi,
        creation,
        options,
        goMulti,
        goSolo,
    }

    public class Menu
    {
        public MenuState menuState;
        public MultiMenu multiMenu;
        public Options optionsMenu;
        public GestionCompetences gestion;

        //
        private int offsetButton = 0;

        private int offsetButtonX = 0;
        private Button butSolo;
        private Button butMulti;
        private Button butOptions;
        private Button butEditor;
        private Button butExit;
        private Button butCampagne;
        private Button butSurvival;
        private Button butCreation;
        private String[] mapspath;
        public String mapname;
        private ListView listViewMaps;

        //
        private Texture2D back;

        private SpriteFont font;

        public Menu(ContentManager Content, GraphicsDevice graphics)
        {
            font = Tools.LoadFont("Fonts/Inversionz_Italic/64", Content);
            Color color = Color.Yellow;
            gestion = new GestionCompetences(Content, graphics);

            if (graphics.Viewport.Height < 1080)
            {
                offsetButton = 320;
                offsetButtonX = 20;
            }

            menuState = MenuState.main;

            optionsMenu = new Options(Content, graphics);

            multiMenu = new MultiMenu(Content, graphics);

            back = Tools.LoadTexture("Menu/MENU", Content);

            butSolo = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 0) }, new int[] { 80 }, 5, font);
            butCreation = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 3) }, new int[] { 80 }, 5, font);
            butMulti = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 19) }, new int[] { 80 }, 5, font);
            butEditor = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 22) }, new int[] { 80 }, 5, font);
            butOptions = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 23) }, new int[] { 80 }, 5, font);
            butExit = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 25) }, new int[] { 80 }, 5, font);
            butCampagne = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 1) }, new int[] { 80 }, 5, font);
            butSurvival = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 2) }, new int[] { 80 }, 5, font);

            butSolo.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 600 + offsetButton), color);
            butMulti.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 500 + offsetButton), color);
            butEditor.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 400 + offsetButton), color);
            butOptions.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 300 + offsetButton), color);
            butExit.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 200 + offsetButton), color);
            butCampagne.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 600 + offsetButton), color);
            butSurvival.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 500 + offsetButton), color);
            butCreation.setPositionAndColor(new Vector2(20 + offsetButtonX, graphics.Viewport.Height - 400 + offsetButton), color);

            optionsMenu.submenu = SubMenuOpt.general;
        }

        public void LaunchMenuSound(Sound sound)
        {
            sound.soundPlayer.Stop();
            sound.soundPlayer = sound.sounds[(int)SoundsName.dangerzone].CreateInstance();
            if (sound.playEffects)
            {
                sound.soundPlayer.Play();
            }
        }

        public void Update(ref GameState state, double mouseCoef, ContentManager Content, ref bool fullScreenON, ref bool soundON, Sound play, Controles controles)
        {
            switch (menuState)
            {
                case MenuState.main:
                    butSolo.Update(mouseCoef, controles);
                    butMulti.Update(mouseCoef, controles);
                    butOptions.Update(mouseCoef, controles);
                    butEditor.Update(mouseCoef, controles);
                    butExit.Update(mouseCoef, controles);

                    if (butSolo.isCliked)
                    {
                        menuState = MenuState.solo;
                        butSolo.isCliked = false;
                        LoadListMaps(Content);
                    }
                    if (butEditor.isCliked)
                    {
                        menuState = MenuState.editor;
                        butEditor.isCliked = false;
                    }
                    if (butOptions.isCliked)
                    {
                        menuState = MenuState.options;
                        butOptions.isCliked = false;
                    }
                    if (butMulti.isCliked)
                    {
                        menuState = MenuState.multi;
                        butMulti.isCliked = false;
                    }
                    if (butExit.isCliked)
                    {
                        state = GameState.exit;
                    }
                    break;

                case MenuState.selection:

                    listViewMaps.Update(mouseCoef, controles);
                    Button item = listViewMaps.GetClickedButton();
                    if (item != null)
                    {
                        this.mapname = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Survival/") + item.text[0];
                        state = GameState.solo;
                    }
                    break;

                case MenuState.solo:
                    butSurvival.Update(mouseCoef, controles);
                    butCampagne.Update(mouseCoef, controles);
                    butCreation.Update(mouseCoef, controles);

                    if (butCampagne.isCliked)
                    {
                        butCampagne.isCliked = false;
                        state = GameState.scenario;
                    }
                    else if (butSurvival.isCliked)
                    {
                        butSurvival.isCliked = false;
                        menuState = MenuState.selection;
                    }
                    else if (butCreation.isCliked)
                    {
                        butCreation.isCliked = false;
                        menuState = MenuState.creation;
                    }
                    break;

                case MenuState.editor:
                    state = GameState.editor;
                    break;

                case MenuState.multi:
                    multiMenu.Update(ref menuState, mouseCoef, Content, controles);

                    if (menuState == MenuState.goMulti)
                    {
                        state = GameState.multi;
                    }

                    break;

                case MenuState.creation:
                    state = GameState.creation;
                    break;

                case MenuState.options:
                    optionsMenu.Update(ref menuState, mouseCoef, ref fullScreenON, ref soundON, play, controles);

                    break;

                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(back, Vector2.Zero, Color.White);
            switch (menuState)
            {
                case MenuState.main:
                    butSolo.Draw(spriteBatch);
                    butMulti.Draw(spriteBatch);
                    butOptions.Draw(spriteBatch);
                    butEditor.Draw(spriteBatch);
                    butExit.Draw(spriteBatch);
                    break;

                case MenuState.selection:
                    spriteBatch.DrawString(font, "selection:", new Vector2(10, 400), Color.Black);
                    listViewMaps.Draw(spriteBatch);
                    break;

                case MenuState.solo:
                    butSurvival.Draw(spriteBatch);
                    butCampagne.Draw(spriteBatch);
                    butCreation.Draw(spriteBatch);
                    break;

                case MenuState.multi:
                    multiMenu.Draw(spriteBatch);
                    break;

                case MenuState.options:
                    optionsMenu.Draw(spriteBatch);
                    break;

                default:
                    break;
            }
        }

        private void LoadListMaps(ContentManager Content)
        {
            listViewMaps = new ListView(Tools.LoadTexture("Menu/fonditem", Content), new Vector2(10, 500), font, Color.Black, 6);
            mapspath = Directory.GetFiles((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Survival/"));
            for (int i = 0; i < mapspath.Length; i++)
            {
                mapspath[i] = mapspath[i].Replace((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Survival/"), "").Replace(".xml", "");
                listViewMaps.AddCell(new string[] { mapspath[i].ToLower() }, new int[] { 200 }, 10);
            }
        }
    }
}