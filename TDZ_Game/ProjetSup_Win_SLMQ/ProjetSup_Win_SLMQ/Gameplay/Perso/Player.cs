using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public class Player : Perso
    {
        public int oldkill = 0;
        public int accuracy;
        public string name = "InsertName";
        public int experience;
        public int niveau;
        public int pointRestants;
        public int attaquePoint;
        public int defensePoint;
        public int dexteritePoint;
        public int vitessePoint;
        public bool newChar;
        public int money;
        public int currentLevel;
        public Arm arm;
        public GraphicsDevice graphics;
        public bool God = false;
        public FlagsType currenTeam;
        public int spritePerso;
        public bool scoreKey;
        public bool isDie;

        public Player(Vector2 persoPos, Texture2D[] persoImage, int playerNum, GraphicsDevice graphics, Weapon persoWeapon, bool revive, FlagsType team)
            : base(persoImage, persoPos, playerNum, persoWeapon, objectType.player, revive, 5)
        {
            this.pV = 1000;
            scoreKey = false;
            currentLevel = 0;
            money = 1000;
            this.pVMax = this.pV;
            this.attaque = 0.1f;
            this.defense = 2f;
            accuracy = 20;
            base.accelerate = 1;
            base.decelerate = 2;
            base.fall = 1f;
            base.jump = 20;
            base.vitesseMax = 10;
            base.fallMax = 20;
            newChar = true;
            niveau = 1;
            pointRestants = 10;
            currenTeam = team;
            spritePerso = 0;
            //
            isDie = false;
            base.accelerateInit = accelerate;
            base.decelerateInit = decelerate;
            base.fallInit = fall;
            base.jumpInit = jump;
            base.vitesseMaxInit = vitesseMax;
            base.fallMaxInit = fallMax;
            this.graphics = graphics;
            arm = new Arm(graphics);
        }

        public void Shoot(Controles controles, Camera camera, Shot shots, Sound sound, List<Animate> animationList, List<ParticleEngine> particle)
        {
            Random rnd = new Random();
            currentWeapon.animationn();
            Vector2 dir = Vector2.Normalize(new Vector2(controles.cursorPosition.X - currentWeapon.position.X + camera.Xcurrent + camera.Xspecial, controles.cursorPosition.Y - currentWeapon.position.Y + camera.Ycurrent + camera.Yspecial));
            Vector2 oriTir = currentWeapon.position + (dir * currentWeapon.image[0].Width);
            if (direction == Direction.left)
            {
                oriTir.X += 15;
            }
            else
            {
                oriTir.X -= 15;
            }

            double angle = Math.Atan2(controles.cursorPosition.Y + camera.Ycurrent + camera.Yspecial - (currentWeapon.position + (dir * currentWeapon.image[0].Width / 2)).Y, controles.cursorPosition.X + camera.Xcurrent + camera.Xspecial - (currentWeapon.position + (dir * currentWeapon.image[0].Width / 2)).X);
            currentWeapon.Update(this, angle, controles);

            Vector2 destiArm = currentWeapon.position + (dir * currentWeapon.image[0].Width / 2);
            double armangle = Math.Atan2(destiArm.Y - arm.position.Y,
                                  destiArm.X - arm.position.X);

            arm.Update(this, armangle, dir);

            if (currentWeapon.weaponType == cadenceType.none && controles.Click())
            {
                shots.tirList.Add(new Tirs(currentWeapon.ammoTexture, (int)(currentWeapon.damage * attaque), oriTir, dir * 40, currentWeapon.range, id, angle, currentWeapon.nTexture, currentWeapon.tirtype));
                sound.Play(currentWeapon.shotSound);
                if (currentWeapon.count2 < 7 && direction == Direction.right)
                {
                    currentWeapon.count2 = 2;
                }
                if (currentWeapon.count2 < 7 && direction == Direction.left)
                {
                    currentWeapon.count2 = 3;
                }
            }
            if (currentWeapon.weaponType == cadenceType.semiAuto)
            {
                currentWeapon.autoTirs += currentWeapon.cadence;
                if (currentWeapon.autoTirs > 1 && controles.Click() && currentWeapon.currentAmo > 0)
                {
                    int numX = rnd.Next(-currentWeapon.accuracy - accuracy, currentWeapon.accuracy + accuracy);
                    int numY = rnd.Next(-currentWeapon.accuracy - accuracy, currentWeapon.accuracy + accuracy);
                    dir = Vector2.Normalize(new Vector2(controles.cursorPosition.X - currentWeapon.position.X + camera.Xcurrent + camera.Xspecial + numX, controles.cursorPosition.Y - currentWeapon.position.Y + camera.Ycurrent + camera.Yspecial + numY));
                    shots.tirList.Add(new Tirs(currentWeapon.ammoTexture, (int)(currentWeapon.damage * attaque), oriTir, dir * 40, currentWeapon.range, id, angle, currentWeapon.nTexture, currentWeapon.tirtype));
                    sound.Play(currentWeapon.shotSound);

                    currentWeapon.currentAmo--;
                    currentWeapon.autoTirs = 0;
                }
            }
            else if (currentWeapon.weaponType == cadenceType.auto && controles.StayClick() && currentWeapon.currentAmo > 0)
            {
                currentWeapon.autoTirs += currentWeapon.cadence;
                if (currentWeapon.autoTirs > 1)
                {
                    for (int i = 0; i < currentWeapon.autoTirs - 1; i++)
                    {
                        int numX = rnd.Next(-currentWeapon.accuracy - accuracy, currentWeapon.accuracy + accuracy);
                        int numY = rnd.Next(-currentWeapon.accuracy - accuracy, currentWeapon.accuracy + accuracy);
                        dir = Vector2.Normalize(new Vector2(controles.cursorPosition.X - currentWeapon.position.X + camera.Xcurrent + camera.Xspecial + numX, controles.cursorPosition.Y - currentWeapon.position.Y + camera.Ycurrent + camera.Yspecial + numY));
                        shots.tirList.Add(new Tirs(currentWeapon.ammoTexture, (int)(currentWeapon.damage * attaque), oriTir, dir * 40, currentWeapon.range, id, angle, currentWeapon.nTexture, currentWeapon.tirtype));

                        currentWeapon.currentAmo--;
                    }
                    sound.Play(currentWeapon.shotSound);
                    currentWeapon.autoTirs = 0;
                }
            }
            else if (currentWeapon.weaponType == cadenceType.rafales && controles.StayClick() && currentWeapon.currentAmo > 0)
            {
                if (currentWeapon.rafalstirs < 3)
                {
                    currentWeapon.autoTirs += currentWeapon.cadence;
                    if (currentWeapon.autoTirs > 1)
                    {
                        currentWeapon.rafalstirs++;
                        for (int i = 0; i < currentWeapon.autoTirs - 1; i++)
                        {
                            int numX = rnd.Next(-currentWeapon.accuracy - accuracy, currentWeapon.accuracy + accuracy);
                            int numY = rnd.Next(-currentWeapon.accuracy - accuracy, currentWeapon.accuracy + accuracy);
                            dir = Vector2.Normalize(new Vector2(controles.cursorPosition.X - currentWeapon.position.X + camera.Xcurrent + camera.Xspecial + numX, controles.cursorPosition.Y - currentWeapon.position.Y + camera.Ycurrent + camera.Yspecial + numY));
                            shots.tirList.Add(new Tirs(currentWeapon.ammoTexture, (int)(currentWeapon.damage * attaque), oriTir, dir * 40, currentWeapon.range, id, angle, currentWeapon.nTexture, currentWeapon.tirtype));
                            sound.Play(currentWeapon.shotSound);

                            currentWeapon.currentAmo--;
                        }
                        currentWeapon.autoTirs = 0;
                    }
                }
            }
            else
                currentWeapon.rafalstirs = 0;
        }

        public override void Die(Map map, Sound sound)
        {
            if (pV <= 0)
            {
                if (this.currentItem != null && this.currentItem.type == objectType.vehicule)
                {
                    ((Vehicule)(this.currentItem)).quitVehicule(this, (Vehicule)this.currentItem);
                }
                if (lifes > 0 || revive)
                {
                    if (currenTeam == FlagsType.red)
                    {
                        position = map.spawnPoint2;
                    }
                    else
                    {
                        position = map.spawnPoint;
                    }
                    currentWeapon.currentAmo = currentWeapon.capacity;
                    pV = pVMax;
                    lifes--;
                    for (int i = 0; i < ItemList.Count; i++)
                    {
                        if (ItemList[i].type == objectType.flag)
                        {
                            ItemList[i].isOnMap = true;
                            ((Flag)ItemList[i]).isCaptured = false;
                            map.itemList.Add(ItemList[i]);
                            ItemList.RemoveAt(i);
                            break;
                        }
                    }
                }
                else
                {
                    IsAlive = false;
                }
                sound.Play(SoundsName.die);
                isDie = true;
            }
        }

        public void UpdateLevel(Controles controles, List<Animate> anim, ContentManager Content)
        {
            if (experience > (int)(Math.Pow(1.5, niveau) * Math.PI * 100))
            {
                experience -= (int)(Math.Pow(1.5, niveau) * Math.PI * 100);
                niveau++;
                pointRestants += 5;
                money += (int)(Math.Pow(1.2, niveau) * 783);
                anim.Add(new Animate(new Texture2D[1] { Tools.LoadTexture("Others/LevelUp", Content) }, position, new Vector2(0, -2), 200, 100, true));
            }
            if (Player1Events.killplayer0 > oldkill)
            {
                experience += 50;
                money += 50;
            }
            oldkill = Player1Events.killplayer0;
            if (controles.God())
            {
                if (God)
                {
                    God = false;
                }
                else
                    God = true;
            }
        }

        public void Deserialise(SavPerso Serial, ContentManager Content)
        {
            money = Serial.money;
            currentLevel = Serial.currentLevel;
            this.name = Serial.name;
            this.experience = Serial.experience;
            this.niveau = Serial.niveau;
            pointRestants = Serial.pointRestants;
            attaquePoint = Serial.attaquePoint;
            defensePoint = Serial.defensePoint;
            dexteritePoint = Serial.dexteritePoint;
            vitessePoint = Serial.vitessePoint;

            this.pV = Serial.pV;
            this.pVMax = Serial.pVMax;
            this.attaque = Serial.attaque;
            this.defense = Serial.defense;
            accuracy = Serial.accuracy;
            base.accelerate = Serial.accelerateInit;
            base.decelerate = Serial.decelerateInit;
            base.fall = Serial.fallInit;
            base.jump = Serial.jumpInit;
            base.vitesseMax = Serial.vitesseMaxInit;
            base.fallMax = Serial.fallMaxInit;

            base.accelerateInit = accelerate;
            base.decelerateInit = decelerate;
            base.fallInit = fall;
            base.jumpInit = jump;
            base.vitesseMaxInit = vitesseMax;
            base.fallMaxInit = fallMax;
            newChar = Serial.newChar;
            this.lifes = Serial.lifes;
            this.spritePerso = Serial.spritePerso;

            this.currentWeapon = new Weapon(Serial.currentWeapon.prix, Serial.currentWeapon.ammoTexture,
                Serial.currentWeapon.itemImage, Serial.currentWeapon.damage,
                Serial.currentWeapon.cadence, Serial.currentWeapon.cadenceType,
                Serial.currentWeapon.munitions, Serial.currentWeapon.currentAmo,
                Serial.currentWeapon.capacity, Serial.currentWeapon.accuracy,
                Serial.currentWeapon.range, Serial.currentWeapon.position,
                Serial.currentWeapon.speed, Serial.currentWeapon.weaponName,
                Serial.currentWeapon.isOnMap, Serial.currentWeapon.id,
                Content, Serial.currentWeapon.sound, Serial.currentWeapon.tirs, Serial.currentWeapon.hasSpecialAttack,
                Serial.currentWeapon.numBalle, Serial.currentWeapon.numArme);
            for (int i = 0; i < Serial.WeaponsList.Length; i++)
            {
                ItemList.Add(new Weapon(Serial.WeaponsList[i].prix, Serial.WeaponsList[i].ammoTexture,
                    Serial.WeaponsList[i].itemImage, Serial.WeaponsList[i].damage,
                    Serial.WeaponsList[i].cadence, Serial.WeaponsList[i].cadenceType,
                    Serial.WeaponsList[i].munitions, Serial.WeaponsList[i].currentAmo,
                    Serial.WeaponsList[i].capacity, Serial.WeaponsList[i].accuracy,
                    Serial.WeaponsList[i].range, Serial.WeaponsList[i].position,
                    Serial.WeaponsList[i].speed, Serial.WeaponsList[i].weaponName,
                    Serial.WeaponsList[i].isOnMap, Serial.WeaponsList[i].id, Content,
                    Serial.WeaponsList[i].sound, Serial.WeaponsList[i].tirs, Serial.WeaponsList[i].hasSpecialAttack,
                    Serial.WeaponsList[i].numBalle, Serial.WeaponsList[i].numArme));
            }
        }

        public void Serialize(SavPerso Serial)
        {
            List<Weapon> weaplist = new List<Weapon>();
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].GetType() == typeof(Weapon))
                    weaplist.Add((Weapon)ItemList[i]);
            }
            Serial.WeaponsList = new SavWeapon[weaplist.Count];
            SavWeapon savweapon = new SavWeapon();
            savweapon.accuracy = currentWeapon.accuracy;
            savweapon.prix = currentWeapon.prix;
            savweapon.ammoTexture = currentWeapon.ammo;
            savweapon.cadence = currentWeapon.cadence;
            savweapon.cadenceType = currentWeapon.weaponType;
            savweapon.capacity = currentWeapon.capacity;
            savweapon.currentAmo = currentWeapon.currentAmo;
            savweapon.damage = currentWeapon.damage;
            savweapon.id = currentWeapon.id;
            savweapon.isOnMap = currentWeapon.isOnMap;
            savweapon.itemImage = currentWeapon.imagename;
            savweapon.munitions = currentWeapon.munitions;
            savweapon.tirs = currentWeapon.tirtype;
            savweapon.position = currentWeapon.position;
            savweapon.range = currentWeapon.range;
            savweapon.speed = currentWeapon.speed;
            savweapon.weaponName = currentWeapon.weaponName;
            savweapon.sound = currentWeapon.shotSound;
            savweapon.tirs = currentWeapon.tirtype;
            savweapon.numBalle = currentWeapon.numBalle;
            savweapon.numArme = currentWeapon.numArme;
            savweapon.hasSpecialAttack = currentWeapon.hasSpecialAttack;
            Serial.currentWeapon = savweapon;

            for (int i = 0; i < weaplist.Count; i++)
            {
                savweapon = new SavWeapon();
                savweapon.prix = weaplist[i].prix;
                savweapon.accuracy = weaplist[i].accuracy;
                savweapon.ammoTexture = weaplist[i].ammo;
                savweapon.cadence = weaplist[i].cadence;
                savweapon.cadenceType = weaplist[i].weaponType;
                savweapon.capacity = weaplist[i].capacity;
                savweapon.currentAmo = weaplist[i].currentAmo;
                savweapon.damage = weaplist[i].damage;
                savweapon.id = weaplist[i].id;
                savweapon.tirs = weaplist[i].tirtype;
                savweapon.isOnMap = weaplist[i].isOnMap;
                savweapon.itemImage = weaplist[i].imagename;
                savweapon.munitions = weaplist[i].munitions;
                savweapon.position = weaplist[i].position;
                savweapon.range = weaplist[i].range;
                savweapon.speed = weaplist[i].speed;
                savweapon.weaponName = weaplist[i].weaponName;
                savweapon.sound = weaplist[i].shotSound;
                savweapon.numArme = weaplist[i].numArme;
                savweapon.numBalle = weaplist[i].numBalle;
                savweapon.hasSpecialAttack = weaplist[i].hasSpecialAttack;
                Serial.WeaponsList[i] = savweapon;
            }

            Serial.name = name;
            Serial.experience = experience;
            Serial.niveau = niveau;
            Serial.pointRestants = pointRestants;
            Serial.attaquePoint = attaquePoint;
            Serial.defensePoint = defensePoint;
            Serial.dexteritePoint = dexteritePoint;
            Serial.vitessePoint = vitessePoint;
            Serial.lifes = lifes;
            Serial.accuracy = accuracy;
            Serial.accelerateInit = accelerateInit;
            Serial.decelerateInit = decelerateInit;
            Serial.fallInit = fallInit;
            Serial.jumpInit = jumpInit;
            Serial.vitesseMaxInit = vitesseMaxInit;
            Serial.fallMaxInit = fallMaxInit;
            Serial.attaque = attaque;
            Serial.spritePerso = spritePerso;

            Serial.pV = pV;
            Serial.pVMax = pVMax;
            Serial.defense = defense;
            Serial.newChar = newChar;
            Serial.money = money;
            Serial.currentLevel = currentLevel;
        }
    }
}