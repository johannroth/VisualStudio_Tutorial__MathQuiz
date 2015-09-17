using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisualStudio_Tutorial__MathQuiz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Create a new randomizer object.
        Random randomizer = new Random();

        // Integers to store values for additon problem.
        int addend1;
        int addend2;

        // Values for other problems
        int minuend;
        int subtrahend;

        int multiplicand;
        int multiplier;

        int dividend;
        int divisor;

        // This integer keeps track of the remaining time.
        int timeLeft;

        public void StartTheQuiz()
        {
            // Fill addition problem with random values.
            addend1 = randomizer.Next(51);
            addend2 = randomizer.Next(51);      

            // Convert the random values to strings and write them to labels.
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();

            // sum is the name of the NumericUpDown control. Make sure, that
            // when starting the quiz, its value is 0.
            sum.Value = 0;

            // Fill in subtraction problem.
            minuend = randomizer.Next(1, 101);
            subtrahend = randomizer.Next(1, minuend);
            minusLeftLabel.Text = minuend.ToString();
            minusRigthLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            // Fill in the multiplication problem.
            multiplicand = randomizer.Next(2, 11);
            multiplier = randomizer.Next(2, 21);
            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            // Fill in the division problem.
            divisor = randomizer.Next(2, 11);
            int temporaryQuotient = randomizer.Next(2, 11);
            dividend = divisor * temporaryQuotient;
            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;

            // Initialize timers
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                timer1.Stop();
                string timeLeftMessage = "It only took you " + (30 - timeLeft).ToString() + " seconds.";
                MessageBox.Show("You got all the answers right! " + timeLeftMessage, "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                timeLeft--;
                string secondTxt = " seconds";
                timeLabel.Text = timeLeft.ToString() + secondTxt;
            }
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't make it in time!", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }
        }

        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value) &&
                (minuend - subtrahend == difference.Value) && 
                (multiplicand * multiplier == product.Value) && 
                (dividend / divisor == quotient.Value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;
            int lengthOfAnswer = answerBox.Value.ToString().Length;
            answerBox.Select(0, lengthOfAnswer);
        }

        private void sum_ValueChanged(object sender, EventArgs e)
        {
            Console.Beep();
        }
    }
}
