#region Using Statements

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteBatch spriteBatchMini;

        public GameMain()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            this.IsMouseVisible = false;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();
            Window.AllowUserResizing = false;
            Window.Title = "The Danger Zone";
            //Window.Position = new Point(0, 0);
        }

        protected override void Initialize()
        {
            gameState = GameState.initialize;
            base.Initialize();
        }

        protected override void UnloadContent()
        {
        }
    }

    public enum GameState
    {
        initialize,
        menu,
        multi,
        solo,
        creation,
        editor,
        scenario,
        pause,
        multiPause,
        gameOver,
        exit
    }
}