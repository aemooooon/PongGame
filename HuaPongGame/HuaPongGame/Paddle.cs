/*
 The Paddle Class hold a various of method that to Create Paddle, Draw Paddle, Move Paddle under the different condition.
 */
using System;
using System.Drawing;

namespace HuaPongGame
{
    public class Paddle:Props
    {
        //Const
        private const int BOUNCE_RANGE_MIN = 10; //The minimal range of bounce moveing 

        //define 3 sets of paddle size 
        private const int SMALL_PADDLE_WIDTH = 30; 
        private const int SMALL_PADDLE_HEIGHT = 180;
        private const int MEDIUM_PADDLE_WIDTH = 38;
        private const int MEDIUM_PADDLE_HEIGHT = 230;
        private const int BIG_PADDLE_WIDTH = 47;
        private const int BIG_PADDLE_HEIGHT = 280;

        //Field
        private int paddleWidth;
        private int paddleHeight;
        private Graphics graphics;
        private Bitmap bitmapPaddle;
        private Point paddlePosition;
        private Size boundries;

        //Constructor
        /// <summary>
        /// Paddle class constructor
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="bitmapPaddle"></param>
        /// <param name="paddlePosition"></param>
        /// <param name="boundries"></param>
        /// <param name="random"></param>
        /// <param name="difficultyFactor"></param>
        public Paddle(Graphics graphics, Bitmap bitmapPaddle, Point paddlePosition, Size boundries, Random random, int difficultyFactor)
        {
            //via parameter difficultyFactor to judge what size to suit the paddle
            switch (difficultyFactor)
            {
                case (int)Difficults.easy:
                    PaddleWidth =BIG_PADDLE_WIDTH;
                    PaddleHeight =BIG_PADDLE_HEIGHT;
                    break;
                case (int)Difficults.medium:
                    PaddleWidth = MEDIUM_PADDLE_WIDTH;
                    PaddleHeight = MEDIUM_PADDLE_HEIGHT;
                    break;
                case (int)Difficults.difficult:
                    PaddleWidth =SMALL_PADDLE_WIDTH;
                    PaddleHeight =SMALL_PADDLE_HEIGHT;
                    break;
                default:
                    break;
            }
            this.graphics = graphics;
            this.bitmapPaddle = bitmapPaddle;
            this.paddlePosition = paddlePosition;
            this.boundries = boundries;
            this.random = random;
        }

        /// <summary>
        /// Draw paddle
        /// </summary>
        public override void Draw()
        {
            graphics.DrawImage(bitmapPaddle, paddlePosition.X, paddlePosition.Y, PaddleWidth, PaddleHeight);
        }

        /// <summary>
        /// Move paddle
        /// </summary>
        public override void Move()
        {
            //up direction
            if (GlobalData.Direction==Enum.GetName(typeof(DirectionEnum),0)
                || GlobalData.Direction == Enum.GetName(typeof(DirectionEnum), 2))
            {
                if (paddlePosition.Y<=paddleHeight)
                {
                    paddlePosition.Y = 0;
                }
                else
                {
                    paddlePosition.Y = paddlePosition.Y - paddleHeight;
                }
            }

            //down direction
            if (GlobalData.Direction == Enum.GetName(typeof(DirectionEnum), 1) 
                || GlobalData.Direction == Enum.GetName(typeof(DirectionEnum), 3))
            {
                if (paddlePosition.Y>=boundries.Height-paddleHeight)
                {
                    paddlePosition.Y = boundries.Height - paddleHeight;
                }
                else
                {
                    if (boundries.Height - paddleHeight - paddlePosition.Y < paddleHeight)
                    {
                        paddlePosition.Y = paddlePosition.Y + (boundries.Height - paddleHeight - paddlePosition.Y);
                    }
                    else
                    {
                        paddlePosition.Y = paddlePosition.Y + paddleHeight;
                    }
                }
            }
        }

        /// <summary>
        /// Move paddle by mouse move
        /// </summary>
        /// <param name="mousePosition">mouse position Y from form</param>
        public override void Move(int mousePosition)
        {
            if ((mousePosition < 0) || (mousePosition > (boundries.Height - PaddleHeight)))
            {
                if (mousePosition < 0)
                {
                    paddlePosition.Y = 0;
                }
                else
                {
                    paddlePosition.Y = boundries.Height - PaddleHeight;
                }
            }
            else
            {
                paddlePosition.Y = mousePosition;
            }
        }

        /// <summary>
        /// Move paddle down
        /// </summary>
        public void DecreasePaddleHeight()
        {
            //Avoid paddle over the lower bound
            if (boundries.Height - paddleHeight - paddlePosition.Y < paddleHeight)
            {
                paddlePosition.Y = paddlePosition.Y + (boundries.Height - paddleHeight - paddlePosition.Y);
            }
            else
            {
                paddlePosition.Y = paddlePosition.Y + random.Next(BOUNCE_RANGE_MIN, paddleHeight);
                //switch (GlobalData.DifficultyFactor)
                //{
                //    case (int)Difficults.easy:
                //        paddlePosition.Y = paddlePosition.Y + random.Next(BOUNCE_RANGE_MIN, paddleHeight);
                //        break;
                //    case (int)Difficults.medium:
                //        paddlePosition.Y = paddlePosition.Y + MEDIUM_PADDLE_HEIGHT / GlobalData.Half / GlobalData.Half;
                //        break;
                //    case (int)Difficults.difficult:
                //        paddlePosition.Y = paddlePosition.Y + random.Next(BOUNCE_RANGE_MIN* GlobalData.Half* GlobalData.Half, SMALL_PADDLE_HEIGHT * GlobalData.Half);
                //        break;
                //    default:
                //        break;
                //}
            }
        }

        /// <summary>
        /// Move paddle up
        /// </summary>
        public void IncreasePaddleHeight()
        {
            //Avoid paddle over the upper bound
            if (paddlePosition.Y <= paddleHeight)
            {
                paddlePosition.Y = 0;
            }
            else
            {
                //paddlePosition.Y = paddlePosition.Y - random.Next(BOUNCE_RANGE_MIN, paddleHeight);
                switch (GlobalData.DifficultyFactor)
                {
                    case (int)Difficults.easy:
                        paddlePosition.Y = paddlePosition.Y - BIG_PADDLE_HEIGHT / GlobalData.Half / GlobalData.Half;
                        break;
                    case (int)Difficults.medium:
                        paddlePosition.Y = paddlePosition.Y - MEDIUM_PADDLE_HEIGHT / GlobalData.Half;
                        break;
                    case (int)Difficults.difficult:
                        paddlePosition.Y = paddlePosition.Y - random.Next(BOUNCE_RANGE_MIN* GlobalData.Half* GlobalData.Half, SMALL_PADDLE_HEIGHT*GlobalData.Half* GlobalData.Half);
                        break;
                    default:
                        break;
                }
            }
        }

        //Properties
        public Point PaddlePosition { get => paddlePosition; set => paddlePosition = value; }
        public int PaddleWidth { get => paddleWidth; set => paddleWidth = value; }
        public int PaddleHeight { get => paddleHeight; set => paddleHeight = value; }
    }
}
