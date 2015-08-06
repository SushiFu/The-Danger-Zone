using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ProjetSup_Win_SLMQ
{
    public class Map
    {
        public List<PlatForm> platFormList;
        public List<Vector2> IASpawnList;
        public List<Water> waterlist;
        public List<Item> itemList;
        public List<Explosion> explosionList;
        public List<Item> Items = new List<Item>();
        public String[] worldTextes;
        public Tile[,] world;
        public Vector2 spawnPoint = new Vector2(1, 1);
        public Vector2 spawnPoint2 = new Vector2(1, 1);
        public Vector2 drapeau1 = new Vector2(1, 1);
        public Vector2 drapeau2 = new Vector2(1, 1);
        public int height;
        public int width;
        private int n_texture;
        private Vector2 position;
        private Tile empty;
        public List<ParticleEngine> particleEngine;
        private Loading loading = new Loading();
        public ContentManager content;

        public void Load(String filename, ContentManager Content)
        {
            waterlist = new List<Water>();
            this.content = Content;
            Texture2D eau = Tools.LoadTexture("SpriteTexture/Eau", Content);
            particleEngine = new List<ParticleEngine>();
            XmlSerializer serializer = new XmlSerializer(typeof(Serialize));
            FileStream fs = new FileStream(filename, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);
            Serialize obj = (Serialize)serializer.Deserialize(reader);
            int[][] intMap = obj.world;
            itemList = new List<Item>();
            worldTextes = obj.texte;
            fs.Close();
            platFormList = new List<PlatForm>();
            IASpawnList = new List<Vector2>();
            explosionList = new List<Explosion>();
            height = intMap.Length;
            width = intMap[0].Length;
            world = new Tile[height, width];
            empty = new Tile(0, false, "SpriteTexture/Others/Empty", position, false, false, objectType.nothing, 0, this);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    n_texture = intMap[y][x];
                    position = new Vector2(x * 75, y * 75);
                    if (n_texture == 0)
                        world[y, x] = empty;
                    else if (n_texture == 1)
                    {
                        spawnPoint.X = x * 75;
                        spawnPoint.Y = y * 75;
                        world[y, x] = new Tile(10, true, loading.ToTexture(n_texture), position, false, false, objectType.nothing, 0, this);
                    }
                    else if (n_texture == 2)
                    {
                        world[y, x] = empty;
                        IASpawnList.Add(new Vector2(x * 75, y * 75));
                    }
                    else if (n_texture == 3)
                    {
                        spawnPoint2.X = x * 75;
                        spawnPoint2.Y = y * 75;
                        world[y, x] = new Tile(10, true, loading.ToTexture(n_texture), position, false, false, objectType.nothing, 0, this);
                    }
                    else if ((n_texture - n_texture % 10) == 10)
                    {
                        world[y, x] = new Tile(100, true, loading.ToTexture(n_texture), position, true, true, objectType.floor, 0, this);
                    }
                    else if (n_texture == 35)
                    {
                        world[y, x] = new Tile(1500, true, "SpriteTexture/lave", position, true, false, objectType.spikes, 0, this);
                        Random rn = new Random();
                        double num = rn.NextDouble();
                        if (!world[y - 1, x].bloque)
                            particleEngine.Add(new ParticleEngine((float)(num / 2.5), position + new Vector2(37, -10), new Vector2(0, -5), 5, TexturesGame.test[0], 100, 35, 10, 0, 1, 0.5f, true));
                    }
                    else if (n_texture == 36)
                    {
                        world[y, x] = new Tile(3500, true, "SpriteTexture/fer", position, true, true, objectType.floor, 0, this);
                    }
                    else if (n_texture == 37)
                    {
                        world[y, x] = new Tile(2500, true, "SpriteTexture/Eau", position, false, false, objectType.eau, 0, this);
                        waterlist.Add(new Water(eau, position));
                    }
                    else if (n_texture == 38)
                    {
                        world[y, x] = new Tile(1500, true, "SpriteTexture/Echelle", position, false, false, objectType.echelle, 0, this);
                    }
                    else if (n_texture == 39)
                    {
                        world[y, x] = new Tile(1500, true, "SpriteTexture/iron", position, true, false, objectType.floor, 0, this);
                    }
                    else if (n_texture - n_texture % 10 == 30)
                        world[y, x] = new Tile(200, true, loading.ToTexture(n_texture), position, true, false, objectType.spikes, 0, this);
                    else if (n_texture == 40 || n_texture == 41)
                        world[y, x] = new Tile(1, true, loading.ToTexture(n_texture), position, true, true, objectType.ice, 0, this);
                    else if (n_texture == 43)
                    {
                        drapeau1 = position;
                        world[y, x] = empty;
                    }
                    else if (n_texture == 44)
                    {
                        drapeau2 = position;
                        world[y, x] = empty;
                    }
                    else if (n_texture == 50)
                    {
                        world[y, x] = empty;
                        platFormList.Add(new PlatForm(position, new Vector2(position.X + 350, position.Y), new Vector2(5, 0), objectType.platformH, 200 + platFormList.Count));
                    }
                    else if (n_texture == 51)
                    {
                        world[y, x] = empty;
                        platFormList.Add(new PlatForm(new Vector2(position.X + 350, position.Y), position, new Vector2(-5, 0), objectType.platformH, 200 + platFormList.Count));
                    }
                    else if (n_texture == 52)
                    {
                        world[y, x] = empty;
                        platFormList.Add(new PlatForm(new Vector2(position.X, position.Y + 150), new Vector2(position.X, position.Y + 525), new Vector2(0, 3), objectType.platformV, 200 + platFormList.Count));
                    }
                    else if (n_texture == 53)
                    {
                        world[y, x] = empty;
                        platFormList.Add(new PlatForm(new Vector2(position.X, position.Y + 525), new Vector2(position.X, position.Y + 150), new Vector2(0, -3), objectType.platformV, 200 + platFormList.Count));
                    }
                    else if (n_texture == 54)
                    {
                        world[y, x] = new Tile(1, true, loading.ToTexture(n_texture), position, true, true, objectType.explosif, 0, this);
                    }
                    else if ((n_texture - n_texture % 100) == 100)
                    {
                        world[y, x] = new Tile(2, false, loading.ToTexture(0), position, false, false, objectType.caseTexte, n_texture % 100, this);
                    }
                    else if ((n_texture - n_texture % 100) == 200)
                    {
                        world[y, x] = new Tile(2, false, loading.ToTexture(0), position, false, false, objectType.caseMusic, n_texture % 200, this);
                    }
                    else if ((n_texture - n_texture % 100) == 300)
                    {
                        world[y, x] = new Tile(2, true, "SpriteTexture/Caisse", position, true, true, objectType.caisse, n_texture % 300, this);
                    }
                    else
                        world[y, x] = new Tile(100, true, loading.ToTexture(n_texture), position, true, true, objectType.floor, 0, this);
                }
            }
        }

        public void Update()
        {
            if (platFormList != null)
            {
                for (int i = 0; i < platFormList.Count; i++)
                {
                    platFormList[i].Update();
                }
            }
        }
    }
}