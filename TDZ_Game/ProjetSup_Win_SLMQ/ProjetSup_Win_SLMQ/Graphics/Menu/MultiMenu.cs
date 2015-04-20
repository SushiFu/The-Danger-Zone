using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

namespace ProjetSup_Win_SLMQ
{
    public enum SubMenuMulti
    {
        selection,
        name,
        mode,
        map,
        goMulti,
        list
    }

    public class MultiMenu
    {
        public SubMenuMulti submenu;

        //
        public ServerInfo tmpserv;

        public ConnectMaster mastersrv;

        //
        public Button create;

        public Button join;
        public Button ctf;
        public Button tdm;

        //
        private ListView listViewServs;

        private ListView listViewMaps;
        private TextView textViewName;
        public String[] mapspath;

        //
        private SpriteFont font;

        public MultiMenu(ContentManager Content, GraphicsDevice graphics)
        {
            font = Tools.LoadFont("Fonts/Inversionz_Italic/64", Content);
            Color color = Color.Yellow;

            submenu = SubMenuMulti.selection;

            create = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 20) }, new int[] { 80 }, 5, font);
            join = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 21) }, new int[] { 80 }, 5, font);
            ctf = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 50) }, new int[] { 80 }, 5, font);
            tdm = new Button(Tools.LoadTexture("Menu/BoutonMenu", Content), new string[] { Langage.getString(Langage.langueactuelle, 49) }, new int[] { 80 }, 5, font);
            create.setPositionAndColor(new Vector2(20, 850), color);
            join.setPositionAndColor(new Vector2(20, 750), color);
            ctf.setPositionAndColor(new Vector2(20, 850), color);
            tdm.setPositionAndColor(new Vector2(20, 750), color);

            tmpserv = new ServerInfo("", "", 0, 0, ModeMulti.ctf);

            textViewName = new TextView(graphics, font, "", true, true);
            textViewName.SetPositionAndColor(new Vector2(200, 600), Color.Black);
        }

        public void Update(ref MenuState state, double mouseCoef, ContentManager Content, Controles controles)
        {
            switch (submenu)
            {
                case SubMenuMulti.selection:
                    create.Update(mouseCoef, controles);
                    join.Update(mouseCoef, controles);
                    if (create.isCliked)
                    {
                        submenu = SubMenuMulti.name;
                        create.isCliked = false;
                    }
                    if (join.isCliked)
                    {
                        submenu = SubMenuMulti.list;
                        mastersrv = new ConnectMaster(requestType.getlist, "", "", ModeMulti.ctf);
                        LoadListServs(Content);
                        join.isCliked = false;
                    }
                    break;

                case SubMenuMulti.name:
                    textViewName.Update(controles);
                    if (textViewName.IsFinish)
                    {
                        textViewName.IsFinish = false;
                        tmpserv.name = textViewName.text;
                        submenu = SubMenuMulti.mode;
                    }
                    break;

                case SubMenuMulti.mode:
                    ctf.Update(mouseCoef, controles);
                    tdm.Update(mouseCoef, controles);
                    if (ctf.isCliked)
                    {
                        ctf.isCliked = false;
                        tmpserv.mode = ModeMulti.ctf;
                        submenu = SubMenuMulti.map;
                        LoadListMaps(Content);
                    }
                    if (tdm.isCliked)
                    {
                        tdm.isCliked = false;
                        tmpserv.mode = ModeMulti.tdm;
                        submenu = SubMenuMulti.map;
                        LoadListMaps(Content);
                    }
                    break;

                case SubMenuMulti.map:
                    listViewMaps.Update(mouseCoef, controles);
                    Button tmp = listViewMaps.GetClickedButton();
                    if (tmp != null)
                    {
                        tmpserv.mapName = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Multi/") + tmp.text[0];
                        mastersrv = new ConnectMaster(requestType.create, tmpserv.name, tmpserv.mapName, tmpserv.mode);
                        tmpserv = mastersrv.serverCreated;
                        state = MenuState.goMulti;
                    }
                    break;

                case SubMenuMulti.list:
                    listViewServs.Update(mouseCoef, controles);
                    int index = listViewServs.GetIndexClicked();
                    if (index != -1)
                    {
                        tmpserv = mastersrv.serversConnected[index];
                        tmpserv.mapName = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Multi/") + tmpserv.mapName;
                        state = MenuState.goMulti;
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
                case SubMenuMulti.selection:
                    create.Draw(spriteBatch);
                    join.Draw(spriteBatch);
                    break;

                case SubMenuMulti.name:
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 45), new Vector2(200, 550), Color.Black);
                    textViewName.Draw(spriteBatch);
                    break;

                case SubMenuMulti.mode:
                    ctf.Draw(spriteBatch);
                    tdm.Draw(spriteBatch);
                    break;

                case SubMenuMulti.map:
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 46), new Vector2(10, 430), Color.Black);
                    listViewMaps.Draw(spriteBatch);
                    break;

                case SubMenuMulti.list:
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 47), new Vector2(10, 480), Color.Black);

                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 54), new Vector2(10, 550), Color.Black);
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 55), new Vector2(600, 550), Color.Black);
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 56), new Vector2(1200, 550), Color.Black);
                    spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 57), new Vector2(1650, 550), Color.Black);

                    listViewServs.Draw(spriteBatch);
                    if (mastersrv.serversConnected.Count == 0)
                    {
                        spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 48), new Vector2(200, 800), Color.Black);
                    }
                    break;

                default:
                    break;
            }
        }

        private void LoadListServs(ContentManager Content)
        {
            listViewServs = new ListView(Tools.LoadTexture("Menu/fonditem", Content), new Vector2(10, 610), font, Color.Black, 5);

            for (int i = 0; i < mastersrv.serversConnected.Count; i++)
            {
                listViewServs.AddCell(new string[] { mastersrv.serversConnected[i].name, mastersrv.serversConnected[i].mapName, mastersrv.serversConnected[i].nbPlayers.ToString(), mastersrv.serversConnected[i].mode.ToString() },
                    new int[] { 10, 600, 1500, 1700 }, 10);
            }
        }

        private void LoadListMaps(ContentManager Content)
        {
            listViewMaps = new ListView(Tools.LoadTexture("Menu/fonditem", Content), new Vector2(10, 500), font, Color.Black, 6);
            mapspath = Directory.GetFiles((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Multi/"));
            for (int i = 0; i < mapspath.Length; i++)
            {
                mapspath[i] = mapspath[i].Replace((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Multi/"), "").Replace(".xml", "");
                listViewMaps.AddCell(new string[] { mapspath[i].ToLower() }, new int[] { 200 }, 10);
            }
        }
    }
}