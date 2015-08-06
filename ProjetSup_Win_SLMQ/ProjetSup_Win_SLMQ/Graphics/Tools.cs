using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace ProjetSup_Win_SLMQ
{
    public class Tools
    {
        public static Texture2D LoadTexture(String filepath, ContentManager Content)
        {
            return Content.Load<Texture2D>(filepath);
        }

        public static SpriteFont LoadFont(String filepath, ContentManager Content)
        {
            return Content.Load<SpriteFont>(filepath);
        }

        public static SoundEffect LoadSoundEffect(String filepath, ContentManager Content)
        {
            return Content.Load<SoundEffect>(filepath);
        }
    }
}

