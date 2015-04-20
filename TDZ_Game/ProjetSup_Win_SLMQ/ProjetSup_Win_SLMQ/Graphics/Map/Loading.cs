using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    internal class Loading
    {
        private Textures textures = new Textures();

        public String ToTexture(int n)
        {
            String result;
            switch (n)
            {
                case 0:
                    result = textures.empty;
                    break;

                case 1:
                    result = textures.spawn;
                    break;

                case 3:
                    result = textures.spawn2;
                    break;

                case 10:
                    result = textures.dirt;
                    break;

                case 11:
                    result = textures.norm;
                    break;

                case 12:
                    result = textures.left;
                    break;

                case 13:
                    result = textures.right;
                    break;

                case 14:
                    result = textures.up;
                    break;

                case 15:
                    result = textures.upLeft;
                    break;

                case 16:
                    result = textures.upRight;
                    break;

                case 17:
                    result = textures.rl;
                    break;

                case 20:
                    result = textures.dalle;
                    break;

                case 21:
                    result = textures.pierre;
                    break;

                case 22:
                    result = textures.brique;
                    break;

                case 30:
                    result = textures.SpUp;
                    break;

                case 31:
                    result = textures.SpDown;
                    break;

                case 32:
                    result = textures.SpRight;
                    break;

                case 33:
                    result = textures.SpLeft;
                    break;

                case 40:
                    result = textures.glace;
                    break;

                case 41:
                    result = textures.glace2;
                    break;

                case 54:
                    result = textures.baril;
                    break;

                default:
                    result = textures.dirt;
                    break;
            }
            return result;
        }
    }
}