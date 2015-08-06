using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Tile
    {
        public String texture { get; private set; }

        public int lifePoint;
        public int contentIndex;
        public bool draw;
        public bool bloque;
        public bool hitable;
        public bool alive;
        public objectType type;
        private Map map;

        public Vector2 position { get; private set; }

        public Tile(int lifePoint, bool draw, String texture, Vector2 position, bool bloque, bool hitable, objectType type, int contentIndex, Map map)
        {
            this.contentIndex = contentIndex;
            this.texture = texture;
            this.lifePoint = lifePoint;
            this.draw = draw;
            this.position = position;
            this.bloque = bloque;
            this.hitable = hitable;
            this.type = type;
            this.map = map;
            alive = true;
        }

        public void Hit(int dammage, ref List<Explosion> listExplose, List<ParticleEngine> particles, Sound sound, Map mapWorld, ContentManager Content, List<Vector2> explose)
        {
            if (hitable)
            {
                lifePoint -= dammage;
                if (lifePoint <= 0)
                {
                    bloque = false;
                    draw = false;
                    alive = false;
                    hitable = false;

                    if (type == objectType.explosif)
                    {
                        listExplose.Add(new Explosion(position, map, particles, sound));
                    }
                    else if (type == objectType.caisse)
                    {
                        switch (contentIndex)
                        {
                            case 2:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[0] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.right, new Vector2(30, 30), objectType.champi, 300));
                                break;

                            case 3:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[1] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.stop, new Vector2(30, 30), objectType.vie, 300));
                                break;

                            case 4:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[2] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.stop, new Vector2(30, 30), objectType.chargeur, 300));
                                break;

                            case 5:

                                Vehicule Hornet = new Vehicule(vehiculeType.Hornet, new Vector2(position.X, position.Y - 150), 2, 20, new Vector2(3, 3), Direction.stop, true, new Vector2(182, 120), TexturesGame.itemTab[0], 0);
                                Hornet.name = "Hornet";
                                Hornet.VehicleGun.Add(new Weapon(4000, "Others/Munitions/bullet", "AK47", 110, 0.1f, cadenceType.auto, 2500, 150, 150, 15, 120, Vector2.Zero, Vector2.Zero, "hornet_right", true, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 2, 0));
                                Hornet.VehicleGun[0].weaponName = "Hornet Gun";
                                Hornet.VehicleGun.Add(new Weapon(4000, "Others/Munitions/rpgammo22", "AK47", 300, 0.05f, cadenceType.semiAuto, 500, 6, 6, 8, 200, Vector2.Zero, Vector2.Zero, "hornet_right", true, 0, Content, SoundsName.rocket, tirTypes.explosive, false, 2, 0));
                                Hornet.VehicleGun[1].weaponName = "Hornet Rocket";
                                mapWorld.itemList.Add(Hornet);
                                break;

                            case 6:
                                Vehicule Banshee = new Vehicule(vehiculeType.Banshee, new Vector2(position.X, position.Y - 150), 1, 20, new Vector2(3, 2), Direction.stop, true, new Vector2(147, 117), TexturesGame.itemTab[1], 0);
                                Banshee.name = "Banshee";
                                Banshee.VehicleGun.Add(LoadWeapons.LoadBansheeBomb(Content));
                                Banshee.VehicleGun.Add(LoadWeapons.LoadBansheeGun(Content));
                                mapWorld.itemList.Add(Banshee);
                                break;

                            case 7:
                                listExplose.Add(new Explosion(position, map, particles, sound));

                                break;

                            case 8:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[3] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.right, new Vector2(30, 30), objectType.etoile, 300));
                                break;

                            case 9:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[4] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.right, new Vector2(30, 30), objectType.UnlimitedAmmo, 300));
                                break;

                            case 10:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[5] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.right, new Vector2(30, 30), objectType.attaqueUp, 300));
                                break;

                            case 11:
                                mapWorld.Items.Add(new Item(new Texture2D[1] { TexturesGame.bonusTab[6] }, new Vector2(position.X, position.Y - 40), new Vector2(0, 0), true, Direction.right, new Vector2(30, 30), objectType.defenseUp, 300));
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        mapWorld.particleEngine.Add(new ParticleEngine(7, position + new Vector2(38, 38), new Vector2(0, 1), 5, TexturesGame.test[1], 60, 38, 0, 38, 0, 0.5f, false));
                        this.type = objectType.nothing;
                    }
                }
            }
        }
    }
}