using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    [Serializable]
    public class Opt
    {
        public bool Fullscreen;
        public bool SoundOn;
        public string name;
        public Keys[] controleTab;
        public string masterServerAdress;
        public Langue language;
    }
}