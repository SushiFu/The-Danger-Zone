using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class Water
    {
        public Texture2D image;
        public Vector2 position;

        public Water(Texture2D im, Vector2 pos)
        {
            this.image = im;
            this.position = pos;
        }
    }
}