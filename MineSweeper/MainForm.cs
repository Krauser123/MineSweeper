﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MainForm : Form
    {
        public static Image ImageBomb;
        public static Image ImageFlag;
        private readonly List<int> existingMines = new List<int>();
        private int numberOfMinesSetByUser = 10;
        private int clickCounter = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Assembly mineSweeperAssembly = Assembly.GetExecutingAssembly();
            Stream streamBomb = mineSweeperAssembly.GetManifestResourceStream("MineSweeper.bomb.png");
            Stream streamFlag = mineSweeperAssembly.GetManifestResourceStream("MineSweeper.flag.png");
            ImageBomb = Image.FromStream(streamBomb);
            ImageFlag = Image.FromStream(streamFlag);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            numberOfMinesSetByUser = (int)nudNumMinas.Value;

            if (btnStart.Text == "Start")
            {
                //Create controls
                CreateControls();

                //Distribute the mines
                GenerateMineField();

                btnStart.Text = "Restart";
            }
            else
            {
                pnlMain.Controls.Clear();
                existingMines.Clear();

                this.Text = "Start";

                CreateControls();
                GenerateMineField();
            }
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            //Cast to custom class
            BombButton button = (BombButton)sender;

            if (e.Button == MouseButtons.Right)
            {
                //Set flag, if is flagged remove
                button.SetFlagImage();
            }
            else
            {

                if (button.HasAMine)
                {
                    GameEndedWithBoom();
                }
                else
                {
                    //Set as clicked
                    button.FlatStyle = FlatStyle.Flat;
                    button.Enabled = false;

                    //Discover the squares around
                    OpenSquaresAround(button.Name);

                    //Increase click counter (also know as attempts)
                    clickCounter++;

                    if (clickCounter >= 10)
                    {
                        int iNumDesac = 0;

                        foreach (Button buttonInPanel in pnlMain.Controls)
                        {
                            if (buttonInPanel.Enabled == false)
                            {
                                iNumDesac++;
                            }
                        }

                        if (iNumDesac == 100)
                        {
                            GameEndedWithWin();
                        }
                    }
                }
            }
        }


        public void CreateControls()
        {
            //Active the panel
            pnlMain.Enabled = true;

            int panelSize = pnlMain.Size.Width;
            int increaseSize = 50;

            //Size of each button
            int buttonSize = 50;

            //Set number of control by row
            int iCount = panelSize / increaseSize;
            int iCountParcial = 0;

            int locationA = 0;
            int locationB = 0;


            for (int i = 0; i <= 100; i++)
            {
                BombButton button = new BombButton
                {
                    Name = "btnBoom_" + i
                };

                //Asign a new event handler
                button.MouseDown += new MouseEventHandler(Button_MouseClick);

                pnlMain.Controls.Add(button);
                Point buttonLocation = new Point(locationA, locationB);

                button.Location = buttonLocation;
                button.Text = string.Empty;
                button.Size = new Size(buttonSize, buttonSize);

                //Increase counter
                iCountParcial++;

                //Move the location point
                if (iCountParcial < iCount)
                {
                    locationA += increaseSize;
                }
                else
                {
                    iCountParcial = 0;
                    locationA = 0;
                    locationB += increaseSize;
                }
            }
        }

        public void GenerateMineField()
        {
            Random rndAlet = new Random();
            for (int i = 0; i < numberOfMinesSetByUser; i++)
            {

                int iPosA;
                do
                {
                    iPosA = rndAlet.Next(0, 101);
                }
                while (existingMines.Contains(iPosA) == true);

                //Get button by name, to place the mine
                BombButton btnMina = (BombButton)pnlMain.Controls["btnBoom_" + iPosA];
                btnMina.HasAMine = true;

                //Add to existing mines
                existingMines.Add(iPosA);
            }
        }


        /// <summary>
        /// Calc and mark the mines around
        /// </summary>
        /// <param name="buttonName"></param>
        public void OpenSquaresAround(string buttonName)
        {
            int[] buttonsForCalc = new int[] { -11, -10, -9, -1, 1, 9, 10, 11 };

            int numberOfButton = Convert.ToInt32(buttonName.Substring(8));

            //Left border
            if ((numberOfButton % 10) == 0 || numberOfButton == 0)
            {
                //Clean the array
                Array.Clear(buttonsForCalc, 0, buttonsForCalc.Length);
                buttonsForCalc = new int[] { -10, -9, 1, 10, 11 };
            }

            //Right border
            if (buttonName.Contains("9") && numberOfButton != 90)
            {
                Array.Clear(buttonsForCalc, 0, buttonsForCalc.Length);
                buttonsForCalc = new int[] { -11, -10, -1, 9, 10 };
            }

            int numberOfMinesAround = 0;

            try
            {
                foreach (int numToSubstract in buttonsForCalc)
                {
                    //Substract the number from the origin button (numberOfButton)
                    int realButtonNumber = numberOfButton + (numToSubstract);

                    BombButton btnCalc = (BombButton)pnlMain.Controls["btnBoom_" + realButtonNumber];

                    //If null not exists and continues with other
                    if (btnCalc != null)
                    {
                        if (btnCalc.HasAMine)
                        {
                            numberOfMinesAround++;
                        }
                        else
                        {
                            //If we don't have mines, we disable the button
                            btnCalc.FlatStyle = FlatStyle.Flat;
                            btnCalc.Enabled = false;
                        }
                    }
                }

                //Set the number of mines around
                BombButton currentButton = (BombButton)pnlMain.Controls["btnBoom_" + numberOfButton];
                currentButton.Text = numberOfMinesAround.ToString();
                currentButton.AdjacentMines = numberOfMinesAround;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has been found: {ex}");
            }
        }


        public void GameEndedWithWin()
        {
            MessageBox.Show("You win!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlMain.Enabled = false;
            btnStart.Text = "Restart";

        }

        private void GameEndedWithBoom()
        {
            MessageBox.Show("Try again!", "Boom!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            for (int i = 0; i < 100; i++)
            {
                //Set bomb image when the button have mines
                BombButton currentButton = (BombButton)pnlMain.Controls["btnBoom_" + i];
                if (currentButton.HasAMine)
                {
                    currentButton.SetBombImage();
                }
            }

            pnlMain.Enabled = false;
            btnStart.Text = "Restart";
        }

    }
}