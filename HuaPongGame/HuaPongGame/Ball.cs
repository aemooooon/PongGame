/*
 The Ball Class that to Create Ball, Draw Ball, Move Ball, Bounce edge of Paddle, Form and Check Score.
 */
using System;
using System.Drawing;
using System.Media;
using System.Threading;

namespace HuaPongGame
{
    public class Ball:Props
    {
        //Const
        public const int SIZE = 50;
        private const int SLEEP_TIME = 300;

        //Field
        private Point ballPosition;
        private Point velocity;
        private Size boundries;
        private Graphics graphics;
        private bool leftScore;
        private bool rightScore;
        private Bitmap bitmapBall;
        private SoundPlayer playerWindows_Information_Bar;
        private SoundPlayer Windows_Hardware_Fail;
        private SoundPlayer playertada;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics">The instance of Graphics</param>
        /// <param name="ballPosition">The position of Ball</param>
        /// <param name="velocity">The speed of ball</param>
        /// <param name="boundries">Form interface size</param>
        /// <param name="bitmapBall">The ball picture</param>
        /// <param name="random">The instance of Random</param>
        public Ball(Graphics graphics, Point ballPosition, Point velocity, Size boundries, Bitmap bitmapBall,Random random)
        {
            this.random = random;
            this.graphics = graphics;
            this.ballPosition = ballPosition;
            this.velocity = velocity;
            this.boundries = boundries;
            this.bitmapBall = bitmapBall;
            leftScore = false;
            rightScore = false;

            playerWindows_Information_Bar = new SoundPlayer(Properties.Resources.Windows_Information_Bar);
            Windows_Hardware_Fail = new SoundPlayer(Properties.Resources.Windows_Hardware_Fail);
            playertada = new SoundPlayer(Properties.Resources.tada);
        }

        /// <summary>
        /// Draw ball
        /// </summary>
        public override void Draw()
        {
            graphics.DrawImage(bitmapBall, ballPosition.X, ballPosition.Y, SIZE, SIZE);
        }

        /// <summary>
        /// Move ball
        /// </summary>
        public override void Move()
        {
            if (velocity.Y == 0||velocity.X==0) //avoid the ball move on vertical or horizontal direction
            {
                velocity.Y += SIZE;
                velocity.X += SIZE;
            }
            ballPosition.X = ballPosition.X + velocity.X;
            ballPosition.Y = ballPosition.Y + velocity.Y - velocity.Y / GlobalData.Third; //change the direction of the ball in horizontal
        }

        /// <summary>
        /// Bounce ball
        /// </summary>
        public void BounceBall()
        {
            if ((ballPosition.Y < 0) || ((ballPosition.Y + SIZE) > boundries.Height))
            {
                if (GlobalData.SoundSwitch == true)
                {
                    using (playerWindows_Information_Bar)
                    {
                        playerWindows_Information_Bar.Play();
                    }
                }
                velocity.Y = -(velocity.Y);
            }
        }

        /// <summary>
        /// Bounce paddle
        /// </summary>
        public void BounceOffPaddle()
        {
            if (GlobalData.SoundSwitch == true)
            {
                using (playerWindows_Information_Bar)
                {
                    playerWindows_Information_Bar.Play();
                }
            }
            velocity.X = -(velocity.X);
        }

        /// <summary>
        /// Bounce paddle edge
        /// </summary>
        public void BounceOffPaddleEdge()
        {
            if (GlobalData.SoundSwitch == true)
            {
                using (playerWindows_Information_Bar)
                {
                    playerWindows_Information_Bar.Play();
                }
            }
            velocity.Y = -(velocity.Y);
        }

        /// <summary>
        /// Check and set score and start next round
        /// </summary>
        public void CheckPoint()
        {
            if (ballPosition.X + SIZE > boundries.Width)
            {
                if (GlobalData.SoundSwitch == true)
                {
                    using (Windows_Hardware_Fail)
                    {
                        Windows_Hardware_Fail.Play();
                    }
                }
                leftScore = true;
                Thread.Sleep(SLEEP_TIME);
                ballPosition.X = boundries.Width / GlobalData.Half;
                ballPosition.Y = boundries.Height / GlobalData.Half;
                velocity.X = -velocity.X; // if the player win 1 score, the ball will go to the direction of player
                if (random.Next(0, GlobalData.Half) ==0) //judge direction on Y
                {
                    velocity.Y = -velocity.Y;
                }
                else
                {
                    velocity.Y = velocity.Y;
                }
            }
            if (ballPosition.X < 0)
            {
                if (GlobalData.SoundSwitch == true)
                {
                    using (playertada)
                    {
                        playertada.Play();
                    }
                }
                rightScore = true;
                Thread.Sleep(SLEEP_TIME);
                ballPosition.X = boundries.Width / GlobalData.Half;
                ballPosition.Y = boundries.Height / GlobalData.Half;
                velocity.X = -velocity.X; // if the player win 1 score, the ball will go to the direction of player
                if (random.Next(0, GlobalData.Half) == 0)
                {
                    velocity.Y = -velocity.Y;
                }
                else
                {
                    velocity.Y = velocity.Y;
                }
            }
        }

        //Properties
        public Point BallPosition { get => ballPosition; set => ballPosition = value; }
        public bool LeftScore { get => leftScore; set => leftScore = value; }
        public bool RightScore { get => rightScore; set => rightScore = value; }
    }
}
