using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class Explosion
    {
        public Vector2 position;
        public Vector2 size;
        public int iteration;
        public bool suprr;
        public List<Vector2> touchedTile;
        public bool offsetPlus;

        public Explosion(Vector2 position, Map map, List<ParticleEngine> particles, Sound sound)
        {
            this.position = new Vector2(position.X - 100, position.Y - 100);
            size = new Vector2(275, 275);
            iteration = 0;
            suprr = false;
            touchedTile = new List<Vector2>();

            if (position.X / 75 >= 0 && position.X / 75 < map.width - 1 && position.Y / 75 + 2 >= 0 && position.Y / 75 + 2 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75, position.Y / 75 + 2));

            if (position.X / 75 + 1 >= 0 && position.X / 75 + 1 < map.width - 1 && position.Y / 75 + 1 >= 0 && position.Y / 75 + 1 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 + 1, position.Y / 75 + 1));

            if (position.X / 75 >= 0 && position.X / 75 < map.width - 1 && position.Y / 75 + 1 >= 0 && position.Y / 75 + 1 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75, position.Y / 75 + 1));

            if (position.X / 75 - 1 >= 0 && position.X / 75 - 1 < map.width - 1 && position.Y / 75 + 1 >= 0 && position.Y / 75 + 1 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 - 1, position.Y / 75 + 1));

            if (position.X / 75 + 2 >= 0 && position.X / 75 + 2 < map.width - 1 && position.Y / 75 >= 0 && position.Y / 75 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 + 2, position.Y / 75));

            if (position.X / 75 + 1 >= 0 && position.X / 75 + 1 < map.width - 1 && position.Y / 75 >= 0 && position.Y / 75 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 + 1, position.Y / 75));

            if (position.X / 75 >= 0 && position.X / 75 < map.width - 1 && position.Y / 75 >= 0 && position.Y / 75 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75, position.Y / 75));

            if (position.X / 75 - 1 >= 0 && position.X / 75 - 1 < map.width - 1 && position.Y / 75 >= 0 && position.Y / 75 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 - 1, position.Y / 75));

            if (position.X / 75 - 2 >= 0 && position.X / 75 - 2 < map.width - 1 && position.Y / 75 >= 0 && position.Y / 75 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 - 2, position.Y / 75));

            if (position.X / 75 + 1 >= 0 && position.X / 75 + 1 < map.width - 1 && position.Y / 75 - 1 >= 0 && position.Y / 75 - 1 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 + 1, position.Y / 75 - 1));

            if (position.X / 75 >= 0 && position.X / 75 < map.width - 1 && position.Y / 75 - 1 >= 0 && position.Y / 75 - 1 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75, position.Y / 75 - 1));

            if (position.X / 75 - 1 >= 0 && position.X / 75 - 1 < map.width - 1 && position.Y / 75 - 1 >= 0 && position.Y / 75 - 1 < map.height - 1)
                touchedTile.Add(new Vector2(position.X / 75 - 1, position.Y / 75 - 1));

            if (position.X / 75 >= 0 && position.X / 75 < map.width - 1 && position.Y / 75 - 2 >= 0 && position.Y / 75 - 2 < map.height)
                touchedTile.Add(new Vector2(position.X / 75, position.Y / 75 - 2));

            sound.Play(SoundsName.explosion);

            map.particleEngine.Add(new ParticleEngine(9, position + new Vector2(38, 38), new Vector2(7, -12) * 0.7f, 8, TexturesGame.test[3], 90, 5, 2, 5, 2, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(4, position + new Vector2(38, 38), new Vector2(7, -12) * 0.7f, 8, TexturesGame.test[2], 90, 5, 2, 5, 2, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(2, position + new Vector2(38, 38), new Vector2(7, -12) * 0.7f, 6, TexturesGame.test[0], 90, 5, 2, 5, 2, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(9, position + new Vector2(38, 38), new Vector2(0, -12) * 0.7f, 8, TexturesGame.test[3], 90, 5, 4, 5, 4, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(4, position + new Vector2(38, 38), new Vector2(0, -12) * 0.7f, 8, TexturesGame.test[2], 90, 5, 4, 5, 4, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(2, position + new Vector2(38, 38), new Vector2(0, -12) * 0.7f, 6, TexturesGame.test[0], 90, 5, 4, 5, 4, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(9, position + new Vector2(38, 38), new Vector2(-7, -12) * 0.7f, 8, TexturesGame.test[3], 90, 5, 2, 5, 2, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(4, position + new Vector2(38, 38), new Vector2(-7, -12) * 0.7f, 8, TexturesGame.test[2], 90, 5, 2, 5, 2, 0.25f, false));
            map.particleEngine.Add(new ParticleEngine(2, position + new Vector2(38, 38), new Vector2(-7, -12) * 0.7f, 6, TexturesGame.test[0], 90, 5, 2, 5, 2, 0.25f, false));
        }
    }
}