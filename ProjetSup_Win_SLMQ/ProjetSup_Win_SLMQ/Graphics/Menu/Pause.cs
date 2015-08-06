using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetSup_Win_SLMQ
{
    public class Pause
    {
        private Button resume;
        private Button gomenu;
        public bool resumeGame;
        public bool menu;

        public Pause(ContentManager Content)
        {
            resume = new Button(Tools.LoadTexture("Menu/Game", Content));
            gomenu = new Button(Tools.LoadTexture("Menu/GoMenu", Content));
            resume.setPositionAndColor(new Vector2(500, 500), Color.Black);
            gomenu.setPositionAndColor(new Vector2(500, 700), Color.Black);
        }

        public void draw(SpriteBatch sb, ContentManager content)
        {
            sb.Draw(Tools.LoadTexture("Menu/Pause2", content), new Vector2(0, 0), Color.White);
            resume.Draw(sb);
            gomenu.Draw(sb);
        }

        public void Update(double mouseCoef, Controles controle)
        {
            resume.Update(mouseCoef, controle);
            gomenu.Update(mouseCoef, controle);
            if (resume.isCliked)
            {
                resume.isCliked = false;
                resumeGame = true;
            }
            else if (gomenu.isCliked)
            {
                gomenu.isCliked = false;
                menu = true;
            }
        }
    }
}