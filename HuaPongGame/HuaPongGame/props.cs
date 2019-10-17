/*
The Props class is an abstract base class which is declar the Draw and Move method that Ball and Paddle both have.
*/
using System;

namespace HuaPongGame
{
    public abstract class Props
    {
        //public field
        public Random random;

        //abstract method
        public abstract void Draw();
        public abstract void Move();

        public virtual void Move(int mousePosition)
        {
            // this virtual Move method is Paddle to call when single player utilize mouse to control Paddle
        }
    }
}
