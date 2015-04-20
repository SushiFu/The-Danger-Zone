using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public static class LoadWeapons
    {
        public static List<Weapon> AllWeapon(ContentManager Content)
        {
            List<Weapon> retour = new List<Weapon>();

            //a mettre dans l'ordre de prix

            retour.Add(LoadGun(Content));
            retour.Add(LoadSig(Content));
            retour.Add(LoadAK47(Content));
            retour.Add(LoadSpiker(Content));
            retour.Add(LoadNeedlerCarabine(Content));
            retour.Add(LoadShotGun(Content));
            retour.Add(LoadNeedler(Content));
            retour.Add(LoadSpartanLaser(Content));
            retour.Add(LoadSniper(Content));
            retour.Add(LoadBatteuse(Content));
            retour.Add(LoadminiRPG(Content));
            retour.Add(LoadLanceFlamme(Content));
            retour.Add(LoadUSP(Content));
            retour.Add(LoadRocketLauncher(Content));
            return retour;
        }

        public static Weapon LoadBatteuse(ContentManager Content)
        {
            return new Weapon(40000, "Others/Munitions/bullet", "minig", 70, 0.9, cadenceType.auto, 1000, 250, 250, 10, 75, Vector2.Zero, Vector2.Zero, "Minigun", false, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 0, 4);
        }

        public static Weapon LoadLanceFlamme(ContentManager Content)
        {
            return new Weapon(50000, "Others/Munitions/flammes", "Flamethrower", 10, 10, cadenceType.auto, 8000, 2000, 2000, 100, 15, Vector2.Zero, Vector2.Zero, "Flamethrower", false, 0, Content, SoundsName.FlameThrower, tirTypes.bullet, false, 1, 1);
        }

        public static Weapon LoadBansheeBomb(ContentManager Content)
        {
            return new Weapon(10, "Others/Munitions/bansheebomb", "SIG", 200, 0.03, cadenceType.semiAuto, 80, 20, 20, 15, 175, Vector2.Zero, Vector2.Zero, "BansheeBomb", false, 0, Content, SoundsName.fusil, tirTypes.explosive, false, 5, 10);
        }

        public static Weapon LoadBansheeGun(ContentManager Content)
        {
            return new Weapon(10, "Others/Munitions/banshee_shot", "SIG", 80, 1.5f, cadenceType.auto, 800, 200, 200, 15, 175, Vector2.Zero, Vector2.Zero, "BansheeGun", false, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 4, 10);
        }

        public static Weapon LoadSniper(ContentManager Content)
        {
            return new Weapon(15000, "Others/Munitions/ball", "sniper", 2000, 0.02, cadenceType.semiAuto, 60, 15, 15, 0, 400, Vector2.Zero, Vector2.Zero, "Sniper", false, 0, Content, SoundsName.sniper, tirTypes.bullet, true, 0, 12);
        }

        public static Weapon LoadAK47(ContentManager Content)
        {
            return new Weapon(4000, "Others/Munitions/bullet", "AK47", 80, 0.4f, cadenceType.auto, 200, 50, 50, 10, 70, Vector2.Zero, Vector2.Zero, "AK47", true, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 2, 0);
        }

        public static Weapon LoadIAGun(ContentManager Content)
        {
            return new Weapon(10, "Others/Munitions/bullet", "SIG", 50, 0.5, cadenceType.auto, 2000, 100, 100, 10, 100, Vector2.Zero, Vector2.Zero, "SIG556", false, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 2, 10);
        }

        public static Weapon LoadNeedlerCarabine(ContentManager Content)
        {
            return new Weapon(6000, "Others/Munitions/green_bullet", "NeedlerCarabine", 75, 0.5, cadenceType.semiAuto, 120, 35, 35, 5, 110, Vector2.Zero, Vector2.Zero, "NeedlerCarabine", false, 0, Content, SoundsName.lazergun, tirTypes.bullet, false, 6, 7);
        }

        public static Weapon LoadSpartanLaser(ContentManager Content)
        {
            return new Weapon(9000, "Others/ball", "SpartanLaser", 150, 20, cadenceType.auto, 2000, 513, 500, 1, 100, Vector2.Zero, Vector2.Zero, "SpartanLaser", false, 0, Content, SoundsName.laser, tirTypes.blaster, false, 10, 13);
        }

        //laser, sniper
        public static Weapon LoadSig(ContentManager Content)
        {
            return new Weapon(2000, "Others/Munitions/bullet", "SIG", 50, 0.7, cadenceType.rafales, 200, 45, 45, 8, 150, Vector2.Zero, Vector2.Zero, "SIG556", false, 0, Content, SoundsName.mitrailleuse, tirTypes.bullet, false, 2, 10);
        }

        public static Weapon LoadGun(ContentManager Content)
        {
            return new Weapon(500, "Others/Munitions/green_rond_bullet", "gun", 100, 0.7, cadenceType.semiAuto, 60, 12, 12, 12, 30, Vector2.Zero, Vector2.Zero, "gun", false, 0, Content, SoundsName.silencieux, tirTypes.bullet, false, 2, 2);
        }

        public static Weapon LoadNeedler(ContentManager Content)//explosion du nombre de fleches apres clic droit
        {
            return new Weapon(8000, "Others/Munitions/needler_muni", "Needler", 30, 0.22, cadenceType.auto, 160, 40, 40, 14, 75, Vector2.Zero, Vector2.Zero, "Needler", false, 0, Content, SoundsName.blaster, tirTypes.bullet, false, 7, 6);
        }

        public static Weapon LoadSpiker(ContentManager Content)
        {
            return new Weapon(5000, "Others/ball", "Spiker", 60, 0.12, cadenceType.auto, 180, 45, 45, 15, 30, Vector2.Zero, Vector2.Zero, "Spiker", false, 0, Content, SoundsName.blaster, tirTypes.bullet, false, 3, 14);
        }

        /* public static Weapon LoadHammerAntigrav(ContentManager Content)
         {
             return new Weapon("Others/ball", "HammerAntigrav", 100, 0.7, cadenceType.semiAuto, 4000, 0, 0, 0, 15, Vector2.Zero, Vector2.Zero, "HammerAntigrav", false, 0, Content, tirTypes.bullet,false);
         }*/

        public static Weapon LoadminiRPG(ContentManager Content)//clic droit explosion
        {
            return new Weapon(35000, "Others/Munitions/rpgammo22", "MiniRPG", 1, 0.01, cadenceType.semiAuto, 4, 1, 1, 12, 100, Vector2.Zero, Vector2.Zero, "MiniRPG", false, 0, Content, SoundsName.rocket, tirTypes.explosive, false, 8, 5);
        }

        public static Weapon LoadShotGun(ContentManager Content)
        {
            return new Weapon(10000, "Others/Munitions/ball", "ShotGun", 100, 5, cadenceType.auto, 30, 8, 8, 35, 30, Vector2.Zero, Vector2.Zero, "ShotGun", false, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 2, 9);
        }

        public static Weapon LoadMA5D(ContentManager Content)
        {
            return new Weapon(5500, "Others/Munitions/bullet", "MA5D", 60, 0.2, cadenceType.auto, 200, 50, 50, 12, 80, Vector2.Zero, Vector2.Zero, "M5AD", false, 0, Content, SoundsName.fusil, tirTypes.bullet, false, 0, 3);
        }

        public static Weapon LoadUSP(ContentManager Content)
        {
            return new Weapon(900, "Others/Munitions/green_rond_bullet", "silencieux", 70, 0.8, cadenceType.semiAuto, 100, 20, 20, 11, 60, Vector2.Zero, Vector2.Zero, "USP45", false, 0, Content, SoundsName.silencieux, tirTypes.bullet, false, 6, 11);
        }

        public static Weapon LoadRocketLauncher(ContentManager Content)
        {
            return new Weapon(40000, "Others/Munitions/rpgammo22", "rocket_launcher", 1, 0.05, cadenceType.semiAuto, 12, 4, 4, 10, 120, Vector2.Zero, Vector2.Zero, "Rocket-Launcher", false, 0, Content, SoundsName.rocket, tirTypes.explosive, false, 9, 8);
        }

        public static Weapon LoadKnife(ContentManager Content)
        {
            return new Weapon(0, "Others/null", "knife/knife", 1000, 0.1, cadenceType.none, 0, 0, 0, 0, 1, Vector2.Zero, Vector2.Zero, "Knife", false, 0, Content, SoundsName.epee, tirTypes.none, false, 10, 15);
        }
    }
}