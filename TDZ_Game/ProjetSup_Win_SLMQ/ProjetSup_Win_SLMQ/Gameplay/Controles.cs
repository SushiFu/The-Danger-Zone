using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public enum controle
    {
        Up,
        Left,
        Right,
        Down,
        Reload,
        Drop,
        Lacher,
        Action,
    }

    public class Controles
    {
        public GamePadState gamePad;
        public GamePadState oldgamePad;
        public MouseState mouse;
        public MouseState oldmouse;
        public KeyboardState keyboard;
        public KeyboardState oldkeyboard;
        public Vector2 cursorPosition;
        public Keys[] controleTab;
        public Buttons[] buttonTab;

        public Controles()
        {
            controleTab = new Keys[8];
            controleTab[0] = Keys.Z;
            controleTab[1] = Keys.Q;
            controleTab[2] = Keys.D;
            controleTab[3] = Keys.S;
            controleTab[4] = Keys.R;
            controleTab[5] = Keys.X;
            controleTab[6] = Keys.E;
            controleTab[7] = Keys.A;

            //
            buttonTab = new Buttons[9];
            buttonTab[0] = Buttons.LeftThumbstickUp;
            buttonTab[1] = Buttons.LeftThumbstickLeft;
            buttonTab[2] = Buttons.LeftThumbstickRight;
            buttonTab[3] = Buttons.Back;
            buttonTab[4] = Buttons.X;
            buttonTab[5] = Buttons.Y;
            buttonTab[6] = Buttons.A;
            buttonTab[7] = Buttons.B;
        }

        public void Update(GamePadState gamePad, MouseState mouse, KeyboardState keyboard)
        {
            oldgamePad = this.gamePad;
            oldkeyboard = this.keyboard;
            oldmouse = this.mouse;
            this.mouse = mouse;
            this.keyboard = keyboard;
            this.gamePad = gamePad;
        }

        #region Input

        #region click

        public bool Click()
        {
            return (mouse.LeftButton == ButtonState.Pressed && oldmouse.LeftButton == ButtonState.Released) || gamePad.Triggers.Right > 0 && !(oldgamePad.Triggers.Right > 0);
        }

        public bool scrollUp()
        {
            return (mouse.ScrollWheelValue - oldmouse.ScrollWheelValue > 0);
        }

        public bool scrollDown()
        {
            return (mouse.ScrollWheelValue - oldmouse.ScrollWheelValue < 0);
        }

        public bool StayClick()
        {
            return (mouse.LeftButton == ButtonState.Pressed || gamePad.Triggers.Right > 0);
        }

        public bool RightClick()
        {
            return (mouse.RightButton == ButtonState.Pressed && oldmouse.RightButton == ButtonState.Released) || gamePad.Triggers.Left > 0 && !(oldgamePad.Triggers.Left > 0);
        }

        public bool StayRightClick()
        {
            return (mouse.RightButton == ButtonState.Pressed || gamePad.Triggers.Left > 0);
        }

        public bool Score()
        {
            return (keyboard.IsKeyDown(Keys.Tab) || gamePad.Buttons.Back == ButtonState.Pressed);
        }

        #endregion click

        #region Enter

        public bool Enter()
        {
            return keyboard.IsKeyDown(Keys.Enter) && !oldkeyboard.IsKeyDown(Keys.Enter);
        }

        #endregion Enter

        #region left

        public bool Left()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Left]) && !(oldkeyboard.IsKeyDown(controleTab[(int)controle.Left])) || (gamePad.ThumbSticks.Left.X < 0 && !(oldgamePad.ThumbSticks.Left.X < 0)));
        }

        public bool StayLeft()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Left]) || gamePad.ThumbSticks.Left.X < 0);
        }

        #endregion left

        #region right

        public bool Right()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Right]) && !(oldkeyboard.IsKeyDown(controleTab[(int)controle.Right])) || (gamePad.ThumbSticks.Left.X > 0 && !(oldgamePad.ThumbSticks.Left.X > 0)));
        }

        public bool StayRight()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Right]) || gamePad.ThumbSticks.Left.X > 0);
        }

        #endregion right

        #region Up

        public bool Up()
        {
            return ((keyboard.IsKeyDown(controleTab[(int)controle.Up]) && !oldkeyboard.IsKeyDown(controleTab[(int)controle.Up])) || gamePad.Buttons.A == ButtonState.Pressed && oldgamePad.Buttons.A == ButtonState.Released);
        }

        public bool StayUp()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Up]) || (gamePad.ThumbSticks.Left.Y > 0));
        }

        public bool Down()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Down]) && !oldkeyboard.IsKeyDown(controleTab[(int)controle.Down]));
        }

        public bool StayDown()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Down]) || (gamePad.ThumbSticks.Left.Y < 0));
        }

        #endregion Up

        #region Reload

        public bool Reload()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Reload]) && !oldkeyboard.IsKeyDown(controleTab[(int)controle.Reload]) || gamePad.Buttons.X == ButtonState.Pressed && oldgamePad.Buttons.X == ButtonState.Released);
        }

        public bool StayReload()
        {
            return ((keyboard.IsKeyDown(controleTab[(int)controle.Reload]) || gamePad.Buttons.X == ButtonState.Pressed));
        }

        #endregion Reload

        #region Drop

        public bool Drop()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Drop]) && !oldkeyboard.IsKeyDown(controleTab[(int)controle.Drop]) || gamePad.Buttons.Y == ButtonState.Pressed && oldgamePad.Buttons.Y == ButtonState.Released);
        }

        public bool StayDrop()
        {
            return ((keyboard.IsKeyDown(controleTab[(int)controle.Drop])) || gamePad.Buttons.Y == ButtonState.Pressed);
        }

        #endregion Drop

        #region Lacher

        public bool Lacher()
        {
            return ((keyboard.IsKeyDown(controleTab[(int)controle.Lacher]) && !oldkeyboard.IsKeyDown(controleTab[(int)controle.Lacher]) || gamePad.Buttons.B == ButtonState.Pressed && oldgamePad.Buttons.B == ButtonState.Released));
        }

        public bool StayLacher()
        {
            return ((keyboard.IsKeyDown(controleTab[(int)controle.Lacher])) || gamePad.Buttons.B == ButtonState.Pressed);
        }

        #endregion Lacher

        #region Action

        public bool Action()
        {
            return (((keyboard.IsKeyDown(controleTab[(int)controle.Action]) && !(oldkeyboard.IsKeyDown(controleTab[(int)controle.Action]))) || gamePad.Buttons.A == ButtonState.Pressed && oldgamePad.Buttons.A == ButtonState.Released));
        }

        public bool StayAction()
        {
            return (keyboard.IsKeyDown(controleTab[(int)controle.Action]) || gamePad.Buttons.A == ButtonState.Pressed);
        }

        #endregion Action

        public bool Pause()
        {
            return (((keyboard.IsKeyDown(Keys.Escape) && !(oldkeyboard.IsKeyDown(Keys.Escape))) || gamePad.Buttons.Start == ButtonState.Pressed && oldgamePad.Buttons.Start == ButtonState.Released));
        }

        public bool God()
        {
            return (keyboard.IsKeyDown(Keys.G) && !(oldkeyboard.IsKeyDown(Keys.G)));
        }

        #endregion Input
    }
}