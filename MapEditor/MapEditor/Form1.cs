using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        #region Set Variable

        private int mousseX;
        private int mousseY;
        private Form2 form2 = new Form2();
        private int offsetX = 0;
        private int offsetY = 0;
        private int posX;
        private int posY;
        private int fisrtX;
        private int firstY;
        private int countClick = 0;
        private int lineLenght;
        private int squareHeight;
        private int lineSign;
        private int squareSign;
        private int type;
        private Function function = new Function();
        private int[,] world;
        private List<string> worldTextes;
        private int nTexte = -1;
        private Bitmap texture = new Bitmap("./Resources/Grille.png");
        private Bitmap grille = new Bitmap("./Resources/Grille.png");
        private int width;
        private int height;
        private int n_texture;
        private Graphics g;
        private Bitmap bmp = new Bitmap("./Resources/maptest.bmp");

        private NonPrint nonPrint = new NonPrint();
        private string filename = "";
        private bool drawLine = false;
        private bool drawPoint = true;
        private bool drawSquare = false;
        private bool drawDiagonal = false;
        private bool full = false;
        private int coef = 1;
        private int nMap = -1;
        private int nBack = 0;
        private RectangleF rec;
        private Map[] retourSuiv = new Map[20];

        private int sizeSquare;
        private Size im;

        #endregion Set Variable

        public Form1()
        {
            InitializeComponent();
            worldTextes = new List<string>();
            sizeSquare = 75;
            width = ((bmp.Width) / 75) + 1;
            height = ((bmp.Height) / 75) + 1;
            world = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    world[y, x] = type;
            }

            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(-offsetX, -offsetY);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            rec = new RectangleF(0, 0, sizeSquare, sizeSquare);
        }

        #region Mouse Click

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            posX = (e.Location.X) - (e.Location.X) % sizeSquare;
            posY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
            if (posX / sizeSquare < width && posY / sizeSquare < height)
            {
                if (countClick > 0)
                {
                    countClick = 0;
                    if (drawLine)
                    {
                        posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                        posY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
                        lineLenght = posX / sizeSquare - fisrtX / sizeSquare;
                        squareHeight = posY / sizeSquare - firstY / sizeSquare;
                        if (Math.Abs(lineLenght) >= Math.Abs(squareHeight))
                        {
                            if (lineLenght >= 0)
                            {
                                for (int i = 0; i <= lineLenght; i++)
                                {
                                    texture = function.ToTexture(type);
                                    rec = new RectangleF(fisrtX + i * sizeSquare, firstY, sizeSquare, sizeSquare);
                                    pictureBox1.CreateGraphics().DrawImage(texture, rec);

                                    world[firstY / sizeSquare, fisrtX / sizeSquare + i] = type;
                                }
                            }
                            if (lineLenght < 0)
                            {
                                for (int i = lineLenght; i <= 0; i++)
                                {
                                    texture = function.ToTexture(type);
                                    rec = new RectangleF(fisrtX + i * sizeSquare, firstY, sizeSquare, sizeSquare);
                                    pictureBox1.CreateGraphics().DrawImage(texture, rec);

                                    world[firstY / sizeSquare, fisrtX / sizeSquare + i] = type;
                                }
                            }
                        }
                        else
                        {
                            if (squareHeight >= 0)
                            {
                                for (int i = 0; i <= squareHeight; i++)
                                {
                                    texture = function.ToTexture(type);
                                    rec = new RectangleF(fisrtX, firstY + i * sizeSquare, sizeSquare, sizeSquare);
                                    pictureBox1.CreateGraphics().DrawImage(texture, rec);
                                    world[firstY / sizeSquare + i, fisrtX / sizeSquare] = type;
                                }
                            }
                            if (squareHeight < 0)
                            {
                                for (int i = squareHeight; i <= 0; i++)
                                {
                                    texture = function.ToTexture(type);
                                    rec = new RectangleF(fisrtX, firstY + i * sizeSquare, sizeSquare, sizeSquare);
                                    pictureBox1.CreateGraphics().DrawImage(texture, rec);
                                    world[firstY / sizeSquare + i, fisrtX / sizeSquare] = type;
                                }
                            }
                        }
                    }
                    else if (drawDiagonal)
                    {
                        posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                        posY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
                        lineLenght = posX / sizeSquare - fisrtX / sizeSquare;
                        squareHeight = posY / sizeSquare - firstY / sizeSquare;
                        if (lineLenght < 0)
                            lineSign = -1;
                        else
                            lineSign = 1;
                        if (squareHeight < 0)
                            squareSign = -1;
                        else
                            squareSign = 1;
                        for (int i = 0; i <= Math.Abs(squareHeight); i++)
                        {
                            for (int j = 0; j <= Math.Abs(lineLenght); j++)
                            {
                                texture = function.ToTexture(type);
                                rec = new RectangleF(fisrtX + i * sizeSquare * lineSign, firstY + i * sizeSquare * squareSign, sizeSquare, sizeSquare);
                                pictureBox1.CreateGraphics().DrawImage(texture, rec);
                                world[firstY / sizeSquare + i * squareSign, fisrtX / sizeSquare + i * lineSign] = type;
                            }
                        }
                    }
                    else if (drawSquare)
                    {
                        posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                        posY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
                        lineLenght = posX / sizeSquare - fisrtX / sizeSquare;
                        squareHeight = posY / sizeSquare - firstY / sizeSquare;
                        if (lineLenght < 0)
                            lineSign = -1;
                        else
                            lineSign = 1;
                        if (squareHeight < 0)
                            squareSign = -1;
                        else
                            squareSign = 1;
                        for (int i = 0; i <= Math.Abs(squareHeight); i++)
                        {
                            for (int j = 0; j <= Math.Abs(lineLenght); j++)
                            {
                                texture = function.ToTexture(type);
                                rec = new RectangleF(fisrtX + j * sizeSquare * lineSign, firstY + i * sizeSquare * squareSign, sizeSquare, sizeSquare);
                                pictureBox1.CreateGraphics().DrawImage(texture, rec);
                                world[firstY / sizeSquare + i * squareSign, fisrtX / sizeSquare + j * lineSign] = type;
                            }
                        }
                    }
                }
                else
                {
                    if (drawPoint || type == 1 || type == 2 || type == 100 || type == 50 || type == 51 || type == 52 || type == 53 || type == 200 || type == 3)
                    {
                        if (type == 1 || type == 3)
                        {
                            posX = (e.Location.X) - (e.Location.X) % (2 * sizeSquare);
                            posY = (e.Location.Y) - (e.Location.Y) % (3 * sizeSquare);
                            world[posY / sizeSquare + 1, posX / sizeSquare + 1] = 0;
                            world[posY / sizeSquare + 1, posX / sizeSquare] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 1] = 0;
                            world[posY / sizeSquare + 2, posX / sizeSquare] = 0;
                            world[posY / sizeSquare + 2, posX / sizeSquare + 1] = 0;
                            texture = function.ToTexture(type);
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type;
                        }
                        else if (type == 2)
                        {
                            posX = (e.Location.X) - (e.Location.X) % (sizeSquare);
                            posY = (e.Location.Y) - (e.Location.Y) % (sizeSquare);
                            world[posY / sizeSquare + 1, posX / sizeSquare] = 0;
                            texture = nonPrint.spawnIA;
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type;
                        }
                        else if (type == 100)
                        {
                            posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                            posY = 0;
                            texture = nonPrint.caseText;
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type + nTexte;
                        }
                        else if (type == 200)
                        {
                            posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                            posY = 0;
                            texture = nonPrint.caseMusic;
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type;
                        }
                        else if (type == 50 || type == 51)
                        {
                            posX = (e.Location.X) - (e.Location.X) % (sizeSquare);
                            posY = (e.Location.Y) - (e.Location.Y) % (sizeSquare);
                            world[posY / sizeSquare, posX / sizeSquare + 1] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 2] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 3] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 4] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 5] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 6] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 7] = 0;

                            texture = function.ToTexture(type);
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type;
                        }
                        else if (type == 52 || type == 53)
                        {
                            posX = (e.Location.X) - (e.Location.X) % (sizeSquare);
                            posY = (e.Location.Y) - (e.Location.Y) % (sizeSquare);
                            world[posY / sizeSquare, posX / sizeSquare + 1] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 2] = 0;
                            world[posY / sizeSquare, posX / sizeSquare + 3] = 0;
                            for (int i = 1; i < 7; i++)
                            {
                                world[posY / sizeSquare + i, posX / sizeSquare] = 0;
                                world[posY / sizeSquare + i, posX / sizeSquare + 1] = 0;
                                world[posY / sizeSquare + i, posX / sizeSquare + 2] = 0;
                                world[posY / sizeSquare + i, posX / sizeSquare + 3] = 0;
                            }
                            texture = function.ToTexture(type);
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type;
                        }
                        else
                        {
                            posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                            posY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
                            texture = function.ToTexture(type);
                            rec = new RectangleF(posX, posY, sizeSquare, sizeSquare);
                            pictureBox1.CreateGraphics().DrawImage(texture, rec);
                            world[posY / sizeSquare, posX / sizeSquare] = type;
                        }
                    }
                    else if (full)
                    {
                        texture = function.ToTexture(type);
                        posX = (e.Location.X) - (e.Location.X) % sizeSquare;
                        posY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
                        Remplire(posX / sizeSquare, posY / sizeSquare, world[posY / sizeSquare, posX / sizeSquare]);
                    }
                    else
                    {
                        fisrtX = (e.Location.X) - (e.Location.X) % sizeSquare;
                        firstY = (e.Location.Y) - (e.Location.Y) % sizeSquare;
                        countClick++;
                    }
                }
                pictureBox1.Image = bmp;
                pictureBox1.Location = new System.Drawing.Point(-offsetX, -offsetY);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                if (new Map(world) != null || new Map(world).map != retourSuiv[nMap].map)
                {
                    if (nMap < 19)
                    {
                        nMap++;
                        retourSuiv[nMap] = new Map(world);
                    }
                    else
                    {
                        Map temp;
                        for (int i = 0; i < 19; i++)
                        {
                            temp = retourSuiv[i];
                            retourSuiv[i] = retourSuiv[i + 1];
                            retourSuiv[i + 1] = temp;
                        }
                        retourSuiv[19] = new Map(world);
                    }
                }
                if (nMap - 1 >= 0)
                    annulerToolStripMenuItem.Enabled = true;
                else
                    annulerToolStripMenuItem.Enabled = false;

                if (nBack > 0)
                    retablirToolStripMenuItem.Enabled = true;
                else
                    retablirToolStripMenuItem.Enabled = false;
            }
        }

        #endregion Mouse Click

        #region functions

        private void Remplire(int xArray, int yArray, int caseType)
        {
            pictureBox1.CreateGraphics().DrawImage(texture, xArray * sizeSquare, yArray * sizeSquare);
            world[yArray, xArray] = type;
            if (yArray + 1 < height && world[yArray + 1, xArray] == caseType)
                Remplire(xArray, yArray + 1, caseType);
            if (yArray > 0 && world[yArray - 1, xArray] == caseType)
                Remplire(xArray, yArray - 1, caseType);
            if (xArray + 1 < width && world[yArray, xArray + 1] == caseType)
                Remplire(xArray + 1, yArray, caseType);
            if (xArray > 0 && world[yArray, xArray - 1] == caseType)
                Remplire(xArray - 1, yArray, caseType);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            try
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                        g.DrawImage(grille, x * sizeSquare, y * sizeSquare);
                }
                rec = new RectangleF(0, 0, sizeSquare, sizeSquare);
                int yInit = offsetY / sizeSquare;
                int yEnd = offsetY / sizeSquare + 10 * coef;
                if (yEnd > height)
                    yEnd = height;
                int xInit = offsetX / sizeSquare;
                int xEnd = offsetX / sizeSquare + 20 * coef;
                if (xEnd > width)
                    xEnd = width;

                for (int y = yInit; y < yEnd; y++)
                {
                    for (int x = xInit; x < xEnd; x++)
                    {
                        if (world[y, x] != 0)
                        {
                            if (world[y, x] - (world[y, x] % 10) == 10)
                            {
                                n_texture = function.GrassTest(world, x, y, width);

                                texture = function.ToTexture(n_texture);
                                world[y, x] = n_texture;
                            }
                            else if (world[y, x] >= 200 && world[y, x] < 300)
                                texture = nonPrint.caseMusic;
                            else if (world[y, x] >= 100 && world[y, x] < 300)
                                texture = nonPrint.caseText;
                            else
                                texture = function.ToTexture(world[y, x]);

                            rec.X = x * sizeSquare;
                            rec.Y = y * sizeSquare;

                            if (world[y, x] == 1 || world[y, x] == 3)
                            {
                                Rectangle rec2 = new Rectangle(x * sizeSquare, y * sizeSquare, sizeSquare * 2, sizeSquare * 3);
                                g.DrawImage(texture, rec2);
                            }
                            if (world[y, x] == 2)
                            {
                                texture = nonPrint.spawnIA;
                                Rectangle rec2 = new Rectangle(x * sizeSquare, y * sizeSquare, sizeSquare, sizeSquare * 2);
                                g.DrawImage(texture, rec2);
                            }
                            else if (world[y, x] == 50 || world[y, x] == 51)
                            {
                                Rectangle rec2 = new Rectangle(x * sizeSquare, y * sizeSquare, sizeSquare * 8, sizeSquare);
                                g.DrawImage(texture, rec2);
                            }
                            else if (world[y, x] == 52 || world[y, x] == 53)
                            {
                                Rectangle rec2 = new Rectangle(x * sizeSquare, y * sizeSquare, sizeSquare * 4, sizeSquare * 7);
                                g.DrawImage(texture, rec2);
                            }
                            else
                                g.DrawImage(texture, rec);
                        }
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void effacerToutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    world[y, x] = 0;
                }
            }
            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(-offsetX, -offsetY);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            worldTextes.Clear();
        }

        private void tailleNormalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coef = 1;
            sizeSquare = 75;
            bmp = new Bitmap("./Resources/maptest.bmp");
            width = ((bmp.Width) / 75) + 1;
            height = ((bmp.Height) / 75) + 1;
            type = 0;

            world = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    world[y, x] = type;
            }

            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            filename = "";
            retourSuiv = new Map[20];
            nMap = -1;
            this.panel1.VerticalScroll.Value = 0;
            this.panel1.HorizontalScroll.Value = 0;
        }

        private void soloToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coef = 1;
            sizeSquare = 75;
            bmp = new Bitmap("./Resources/Solo.bmp");
            width = ((bmp.Width) / 75) + 1;
            height = ((bmp.Height) / 75) + 1;

            type = 0;

            world = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    world[y, x] = type;
            }

            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            filename = "";
            retourSuiv = new Map[20];
            nMap = -1;
            this.panel1.VerticalScroll.Value = 0;
            this.panel1.HorizontalScroll.Value = 0;
        }

        private void grandeTailleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            coef = 1;
            sizeSquare = 75;
            bmp = new Bitmap("./Resources/GreatSize.bmp");
            width = ((bmp.Width) / 75) + 1;
            height = ((bmp.Height) / 75) + 1;

            type = 0;

            world = new int[height, width];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    world[y, x] = type;
            }

            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            filename = "";
            retourSuiv = new Map[20];
            nMap = -1;
            this.panel1.VerticalScroll.Value = 0;
            this.panel1.HorizontalScroll.Value = 0;
        }

        #region Boutons

        private void gommeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            texture = grille;
            type = 0;
            n_texture = 0;
        }

        private void herbeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 10;
        }

        private void pierreToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            type = 21;
        }

        private void briqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 22;
        }

        private void dalleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 20;
        }

        private void pointDeSpawnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 1;
        }

        #endregion Boutons

        #endregion functions

        private void enregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[][] convert = function.ToJagged(world);
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".xml";
            saveFileDialog1.ShowDialog();
            filename = saveFileDialog1.FileName;
            string[] file2 = worldTextes.ToArray();
            var tuple = Tuple.Create(file2, convert);
            Pair pair = new Pair(file2, convert);
            Serialize obj = new Serialize();
            obj.texte = file2;
            obj.world = convert;
            if (filename != "")
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Serialize));
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                writer.Serialize(file, obj);
                file.Close();

                /*
                System.Xml.Serialization.XmlSerializer writer2 = new System.Xml.Serialization.XmlSerializer(typeof(string[]));
                System.IO.StreamWriter file3 = new System.IO.StreamWriter(textFile);
                writer2.Serialize(file3, file2);
                file3.Close();*/
            }
            retourSuiv = new Map[20];
            nMap = -1;
        }

        private void ouvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            filename = openFileDialog1.FileName;
            if (filename != "openFileDialog1")
            {
                retourSuiv = new Map[20];
                nTexte = worldTextes.Count() - 1;
                nMap = -1;
                XmlSerializer serializer = new XmlSerializer(typeof(Serialize));
                FileStream fs = new FileStream(filename, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);
                Serialize obj = (Serialize)serializer.Deserialize(reader);
                int[][] intmap = obj.world;
                worldTextes = obj.texte.ToList();
                fs.Close();
                if (intmap[1].Length > 400)
                    soloToolStripMenuItem_Click(sender, e);
                else if (intmap[1].Length > 200)
                    grandeTailleToolStripMenuItem_Click(sender, e);
                else
                    tailleNormalToolStripMenuItem_Click(sender, e);

                world = function.ToMatrice(intmap);
            }
        }

        private void enregistrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (filename != "")
            {
                int[][] convert = function.ToJagged(world);
                string[] file2 = worldTextes.ToArray();
                Serialize obj = new Serialize();
                obj.texte = file2;
                obj.world = convert;
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Serialize));
                System.IO.StreamWriter file = new System.IO.StreamWriter(filename);
                writer.Serialize(file, obj);
                file.Close();
            }
            else
            {
                enregistrerSousToolStripMenuItem_Click(sender, e);
            }
            retourSuiv = new Map[20];
            nMap = -1;
        }

        #region Bouton formes

        private void ligneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawLine = true;
            drawPoint = false;
            drawSquare = false;
            drawDiagonal = false;
            full = false;
        }

        private void pointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawLine = false;
            drawPoint = true;
            drawSquare = false;
            drawDiagonal = false;
            full = false;
        }

        private void carréToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSquare = true;
            drawPoint = false;
            drawLine = false;
            drawDiagonal = false;
            full = false;
        }

        private void diagonaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSquare = false;
            drawPoint = false;
            drawLine = false;
            drawDiagonal = true;
            full = false;
        }

        private void remplissageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawSquare = false;
            drawPoint = false;
            drawLine = false;
            drawDiagonal = false;
            full = true;
        }

        #endregion Bouton formes

        private void annulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nMap - 1 >= 0)
            {
                nMap--;
                nBack++;
                world = retourSuiv[nMap].map;

                pictureBox1.Image = bmp;
                pictureBox1.Location = new System.Drawing.Point(-offsetX, -offsetY);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                annulerToolStripMenuItem.Enabled = true;
            }
            else
                annulerToolStripMenuItem.Enabled = false;
            if (nBack > 0)
                retablirToolStripMenuItem.Enabled = true;
            else
                retablirToolStripMenuItem.Enabled = false;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripLabel1_MouseMove(object sender, MouseEventArgs e)
        {
            mousseY = e.Location.Y;
            mousseX = e.Location.X;

            string text = "x:" + (e.Location.X - e.Location.X % sizeSquare) / sizeSquare + "y:" + (e.Location.Y - e.Location.Y % sizeSquare) / sizeSquare;
            toolStripLabel1.Text = text;
            if (form2.finish)
            {
                worldTextes.Add(form2.richTextBox1.Text);
                nTexte++;
                form2.Hide();
                form2.finish = false;
            }
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                offsetX = e.NewValue;
            if (e.ScrollOrientation == ScrollOrientation.VerticalScroll)
                offsetY = e.NewValue;
            //pictureBox1.Location = new System.Drawing.Point(-offsetX, -offsetY);
        }

        private void retablirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nBack > 0 && retourSuiv[nMap + 1] != null)
            {
                nMap++;
                nBack--;
                world = retourSuiv[nMap].map;

                pictureBox1.Image = bmp;
                pictureBox1.Location = new System.Drawing.Point(-offsetX, -offsetY);
                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                retablirToolStripMenuItem.Enabled = true;
            }
            else
                retablirToolStripMenuItem.Enabled = false;

            if (nMap - 1 >= 0)
                annulerToolStripMenuItem.Enabled = true;
            else
                annulerToolStripMenuItem.Enabled = false;
        }

        private void textcubeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            form2 = new Form2();
            form2.Show();
            type = 100;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e) //+
        {
            this.panel1.HorizontalScroll.Value = 0;
            this.panel1.VerticalScroll.Value = 0;
            if (coef > 1)
                coef--;
            sizeSquare = (int)(75 / coef);
            im = new Size((int)(width * 75 / coef), (int)(height * 75 / coef));
            bmp = new Bitmap(bmp, im);

            offsetY = this.panel1.HorizontalScroll.Value;
            offsetX = this.panel1.VerticalScroll.Value;
            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point((int)(0), (int)(0));
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            rec = new RectangleF(0, 0, sizeSquare, sizeSquare);
        }

        private void zoomToolStripMenuItem1_Click(object sender, EventArgs e) //-
        {
            this.panel1.HorizontalScroll.Value = 0;
            this.panel1.VerticalScroll.Value = 0;
            if (coef < 6)
                coef++;
            sizeSquare = (int)(75 / coef);
            im = new Size((int)(width * 75 / coef), (int)(height * 75 / coef));
            bmp = new Bitmap(bmp, im);

            offsetY = this.panel1.HorizontalScroll.Value;
            offsetX = this.panel1.VerticalScroll.Value;
            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            rec = new RectangleF(0, 0, sizeSquare, sizeSquare);
        }

        private void toolStripSplitButton4_ButtonClick(object sender, EventArgs e)
        {
            enregistrerToolStripMenuItem_Click(sender, e);
        }

        private void restaureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.panel1.HorizontalScroll.Value = 0;
            this.panel1.VerticalScroll.Value = 0;
            coef = 1;
            sizeSquare = (int)(75 / coef);
            im = new Size((int)(width * 75 / coef), (int)(height * 75 / coef));
            bmp = new Bitmap(bmp, im);
            offsetY = this.panel1.HorizontalScroll.Value;
            offsetX = this.panel1.VerticalScroll.Value;
            pictureBox1.Image = bmp;
            pictureBox1.Location = new System.Drawing.Point(0, 0);
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            rec = new RectangleF(0, 0, sizeSquare, sizeSquare);
        }

        private void versLeHautToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 30;
        }

        private void versLeBasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 31;
        }

        private void versLaDroiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 32;
        }

        private void versLaGaucheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 33;
        }

        private void barreMobileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 50;
        }

        private void theDangerZoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 200;
        }

        private void rLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 51;
        }

        private void hBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 52;
        }

        private void basHautToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 53;
        }

        private void soldatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 2;
        }

        private void glace2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 41;
        }

        private void glaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 40;
        }

        private void barilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 54;
        }

        private void videToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 301;
        }

        private void vitessePlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 302;
        }

        private void viToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 303;
        }

        private void munitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 304;
        }

        private void hornetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 305;
        }

        private void bansheeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 306;
        }

        private void laveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 35;
        }

        private void fToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 36;
        }

        private void eauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 37;
        }

        private void echelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 38;
        }

        private void equipe1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 43;
        }

        private void equipe2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 44;
        }

        private void pointDeSpawn2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 3;
        }

        private void glace1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 40;
        }

        private void glace2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            type = 41;
        }

        private void explosifToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 307;
        }

        private void etoileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 308;
        }

        private void munitionsInfiniToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 309;
        }

        private void attaquePlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 310;
        }

        private void defensePlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 311;
        }

        private void plaqueDacierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 36;
        }

        private void ferindestructibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 39;
        }

        private void sableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            type = 24;
        }
    }
}