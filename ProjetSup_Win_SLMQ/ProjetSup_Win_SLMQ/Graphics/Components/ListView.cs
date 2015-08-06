using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ProjetSup_Win_SLMQ
{
    public class ListView
    {
        public Texture2D defaultCell;
        private SpriteFont font;
        private Color textColor;
        private Vector2 initialPos;
        private List<Button> listview;
        private int nbMaxCell;
        private int topCell;
        private Rectangle rectListView;

        public ListView(Texture2D defaultCell, Vector2 initialPos, SpriteFont font, Color textColor, int nbMaxCell)
        {
            this.textColor = textColor;
            this.initialPos = initialPos;
            this.font = font;
            this.defaultCell = defaultCell;
            listview = new List<Button>();
            this.nbMaxCell = nbMaxCell;
            topCell = 0;

            rectListView = new Rectangle((int)initialPos.X, (int)initialPos.Y, defaultCell.Width, defaultCell.Height * nbMaxCell);
        }

        public void AddCell(String[] text, int[] textXOffsets, int textYOffset)
        {
            Button tmp = new Button(defaultCell, text, textXOffsets, textYOffset, font);
            tmp.setPositionAndColor(new Vector2(initialPos.X, initialPos.Y + listview.Count * defaultCell.Height), textColor);
            listview.Add(tmp);
        }

        public void updateString(string[] strin, int num)
        {
            listview[num].SetText(strin);
        }

        public void Update(double mouseCoef, Controles controles)
        {
            Rectangle rectCursor = new Rectangle((int)controles.cursorPosition.X, (int)controles.cursorPosition.Y, 5, 5);

            if (controles.scrollDown() && listview.Count > nbMaxCell && rectListView.Intersects(rectCursor) && listview.Count - topCell > nbMaxCell)
            {
                for (int i = topCell; i < listview.Count; i++)
                {
                    Vector2 newVectPos = new Vector2(listview[i].position.X, listview[i].position.Y - defaultCell.Height);
                    listview[i].setPositionAndColor(newVectPos, textColor);
                }
                topCell++;
            }
            if (controles.scrollUp() && listview.Count > nbMaxCell && rectListView.Intersects(rectCursor) && topCell > 0)
            {
                for (int i = topCell - 1; i < listview.Count; i++)
                {
                    Vector2 newVectPos = new Vector2(listview[i].position.X, listview[i].position.Y + defaultCell.Height);
                    listview[i].setPositionAndColor(newVectPos, textColor);
                }
                topCell--;
            }
            for (int i = 0; i < listview.Count; i++)
            {
                listview[i].Update(mouseCoef, controles);
            }
        }

        public Button GetClickedButton()
        {
            for (int i = 0; i < listview.Count; i++)
            {
                if (listview[i].isCliked)
                {
                    listview[i].isCliked = false;
                    return listview[i];
                }
            }
            return null;
        }

        public int GetIndexClicked()
        {
            for (int i = 0; i < listview.Count; i++)
            {
                if (listview[i].isCliked)
                {
                    listview[i].isCliked = false;
                    return i;
                }
            }
            return -1;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (nbMaxCell > listview.Count)
                nbMaxCell = listview.Count;
            for (int i = topCell; i < topCell + nbMaxCell; i++)
            {
                listview[i].Draw(spriteBatch);
            }
        }
    }
}