using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public enum FlagsType
    {
        red = 0,
        blue = 2
    }

    public class Flag: Item
    {
        public Vector2 origin{ get; private set; }

        public FlagsType flagType;
        public bool isCaptured;

        public Flag(Vector2 position, ContentManager Content, FlagsType type)
            : base(TexturesGame.LoadFlags(Content), position, Vector2.Zero, true, Direction.left, new Vector2(TexturesGame.flags[0].Width, TexturesGame.flags[0].Height), objectType.flag, 1)
        {
            origin = position;
            isCaptured = false;
            this.flagType = type;
        }

        public bool Update(Player player, Map map)
        {
            if (player.nearitem.Contains(this) && player.currenTeam != flagType)
            {
                player.ItemList.Add(this);
                map.itemList.Remove(this);
                isCaptured = true;
                isOnMap = false;
            }

            if (player.nearitem.Contains(this) && player.currenTeam == flagType)
            {
                this.position = origin;
            }

            if (player.ItemList.Contains(this))
            {
                position = new Vector2(player.position.X + player.image[0].Width / 2, player.position.Y - 30);
                foreach (Item item in player.nearitem)
                {
                    if (item.type == objectType.flag && ((Flag)item).position == ((Flag)item).origin)
                    {
                        this.position = origin;
                        map.itemList.Add(this);
                        player.ItemList.Remove(this);
                        isOnMap = true;
                        isCaptured = false;
                        return true;
                    }
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            if (isCaptured)
            {
                spriteBatch.Draw(image[(int)flagType + 1], DecaleXY(position, camera), Color.White);
            }
            else
            {
                spriteBatch.Draw(image[(int)flagType], DecaleXY(position, camera), Color.White);
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

