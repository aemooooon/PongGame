/* Program name: 	    <<HuaPongGame>>
   Project file name:   <<HuaPongGame>>
   Author:		        <<Hua WANG>>
   Date:	            28-04-2018 
   Language:		    C#
   Platform:		    Microsoft Visual Studio 2017
   Purpose:		        <<The HuaPonegame is two-dimensional game that simulates table tennis.>>
   Description:		    <<In this game, the player could choose easy, medium and difficult, and also could
                        choose single player or double player. User coulde click the sound switch to turn on
                        or turn off the game sound effects. When user chose single player to paly, they could 
                        use mouse to control a paddle that moves vertically.If the user chose double player, 
                        the left side player could via press W, S key to control. The paddle move up and down, 
                        the right side player via press Up arror key and Down arror key to move the right side paddle.
                        on the right side of the game area, and competes against a computer control
                        paddle on the left hand side. If the ball moves off the side of the game area, 
                        the opponent earns a point. The aim of the game is for the player to gain 10 points
                        before the computer. If the player gain one score, after that the direction of the
                        ball will change the direction of the ball, Vice versa.>>
   Known Bugs:		    << >>
   Additional Features: 1. add sound when user trigger event and could turn on or turn off the sound.
                        2. Allow for two players that use keyboard and mouse to control paddle of left and right side.
                        3. Provide the white-space that be press to pause the game.
                        4. User could choose the easy or difficult of game.
                        5. In different difficulty, the speed of ball, the size of paddle, the speed of paddle are all different.
*/
using System;
using System.Media;
using System.Windows.Forms;

namespace HuaPongGame
{
    public partial class FormConf : Form
    {
        //Constructor
        public FormConf()
        {
            InitializeComponent();

            Text = GlobalData.GameTitle; //give value to Form.Text from GlobalData properties
            pictureBox3.Image = Properties.Resources.VolumeOn; //give default value of the pictureBox3 which is sound switch picture
        }

        #region PictureBox Click event handler
        //game start pictureBox1 event handler
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //judge the factor of game difficult or easy and get the value to DifficultyFactor property
            if (radioButton1.Checked == true)
            {
                GlobalData.DifficultyFactor = Convert.ToInt16(radioButton1.Tag);
            }
            else if (radioButton2.Checked == true)
            {
                GlobalData.DifficultyFactor = Convert.ToInt16(radioButton2.Tag);
            }
            else if (radioButton3.Checked == true)
            {
                GlobalData.DifficultyFactor = Convert.ToInt16(radioButton3.Tag);
            }

            //if the one player be selected, to show the FromOnePlayer
            if (radioButton4.Checked == true)
            {
                FormOnePlayer formOnePlayer = new FormOnePlayer(); //instailization formOnePlayer
                Hide();
                formOnePlayer.ShowDialog(); //display formOnePlayer
                Application.Exit(); //Exit application include two form
            }

            //if the Two player be selected, to show the FromTwoPlayer
            if (radioButton5.Checked == true)
            {
                FormTwoPlayer formTwoPlayer = new FormTwoPlayer(); //instailization formTwoPlayer
                Hide();
                formTwoPlayer.ShowDialog(); //display formTwoPlayer
                Application.Exit(); //Exit application include two form
            }
        }


        //judge the Sound Switch on or off
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //user click pictureBox to change the sound switch and change its Image
            GlobalData.SoundSwitch = !GlobalData.SoundSwitch;
            if (GlobalData.SoundSwitch == true)
            {
                pictureBox3.Image = Properties.Resources.VolumeOn;
            }
            else
            {
                pictureBox3.Image = Properties.Resources.VolumeOff;
            }
        }
        #endregion

        #region PictrueBox Mouse Hover special effects
        //pictureBox1 Mouse Hover event handler, to give the sound effict
        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            if (GlobalData.SoundSwitch == true)
            {
                using (SoundPlayer player = new SoundPlayer(Properties.Resources.Speech_On))
                {
                    player.Play();
                }
            }
        }


        //pictureBox3 Mouse Hover event handler, to give the sound effict
        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            if (GlobalData.SoundSwitch == true)
            {
                using (SoundPlayer player = new SoundPlayer(Properties.Resources.Speech_On))
                {
                    player.Play();
                }
            }
        }
        #endregion
    }
}
