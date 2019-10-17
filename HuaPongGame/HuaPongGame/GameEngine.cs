/*
 The GameEngine Class that to control the behaviours of Ball and Paddle and pass paremeters via FormConf, FormOnePlayer and FormTwoPlayer to each class
 */
using System;
using System.Drawing;

namespace HuaPongGame
{
    public class GameEngine
    {
        //Field
        private Paddle paddleLeft;
        private Paddle paddleRight;
        private Ball ball;
        private Point paddlePosition;
        private int leftTotalScore;
        private int rightTotalScore;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="boundries"></param>
        /// <param name="random"></param>
        /// <param name="bitmapBall"></param>
        /// <param name="bitmapPaddleLeft"></param>
        /// <param name="bitmapPaddleRight"></param>
        /// <param name="difficultyFactor"></param>
        public GameEngine(Graphics graphics, Size boundries, Random random, Bitmap bitmapBall,Bitmap bitmapPaddleLeft,Bitmap bitmapPaddleRight,int difficultyFactor)
        {
            paddleLeft = new Paddle(graphics, bitmapPaddleLeft, new Point(0, paddlePosition.Y), boundries, random, difficultyFactor);
            paddlePosition.X = boundries.Width - paddleLeft.PaddleWidth;
            paddlePosition.Y = boundries.Height / GlobalData.Half;
            paddleRight = new Paddle(graphics, bitmapPaddleRight, new Point(paddlePosition.X, paddlePosition.Y), boundries, random, difficultyFactor);
            ball = new Ball(graphics, new Point((boundries.Width / GlobalData.Half), (boundries.Height / GlobalData.Half)), new Point(difficultyFactor, difficultyFactor), boundries, bitmapBall,random);
        }

        /// <summary>
        /// Get Collision
        /// </summary>
        public void Collision()
        {
            if ((ball.BallPosition.Y + Ball.SIZE >= paddleLeft.PaddlePosition.Y) 
                && (ball.BallPosition.Y <= paddleLeft.PaddlePosition.Y + paddleLeft.PaddleHeight) 
                && (ball.BallPosition.X <= paddleLeft.PaddlePosition.X + paddleLeft.PaddleWidth) 
                && (ball.BallPosition.X >= paddleLeft.PaddlePosition.X) 
                && (ball.BallPosition.X != paddleLeft.PaddlePosition.X))
            {
                ball.BounceOffPaddle();
            }
            else if (ball.BallPosition.X == paddleLeft.PaddlePosition.X + paddleLeft.PaddleWidth)
            {
                ball.BounceOffPaddleEdge();
            }

            if ((ball.BallPosition.Y + Ball.SIZE >= paddleRight.PaddlePosition.Y) 
                && (ball.BallPosition.Y <= paddleRight.PaddlePosition.Y + paddleRight.PaddleHeight) 
                && (ball.BallPosition.X + Ball.SIZE >= paddleRight.PaddlePosition.X) 
                && (ball.BallPosition.X + Ball.SIZE <= paddleRight.PaddlePosition.X + paddleRight.PaddleWidth) 
                && (ball.BallPosition.X != paddleRight.PaddlePosition.X))
            {
                ball.BounceOffPaddle();
            }
            else if (ball.BallPosition.X + Ball.SIZE == paddleRight.PaddlePosition.X)
            {
                ball.BounceOffPaddleEdge();
            }
        }

        /// <summary>
        /// Control Right paddle by player move mouse
        /// </summary>
        /// <param name="mousePosition">user cursor Y point</param>
        public void MouseControll(int mousePosition)
        {
            paddleRight.Move(mousePosition);
        }

        //Control Left paddle by computer
        public void ComputerControllPaddle()
        {
            if ((paddleLeft.PaddlePosition.Y + paddleLeft.PaddleHeight) < ball.BallPosition.Y)
            {
                paddleLeft.DecreasePaddleHeight();
            }
            if (paddleLeft.PaddlePosition.Y > ball.BallPosition.Y + Ball.SIZE)
            {
                paddleLeft.IncreasePaddleHeight();
            }
        }

        //Control Paddle via press key of S, W, Up, Down
        public void KeyControllPaddle()
        {
            if (GlobalData.Direction == Enum.GetName(typeof(DirectionEnum),0)
                || GlobalData.Direction == Enum.GetName(typeof(DirectionEnum),1))
            {
                paddleLeft.Move();
            }
            if (GlobalData.Direction == Enum.GetName(typeof(DirectionEnum),2)
                || GlobalData.Direction == Enum.GetName(typeof(DirectionEnum),3))
            {
                paddleRight.Move();
            }
        }

        public void RunPaddle()
        {
            paddleLeft.Draw();
            paddleRight.Draw();
        }

        public void RunBall()
        {
            ball.CheckPoint();
            ball.Move();
            ball.BounceBall();
            ball.Draw();
        }

        //To calculate score of each side
        public void CalculateScore()
        {
            if (ball.LeftScore == true && ball.RightScore == false)
            {
                if (leftTotalScore>=0&& leftTotalScore < 10) //judge Score out of index
                {
                    leftTotalScore++;
                }
                ball.LeftScore = false;
            }
            if (ball.RightScore == true && ball.LeftScore == false)
            {
                if (rightTotalScore >= 0 && rightTotalScore < 10) //judge Score out of index
                {
                    rightTotalScore++;
                }
                ball.RightScore = false;
            }
        }

        public int GetLeftScoring()
        {
            CalculateScore();
            return leftTotalScore;
        }

        public int GetRightScoring()
        {
            CalculateScore();
            return rightTotalScore;
        }
    }
}
