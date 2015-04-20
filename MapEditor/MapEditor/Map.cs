using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor
{
    public class Map
    {
        public int[,] map { get; private set; }

        public Map(int[,] array)
        {
            map = new int[array.GetLength(0), array.GetLength(1)];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    map[i, j] = array[i, j];
                }
            }
        }
    }
}