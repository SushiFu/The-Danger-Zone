#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

#endregion Using Statements

namespace ProjetSup_Win_SLMQ
{
    public partial class GameMain : Game
    {
        protected override void Update(GameTime gameTime)
        {
            UpdateOption();
            currentPlayer.UpdateLevel(controles, animationList, Content);
            if (controles.Pause())
            {
                if (gameState == GameState.scenario || gameState == GameState.solo)
                {
                    oldGameState = gameState;
                    gameState = GameState.pause;
                    pause = new Pause(Content);
                }
                else if (gameState == GameState.pause || gameState == GameState.multiPause)
                {
                    gameState = oldGameState;
                }
                else if (gameState == GameState.creation)
                {
                    currentPlayer.newChar = false;
                    SavPerso obj = new SavPerso();
                    currentPlayer.Serialize(obj);
                    System.Xml.Serialization.XmlSerializer writer2 = new System.Xml.Serialization.XmlSerializer(typeof(SavPerso));
                    System.IO.StreamWriter file2 = new System.IO.StreamWriter((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Sauvegardes/") + currentPlayer.name);
                    writer2.Serialize(file2, obj);
                    file2.Close();
                    gameState = GameState.menu;
                    menu.menuState = MenuState.main;
                    menu = new Menu(Content, GraphicsDevice);
                }
                else if (gameState == GameState.menu)
                {
                    gameState = GameState.menu;
                    menu = new Menu(Content, GraphicsDevice);
                }
                else if (gameState == GameState.multi)
                {
                    oldGameState = gameState;
                    gameState = GameState.multiPause;
                    pause = new Pause(Content);
                }
                else
                {
                    gameState = GameState.menu;
                    menu.menuState = MenuState.main;
                    menu = new Menu(Content, GraphicsDevice);
                    sound.soundPlayer.Stop();
                    menu.LaunchMenuSound(sound);
                }
            }

            //Events States Mouse and Keyboard

            msg = "";

            switch (gameState)
            {
                case GameState.solo:

                    sound.UpdateMusic();
                    destrucTileList = new List<Vector3>();
                    players[hostPlayer].Update(animationList);
                    Player1Events.CheckEvents(players[hostPlayer], mapWorld, destrucTileList, hostPlayer, shotsPlayer, sound, ref msg, controles, entityList, animationList, particlesEngineList, Content, explosionReseau);
                    Player1Events.MouseEvents(players[hostPlayer], shotsPlayer, camera, sound, controles, animationList, particlesEngineList);
                    shotsPlayer.Update(animationList);
                    UpdateItem();
                    UpdateExplosion();
                    UpdateAnimation();
                    Reset();
                    UpdateParticleEngine();

                    #region IA Update

                    if (!currentPlayer.God)
                    {
                        for (int i = 0; i < Enemys.Count; i++)
                        {
                            Enemys[i].shotsIA.otherTirList = new List<Tirs>();
                            Enemys[i].shotsIA.otherTirList = shotsPlayer.tirList;
                            for (int j = 0; j < Enemys[i].shotsIA.tirList.Count; j++)
                            {
                                shotsPlayer.otherTirList.Add(Enemys[i].shotsIA.tirList[j]);
                            }
                        }

                        for (int i = 0; i < Enemys.Count; i++)
                        {
                            IA.IACheckEvents(Enemys[i], IAType.Survival, mapWorld, players, Enemys[i].shotsIA, destrucTileList, hostPlayer, sound, entityList, animationList, particlesEngineList, Content, Enemys, explosionReseau);
                            LoadSurvival(Enemys);
                        }

                        for (int i = 0; i < Enemys.Count; i++)
                        {
                            Enemys[i].shotsIA.Update(animationList);
                        }
                    }

                    #endregion IA Update

                    mapWorld.Update();
                    camera.UpdateCentered(players, hostPlayer, scaleNormValue, GraphicsDevice, controles);
                    break;

                case GameState.pause:
                    pause.Update(mouseCoef, controles);
                    if (pause.menu)
                    {
                        gameState = GameState.menu;
                        menu.menuState = MenuState.main;
                        menu = new Menu(Content, GraphicsDevice);
                        sound.soundPlayer.Stop();
                        menu.LaunchMenuSound(sound);
                    }
                    else if (pause.resumeGame)
                        gameState = oldGameState;
                    break;

                case GameState.scenario:
                    sound.UpdateMusic();

                    solo.Update(players[hostPlayer], mapWorld);
                    if (solo.levelUp)
                    {
                        solo = new Solo(solo.currentLevel, solo.player);
                        LoadContent();
                    }
                    if (currentPlayer.lifes < 1)
                    {
                        gameState = GameState.gameOver;
                    }
                    players[hostPlayer].Update(animationList);
                    Player1Events.CheckEvents(players[hostPlayer], mapWorld, destrucTileList, hostPlayer, shotsPlayer, sound, ref msg, controles, entityList, animationList, particlesEngineList, Content, explosionReseau);
                    Player1Events.MouseEvents(players[hostPlayer], shotsPlayer, camera, sound, controles, animationList, particlesEngineList);
                    shotsPlayer.Update(animationList);
                    UpdateItem();
                    UpdateExplosion();
                    UpdateAnimation();
                    Reset();
                    UpdateParticleEngine();
                    if (!currentPlayer.God)
                    {
                        for (int i = 0; i < Enemys.Count; i++)
                        {
                            Enemys[i].shotsIA.otherTirList = new List<Tirs>();
                            Enemys[i].shotsIA.otherTirList = shotsPlayer.tirList;
                            for (int j = 0; j < Enemys[i].shotsIA.tirList.Count; j++)
                            {
                                shotsPlayer.otherTirList.Add(Enemys[i].shotsIA.tirList[j]);
                            }
                        }

                        for (int i = 0; i < Enemys.Count; i++)
                        {
                            IA.IACheckEvents(Enemys[i], IAType.patr_Snd, mapWorld, players, Enemys[i].shotsIA, destrucTileList, hostPlayer, sound, entityList, animationList, particlesEngineList, Content, Enemys, explosionReseau);
                            if (!Enemys[i].IsAlive)
                            {
                                entityList.Remove(Enemys[i]);
                                Enemys.RemoveAt(i);
                            }
                        }

                        for (int i = 0; i < Enemys.Count; i++)
                        {
                            Enemys[i].shotsIA.Update(animationList);
                        }
                    }

                    mapWorld.Update();
                    camera.UpdateCentered(players, hostPlayer, scaleNormValue, GraphicsDevice, controles);
                    break;

                case GameState.multi:
                    sound.UpdateMusic();
                    destrucTileList = new List<Vector3>();
                    players[hostPlayer].Update(animationList);
                    Player1Events.CheckEvents(players[hostPlayer], mapWorld, destrucTileList, hostPlayer, shotsPlayer, sound, ref msg, controles, entityList, animationList, particlesEngineList, Content, explosionReseau);
                    Player1Events.MouseEvents(players[hostPlayer], shotsPlayer, camera, sound, controles, animationList, particlesEngineList);
                    //Player1Events.SurvivalLvL0 = Player1Events.killplayer0 / 50;
                    UpdateParticleEngine();
                    shotsPlayer.Update(animationList);
                    UpdateExplosion();
                    UpdateItem();
                    UpdateAnimation();
                    Reset();
                    camera.UpdateCentered(players, hostPlayer, scaleNormValue, GraphicsDevice, controles);
                    bool flagCapture = false;
                    if (client.mode == ModeMulti.ctf)
                    {
                        foreach (Flag flag in flags)
                        {
                            flagCapture = flag.Update(players[hostPlayer], mapWorld);
                        }
                    }
                    UpdateNetwork(flagCapture);
                    break;

                case GameState.multiPause:

                    sound.UpdateMusic();
                    destrucTileList = new List<Vector3>();
                    players[hostPlayer].Update(animationList);
                    Player1Events.CheckEvents(players[hostPlayer], mapWorld, destrucTileList, hostPlayer, shotsPlayer, sound, ref msg, controles, entityList, animationList, particlesEngineList, Content, explosionReseau);
                    Player1Events.MouseEvents(players[hostPlayer], shotsPlayer, camera, sound, controles, animationList, particlesEngineList);
                    //Player1Events.SurvivalLvL0 = Player1Events.killplayer0 / 50;
                    UpdateParticleEngine();
                    shotsPlayer.Update(animationList);
                    UpdateExplosion();
                    UpdateItem();
                    UpdateAnimation();
                    Reset();
                    camera.UpdateCentered(players, hostPlayer, scaleNormValue, GraphicsDevice, controles);
                    flagCapture = false;
                    if (client.mode == ModeMulti.ctf)
                    {
                        foreach (Flag flag in flags)
                        {
                            flagCapture = flag.Update(players[hostPlayer], mapWorld);
                        }
                    }
                    UpdateNetwork(flagCapture);
                    pause.Update(mouseCoef, controles);
                    if (pause.menu)
                    {
                        for (int i = 0; i < players[hostPlayer].ItemList.Count; i++)
                        {
                            if (players[hostPlayer].ItemList[i].type == objectType.flag)
                            {
                                players[hostPlayer].ItemList[i].isOnMap = true;
                                ((Flag)players[hostPlayer].ItemList[i]).isCaptured = false;
                                mapWorld.itemList.Add(players[hostPlayer].ItemList[i]);
                                players[hostPlayer].ItemList.RemoveAt(i);
                                break;
                            }
                        }
                        client.SendExit(hostPlayer);
                        gameState = GameState.menu;
                        menu.menuState = MenuState.main;
                        menu = new Menu(Content, GraphicsDevice);
                        sound.soundPlayer.Stop();
                        menu.LaunchMenuSound(sound);
                    }
                    else if (pause.resumeGame)
                        gameState = oldGameState;
                    break;

                case GameState.menu:
                    menu.Update(ref gameState, mouseCoef, Content, ref fullScreenON, ref soundON, sound, controles);

                    if (gameState == GameState.exit)
                    {
                        Opt option = new Opt();
                        option.Fullscreen = fullScreenON;
                        option.SoundOn = soundON;
                        option.controleTab = controles.controleTab;
                        option.name = currentPlayer.name;
                        option.language = Langage.langueactuelle;
                        option.masterServerAdress = ConnectMaster.MasterIPAdress;
                        System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Opt));
                        System.IO.StreamWriter file = new System.IO.StreamWriter((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Options/OptionMenu"));
                        writer.Serialize(file, option);
                        file.Close();

                        SavPerso obj = new SavPerso();
                        currentPlayer.Serialize(obj);
                        System.Xml.Serialization.XmlSerializer writer2 = new System.Xml.Serialization.XmlSerializer(typeof(SavPerso));
                        System.IO.StreamWriter file2 = new System.IO.StreamWriter((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Sauvegardes/") + currentPlayer.name);
                        writer2.Serialize(file2, obj);
                        file2.Close();

                        Exit();
                    }
                    if (gameState == GameState.multi || gameState == GameState.solo || gameState == GameState.editor || gameState == GameState.scenario)
                    {
                        LoadContent();
                    }

                    break;

                case GameState.creation:
                    menu.gestion.Update(ref currentPlayer, mouseCoef, controles, Content, GraphicsDevice);
                    break;

                default:
                    break;
            }

            //Save old Events State

            base.Update(gameTime);
        }

        private void Reset()
        {
            shotsPlayer.otherTirList = new List<Tirs>();
        }

        private void UpdateParticleEngine()
        {
            for (int i = 0; i < mapWorld.particleEngine.Count; i++)
            {
                mapWorld.particleEngine[i].Update(particleList);
                if (mapWorld.particleEngine[i].remove)
                    mapWorld.particleEngine.RemoveAt(i);
            }
            for (int i = 0; i < particleList.Count; i++)
            {
                particleList[i].Update(mapWorld, particleList);
                if (particleList[i].remove)
                    particleList.RemoveAt(i);
            }
        }

        private void UpdateNetwork(bool isFlagCaptured)
        {
            client.SendPacket(players, hostPlayer, destrucTileList, shotsPlayer, isFlagCaptured);

            client.GetPacket(players, hostPlayer, mapWorld, shotsPlayer, Content, GraphicsDevice, mapWorld.explosionList, particlesEngineList, sound, explosionReseau, entityList);
        }

        private void UpdateItem()
        {
            for (int i = 0; i < mapWorld.Items.Count; i++)
            {
                mapWorld.Items[i].update(mapWorld, sound, shotsPlayer, entityList, animationList);
                if (mapWorld.Items[i].remove)
                    mapWorld.Items.RemoveAt(i);
            }
        }

        private void UpdateAnimation()
        {
            for (int i = 0; i < animationList.Count; i++)
            {
                animationList[i].Update();
                if (animationList[i].remove)
                {
                    animationList.RemoveAt(i);
                }
            }
        }

        private void UpdateExplosion()
        {
            for (int i = 0; i < mapWorld.explosionList.Count; i++)
            {
                mapWorld.explosionList[i].iteration++;
                if (mapWorld.explosionList[i].iteration > 20)
                    mapWorld.explosionList.RemoveAt(i);
            }
        }

        private void UpdateOption()
        {
            controles.Update(GamePad.GetState(PlayerIndex.One), Mouse.GetState(), Keyboard.GetState());
            UpdateMouse();
            if (menu.optionsMenu.toFullScreen)
            {
                //Window.Position = new Point(0, 0);
                for (int i = 0; i < 30; i++)
                {
                    graphics.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height + 1;
                    Window.AllowUserResizing = false;
                    Window.IsBorderless = true;
                    graphics.ApplyChanges();
                    menu.optionsMenu.toFullScreen = false;
                }
                for (int i = 0; i < 10; i++)
                {
                    graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width + 1;
                    Window.AllowUserResizing = false;
                    Window.IsBorderless = true;
                    graphics.ApplyChanges();
                    menu.optionsMenu.toFullScreen = false;
                }
            }
            if (menu.optionsMenu.toWindowed)
            {
                for (int i = 0; i < 30; i++)
                {
                    graphics.PreferredBackBufferHeight = GraphicsDevice.Viewport.Height - 1;
                    Window.AllowUserResizing = false;
                    Window.IsBorderless = false;
                    graphics.ApplyChanges();
                    menu.optionsMenu.toWindowed = false;
                }
                for (int i = 0; i < 10; i++)
                {
                    graphics.PreferredBackBufferWidth = GraphicsDevice.Viewport.Width - 1;
                    Window.AllowUserResizing = false;
                    Window.IsBorderless = false;
                    graphics.ApplyChanges();
                    menu.optionsMenu.toFullScreen = false;
                }
            }
            sound.playEffects = soundON;
        }

        private void UpdateMouse()
        {
            if (controles.gamePad.Buttons.A == ButtonState.Pressed || controles.gamePad.ThumbSticks.Right.X != 0 || controles.gamePad.ThumbSticks.Right.Y != 0 || controles.gamePad.ThumbSticks.Left.X != 0 || controles.gamePad.ThumbSticks.Left.Y != 0)
            {
                float yVal = 0;
                float xVal = 0;

                if (Math.Abs(controles.gamePad.ThumbSticks.Right.X) > 0.2)
                    xVal = controles.gamePad.ThumbSticks.Right.X;

                if (Math.Abs(controles.gamePad.ThumbSticks.Right.Y) > 0.2)
                    yVal = controles.gamePad.ThumbSticks.Right.Y;

                Mouse.SetPosition((int)(Mouse.GetState().X + xVal * 15), (int)(Mouse.GetState().Y - yVal * 15));

                if ((gameState == GameState.solo || gameState == GameState.scenario || gameState == GameState.multi) && !currentPlayer.currentWeapon.specialAttackEnabled)
                {
                    Vector2 screenPos = new Vector2((currentPlayer.position.X - camera.Xcurrent) * scaleNormValue, scaleNormValue * (currentPlayer.position.Y - camera.Ycurrent));

                    Vector2 dir = (Vector2.Normalize(new Vector2(Mouse.GetState().X - screenPos.X, Mouse.GetState().Y - screenPos.Y))) * 250;

                    if (controles.gamePad.ThumbSticks.Right.X > 0.5)
                        dir.X = Math.Abs(dir.X);
                    else if (controles.gamePad.ThumbSticks.Right.X < -0.5)
                        dir.X = -Math.Abs(dir.X);
                    else if (controles.gamePad.ThumbSticks.Left.X < -0.5)
                        dir.X = -Math.Abs(dir.X);
                    else if (controles.gamePad.ThumbSticks.Left.X > 0.5)
                        dir.X = Math.Abs(dir.X);

                    Mouse.SetPosition((int)(dir.X + screenPos.X), (int)(dir.Y + screenPos.Y));
                }
            }

            controles.cursorPosition = new Vector2((float)(Mouse.GetState().X * mouseCoef) - cursor.Width / 2, (float)(Mouse.GetState().Y * mouseCoef) - cursor.Height / 2);
        }
    }
}