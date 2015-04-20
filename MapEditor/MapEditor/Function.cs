using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor
{
    public class Function
    {
        private Textures textures = new Textures();

        public int GrassTest(int[,] world, int x, int y, int maxX)
        {
            int result;
            int up;
            int left;
            int right;
            if (x == 0)
                left = 1;
            else
                left = world[y, x - 1];
            if (y == 0)
                up = 1;
            else
                up = world[y - 1, x];
            if (x == maxX - 1)
                right = 1;
            else
                right = world[y, x + 1];

            if (left != 0 && right != 0 && up != 0)
                result = 10;
            else if (left == 0 && right != 0 && up != 0)
                result = 12;
            else if (left != 0 && right == 0 && up != 0)
                result = 13;
            else if (left != 0 && right != 0 && up == 0)
                result = 11;
            else if (left == 0 && right == 0 && up != 0)
                result = 17;
            else if (left == 0 && right != 0 && up == 0)
                result = 15;
            else if (left != 0 && right == 0 && up == 0)
                result = 16;
            else
                result = 14;
            return result;
        }

        public Bitmap ToTexture(int n)
        {
            Bitmap result = new Bitmap("./Resources/Grille.png");
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

                case 24:
                    result = textures.sand;
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

                case 35:
                    result = textures.Lave;
                    break;

                case 36:
                    result = textures.fer;
                    break;

                case 37:
                    result = textures.eau;
                    break;

                case 38:
                    result = textures.echelle;
                    break;

                case 39:
                    result = textures.iron;
                    break;

                case 40:
                    result = textures.glace;
                    break;

                case 41:
                    result = textures.glace2;
                    break;

                case 43:
                    result = textures.DrapBleu;
                    break;

                case 44:
                    result = textures.DrapRouge;
                    break;

                case 50:
                    result = textures.BarreLR;
                    break;

                case 51:
                    result = textures.BarreRL;
                    break;

                case 52:
                    result = textures.BarreHB;
                    break;

                case 53:
                    result = textures.BarreBH;
                    break;

                case 54:
                    result = textures.Barril;
                    break;

                case 301:
                    result = textures.Caisse;
                    break;

                case 302:
                    result = textures.CaisseSpeed;
                    break;

                case 303:
                    result = textures.CaisseLife;
                    break;

                case 304:
                    result = textures.CaisseChargeur;
                    break;

                case 305:
                    result = textures.CaisseHornet;
                    break;

                case 306:
                    result = textures.CaisseBanshee;
                    break;

                case 307:
                    result = textures.CaisseExplosion;
                    break;

                case 308:
                    result = textures.CaisseEtoile;
                    break;

                case 309:
                    result = textures.CaisseAmmo;
                    break;

                case 310:
                    result = textures.CaisseAttaque;
                    break;

                case 311:
                    result = textures.CaisseBouclier;
                    break;

                default:
                    result = textures.dirt;
                    break;
            }
            return result;
        }

        public string ToString(int[,] array, int height, int width)
        {
            string result = height + "|" + width;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    result = result + array[y, x] + '|';
                }
            }
            return result;
        }

        public int[][] ToJagged(int[,] array)
        {
            int[][] result = new int[array.GetLength(0)][];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                result[i] = new int[array.GetLength(1)];
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    result[i][j] = array[i, j];
                }
            }

            return result;
        }

        public int[,] ToMatrice(int[][] array)
        {
            int[,] result = new int[array.Length, array[0].Length];
            for (int y = 0; y < array.Length; y++)
            {
                for (int x = 0; x < array[0].Length; x++)
                    result[y, x] = array[y][x];
            }
            return result;
        }
    }
}