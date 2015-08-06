using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    internal class Mitralleuse : Weapon
    {
        public Mitralleuse(Vector2 position, Texture2D texture)
            : base(texture, 10, 0.5, 100, cadenceType.auto, 1, 100, position, new Vector2(0, 0), false)
        {
        }
    }
}