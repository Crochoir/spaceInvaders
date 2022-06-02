using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spaceInvaders
{








    public partial class SpaceInvaders : Form
    {
        bool goUp, goDown, goLeft, goRight;
        bool isPressed;

        int playerScore;

        int bottomBoundary;

        int maxWidth;
        int maxHeight;

        int SpaceShipSpeed = 6;

        int EnemiesOnScreen = 8;

        int speed = 5;


        private void makeBullet()
        {
            PictureBox bullet = new PictureBox();

            bullet.Image = Properties.Resources.bullet1;

            bullet.Size = new Size(5, 20);

            bullet.Tag = "bullet";

            bullet.Left = SpaceShip.Left + SpaceShip.Width / 2;

            bullet.Top = SpaceShip.Top - 20;

            this.Controls.Add(bullet);

            bullet.BringToFront();

        }


        public SpaceInvaders()
        {
            InitializeComponent();

     

        }

        private void SpaceInvaders_Load(object sender, EventArgs e)
        {

        }

        private void SpaceInvaders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }
            if (isPressed)
            {
                isPressed = false;
            }
            
        }


        private void gameOver()
        {
            gameTimer.Stop();
            ScoreText.Text += "game over";
        }


        private void SpaceInvaders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.Space && !isPressed)
            {
                isPressed = true;

                makeBullet();
            }
        }
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // setting directions
            if (goLeft)
            {
                SpaceShip.Left -= SpaceShipSpeed;
            }
            else if (goRight)
            {
                SpaceShip.Left += SpaceShipSpeed;
            }
            else if (goUp)
            {
                SpaceShip.Top -= SpaceShipSpeed;
            }     
            else if(goDown)
            {
                SpaceShip.Top += SpaceShipSpeed;
            }

            if (SpaceShip.Top < 0)
            {
                
            }

            foreach (Control x in this.Controls)
            {

                if (x is PictureBox && x.Tag == "invaders")
                {
                    if (((PictureBox)x).Bounds.IntersectsWith(SpaceShip.Bounds))
                    {
                        gameOver();

                    }

                          ((PictureBox)x).Left += speed;

                    if (((PictureBox)x).Left > 720)
                    {
                        ((PictureBox)x).Top += ((PictureBox)x).Height + 10;

                        ((PictureBox)x).Left = -50;
                    }
                }
            }

            foreach (Control y in this.Controls)
            {
                if (y is PictureBox && y.Tag == "bullet")
                {

                    y.Top -= 20;

                    if (((PictureBox)y).Top < this.Height - 490)
                    {
                        this.Controls.Remove(y);
                    }
                }
            }
            // bullet and enemy collision start
            foreach (Control i in this.Controls)
            {
                foreach (Control j in this.Controls)
                {
                    if (i is PictureBox && i.Tag == "invaders")
                    {
                        if (j is PictureBox && j.Tag == "bullet")
                        {

                            if (i.Bounds.IntersectsWith(j.Bounds))
                            {
                                playerScore++;
                                this.Controls.Remove(i);
                                this.Controls.Remove(j);
                            }
                        }
                    }
                }
            }

            ScoreText.Text = "Score : " + playerScore;

            if (playerScore > EnemiesOnScreen - 1)
            {
                gameOver();
                MessageBox.Show("You Saved Earth");

            }

        }
    }
}
