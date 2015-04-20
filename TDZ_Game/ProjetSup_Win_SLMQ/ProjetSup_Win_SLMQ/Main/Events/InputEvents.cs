using System;
using Microsoft.Xna.Framework.Input;

namespace ProjetSup_Win_SLMQ
{
    public static class InputEvents
    {
        public static void UpdateText(Controles controles, ref string text)
        {
            if (!SpecialChar(controles.keyboard, controles.oldkeyboard, ref text))
            {
                #region AlphaKeys
                Keys[] keys = controles.keyboard.GetPressedKeys();
                Keys[] oldkeys = controles.oldkeyboard.GetPressedKeys();

                if (keys.Length == 1 && oldkeys.Length == 0 && IsAlphaKey(keys[0]))
                {
                    text += keys[0].ToString().ToLower();
                }
                else if (keys.Length == 2 && oldkeys.Length == 1)
                {
                    bool maj = IsMajDown(controles.keyboard);
                    foreach (Keys key in keys)
                    {
                        if (maj && IsAlphaKey(key))
                        {
                            text += key.ToString().ToUpper();
                        }
                    }
                }
                #endregion
            }
        }

        private static bool SpecialChar(KeyboardState keyboardState, KeyboardState oldKeyboardState, ref String text)
        {
            if (keyboardState.IsKeyDown(Keys.Back) && !oldKeyboardState.IsKeyDown(Keys.Back) && text.Length > 0)
            {
                text = text.Remove(text.Length - 1);
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.OemComma) && !oldKeyboardState.IsKeyDown(Keys.OemComma))
            {
                text += ".";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.OemPeriod) && !oldKeyboardState.IsKeyDown(Keys.OemPeriod))
            {
                text += ".";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad0) && !oldKeyboardState.IsKeyDown(Keys.NumPad0))
            {
                text += "0";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad1) && !oldKeyboardState.IsKeyDown(Keys.NumPad1))
            {
                text += "1";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad2) && !oldKeyboardState.IsKeyDown(Keys.NumPad2))
            {
                text += "2";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad3) && !oldKeyboardState.IsKeyDown(Keys.NumPad3))
            {
                text += "3";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad4) && !oldKeyboardState.IsKeyDown(Keys.NumPad4))
            {
                text += "4";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad5) && !oldKeyboardState.IsKeyDown(Keys.NumPad5))
            {
                text += "5";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad6) && !oldKeyboardState.IsKeyDown(Keys.NumPad6))
            {
                text += "6";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad7) && !oldKeyboardState.IsKeyDown(Keys.NumPad7))
            {
                text += "7";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad8) && !oldKeyboardState.IsKeyDown(Keys.NumPad8))
            {
                text += "8";
                return true;
            }
            else if (keyboardState.IsKeyDown(Keys.NumPad9) && !oldKeyboardState.IsKeyDown(Keys.NumPad9))
            {
                text += "9";
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsMajDown(KeyboardState ks)
        {
            if (ks.IsKeyDown(Keys.LeftShift) || ks.IsKeyDown(Keys.RightShift))
            {
                return true;
            }
            return false;
        }

        public static bool IsAlphaKey(Keys key)
        {
            foreach (Keys k in authorizedKeys)
            {
                if (k == key)
                {
                    return true;
                }
            }
            return false;
        }

        private static Keys[] authorizedKeys = new Keys[]
        {
            Keys.A,
            Keys.Z,
            Keys.E,
            Keys.R,
            Keys.T,
            Keys.Y,
            Keys.U,
            Keys.I,
            Keys.O,
            Keys.P,
            Keys.Q,
            Keys.S,
            Keys.D,
            Keys.F,
            Keys.G,
            Keys.H,
            Keys.J,
            Keys.K,
            Keys.L,
            Keys.M,
            Keys.W,
            Keys.X,
            Keys.C,
            Keys.V,
            Keys.B,
            Keys.N,
        };
    }
}

