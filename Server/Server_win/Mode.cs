using System;

namespace Server_win
{
    public enum ModeMulti
    {
        ctf,
        tdm
    }

    public class Mode
    {
        public long minutes = 20 * 60 * 10000;
        public ModeMulti type;
        public int red;
        public int blue;

        public Mode(ModeMulti type)
        {
            this.type = type;
            this.red = 0;
            this.blue = 0;
        }

        public void Update(int count, int index)
        {
            if (type == ModeMulti.ctf && index % 2 == 0)
            {
                red += count;
            }
            else if (type == ModeMulti.ctf && index % 2 == 1)
            {
                blue += count;
            }
            else if (type == ModeMulti.tdm && index % 2 == 0)
            {
                blue += count;
            }
            else if (type == ModeMulti.tdm && index % 2 == 1)
            {
                red += count;
            }
        }
    }
}

