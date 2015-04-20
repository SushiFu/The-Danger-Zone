#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System;
using System.Collections.Generic;

#endregion Using Statements

namespace ProjetSup_Win_SLMQ
{
    public partial class GameMain : Game
    {
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //zoom
            if (GraphicsDevice.Viewport.Height < 1080 && gameState == GameState.menu)
            {
                scaleNormValue = 0.7112f;
                mouseCoef = 1.40625;
            }
            else if (GraphicsDevice.Viewport.Height < 1080)
            {
                scaleNormValue = 0.7112f;
                scaleMiniHud = 0.5325f;
            }
            else
            {
                scaleMiniHud = 0.75f;
                mouseCoef = 1;
                scaleNormValue = 1f;
            }

            Matrix scaleNormal = Matrix.CreateScale(scaleNormValue);
            Matrix scaleMini = Matrix.CreateScale(scaleMiniValue);
            Matrix scaleHUD = Matrix.CreateScale(1);
            Matrix scaleMini2 = Matrix.CreateScale(scaleMiniHud);

            spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, scaleNormal);
            spriteBatchMini.Begin(SpriteSortMode.Deferred, null, null, null, null, null, scaleMini);
            spriteBatchMini2.Begin(SpriteSortMode.Deferred, null, null, null, null, null, scaleMini2);
            sbHUD.Begin(SpriteSortMode.Deferred, null, null, null, null, null, scaleHUD);

            switch (gameState)
            {
                case GameState.solo:

                    background.Draw(spriteBatch, camera);
                    camera.UpdateExplosion(mapWorld);
                    DrawMap();

                    DrawPerso();
                    DrawAnimation();
                    DrawTir();
                    DrawParticles();
                    DrawWater();
                    DrawPlatForm();
                    DrawItems();
                    DrawItem();
                    DrawSpecial();
                    DrawMiniMap();
                    DrawSoloScores();
                    DrawInventory();
                    DrawLifeBar();
                    DrawMessage();

                    break;

                case GameState.scenario:
                    background.Draw(spriteBatch, camera);
                    camera.UpdateExplosion(mapWorld);
                    DrawMap();

                    DrawPerso();
                    DrawAnimation();
                    DrawTir();
                    DrawParticles();
                    DrawWater();
                    DrawPlatForm();
                    DrawItems();
                    DrawItem();
                    DrawSpecial();
                    DrawLifeBar();
                    DrawMiniMap();
                    DrawInventory();

                    DrawSoloScores();
                    DrawMessage();

                    break;

                case GameState.pause:

                    background.Draw(spriteBatch, camera);

                    DrawMap();

                    DrawPerso();
                    DrawAnimation();
                    DrawTir();
                    DrawParticles();
                    DrawWater();
                    DrawPlatForm();
                    DrawItems();

                    DrawInventory();

                    DrawSoloScores();
                    DrawItem();
                    DrawMessage();

                    pause.draw(spriteBatch, Content);
                    break;

                case GameState.multi:
                    background.Draw(spriteBatch, camera);
                    camera.UpdateExplosion(mapWorld);
                    DrawMap();
                    if (client.mode == ModeMulti.ctf)
                    {
                        foreach (Flag flag in flags)
                        {
                            flag.Draw(spriteBatch, camera);
                        }
                    }
                    DrawPerso();
                    DrawAnimation();
                    DrawTir();
                    DrawParticles();
                    DrawWater();
                    DrawPlatForm();
                    DrawItems();
                    DrawItem();
                    DrawSpecial();
                    DrawLifeBar();
                    DrawMiniMap();
                    DrawInventory();
                    DrawSoloScores();
                    DrawMultiScore();
                    DrawMessage();
                    break;

                case GameState.multiPause:
                    background.Draw(spriteBatch, camera);
                    camera.UpdateExplosion(mapWorld);
                    DrawMap();
                    if (client.mode == ModeMulti.ctf)
                    {
                        foreach (Flag flag in flags)
                        {
                            flag.Draw(spriteBatch, camera);
                        }
                    }
                    DrawPerso();
                    DrawAnimation();
                    DrawTir();
                    DrawParticles();
                    DrawWater();
                    DrawPlatForm();
                    DrawItems();
                    DrawItem();
                    DrawSpecial();
                    DrawLifeBar();
                    DrawMiniMap();
                    DrawInventory();
                    DrawSoloScores();
                    DrawMultiScore();
                    DrawMessage();

                    pause.draw(spriteBatch, Content);
                    break;

                case GameState.menu:
                    menu.Draw(spriteBatch);
                    break;

                case GameState.creation:
                    menu.gestion.Draw(currentPlayer, spriteBatch, Content);
                    break;

                case GameState.gameOver:
                    spriteBatch.Draw(GameOver, new Vector2(0, 0), Color.White);
                    break;

                default:
                    break;
            }

            DrawCursor();

            spriteBatch.End();
            spriteBatchMini.End();
            sbHUD.End();
            spriteBatchMini2.End();

            base.Draw(gameTime);
        }

        private void DrawMap()
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (mapWorld.world[j, i].draw)
                    {
                        spriteBatch.Draw(map[i, j], DecaleXY(mapWorld.world[j, i].position, camera), Color.White);
                    }
                    else if (!mapWorld.world[j, i].alive && mapWorld.world[j, i].type != objectType.caisse && mapWorld.world[j, i].type != objectType.explosif)
                    {
                        spriteBatch.Draw(destroyTile, DecaleXY(mapWorld.world[j, i].position, camera), Color.White);
                    }
                }
            }
        }

        private void DrawMultiScore()
        {
            spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 58) + client.redScore.ToString(), new Vector2(1400, 20), Color.Red);
            spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 59) + client.blueScore.ToString(), new Vector2(1400, 70), Color.Blue);
        }

        private void DrawPlatForm()
        {
            for (int i = 0; i < mapWorld.platFormList.Count; i++)
            {
                spriteBatch.Draw(TexturesGame.platForm[0], DecaleXY(mapWorld.platFormList[i].position, camera), Color.White);
            }
        }

        private void DrawAnimation()
        {
            for (int i = 0; i < animationList.Count; i++)
            {
                animationList[i].Draw(spriteBatch, camera);
            }
        }

        private void DrawItems()
        {
            for (int i = 0; i < mapWorld.Items.Count; i++)
            {
                mapWorld.Items[i].Draw(spriteBatch, camera);
            }
        }

        private void DrawParticles()
        {
            for (int i = 0; i < particleList.Count; i++)
            {
                particleList[i].Draw(spriteBatch, camera);
            }
        }

        private void DrawMiniMap()
        {
            spriteBatchMini.Draw(fondRadar, new Vector2(0, 0), Color.White);
            int Xmoins = 0;

            int Xplus = mapWorld.width;
            int Ymoins = 0;
            int Yplus = mapWorld.height;

            if (((int)(currentPlayer.position.X / 75) - 30 - decaleXMiniMap) > 0)
            {
                Xmoins = (int)(currentPlayer.position.X / 75) - 30 - decaleXMiniMap;
                decaleXMiniMap = 0;
            }
            else
                decaleXMiniMap = Math.Abs(((int)(currentPlayer.position.X / 75) - 30));
            if (((int)(currentPlayer.position.X / 75) + 30 + decaleXMiniMap) < mapWorld.width)
            {
                Xplus = ((int)(currentPlayer.position.X / 75) + 30 + decaleXMiniMap);
                decaleXMiniMap = 0;
            }
            else
                decaleXMiniMap = ((int)(currentPlayer.position.X / 75) + 30) - mapWorld.width;

            for (int i = Xmoins; i < Xplus; i++)
            {
                for (int j = 0; j < mapWorld.height; j++)
                {
                    if (mapWorld.world[j, i].draw)
                    {
                        spriteBatchMini.Draw(map[i, j], mapWorld.world[j, i].position - new Vector2(Xmoins * 75, Ymoins * 75), Color.White);
                    }
                }
            }
            //Draw Platform
            for (int i = 0; i < mapWorld.platFormList.Count; i++)
            {
                if (mapWorld.platFormList[i].position.X > Xmoins * 75 && mapWorld.platFormList[i].position.X < Xplus * 75 && mapWorld.platFormList[i].position.Y > Ymoins * 75 && mapWorld.platFormList[i].position.Y < Yplus * 75)
                    spriteBatchMini.Draw(TexturesGame.platForm[0], mapWorld.platFormList[i].position - new Vector2(Xmoins * 75, Ymoins * 75), Color.White);
            }
            //Players
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].position.X > Xmoins * 75 && players[i].position.X < Xplus * 75 && players[i].position.Y > Ymoins * 75 && players[i].position.Y < Yplus * 75)
                    spriteBatchMini.Draw(players[i].image[(int)players[i].activeSprite], new Vector2(players[i].position.X - 200, players[i].position.Y - 300) - new Vector2(Xmoins * 75, Ymoins * 75), null, Color.White, 0f, Vector2.Zero, scaleMiniPersoValue, SpriteEffects.None, 0f);
            }
            // Enemys
            for (int i = 0; i < Enemys.Count; i++)
            {
                if (Enemys[i].position.X > Xmoins * 75 && Enemys[i].position.X < Xplus * 75 && Enemys[i].position.Y > Ymoins * 75 && Enemys[i].position.Y < Yplus * 75)
                    spriteBatchMini.Draw(Enemys[i].image[(int)Enemys[i].activeSprite], new Vector2(Enemys[i].position.X - 200, Enemys[i].position.Y - 300) - new Vector2(Xmoins * 75, Ymoins * 75), null, Color.White, 0f, Vector2.Zero, scaleMiniPersoValue, SpriteEffects.None, 0f);
            }
            //Vehicles
            for (int i = 0; i < mapWorld.itemList.Count; i++)
            {
                if (mapWorld.itemList[i].type == objectType.vehicule)
                {
                    if ((((Vehicule)mapWorld.itemList[i]).vehiculeType == vehiculeType.Hornet || ((Vehicule)mapWorld.itemList[i]).vehiculeType == vehiculeType.Banshee) && mapWorld.itemList[i].isOnMap)
                    {
                        if (mapWorld.itemList[i].position.X > Xmoins * 75 && mapWorld.itemList[i].position.X < Xplus * 75 && mapWorld.itemList[i].position.Y > Ymoins * 75 && mapWorld.itemList[i].position.Y < Yplus * 75)
                            spriteBatchMini.Draw(((Vehicule)mapWorld.itemList[i]).image[0], new Vector2(mapWorld.itemList[i].position.X - 200, mapWorld.itemList[i].position.Y - 300) - new Vector2(Xmoins * 75, Ymoins * 75), null, Color.White, 0f, Vector2.Zero, scaleMiniPersoValue, SpriteEffects.None, 0f);
                    }
                }
            }
        }

        private void DrawTir()
        {
            shotsPlayer.Draw(spriteBatch, camera);
            foreach (Enemy enemy in Enemys)
            {
                enemy.shotsIA.Draw(spriteBatch, camera);
            }
        }

        private void DrawCursor()
        {
            spriteBatch.Draw(cursor, controles.cursorPosition, Color.White);
        }

        private void DrawItem()
        {
            for (int i = 0; i < mapWorld.itemList.Count; i++)
            {
                if (mapWorld.itemList[i].isOnMap && mapWorld.itemList[i].type == objectType.vehicule)
                {
                    spriteBatch.Draw(((Vehicule)mapWorld.itemList[i]).image[22], DecaleXY(mapWorld.itemList[i].position, camera), Color.White);
                }
            }
        }

        private void DrawLifeBar()
        {
            int x = 700;
            int y = 10;
            int For10 = (int)((players[hostPlayer].pV * 10) / players[hostPlayer].pVMax);
            sbHUD.Draw(textBarre, new Vector2(x - 40, y), Color.White);
            sbHUD.Draw(barreVie, new Vector2(x, y), Color.White);
            for (int i = 0; i < For10; i++)
                sbHUD.Draw(caseVie, new Vector2(x + 5 + (i * 9), y + 2), Color.White);
            if (currentPlayer.scoreKey)
            {
                spriteBatch.Draw(Tools.LoadTexture("HUD/HUD_level", Content), new Vector2(320, 10), Color.White);
                spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 10) + currentPlayer.niveau + "          Exp : " + currentPlayer.experience + "/" + (int)(Math.Pow(1.5, currentPlayer.niveau) * Math.PI * 100), new Vector2(330, 16), Color.Black);
                spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 11) + currentPlayer.money + " J", new Vector2(400, 45), Color.Black);
                spriteBatch.DrawString(font, Langage.getString(Langage.langueactuelle, 12) + currentPlayer.lifes, new Vector2(450, 75), Color.Black);
                spriteBatchMini2.DrawString(font, Langage.getString(Langage.langueactuelle, 42) + Player1Events.killplayer0.ToString(), new Vector2(2335, 275), Color.Black);
                spriteBatchMini2.DrawString(font, Langage.getString(Langage.langueactuelle, 10) + Player1Events.SurvivalLvL0.ToString(), new Vector2(2335, 315), Color.Black);
            }
        }

        private void DrawMessage()
        {
            if (msg != "")
                spriteBatch.DrawString(font, msg, new Vector2(400, 200), Color.White);
        }

        private void DrawInventory()
        {
            // spriteBatch.DrawString(font, players[hostPlayer].speed.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 250, GraphicsDevice.Viewport.Height - 100), Color.Black);
            spriteBatchMini2.Draw(HUDarmes, new Vector2(2300, 20), Color.White);
            spriteBatchMini2.DrawString(font, players[hostPlayer].currentWeapon.currentAmo + "/" + players[0].currentWeapon.munitions.ToString(), new Vector2(2335, 220), Color.Black);
            spriteBatchMini2.Draw(players[hostPlayer].currentWeapon.weaponSprite, new Vector2(2350, 60), Color.White);
            spriteBatchMini2.DrawString(font, players[hostPlayer].currentWeapon.weaponName, new Vector2(2335, 170), Color.Black);

            //if (players[hostPlayer].currentItem != null)
            //    spriteBatchMini2.DrawString(font, Langage.getString(Langage.langueactuelle, 43) + players[hostPlayer].currentItem.name, new Vector2(2335, 100), Color.Black);
            //for (int i = 0; i < players[hostPlayer].ItemList.Count; i++)
            //{
            //    //  spriteBatch.DrawString(font, players[hostPlayer].ItemList[i].name, new Vector2(GraphicsDevice.Viewport.X - 1000, GraphicsDevice.Viewport.Y - 400 + 23 * i), Color.Black);
            //}
        }

        private void DrawSoloScores()
        {
            //spriteBatch.DrawString(font, "D :" + players[hostPlayer].nbDeath.ToString(), new Vector2(GraphicsDevice.Viewport.Width - 900, GraphicsDevice.Viewport.Height - 60), Color.Black);
        }

        private void DrawPerso()
        {
            foreach (Player player in players)
            {
                if (player.currentWeapon.isOnMap)
                {
                    player.arm.Draw(spriteBatch, camera);
                }
                player.Draw(spriteBatch, camera);
                if (player.currentWeapon.isOnMap)
                {
                    player.currentWeapon.Draw(spriteBatch, camera, player.direction);
                }
            }
            foreach (Enemy enemy in Enemys)
            {
                enemy.Draw(spriteBatch, camera);
                enemy.currentWeapon.Draw(spriteBatch, camera, enemy.direction);
            }
        }

        public void DrawSpecial()
        {
            players[hostPlayer].currentWeapon.DrawSpecial(spriteBatch, camera.Xcurrent, camera.Ycurrent);
        }

        public void DrawWater()
        {
            for (int i = 0; i < mapWorld.waterlist.Count; i++)
            {
                spriteBatch.Draw(mapWorld.waterlist[i].image, DecaleXY(mapWorld.waterlist[i].position, camera), Color.White);
            }
        }

        private Vector2 DecaleXY(Vector2 pos, Camera camera)
        {
            pos.X -= camera.Xcurrent + camera.Xspecial;
            pos.Y -= camera.Ycurrent + camera.Yspecial;
            return pos;
        }
    }
}