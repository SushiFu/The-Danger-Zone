using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Solo
    {
        private String[] mapspath;
        public String currentMap;
        public Player player;
        public bool levelUp;
        public int currentLevel;

        public Solo(int level, Player player)
        {
            player.currentLevel = level;
            levelUp = false;
            this.currentLevel = level;
            mapspath = Directory.GetFiles((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Solo/"));
            this.player = player;
            for (int i = 0; i < mapspath.Length; i++)
            {
                mapspath[i] = mapspath[i].Replace((Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Solo/"), "").Replace(".xml", "");
            }
            if (level < mapspath.Length)
                currentMap = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Solo/") + mapspath[level];
            else
            {
                currentLevel = 0;
                player.currentLevel = 0;
                level = 0;
                currentMap = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/TDZ/Map/Solo/") + mapspath[0];
            }
        }

        public void Update(Player player, Map map)
        {
            this.player = player;
            if (player.position.X >= (((map.width - 1) * 75) - player.size.X))
            {
                player.experience = player.experience + 541 * (1 + currentLevel);
                levelUp = true;
                currentLevel++;
                player.money += currentLevel * 1000;
                if (player.currentItem != null)
                {
                    if (player.currentItem.GetType() == typeof(Vehicule))
                        ((Vehicule)player.currentItem).quitVehicule(player, ((Vehicule)player.currentItem));
                }
            }
        }
    }
}