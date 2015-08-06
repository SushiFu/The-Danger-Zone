using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProjetSup_Win_SLMQ
{
    public enum AnimDir
    {
        stayLeft = 0,
        beginLeft = 1,
        jumpLeft = 4,
        EndLeft = 10,
        stayRight = 11,
        beginRight = 12,
        jumpRight = 15,
        EndRight = 21,
    }

    public static class TexturesGame
    {
        public static Texture2D[] Backtab;
        public static Texture2D[][] PlayerTab;
        public static Texture2D[][] IATab;
        public static Texture2D[][] itemTab;
        public static Texture2D[] ImpactTab;
        public static Texture2D[][] weaponTab;
        public static Texture2D[] ammoTab;
        public static Texture2D[] armestext;
        public static Texture2D[] platForm;
        public static Texture2D[] test;
        public static Texture2D[] bonusTab;
        public static Texture2D[] speedUp;

        public static void LoadArmes(ContentManager Content)
        {
            weaponTab = new Texture2D[16][];
            weaponTab[0] = new Texture2D[2];
            weaponTab[1] = new Texture2D[2];
            weaponTab[2] = new Texture2D[2];
            weaponTab[3] = new Texture2D[2];
            weaponTab[4] = new Texture2D[2];
            weaponTab[5] = new Texture2D[2];
            weaponTab[6] = new Texture2D[2];
            weaponTab[7] = new Texture2D[2];
            weaponTab[8] = new Texture2D[2];
            weaponTab[9] = new Texture2D[2];
            weaponTab[9] = new Texture2D[2];
            weaponTab[10] = new Texture2D[2];
            weaponTab[11] = new Texture2D[2];
            weaponTab[12] = new Texture2D[2];
            weaponTab[13] = new Texture2D[2];
            weaponTab[14] = new Texture2D[2];
            weaponTab[15] = new Texture2D[10];

            weaponTab[0] = new Texture2D[2];
            weaponTab[0][0] = Tools.LoadTexture("Weapons/AK47_right", Content);
            weaponTab[0][1] = Tools.LoadTexture("Weapons/AK47_left", Content);
            weaponTab[1][0] = Tools.LoadTexture("Weapons/Flamethrower_right", Content);
            weaponTab[1][1] = Tools.LoadTexture("Weapons/Flamethrower_left", Content);
            weaponTab[2][0] = Tools.LoadTexture("Weapons/gun_right", Content);
            weaponTab[2][1] = Tools.LoadTexture("Weapons/gun_left", Content);
            weaponTab[3][0] = Tools.LoadTexture("Weapons/MA5D_right", Content);
            weaponTab[3][1] = Tools.LoadTexture("Weapons/MA5D_left", Content);
            weaponTab[4][0] = Tools.LoadTexture("Weapons/minig_right", Content);
            weaponTab[4][1] = Tools.LoadTexture("Weapons/minig_left", Content);
            weaponTab[5][0] = Tools.LoadTexture("Weapons/MiniRPG_right", Content);
            weaponTab[5][1] = Tools.LoadTexture("Weapons/MiniRPG_left", Content);
            weaponTab[6][0] = Tools.LoadTexture("Weapons/Needler_right", Content);
            weaponTab[6][1] = Tools.LoadTexture("Weapons/Needler_left", Content);
            weaponTab[7][0] = Tools.LoadTexture("Weapons/NeedlerCarabine_right", Content);
            weaponTab[7][1] = Tools.LoadTexture("Weapons/NeedlerCarabine_left", Content);
            weaponTab[8][0] = Tools.LoadTexture("Weapons/rocket_launcher_right", Content);
            weaponTab[8][1] = Tools.LoadTexture("Weapons/rocket_launcher_left", Content);
            weaponTab[9][0] = Tools.LoadTexture("Weapons/ShotGun_right", Content);
            weaponTab[9][1] = Tools.LoadTexture("Weapons/ShotGun_left", Content);
            weaponTab[10][0] = Tools.LoadTexture("Weapons/SIG_right", Content);
            weaponTab[10][1] = Tools.LoadTexture("Weapons/SIG_left", Content);
            weaponTab[11][0] = Tools.LoadTexture("Weapons/silencieux_right", Content);
            weaponTab[11][1] = Tools.LoadTexture("Weapons/silencieux_left", Content);
            weaponTab[12][0] = Tools.LoadTexture("Weapons/sniper_right", Content);
            weaponTab[12][1] = Tools.LoadTexture("Weapons/sniper_left", Content);
            weaponTab[13][0] = Tools.LoadTexture("Weapons/SpartanLaser_right", Content);
            weaponTab[13][1] = Tools.LoadTexture("Weapons/SpartanLaser_left", Content);
            weaponTab[14][0] = Tools.LoadTexture("Weapons/Spiker_right", Content);
            weaponTab[14][1] = Tools.LoadTexture("Weapons/Spiker_left", Content);
            weaponTab[15][0] = Tools.LoadTexture("Weapons/knife/knife_right", Content);
            weaponTab[15][1] = Tools.LoadTexture("Weapons/knife/knife_left", Content);
            weaponTab[15][2] = Tools.LoadTexture("Weapons/" + "knife/" + 1 + "_right", Content);
            weaponTab[15][3] = Tools.LoadTexture("Weapons/" + "knife/" + 2 + "_right", Content);
            weaponTab[15][4] = Tools.LoadTexture("Weapons/" + "knife/" + 3 + "_right", Content);
            weaponTab[15][5] = Tools.LoadTexture("Weapons/" + "knife/" + 4 + "_right", Content);
            weaponTab[15][6] = Tools.LoadTexture("Weapons/" + "knife/" + 5 + "_left", Content);
            weaponTab[15][7] = Tools.LoadTexture("Weapons/" + "knife/" + 6 + "_left", Content);
            weaponTab[15][8] = Tools.LoadTexture("Weapons/" + "knife/" + 7 + "_left", Content);
            weaponTab[15][9] = Tools.LoadTexture("Weapons/" + "knife/" + 8 + "_left", Content);
        }

        public static void LoadBackgrounds(ContentManager Content)
        {
            Backtab = new Texture2D[7];
            Backtab[0] = Tools.LoadTexture("Backgrounds/forest", Content);
            Backtab[1] = Tools.LoadTexture("Backgrounds/mountain_background", Content);
            Backtab[2] = Tools.LoadTexture("Backgrounds/perfect", Content);
            Backtab[3] = Tools.LoadTexture("Backgrounds/perfect2", Content);
            Backtab[4] = Tools.LoadTexture("Backgrounds/shimmerside", Content);
            Backtab[5] = Tools.LoadTexture("Backgrounds/clouds", Content);
            Backtab[6] = Tools.LoadTexture("Backgrounds/desert_background", Content);
        }

        public static Texture2D[] flags;

        public static void LoadImpact(ContentManager Content)
        {
            ImpactTab = new Texture2D[5];
            for (int i = 0; i < 5; i++)
            {
                ImpactTab[i] = Tools.LoadTexture("Impact/" + (int)(i + 1), Content);
            }
        }

        public static void LoadPlayers(ContentManager Content)
        {
            PlayerTab = new Texture2D[2][];
            PlayerTab[0] = new Texture2D[22];

            PlayerTab[0][0] = Tools.LoadTexture("SpritePerso/P1/left/stay", Content);

            for (int i = 0; i < 10; i++)
            {
                PlayerTab[0][i + 1] = Tools.LoadTexture("SpritePerso/P1/left/" + i, Content);
            }

            PlayerTab[0][11] = Tools.LoadTexture("SpritePerso/P1/right/stay", Content);

            for (int i = 0; i < 10; i++)
            {
                PlayerTab[0][i + 12] = Tools.LoadTexture("SpritePerso/P1/right/" + i, Content);
            }

            PlayerTab[1] = new Texture2D[22];

            PlayerTab[1][0] = Tools.LoadTexture("SpritePerso/P2/left/stay", Content);

            for (int i = 0; i < 10; i++)
            {
                PlayerTab[1][i + 1] = Tools.LoadTexture("SpritePerso/P2/left/" + i, Content);
            }

            PlayerTab[1][11] = Tools.LoadTexture("SpritePerso/P2/right/stay", Content);

            for (int i = 0; i < 10; i++)
            {
                PlayerTab[1][i + 12] = Tools.LoadTexture("SpritePerso/P2/right/" + i, Content);
            }
        }

        public static void LoadSpeedUp(ContentManager Content)
        {
            speedUp = new Texture2D[2] { Tools.LoadTexture("Others/Test/speed1", Content), Tools.LoadTexture("Others/Test/speed2", Content) };
        }

        public static void LoadIAs(ContentManager Content)
        {
            IATab = new Texture2D[2][];
            IATab[0] = new Texture2D[22];

            IATab[0][0] = Tools.LoadTexture("SpriteIA/IA1/left/stay", Content);

            for (int i = 0; i < 10; i++)
            {
                IATab[0][i + 1] = Tools.LoadTexture("SpriteIA/IA1/left/" + i, Content);
            }

            IATab[0][11] = Tools.LoadTexture("SpriteIA/IA1/right/stay", Content);

            for (int i = 0; i < 10; i++)
            {
                IATab[0][i + 12] = Tools.LoadTexture("SpriteIA/IA1/right/" + i, Content);
            }
        }

        public static void LoadPlatForm(ContentManager Content)
        {
            platForm = new Texture2D[1] { Tools.LoadTexture("SpriteTexture/Others/Barre", Content) };
        }

        public static void Loadtest(ContentManager Content)
        {
            test = new Texture2D[4]
            {
                Tools.LoadTexture("Others/Test/pixelRouge", Content),
                Tools.LoadTexture("Others/Test/pixGris", Content),
                Tools.LoadTexture("Others/Test/pixJaune", Content),
                Tools.LoadTexture("Others/Test/pixOrange", Content)
            };
        }

        public static void LoadItem(ContentManager Content)
        {
            itemTab = new Texture2D[2][];
            itemTab[0] = new Texture2D[23];
            itemTab[1] = new Texture2D[23];
            //  itemTab[2] = new Texture2D[23];

            for (int i = 0; i < 11; i++)
            {
                itemTab[0][i] = Tools.LoadTexture("SpriteVehicle/hornet_left", Content);
            }
            for (int i = 11; i < 22; i++)
            {
                itemTab[0][i] = Tools.LoadTexture("SpriteVehicle/hornet_right", Content);
            }
            itemTab[0][22] = Tools.LoadTexture("SpriteVehicle/hornet_right", Content);
            for (int i = 0; i < 11; i++)
            {
                itemTab[1][i] = Tools.LoadTexture("SpriteVehicle/banshee_L", Content);
            }
            for (int i = 11; i < 22; i++)
            {
                itemTab[1][i] = Tools.LoadTexture("SpriteVehicle/banshee_R", Content);
            }
            itemTab[1][22] = Tools.LoadTexture("SpriteVehicle/banshee1", Content);
        }

        public static void LoadAmmo(ContentManager Content)
        {
            ammoTab = new Texture2D[11];
            ammoTab[0] = Tools.LoadTexture("Others/Munitions/ball", Content);
            ammoTab[1] = Tools.LoadTexture("Others/Munitions/flammes", Content);
            ammoTab[2] = Tools.LoadTexture("Others/Munitions/bullet", Content);
            ammoTab[3] = Tools.LoadTexture("Others/Munitions/balle_blaster", Content);
            ammoTab[4] = Tools.LoadTexture("Others/Munitions/banshee_shot", Content);
            ammoTab[5] = Tools.LoadTexture("Others/Munitions/bansheebomb", Content);
            ammoTab[6] = Tools.LoadTexture("Others/Munitions/green_bullet", Content);
            ammoTab[7] = Tools.LoadTexture("Others/Munitions/needler_muni", Content);
            ammoTab[8] = Tools.LoadTexture("Others/Munitions/rpgammo22", Content);
            ammoTab[9] = Tools.LoadTexture("Others/null", Content);
            ammoTab[10] = Tools.LoadTexture("Others/ball", Content);
        }

        public static void LoadBonus(ContentManager Content)
        {
            bonusTab = new Texture2D[12];
            bonusTab[0] = Tools.LoadTexture("Others/Test/champiBullshit", Content);
            bonusTab[1] = Tools.LoadTexture("Item/coeur", Content);
            bonusTab[2] = Tools.LoadTexture("Others/Munitions/chargeurs", Content);
            bonusTab[3] = Tools.LoadTexture("Others/Test/Etoile", Content);
            bonusTab[4] = Tools.LoadTexture("Others/Test/Ammo", Content);
            bonusTab[5] = Tools.LoadTexture("Others/Test/AttaqueUp", Content);
            bonusTab[6] = Tools.LoadTexture("Others/Test/Bouclier", Content);
            bonusTab[7] = Tools.LoadTexture("Others/1", Content);
            bonusTab[8] = Tools.LoadTexture("Others/2", Content);
            bonusTab[9] = Tools.LoadTexture("Others/3", Content);
            bonusTab[10] = Tools.LoadTexture("Others/4", Content);
            bonusTab[11] = Tools.LoadTexture("Others/5", Content);
        }

        public static Texture2D[] LoadFlags(ContentManager Content)
        {
            flags = new Texture2D[4];
            flags[0] = Tools.LoadTexture("Item/DrapeauRouge", Content);
            flags[1] = Tools.LoadTexture("Item/MiniDrapeauRouge", Content);
            flags[2] = Tools.LoadTexture("Item/DrapeauBleu", Content);
            flags[3] = Tools.LoadTexture("Item/MiniDrapeauBleu", Content);

            return flags;
        }
    }
}