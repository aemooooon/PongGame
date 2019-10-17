/*
The GlobalData class is a static class that use to store global variables or configuration option, just own static properties which are some read only and some could set.
And also hold some Enum type like the direction of paddle moving, the factor of game difficulty
*/
using System;
namespace HuaPongGame
{
    /// <summary>
    /// direction of key press
    /// </summary>
    public enum DirectionEnum 
    {
        W,S,Up,Down
    }

    /// <summary>
    /// The factor of game difficulty
    /// </summary>
    public enum Difficults
    {
        easy = 25,
        medium = 30,
        difficult = 35
    }

    //This is static properties collects of global variables, some of them just give read only permission
    public static class GlobalData
    {
        private static int half = 2;
        private static int third = 3;
        private static string direction; //direction such as up and down
        private static bool isGameOver = false; //To stop timer tick flag
        private static int fullScore = 10; //full score
        private static string gameTitle = "Pong Game - 2018 S1 Assignment1 (Aemooooon@gmail.com)"; //game title in each form
        private static bool soundSwitch=true; //the switch of sound effect
        private static int difficultyFactor;

        public static int Half { get => half; }
        public static int Third { get => third; }
        public static bool SoundSwitch { get => soundSwitch; set => soundSwitch = value; }
        public static string GameTitle { get => gameTitle; } 
        public static int DifficultyFactor { get => difficultyFactor; set => difficultyFactor = value; }
        public static int FullScore { get => fullScore;} 
        public static bool IsGameOver { get => isGameOver; set => isGameOver = value; }
        public static string Direction { get => direction; set => direction = value; }
    }
}
