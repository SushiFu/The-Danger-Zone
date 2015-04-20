#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

#endregion Using Statements

namespace ProjetSup_Win_SLMQ
{
    public partial class GameMain : Game
    {
        public GameState gameState;

        //
        private int hostPlayer;

        //
        private List<Player> players;

        private List<Enemy> Enemys;

        //
        private Menu menu;

        //
        private Sound sound;

        private Texture2D[,] map;
        private Texture2D destroyTile;
        private Texture2D cursor;
        public Map mapWorld;
        private Background background;
        private Client client;
        private Shot shotsPlayer;
        private Camera camera;

        //
        private Texture2D barreVie;

        private Texture2D caseVie;
        private Texture2D fondRadar;
        private Texture2D textBarre;
        private Texture2D HUDarmes;
        private Texture2D GameOver;
        private List<Entity> entityList;
        private Flag[] flags;
        private SpriteFont font;

        //
        private List<Vector3> destrucTileList;

        private List<Vector2> explosionReseau;
        public Vehicule Hornet, Banshee, Warthog;
        private bool fullScreenON = false;
        private bool soundON = true;
        private String msg = "";
        private Controles controles = new Controles();
        private List<Animate> animationList;
        private List<ParticleEngine> particlesEngineList;
        private List<Particles> particleList;
        private SpriteBatch sbHUD;
        private GameState oldGameState;
        private Player currentPlayer;
        private string personalFolder;
        private Solo solo;
        private Pause pause;
        private float scaleNormValue = 1f;
        private float scaleMiniValue = 0.047f;
        private float scaleMiniPersoValue = 4f;
        private float scaleMiniHud = 0.95f;
        public double mouseCoef = 1;
        private int decaleXMiniMap = 0;
        private SpriteBatch spriteBatchMini2;

        protected override void LoadContent()
        {
            sbHUD = new SpriteBatch(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatchMini = new SpriteBatch(GraphicsDevice);
            spriteBatchMini2 = new SpriteBatch(GraphicsDevice);
            destrucTileList = new List<Vector3>();
            entityList = new List<Entity>();
            animationList = new List<Animate>();
            particlesEngineList = new List<ParticleEngine>();
            particleList = new List<Particles>();
            explosionReseau = new List<Vector2>();

            switch (gameState)
            {
                case GameState.initialize:
                    //Load Cursor Texture and Position

                    personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "TDZ";
                    cursor = Tools.LoadTexture("Others/cursor", Content);
                    controles.cursorPosition = new Vector2();
                    Langage.LoadLanguage();
                    destroyTile = Tools.LoadTexture("SpriteTexture/destroyed", Content);

                    #region Load Textures

                    TexturesGame.LoadAmmo(Content);
                    TexturesGame.LoadPlayers(Content);
                    TexturesGame.LoadArmes(Content);
                    TexturesGame.LoadItem(Content);
                    TexturesGame.LoadImpact(Content);
                    TexturesGame.Loadtest(Content);
                    TexturesGame.LoadBonus(Content);
                    TexturesGame.LoadIAs(Content);
                    TexturesGame.LoadPlatForm(Content);
                    TexturesGame.Loadtest(Content);
                    TexturesGame.LoadSpeedUp(Content);
                    TexturesGame.LoadBackgrounds(Content);

                    #endregion Load Textures

                    menu = new Menu(Content, GraphicsDevice);
                    font = Tools.LoadFont("Fonts/AngryBirds/24", Content);
                    GameOver = Tools.LoadTexture("Menu/GameOver", Content);
                    fondRadar = Tools.LoadTexture("Others/fondRadar", Content);
                    HUDarmes = Tools.LoadTexture("HUD/HUD_armes", Content);
                    gameState = GameState.menu;
                    sound = new Sound(Content);

                    Player player = new Player(new Vector2(0, 0), TexturesGame.PlayerTab[0], 0, GraphicsDevice, LoadWeapons.LoadKnife(Content), true, FlagsType.blue);
                    menu = new Menu(Content, GraphicsDevice);
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Opt));
                        FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Options/OptionMenu", FileMode.Open);
                        XmlReader reader = XmlReader.Create(fs);
                        Opt obj = (Opt)serializer.Deserialize(reader);
                        fullScreenON = obj.Fullscreen;
                        menu.optionsMenu.toFullScreen = obj.Fullscreen;
                        soundON = obj.SoundOn;
                        sound.playEffects = soundON;
                        controles.controleTab = obj.controleTab;
                        Langage.langueactuelle = obj.language;

                        if (obj.masterServerAdress != null)
                        {
                            ConnectMaster.MasterIPAdress = obj.masterServerAdress;
                        }
                        fs.Close();

                        XmlSerializer serializer2 = new XmlSerializer(typeof(SavPerso));
                        FileStream fs2 = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Sauvegardes/" + obj.name, FileMode.Open);
                        XmlReader reader2 = XmlReader.Create(fs2);
                        SavPerso obj2 = (SavPerso)serializer2.Deserialize(reader2);
                        player.Deserialise(obj2, Content);
                        fs2.Close();
                    }
                    catch (Exception)
                    {
                        player = new Player(new Vector2(0, 0), TexturesGame.PlayerTab[0], 0, GraphicsDevice, LoadWeapons.LoadKnife(Content), true, FlagsType.blue);
                        player.ItemList.Add(LoadWeapons.LoadKnife(Content));
                    }
                    currentPlayer = player;
                    solo = new Solo(player.currentLevel, player);
                    menu.LaunchMenuSound(sound);
                    break;

                case GameState.editor:

                    sound.soundPlayer.Stop();

                    // Prepare the process to run
                    ProcessStartInfo start = new ProcessStartInfo();
                    // Enter the executable to run, including the complete path
                    start.FileName = "./MapEditor.exe";
                    // Do you want to show a console window?
                    start.WindowStyle = ProcessWindowStyle.Hidden;
                    start.CreateNoWindow = true;

                    // Run the external process & wait for it to finish
                    using (Process proc = Process.Start(start))
                    {
                        proc.WaitForExit();
                    }


                    gameState = GameState.menu;
                    menu = new Menu(Content, GraphicsDevice);
                    menu.LaunchMenuSound(sound);
                    break;

                case GameState.solo:

                    sound.soundPlayer.Stop();

                    //Load List Tirs and Texture
                    shotsPlayer = new Shot();

                    //Load Map
                    LoadMap(menu.mapname + ".xml");
                    LoadMapTextures();

                    //Load Player Array
                    hostPlayer = 0;
                    LoadPlayer();
                    players[hostPlayer].position = mapWorld.spawnPoint;
                    players[hostPlayer].image = TexturesGame.PlayerTab[players[hostPlayer].spritePerso];
                    // LoadEnemy();
                    LoadSurvivalEnemy();
                    LoadItem();

                    //Load HUD
                    LoadHUD();

                    background = new Background(Content, mapWorld);
                    camera = new Camera(players, hostPlayer, scaleNormValue, GraphicsDevice, mapWorld);

                    break;

                case GameState.scenario:

                    sound.soundPlayer.Stop();

                    //Load List Tirs and Texture
                    shotsPlayer = new Shot();

                    //Load Map
                    LoadMap(solo.currentMap + ".xml");
                    LoadMapTextures();

                    //Load Player Array
                    solo.player.position = mapWorld.spawnPoint;
                    hostPlayer = 0;
                    players = new List<Player>();
                    players.Add(solo.player);
                    entityList.Add(solo.player);

                    Enemys = new List<Enemy>();

                    for (int i = 0; i < mapWorld.IASpawnList.Count; i++)
                    {
                        Enemys.Add(new Enemy(mapWorld.IASpawnList[i], TexturesGame.IATab[0], 100 + i, Content, LoadWeapons.LoadIAGun(Content), false, 0, GraphicsDevice));
                        Enemys[i].iaType = IAType.patrouille;
                        entityList.Add(Enemys[i]);
                        Enemys[i].attaque = currentPlayer.niveau * 0.1f;
                        Enemys[i].defense = 2 / currentPlayer.niveau;
                    }

                    LoadItem();

                    //Load HUD
                    LoadHUD();

                    background = new Background(Content, mapWorld);
                    camera = new Camera(players, hostPlayer, scaleNormValue, GraphicsDevice, mapWorld);

                    break;

                case GameState.multi:
                    sound.soundPlayer.Stop();

                    //Launch Network and Load Map
                    mapWorld = new Map();
                    client = new Client(ConnectMaster.MasterIPAdress, Convert.ToInt32(menu.multiMenu.tmpserv.port), ref hostPlayer, mapWorld, Content, menu.multiMenu.tmpserv.mode);

                    if (client.mode == ModeMulti.ctf)
                    {
                        flags = new Flag[2];
                        flags[0] = new Flag(mapWorld.drapeau2, Content, FlagsType.red);
                        flags[1] = new Flag(mapWorld.drapeau1, Content, FlagsType.blue);
                        foreach (Flag item in flags)
                        {
                            mapWorld.itemList.Add(item);
                        }
                    }

                    LoadMapTextures();

                    //Platforms
                    for (int i = 0; i < mapWorld.platFormList.Count; i++)
                    {
                        entityList.Add(mapWorld.platFormList[i]);
                    }

                    //Load List Tirs and Texture
                    shotsPlayer = new Shot();

                    //Load Player Array
                    LoadPlayer();
                    SetCurrentPlayer();
                    Enemys = new List<Enemy>();

                    //Load HUD
                    LoadHUD();

                    background = new Background(Content, mapWorld);
                    camera = new Camera(players, hostPlayer, scaleNormValue, GraphicsDevice, mapWorld);

                    break;

                default:
                    break;
            }
        }

        private void LoadMap(String filename)
        {
            mapWorld = new Map();
            mapWorld.Load(filename, Content);
            for (int i = 0; i < mapWorld.platFormList.Count; i++)
            {
                entityList.Add(mapWorld.platFormList[i]);
            }
        }

        private void LoadMapTextures()
        {
            map = new Texture2D[mapWorld.world.GetLength(1), mapWorld.world.GetLength(0)];

            for (int i = 0; i < mapWorld.world.GetLength(1); i++)
            {
                for (int j = 0; j < mapWorld.world.GetLength(0); j++)
                {
                    map[i, j] = Tools.LoadTexture(mapWorld.world[j, i].texture, Content);
                }
            }
        }

        private void LoadHUD()
        {
            caseVie = Tools.LoadTexture("HUD/CaseVie", Content);
            barreVie = Tools.LoadTexture("HUD/BarreVie", Content);
            textBarre = Tools.LoadTexture("HUD/TextBarre", Content);
            //CurrentWeaponAmo = Tools.LoadFont( "Fonts/fonttest",Content);
        }

        private void LoadPlayer()
        {
            players = new List<Player>();
            int i;
            for (i = 0; i < hostPlayer; i++)
            {
                Player player;
                if (i % 2 == 0)
                {
                    player = new Player(mapWorld.spawnPoint2, TexturesGame.PlayerTab[0], players.Count, GraphicsDevice, LoadWeapons.LoadKnife(Content), true, FlagsType.red);
                }
                else
                {
                    player = new Player(mapWorld.spawnPoint, TexturesGame.PlayerTab[1], players.Count, GraphicsDevice, LoadWeapons.LoadKnife(Content), true, FlagsType.blue);
                }
                players.Add(player);
                entityList.Add(player);
            }
            currentPlayer.id = i + 1;
            players.Add(currentPlayer);
            entityList.Add(currentPlayer);
        }

        private void SetCurrentPlayer()
        {
            if (hostPlayer % 2 == 0)
            {
                currentPlayer.position = mapWorld.spawnPoint2;
                currentPlayer.currenTeam = FlagsType.red;
                currentPlayer.image = TexturesGame.PlayerTab[0];
            }
            else
            {
                currentPlayer.position = mapWorld.spawnPoint;
                currentPlayer.currenTeam = FlagsType.blue;
                currentPlayer.image = TexturesGame.PlayerTab[1];
            }
        }

        private void LoadEnemy()
        {
            Enemys = new List<Enemy>();

            for (int i = 0; i < 2; i++) ///!
            {
                Enemys.Add(new Enemy(mapWorld.IASpawnList[i], TexturesGame.IATab[0], 100 + i, Content, LoadWeapons.LoadIAGun(Content), true, 0, GraphicsDevice));
            }
        }

        private void LoadSurvivalEnemy()
        {
            Enemys = new List<Enemy>();
            int nbIa = 0;
            if (mapWorld.IASpawnList.Count > 2)
                nbIa = 2;
            else
                nbIa = mapWorld.IASpawnList.Count;

            for (int i = 0; i < nbIa; i++)
            {
                Enemys.Add(new Enemy(mapWorld.IASpawnList[i], TexturesGame.IATab[0], 100 + i, Content, LoadWeapons.LoadIAGun(Content), true, 0, GraphicsDevice));
                entityList.Add(Enemys[i]);
            }
        }

        private void LoadSurvival(List<Enemy> Enemys)
        {
            if (Player1Events.SurvivalLvl1 == Player1Events.SurvivalLvL0 && Player1Events.SurvivalLvl1 > 0 && Enemys.Count < 20)
            {
                for (int i = 0; i < 2; i++)
                {
                    Enemys.Capacity++;
                    Enemys.Add(new Enemy(mapWorld.IASpawnList[Enemys.Count % mapWorld.IASpawnList.Count], TexturesGame.IATab[0], 100 + Enemys.Count, Content, LoadWeapons.LoadIAGun(Content), true, 0, GraphicsDevice));
                    entityList.Capacity++;
                    entityList.Add(Enemys[Enemys.Count - 1]);
                }
                Player1Events.SurvivalLvl1++;
            }
        }

        private void LoadItem()
        {/*

            Hornet = new Vehicule(vehiculeType.Hornet, mapWorld.spawnPoint + new Vector2(600, -100), 2, 20, new Vector2(3, 3), Direction.stop, true, new Vector2(182, 120), TexturesGame.itemTab[0], 0);
            Banshee = new Vehicule(vehiculeType.Banshee, mapWorld.spawnPoint, 1, 20, new Vector2(3, 2), Direction.stop, true, new Vector2(147, 117), TexturesGame.itemTab[1], 0);
            mapWorld.itemList.Add(Hornet);
            entityList.Add(Hornet);
            entityList.Add(Banshee);
            Hornet.name = "Hornet";
            Banshee.name = "Banshee";
            Banshee.VehicleGun.Add(LoadWeapons.LoadBansheeBomb(Content));
            Banshee.VehicleGun.Add(LoadWeapons.LoadBansheeGun(Content));
            mapWorld.itemList.Add(Banshee);*/
            //deplacé dans tile
        }
    }
}