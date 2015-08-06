using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    [Serializable]
    public class SavPerso
    {
        public SavWeapon currentWeapon;

        public SavWeapon[] WeaponsList;
        public int lifes;

        public int accuracy;
        public float accelerateInit;
        public float decelerateInit;
        public float fallInit;
        public float jumpInit;
        public float vitesseMaxInit;
        public float fallMaxInit;
        public float attaque;
        public int pV;
        public int pVMax;
        public float defense;

        public int niveau;
        public string name;

        public int experience;
        public int pointRestants;
        public int attaquePoint;
        public int defensePoint;
        public int dexteritePoint;
        public int vitessePoint;

        public bool newChar;

        public int money;
        public int currentLevel;
        public int spritePerso;
    }
}