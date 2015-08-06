using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    internal class Collisions
    {
        private Entity player;
        private Map map;
        private Sound sound;
        private Shot shots;
        private Vector2 pixHautDroit;
        private Vector2 pixHautGauche;
        private Vector2 pixMidDroit;
        private Vector2 pixMidGauche;
        private Vector2 pixBasDroit;
        private Vector2 pixBasGauche;
        private int marge = 1;
        private List<Tile> tileList;
        private List<Explosion> explosionList;
        private List<int> indexItem;
        private List<Item> itemList;
        private bool testInfinit;
        private List<Entity> entityList;
        private bool oUL;
        private bool dL;
        private bool sang;
        private int touchedPlatIndex;
        private List<Animate> animationList;

        //
        public bool haut, gauche, droite;

        public Collisions(Entity player, Map map, Sound sound, Shot shot, List<Item> itemList, List<Entity> entityList, List<Animate> animationList)
        {
            this.entityList = entityList;
            this.player = player;
            this.map = map;
            this.sound = sound;
            this.shots = shot;
            UpdatePixel();
            this.explosionList = map.explosionList;
            indexItem = new List<int>();
            this.itemList = itemList;
            this.animationList = animationList;
        }

        #region collision

        private int Ma(float num)
        {
            return ((int)(num - (num % 75)) / 75);
        }

        private void UpdatePixel()
        {
            tileList = new List<Tile>();
            pixHautGauche = new Vector2(player.position.X + marge, player.position.Y + marge);

            pixHautDroit = new Vector2(player.position.X + player.size.X - marge, player.position.Y + marge);

            pixMidDroit = new Vector2(player.position.X + player.size.X - marge, player.position.Y + player.size.Y / 2);

            pixMidGauche = new Vector2(player.position.X + marge, player.position.Y + player.size.Y / 2);

            pixBasDroit = new Vector2(player.position.X + player.size.X - marge, player.position.Y + player.size.Y - marge);

            pixBasGauche = new Vector2(player.position.X + marge, player.position.Y + player.size.Y - marge);
        }

        private bool TestPixel(Vector2 pixel)
        {
            if (pixel.X < 0 || pixel.X > ((map.width) * 75 - 30) || pixel.Y < 0 || pixel.Y > ((map.height) * 75 - 30))
                return true;
            else
                return (map.world[(int)Ma(pixel.Y), (int)Ma(pixel.X)].bloque);//try catch a remettre si ca replante ici
        }

        private bool TestPixelObjet(Vector2 pixel)
        {
            bool test = false;
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].id != player.id && entityList[i].GetType() != typeof(Item))
                {
                    test = test || (((pixel.Y >= entityList[i].position.Y) && (pixel.Y <= entityList[i].position.Y + entityList[i].size.Y) && (pixel.X >= entityList[i].position.X) && (pixel.X <= entityList[i].position.X + entityList[i].size.X)));
                    //vehicule ecrase
                }
            }
            return test;
        }

        private bool TestAllPixel()
        {
            bool test = false;
            test = TestPixel(pixBasDroit) || TestPixel(pixBasGauche) || TestPixel(pixHautDroit)
            || TestPixel(pixHautGauche) || TestPixel(pixMidDroit) || TestPixel(pixMidGauche);
            return test;
        }

        private bool TestAllPixelObjet()
        {
            if (player.GetType() != typeof(Item) && player.GetType() != typeof(Vehicule))
            {
                bool test = false;
                test = TestPixelObjet(pixBasDroit) || TestPixelObjet(pixBasGauche) || TestPixelObjet(pixHautDroit)
                || TestPixelObjet(pixHautGauche) || TestPixelObjet(pixMidDroit) || TestPixelObjet(pixMidGauche);
                return test;
            }
            else
                return false;
        }

        private void IsOnMapY(Vector2 positionInitial)
        {
            if (player.position.Y >= (((map.height) * 75 - 30) - player.size.Y))
            {
                if (testInfinit)
                {
                    player.pV = 0;
                    player.Die(map, sound);
                }
                player.position.Y = positionInitial.Y;
                player.speed.Y = 0;
                testInfinit = true;
            }
            if (player.position.Y < 0)
            {
                if (testInfinit)
                {
                    player.pV = 0;
                    player.Die(map, sound);
                }
                player.position.Y = positionInitial.Y;
                player.speed.Y = 0;
                testInfinit = true;
            }
        }

        private void IsOnMapX(Vector2 positionInitial)
        {
            if (player.position.X >= (((map.width - 1) * 75 + 30) - player.size.X))
            {
                if (testInfinit)
                {
                    player.pV = 0;
                    player.Die(map, sound);
                }
                player.position.X = positionInitial.X;
                player.speed.X = 0;
                testInfinit = true;
            }
            if (player.position.X < 0)
            {
                if (testInfinit)
                {
                    player.pV = 0;
                    player.Die(map, sound);
                }
                player.position.X = positionInitial.X;
                player.speed.X = 0;
                testInfinit = true;
            }
        }

        private void TestNum()
        {
            if (Math.Abs(player.speed.Y) > 70)
            {
                player.accelerate = 1;
                player.speed.Y = 0;
            }
            if (Math.Abs(player.speed.X) > 70)//les modif de accelerate font ce bug (IA)
            {
                player.speed.X = 0;
                player.accelerate = 1;
            }
        }

        private void Ymove(Vector2 positionInitial, ref bool fly)
        {
            testInfinit = false;
            bool valid = false;
            bool joueur = false;
            player.position.Y += player.speed.Y;
            UpdatePixel();

            //
            haut = TestPixel(pixHautDroit) || TestPixel(pixHautGauche);
            //
            IsOnMapY(positionInitial);
            //
            UpdatePixel();
            if (player.GetType() != typeof(Item))
            {
                while (TestAllPixelObjet())
                {
                    joueur = true;
                    if (player.speed.Y >= 0)
                        player.position.Y--;
                    else
                        player.position.Y++;

                    IsOnMapY(positionInitial);
                    UpdatePixel();
                }
            }

            UpdatePixel();
            testInfinit = false;
            while (TestAllPixel())
            {
                valid = true;
                if (player.speed.Y >= 0)
                    player.position.Y--;
                else
                    player.position.Y++;

                //

                UpdatePixel();
                IsOnMapY(positionInitial);
                //
            }
            testInfinit = false;
            if (valid || joueur)
            {
                player.speed.Y = 0;
                fly = true;
                valid = false;
            }
        }

        private void Xmove(Vector2 positionInitial)
        {
            bool valid = false;

            player.position.X += (player.speed.X);
            UpdatePixel();
            gauche = TestPixel(pixBasGauche) || TestPixel(pixMidGauche) || TestPixel(pixHautGauche);
            droite = TestPixel(pixBasDroit) || TestPixel(pixMidDroit) || TestPixel(pixHautDroit);
            //
            IsOnMapX(positionInitial);

            //

            UpdatePixel();
            if (player.GetType() != typeof(Item))
            {
                while (TestAllPixelObjet())
                {
                    if (player.speed.X >= 0)
                        player.position.X--;
                    else
                        player.position.X++;

                    IsOnMapX(positionInitial);
                    UpdatePixel();
                }
            }
            testInfinit = false;

            UpdatePixel();
            while (TestAllPixel())
            {
                valid = true;
                if (player.speed.X >= 0)
                    player.position.X--;
                else
                    player.position.X++;

                //

                UpdatePixel();
                IsOnMapX(positionInitial);
                //
            }
            testInfinit = false;
            if (valid)
                player.speed.X = 0;
            if (valid && (player.speed.Y > 0))
            {
                player.speed.Y = 1;
            }
        }

        public void DoMove(ref String msg)
        {
            TestNum();
            bool fly = false;

            if (player.GetType() != typeof(Item))
                detectObject(ref msg);
            player.objetTouched.Clear();
            Vector2 positionInitial = new Vector2(player.position.X, player.position.Y);
            Xmove(positionInitial);
            Ymove(positionInitial, ref fly);

            if (Math.Abs(player.position.X - positionInitial.X) > 70)
                player.position.X = positionInitial.X;
            if (Math.Abs(player.position.Y - positionInitial.Y) > 70)
                player.position.Y = positionInitial.Y;
        }

        #endregion collision

        #region detection des objet

        private void detectObject(ref String msg)
        {
            indexItem.Clear();
            player.position.Y += player.speed.Y;
            player.position.X += player.speed.X;
            if (player.position.Y >= (((map.height) * 75 - 30) - player.size.Y))
            {
                player.pV = 0;
                player.Die(map, sound);
            }

            #region remplire objettouched

            player.objetTouched.Add(map.world[((int)(((player.position.Y) - ((player.position.Y) % 75)) / 75)), ((int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75))].type);
            player.objetTouched.Add(map.world[(int)(((player.position.Y) - ((player.position.Y) % 75)) / 75), (int)(((player.position.X + 1) - ((player.position.X + 1) % 75)) / 75)].type);
            player.objetTouched.Add(map.world[(int)(((player.position.Y + player.size.Y) - ((player.position.Y + player.size.Y) % 75)) / 75), (int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75)].type);
            player.objetTouched.Add(map.world[(int)(((player.position.Y + player.size.Y) - ((player.position.Y + player.size.Y) % 75)) / 75), (int)(((player.position.X + 1) - ((player.position.X + 1) % 75)) / 75)].type);
            player.objetTouched.Add(map.world[(int)(((player.position.Y + (player.size.Y / 2)) - ((player.position.Y + (player.size.Y / 2)) % 75)) / 75), (int)(((player.position.X + 1) - ((player.position.X + 1) % 75)) / 75)].type);
            player.objetTouched.Add(map.world[(int)(((player.position.Y + (player.size.Y / 2)) - ((player.position.Y + (player.size.Y / 2)) % 75)) / 75), (int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75)].type);
            TestObject(new Vector2(player.position.X, player.position.Y), player);
            TestObject(new Vector2(player.position.X, player.position.Y + player.size.Y), player);
            TestObject(new Vector2(player.position.X + player.size.X, player.position.Y), player);
            TestObject(new Vector2(player.position.X + player.size.X, player.position.Y + player.size.Y), player);
            TestObject(new Vector2(player.position.X + player.size.X, player.position.Y + player.size.Y / 2), player);
            TestObject(new Vector2(player.position.X + player.size.X, player.position.Y + player.size.Y / 2), player);
            TestItem(new Vector2(player.position.X, player.position.Y), player);
            TestItem(new Vector2(player.position.X, player.position.Y + player.size.Y), player);
            TestItem(new Vector2(player.position.X + player.size.X, player.position.Y), player);
            TestItem(new Vector2(player.position.X + player.size.X, player.position.Y + player.size.Y), player);
            TestItem(new Vector2(player.position.X + player.size.X, player.position.Y + player.size.Y / 2), player);
            TestItem(new Vector2(player.position.X + player.size.X, player.position.Y + player.size.Y / 2), player);

            #endregion remplire objettouched

            UpdatePixel();
            bool bas = TestPixel(pixBasDroit) || TestPixel(pixBasGauche) || TestPixelObjet(pixBasDroit) || TestPixelObjet(pixBasGauche);

            player.position.Y -= player.speed.Y;
            player.position.X -= player.speed.X;

            player.vitesseMax = player.vitesseMaxInit / 2;
            if (player.objetTouched.Contains(objectType.spikes))
            {
                player.speed.X = -player.speed.X;
                player.speed.Y = -player.speed.Y;
                player.pV -= player.pVMax / 10;
            }
            if (player.objetTouched.Contains(objectType.explosion))
            {
                player.pV -= player.pVMax / 20;
            }

            if ((player.objetTouched.Contains(objectType.platformH) || player.objetTouched.Contains(objectType.platformV)) && !player.objetTouched.Contains(objectType.floor))
            {
                player.position.X += (map.platFormList[touchedPlatIndex - 200].speed.X);
                player.position.Y += (map.platFormList[touchedPlatIndex - 200].oldspeed.Y);
            }
            if (player.objetTouched.Contains(objectType.platformV) && player.objetTouched.Contains(objectType.floor) && bas)
            {
                player.pV = 0;
                player.Die(map, sound);
            }
            if (player.GetType() == typeof(Player))
            {
                if (player.objetTouched.Contains(objectType.champi))
                {
                    player.bonusList.Add(new Bonus((Player)player, BonusType.speedUp, 1000));
                    animationList.Add(new Animate(TexturesGame.speedUp, player.position, new Vector2(0, -0.05f), 500, 50, true));
                }
                if (player.objetTouched.Contains(objectType.etoile))
                {
                    player.bonusList.Add(new Bonus((Player)player, BonusType.Etoile, 700));
                }
                if (player.objetTouched.Contains(objectType.UnlimitedAmmo))
                {
                    player.bonusList.Add(new Bonus((Player)player, BonusType.unlimitedAmmo, 1500));
                }
                if (player.objetTouched.Contains(objectType.attaqueUp))
                {
                    player.bonusList.Add(new Bonus((Player)player, BonusType.attaqueUp, 1200));
                }
                if (player.objetTouched.Contains(objectType.defenseUp))
                {
                    player.bonusList.Add(new Bonus((Player)player, BonusType.defenseUp, 1200));
                }
                if (player.objetTouched.Contains(objectType.vie))
                {
                    ((Player)player).pV = ((Player)player).pVMax;
                    ((Player)player).lifes++;
                }
                if (player.objetTouched.Contains(objectType.chargeur))
                {
                    ((Player)player).currentWeapon.munitions += ((Player)player).currentWeapon.capacity;
                }
            }
            if (player.objetTouched.Contains(objectType.ice))
            {
                if (player.GetType() == typeof(Player))
                {
                    ((Player)player).climb = false;
                }
                if (player.GetType() == typeof(Enemy))
                {
                    ((Enemy)player).climb = false;
                }
                player.accelerate = 0.07f;
                player.decelerate = 0.05f;
                player.vitesseMax = player.vitesseMaxInit * 1.5f;
            }
            else if (player.objetTouched.Contains(objectType.echelle))
            {
                if (player.GetType() == typeof(Player))
                {
                    ((Player)player).njump = 0;
                    ((Player)player).climb = true;
                }
                if (player.GetType() == typeof(Enemy))
                {
                    ((Enemy)player).njump = 0;
                    ((Enemy)player).climb = true;
                }
                player.fallMax = 0;
                player.vitesseMax = player.vitesseMaxInit;
                player.accelerate = player.accelerateInit;
                player.decelerate = player.decelerateInit;
                player.jump = player.jumpInit;
                player.fall = player.fallInit;
            }
            else if (player.objetTouched.Contains(objectType.eau))
            {
                if (player.GetType() == typeof(Player))
                {
                    ((Player)player).njump = 0;
                    ((Player)player).climb = false;
                }
                if (player.GetType() == typeof(Enemy))
                {
                    ((Enemy)player).njump = 0;
                    ((Enemy)player).climb = false;
                }
                player.fallMax = player.fallMaxInit / 3.5f;
                player.jump = player.jumpInit / 4;
                player.vitesseMax = player.vitesseMaxInit / 2.5f;
                player.fall = 0.2f;
                player.accelerate = 0.2f;
                player.decelerate = 0.2f;
            }
            else
            {
                if (player.GetType() == typeof(Player))
                {
                    ((Player)player).climb = false;
                }
                if (player.GetType() == typeof(Enemy))
                {
                    ((Enemy)player).climb = false;
                }
                player.vitesseMax = player.vitesseMaxInit;
                player.accelerate = player.accelerateInit;
                player.decelerate = player.decelerateInit;
                player.jump = player.jumpInit;
                player.fall = player.fallInit;
                player.fallMax = player.fallMaxInit;
            }

            if (map.world[0, ((int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75))].type == objectType.caseTexte && player.GetType() != typeof(Enemy))
            {
                msg = map.worldTextes[map.world[0, ((int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75))].contentIndex];
            }
            if (map.world[0, ((int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75))].type == objectType.caseMusic)
            {
                //TODO: musique a jouer
                //SoundEffect sonAJouer = sound.sounds[map.world[0, ((int)(((player.position.X + player.size.X - 1) - ((player.position.X + player.size.X - 1) % 75)) / 75))].contentIndex];
            }
            if (bas)
            {
                if (player.GetType() == typeof(Player))
                {
                    ((Player)player).njump = 0;
                }
                if (player.GetType() == typeof(Enemy))
                {
                    ((Enemy)player).njump = 0;
                }
            }
        }

        public void TestObject(Vector2 pixel, Entity player)
        {
            for (int i = 0; i < entityList.Count; i++)
            {
                if (((pixel.Y >= entityList[i].position.Y) && (pixel.Y <= entityList[i].position.Y + entityList[i].size.Y) && (pixel.X >= entityList[i].position.X) && (pixel.X <= entityList[i].position.X + entityList[i].size.X)))
                {
                    player.objetTouched.Add(entityList[i].type);
                    if (entityList[i].type == objectType.platformH || entityList[i].type == objectType.platformV)
                        touchedPlatIndex = entityList[i].id;
                }
            }
            for (int i = 0; i < map.Items.Count; i++)
            {
                if (((pixel.Y >= map.Items[i].position.Y) && (pixel.Y <= map.Items[i].position.Y + map.Items[i].size.Y + 5) && (pixel.X >= map.Items[i].position.X) && (pixel.X <= map.Items[i].position.X + map.Items[i].size.X)))
                {
                    player.objetTouched.Add(map.Items[i].type);
                    if (player.GetType() == typeof(Player))
                    {
                        if (map.Items[i].type == objectType.champi)
                        {
                            map.Items.RemoveAt(i);
                        }
                        else if (map.Items[i].type == objectType.vie)
                        {
                            map.Items.RemoveAt(i);
                        }
                        else if (map.Items[i].type == objectType.chargeur)
                        {
                            map.Items.RemoveAt(i);
                        }
                        else if (map.Items[i].type == objectType.etoile)
                        {
                            map.Items.RemoveAt(i);
                        }
                        else if (map.Items[i].type == objectType.attaqueUp)
                        {
                            map.Items.RemoveAt(i);
                        }
                        else if (map.Items[i].type == objectType.defenseUp)
                        {
                            map.Items.RemoveAt(i);
                        }
                        else if (map.Items[i].type == objectType.UnlimitedAmmo)
                        {
                            map.Items.RemoveAt(i);
                        }
                    }
                }
            }
            for (int i = 0; i < explosionList.Count; i++)
            {
                if (((pixel.Y >= explosionList[i].position.Y) && (pixel.Y <= explosionList[i].position.Y + explosionList[i].size.Y) && (pixel.X >= explosionList[i].position.X) && (pixel.X <= explosionList[i].position.X + explosionList[i].size.X)))
                {
                    player.objetTouched.Add(objectType.explosion);
                }
            }
        }

        public void TestItem(Vector2 pos, Entity player)
        {
            if (player.GetType() == typeof(Player))
            {
                ((Player)player).nearitem = new List<Item>();
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (((pos.Y >= itemList[i].position.Y) && (pos.Y <= itemList[i].position.Y + itemList[i].size.Y) && (pos.X >= itemList[i].position.X) && (pos.X <= itemList[i].position.X + itemList[i].size.X)))
                    {
                        ((Player)player).nearitem.Add(itemList[i]);
                    }
                }
            }
        }

        #endregion detection des objet

        #region tirs

        public void IsShooted(Entity player, Shot shot, Sound sound, List<ParticleEngine> particles)
        {
            for (int i = 0; i < shot.otherTirList.Count; i++)
            {
                if ((player.position.Y + player.size.Y >= shot.otherTirList[i].position.Y + 5) && (player.position.Y <= shot.otherTirList[i].position.Y + 5) && (player.position.X + player.size.X >= shot.otherTirList[i].position.X + 5) && (player.position.X <= shot.otherTirList[i].position.X + 5))
                {
                    player.pV -= (int)(shot.otherTirList[i].damage * player.defense);/// player.defense);
                    map.particleEngine.Add(new ParticleEngine(2, shot.otherTirList[i].position, shot.otherTirList[i].vitesse * 0.70f, 5, TexturesGame.test[0], 2000, 0, 5, 0, 5, 0.5f, false));
                    /*if (shot.otherTirList[i].type==tirTypes.explosive)
                    {
                        Explosion explode = new Explosion(shot.otherTirList[i].position, map, particles, sound);
                    }*/
                }
            }

            player.Die(map, sound);
        }

        public void TestTire(Vector2 pos, Vector2 size, Map map)
        {
            pos.X = (int)pos.X + 5;
            pos.Y = (int)pos.Y + 5;
            DownLeft(pos, size, map);
            oUL = ODownLeft(new Vector2(pos.X, pos.Y));
        }

        public bool ODownLeft(Vector2 pixel)
        {
            bool test = false;
            bool test2;
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].id != player.id)
                {
                    test2 = (((pixel.Y >= entityList[i].position.Y) && (pixel.Y <= entityList[i].position.Y + entityList[i].size.Y) && (pixel.X >= entityList[i].position.X) && (pixel.X <= entityList[i].position.X + entityList[i].size.X)));
                    test = test || test2;
                    if (entityList[i].GetType() == typeof(Player) && test2)
                    {
                        sang = true;
                        break;
                    }
                }
            }
            return test;
        }

        public void DownLeft(Vector2 pos, Vector2 size, Map map)
        {
            if (map.world[(int)(((pos.Y + size.Y) - ((pos.Y + size.Y) % 75)) / 75), (int)(((pos.X + 1) - ((pos.X + 1) % 75)) / 75)].bloque)
                dL = true;
            else
                dL = false;
        }

        public void ValideTire(Map map, Shot shot, List<Vector3> destruct, List<ParticleEngine> particles, ContentManager Content, List<Vector2> explosereseau)
        {
            explosereseau.Clear();
            Vector2 size = new Vector2(0, 0);

            Vector2 coef = new Vector2(5, 5);

            for (int i = 0; i < shot.tirList.Count; i++)
            {
                sang = false;
                if (shot.tirList[i].position.X > 0 && shot.tirList[i].position.X < (map.width - 1) * 75 + 10 && shot.tirList[i].position.Y > 0 && shot.tirList[i].position.Y < (map.height - 1) * 75 + 10)
                {
                    TestTire(shot.tirList[i].position, size, map);

                    if (dL || oUL)
                    {
                        if (shot.tirList[i].type == tirTypes.explosive)
                        {
                            explosionList.Add(new Explosion(shot.tirList[i].position, map, particles, sound));
                            explosereseau.Add(shot.tirList[i].position);
                        }
                        else
                        {
                            map.world[((int)(((shot.tirList[i].position.Y + coef.Y) - ((shot.tirList[i].position.Y + coef.Y) % 75)) / 75)), ((int)(((shot.tirList[i].position.X + coef.X - ((shot.tirList[i].position.X + coef.X) % 75)) / 75)))].Hit(shot.tirList[i].damage, ref  explosionList, particles, sound, map, Content, explosereseau); // where u give damages
                            if (shot.tirList[i].type != tirTypes.none)
                            {
                                animationList.Add(new Animate(TexturesGame.ImpactTab, shot.tirList[i].position - new Vector2(20, 20), new Vector2(0, 0), 25, 5, false));
                            }
                            destruct.Add(new Vector3(((int)(((shot.tirList[i].position.X + coef.X) - ((shot.tirList[i].position.X + coef.X) % 75)) / 75)), ((int)(((shot.tirList[i].position.Y + coef.Y) - ((shot.tirList[i].position.Y + coef.Y) % 75)) / 75)), shot.tirList[i].damage));
                        }
                        if (sang)
                        {
                            map.particleEngine.Add(new ParticleEngine(2, shot.tirList[i].position, shot.tirList[i].vitesse * 0.70f, 5, TexturesGame.test[0], 2000, 0, 5, 0, 5, 0.5f, false));
                        }
                        shot.tirList.RemoveAt(i);
                    }
                }
                else
                {
                    shot.tirList.RemoveAt(i);
                }
            }

            if (player.GetType() == typeof(Player))
            {
                for (int i = 0; i < explosionList.Count; i++)
                {
                    for (int j = 0; j < explosionList[i].touchedTile.Count; j++)
                    {
                        if (map.world[(int)explosionList[i].touchedTile[j].Y, (int)explosionList[i].touchedTile[j].X].alive)
                        {
                            map.world[(int)explosionList[i].touchedTile[j].Y, (int)explosionList[i].touchedTile[j].X].Hit(40, ref explosionList, particles, sound, map, Content, explosereseau);
                            destruct.Add(new Vector3(explosionList[i].touchedTile[j].X, explosionList[i].touchedTile[j].Y, 40));
                        }
                    }
                }
            }
        }

        #endregion tirs
    }
}