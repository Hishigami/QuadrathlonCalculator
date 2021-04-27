using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuadrathlonCalculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //End goal
        private const int goal = 1500;
        //Content point values
        private const int dom = 42;
        private const int bunton = 24;
        private const int rook = 30;
        private const int jin = 50;
        private const int bist = 46;

        public MainWindow()
        {
            InitializeComponent();
            //Set defaults for combo boxes
            cmbTime.SelectedIndex = 0;
            cmbHard.SelectedIndex = 0;
        }

        //Compute for Pandemonium Rift
        private void rift_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Pandemonium Rift";
            calc(mode);
        }

        //Compute for Anton raid
        private void anton_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Anton";
            calc(mode);
        }

        //Compute for Pandemonium Rift
        private void luke_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Luke";
            calc(mode);
        }

        //Compute for Pandemonium Rift
        private void hard_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Hard Luke";
            calc(mode);
        }

        //Compute for Pandemonium Rift
        private void beast_Click(object sender, RoutedEventArgs e)
        {
            string mode = "Beast";
            calc(mode);
        }

        //Driver function
        private void calc(string mode)
        {
            //Blanket null value check
            if (intPoints.Value == null || intRift.Value == null || intAnton.Value == null || intLuke == null || intBeast == null)
            {
                txtOutput.Text = ("A blank field was detected.\n" +
                    "Please fill in the blank(s) with a value.");
                return;
            }
            
            //Variable definitions
            int netTotal = (int)(goal - intPoints.Value);
            int weeks = int.Parse(cmbTime.Text);
            int rift = (int)(intRift.Value);
            int anton = (int)(intAnton.Value);
            int luke = (int)(intLuke.Value);
            int hardLuke = int.Parse(cmbHard.Text);
            int beast = (int)(intBeast.Value);
            string output;

            /* Main switch case body
             * In all cases, calculate the user's future point yield based on what they're running right now.
             * Then, we divide it given their remaining time, and the content's point yield per character.
             * We tell the user they don't need to run that content if the numerator is negative; otherwise give them the resulting value.
             * Note that all values are truncated due to being ints; thus, each result should give or take another character in that content at worst.
            */
            switch (mode)
            {
                case "Pandemonium Rift":
                    rift = (netTotal - anton * bunton * weeks - luke * rook * weeks - hardLuke * jin * weeks - beast * bist * weeks)/dom * weeks;
                    if (rift <= 0)
                        output = "You don't need to run " + mode + "!";
                    else
                        output = "You need to run " + mode + " on " + rift + " character(s) to max out the event.";
                    break;

                case "Anton":
                    anton = (netTotal - rift * dom * weeks - luke * rook * weeks - hardLuke * jin * weeks - beast * bist * weeks) / bunton * weeks;
                    if (anton <= 0)
                        output = "You don't need to run " + mode + "!";
                    else
                        output = "You need to run " + mode + " on " + anton + " character(s) to max out the event.";
                    break;

                case "Luke":
                    luke = (netTotal - rift * dom * weeks - anton * bunton * weeks - hardLuke * jin * weeks - beast * bist * weeks) / rook * weeks;
                    if (luke <= 0)
                        output = "You don't need to run " + mode + "!";
                    else
                        output = "You need to run " + mode + " on " + luke + " character(s) to max out the event.";
                    break;

                case "Hard Luke":
                    hardLuke = (netTotal - rift * dom * weeks - anton * bunton * weeks - luke * rook * weeks - beast * bist * weeks) / jin * weeks;
                    if (hardLuke <= 0)
                        output = "You don't need to run " + mode + "!";
                    //Exception case for Hard Luke given the strict 6 runs/week limit
                    else if (hardLuke > 3)
                    {
                        output = "It appears you need more than 3 characters running " + mode + ", but you can only run it up to 6 times per week. "
                            + "Please consider running more characters into other content.";
                    }
                    else
                        output = "You need to run " + mode + " on " + hardLuke + " character(s) to max out the event.";
                    break;

                case "Beast":
                    beast = (netTotal - rift * dom * weeks - anton * bunton * weeks - luke * rook * weeks - hardLuke * jin * weeks) / bist * weeks;
                    if (beast <= 0)
                        output = "You don't need to run " + mode + "!";
                    else
                        output = "You need to run " + mode + " on " + beast + " character(s) to max out the event.";
                    break;

                default:
                    output = "Testing. Or rather, something went wrong.";
                    break;
            }

            //Display resulting message in text box
            txtOutput.Text = output;
        }

        //Revert all fields to default values
        private void reset_Click(object sender, RoutedEventArgs e)
        {
            intPoints.Value = 0;
            cmbTime.SelectedIndex = 0;

            intRift.Value = 0;
            intAnton.Value = 0;
            intLuke.Value = 0;
            cmbHard.SelectedIndex = 0;
            intBeast.Value = 0;

            txtOutput.Text = ("All fields have been reset to their defaults.");
        }

        //Bring up help
        private void help_Click(object sender, RoutedEventArgs e)
        {
            txtOutput.Text = ("Enter your points, remaining time, and four of the content runners " +
                "before calculating characters for target content.\n" +
                "Runs are assumed to be successful and max out their daily/weekly limits.\n" +
                "When in doubt, run another character into any content.\n" +
                "Based on DFOG's Queen of Skardi's Quadrathlon event.");
        }
    }
}
