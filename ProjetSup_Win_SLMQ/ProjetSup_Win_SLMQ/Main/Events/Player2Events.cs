//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Input;
//using System;
//
//namespace ProjetSup_Win_SLMQ
//{
//    public class Player2Events
//    {
//        public static void CheckEvents(KeyboardState keyboardState, KeyboardState oldKeyboardState, Entity player, Map map, ObjetDyna[] obj, int host)
//        {
//            //Collisions collision;
//
//            //KeyEvents(player, keyboardState, oldKeyboardState, collision, map);
//
//            //collision.ValidateMove(player, obj, host, map);
//        }
//
//        private static void KeyEvents(Entity player, KeyboardState keyboardState, KeyboardState oldKeyboardState, Collisions collision, Map map)
//        {
//            if (keyboardState.IsKeyDown(Keys.D))
//            {
//                Physics.MoveRight(player);
//                player.activeSprite = AnimDir.stayRight;
//            }
//            else if (keyboardState.IsKeyDown(Keys.Q))
//            {
//                Physics.MoveLeft(player);
//                player.activeSprite = AnimDir.stayLeft;
//            }
//            else
//            {
//                Physics.Deccelarate(player);
//            }
//            if (keyboardState.IsKeyDown(Keys.Z) && !oldKeyboardState.IsKeyDown(Keys.Z))
//            {
//                Physics.Jump(player);
//            }
//        }
//
//        private static void AllTimeEvents(Entity player)
//        {
//            Physics.Fall(player);
//        }
//    }
//}