using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MainForm : Form
    {
        const string START_BUTTON = "Start";
        const string RESTART_BUTTON = "Restart";
        const string BOOM_BTN_NAME = "btnBoom_";

        public static Image ImageBomb;
        public static Image ImageFlag;
        private readonly List<int> ExistingMines = new List<int>();
        private int NumberOfMinesSetByUser = 10;
        private int ClickCounter = 0;

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
            nudNumMinas.Value = NumberOfMinesSetByUser;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            NumberOfMinesSetByUser = (int)nudNumMinas.Value;

            if (btnStart.Text == START_BUTTON)
            {
                //Create controls
                CreateControls();

                //Distribute the mines
                GenerateMineField();

                btnStart.Text = RESTART_BUTTON;
            }
            else
            {
                pnlMain.Controls.Clear();
                ExistingMines.Clear();

                this.Text = START_BUTTON;

                CreateControls();
                GenerateMineField();
            }
        }

        private void Button_MouseClick(object sender, MouseEventArgs e)
        {
            //Cast to custom class
            BombButton clickedButton = (BombButton)sender;

            if (e.Button == MouseButtons.Right)
            {
                //Set flag, if is flagged remove
                clickedButton.SetFlagImage();
            }
            else
            {
                if (clickedButton.HasAMine)
                {
                    GameEndedWithBoom();
                }
                else
                {
                    //Set as clicked
                    clickedButton.FlatStyle = FlatStyle.Flat;
                    clickedButton.Enabled = false;

                    //Discover the squares around
                    OpenSquaresAround(clickedButton.Name);

                    //Increase click counter (also know as attempts)
                    ClickCounter++;

                    if (ClickCounter >= 10)
                    {
                        int iNumTilesDesactivatePending = 0;

                        foreach (Button buttonInPanel in pnlMain.Controls)
                        {
                            if (buttonInPanel.Enabled == true)
                            {
                                iNumTilesDesactivatePending++;
                            }
                        }

                        if (iNumTilesDesactivatePending == NumberOfMinesSetByUser)
                        {
                            GameEndedWithWin();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Draw boards
        /// </summary>
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
                BombButton currentButton = new BombButton
                {
                    Name = BOOM_BTN_NAME + i
                };

                //Asign a new event handler
                currentButton.MouseDown += new MouseEventHandler(Button_MouseClick);

                pnlMain.Controls.Add(currentButton);
                Point buttonLocation = new Point(locationA, locationB);

                currentButton.Location = buttonLocation;
                currentButton.Text = string.Empty;
                currentButton.Size = new Size(buttonSize, buttonSize);

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
            Random rnd = new Random();
            for (int i = 0; i < NumberOfMinesSetByUser; i++)
            {
                int iPosA;
                do
                {
                    iPosA = rnd.Next(0, 101);
                }
                while (ExistingMines.Contains(iPosA) == true);

                //Get button by name, to place the mine
                BombButton btnMina = (BombButton)pnlMain.Controls[BOOM_BTN_NAME + iPosA];
                btnMina.HasAMine = true;

                //Add to existing mines
                ExistingMines.Add(iPosA);
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

                    BombButton btnCalc = (BombButton)pnlMain.Controls[BOOM_BTN_NAME + realButtonNumber];

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
                BombButton currentButton = (BombButton)pnlMain.Controls[BOOM_BTN_NAME + numberOfButton];
                currentButton.Text = numberOfMinesAround.ToString();
                currentButton.AdjacentMines = numberOfMinesAround;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error has been found: {ex}");
            }
        }

        /// <summary>
        /// Player wins
        /// </summary>
        public void GameEndedWithWin()
        {
            MessageBox.Show("You win!", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
            pnlMain.Enabled = false;
            btnStart.Text = RESTART_BUTTON;
        }

        /// <summary>
        /// Player loose show all bombs in board
        /// </summary>
        private void GameEndedWithBoom()
        {
            MessageBox.Show("Try again!", "Boom!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            for (int i = 0; i < 100; i++)
            {
                //Set bomb image when the button have mines
                BombButton currentButton = (BombButton)pnlMain.Controls[BOOM_BTN_NAME + i];
                if (currentButton.HasAMine)
                {
                    currentButton.SetBombImage();
                }
            }

            pnlMain.Enabled = false;
            btnStart.Text = RESTART_BUTTON;
        }
    }
}
