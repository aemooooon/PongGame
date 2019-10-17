/*
The FormOnePlayer class is game main interface that when user chose one player. User could move mouse between up and down to control paddle.
The class hold the interface and timer1 tick event handler and Check winner.
 */
using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace HuaPongGame
{
    public partial class FormOnePlayer : Form
    {
        #region const
        private const int CURSOR_RANGE = 32;
        private const string MIDDLE_STRING = "Aemooooon";
        private const int MIDDLE_STRING_X =503;
        private const int MIDDLE_STRING_Y =310;
        private const int DRAW_RANGE = 100;
        private const int HALF = 2;
        private const float DASH_RANGE = 6F;
        private const int FONT_SIZE = 24;
        private const string FONT_FAMILY = "Arial";
        #endregion

        #region field
        private Bitmap bitmapCanvas;
        private Bitmap bitmapBall;
        private Bitmap bitmapPaddleLeft;
        private Bitmap bitmapPaddleRight;
        private Graphics bufferGraphics;
        private Graphics graphicsCanvas;
        private Random random;
        private Size boundries;
        private int leftTotalScore;
        private int rightTotalScore;
        private GameEngine gameEngine;
        #endregion

        #region constructor
        public FormOnePlayer()
        {
            InitializeComponent();

            Text = GlobalData.GameTitle;

            //initialization field
            graphicsCanvas = CreateGraphics();
            BackColor = Color.ForestGreen;
            boundries = ClientSize;
            bitmapCanvas = new Bitmap(Width, Height);
            bufferGraphics = Graphics.FromImage(bitmapCanvas);
            bitmapBall = new Bitmap(Properties.Resources.ball);
            bitmapPaddleLeft = new Bitmap(Properties.Resources.LeftPaddle);
            bitmapPaddleRight = new Bitmap(Properties.Resources.RightPaddle);
            random = new Random();
            gameEngine = new GameEngine(bufferGraphics, boundries, random, bitmapBall, bitmapPaddleLeft, bitmapPaddleRight, GlobalData.DifficultyFactor);
        }
        #endregion

        #region timer1 Tick event handler
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (GlobalData.IsGameOver==false) //judge the game whether is over
            {
                bufferGraphics.Clear(BackColor); //clear form background
                gameEngine.RunPaddle();
                gameEngine.ComputerControllPaddle();
                gameEngine.RunBall();
                gameEngine.Collision();

                //draw middle dash line
                Pen pen = new Pen(Color.White, HALF);
                pen.DashPattern = new float[] { DASH_RANGE, DASH_RANGE };
                bufferGraphics.DrawLine(pen, new Point(ClientSize.Width / HALF, 0), new Point(ClientSize.Width / HALF, ClientSize.Height));

                //draw middle cicle
                Pen penCicle = new Pen(Color.White, HALF);
                penCicle.DashPattern = new float[] { HALF, HALF };
                bufferGraphics.DrawEllipse(penCicle, new Rectangle(ClientSize.Width / HALF - DRAW_RANGE, ClientSize.Height / HALF - DRAW_RANGE, DRAW_RANGE * HALF, DRAW_RANGE * HALF));

                //draw string of Aemooooon
                bufferGraphics.DrawString(MIDDLE_STRING, new Font(FONT_FAMILY, FONT_SIZE), new SolidBrush(Color.Green), MIDDLE_STRING_X, MIDDLE_STRING_Y);
                //draw entire canvas
                graphicsCanvas.DrawImage(bitmapCanvas, 0, 0);
                CheckWinner();
            }
        }
        #endregion

        #region MouseMove event handler
        private void FormOnePlayer_MouseMove(object sender, MouseEventArgs e)
        {
            if (gameEngine!=null)
            {
                Cursor.Clip = new Rectangle(Location.X, Location.Y, Size.Width - CURSOR_RANGE, Size.Height - CURSOR_RANGE); //keep the Cursor whthin the Form
                gameEngine.MouseControll(e.Location.Y);
            }
        }
        #endregion

        #region check winner and display the score on screen
        /// <summary>
        /// Display Scores
        /// </summary>
        private void CheckWinner()
        {
            try
            {
                leftTotalScore = gameEngine.GetLeftScoring();
                rightTotalScore = gameEngine.GetRightScoring();

                //show score via PictrueBox
                pictureBoxLeft.Image = Image.FromFile(Application.StartupPath + @"\images\" + leftTotalScore + "-Number.png");
                pictureBoxRight.Image = Image.FromFile(Application.StartupPath + @"\images\" + rightTotalScore + "-Number.png");

                //judge left score
                if (leftTotalScore == GlobalData.FullScore)
                {
                    if (GlobalData.SoundSwitch == true)
                    {
                        using (SoundPlayer player = new SoundPlayer(Properties.Resources.Windows_Logon))
                        {
                            player.Play();
                        }
                    }
                    timer1.Enabled = false;
                    labelLose.Visible = true;
                    GlobalData.IsGameOver = true;
                    dropObject();
                    pbReplay.Visible = true;
                }

                //judge right score
                if (rightTotalScore == GlobalData.FullScore)
                {
                    if (GlobalData.SoundSwitch == true)
                    {
                        using (SoundPlayer player = new SoundPlayer(Properties.Resources.Ring08))
                        {
                            player.Play();
                        }
                    }
                    timer1.Enabled = false;
                    labelWin.Visible = true;
                    GlobalData.IsGameOver = true;
                    dropObject();
                    pbReplay.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        #endregion

        #region form load event handler
        private void FormOnePlayer_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }
        #endregion

        #region Press Space key to pause game
        private void FormOnePlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                timer1.Enabled = !timer1.Enabled;
            }
        }
        #endregion

        #region Destruction of objects method
        /// <summary>
        /// Destruction of objects
        /// </summary>
        private void dropObject()
        {
            bitmapCanvas = null;
            bitmapBall = null;
            bitmapPaddleLeft = null;
            bitmapPaddleRight = null;
            random = null;
            gameEngine = null;
            bufferGraphics = null;
        }
        #endregion

        /// <summary>
        /// Replay game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbReplay_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
