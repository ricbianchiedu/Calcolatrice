using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calcolatrice
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        static double result = 0.0;
        static int state = 0;
        string lastOperator = "";
        bool alreadyDot = false;
        /*
            state = 0  -> Stato iniziale/Attesa input 
            state = 1  -> Inserimento operatore
            state = 2  -> Premuto tasto operazione
            state = 3  -> Errore
        */
        public MainPage()
        {
            InitializeComponent();
        }

        private void btn_Click(System.Object sender, System.EventArgs e)
        {
            Button b = (Button)sender;

            if (state == 0)
            {
                //First key dot
                if (b.Text == "." && b.Text == "0")
                {
                    lblDisplay.Text += b.Text;
                    alreadyDot = true;
                }
                else if(b.Text == "." && b.Text != "0")
                {
                    lblDisplay.Text = "0" + b.Text;
                    alreadyDot = true;

                }
                else
                {
                    lblDisplay.Text = b.Text;
                }

                state = 1;
            }
            else if (state == 1)
            {
                if (b.Text == "." && alreadyDot == false)
                {
                    lblDisplay.Text += b.Text;
                    alreadyDot = true;
                }
                else if (b.Text != "." && alreadyDot == true)
                {
                    lblDisplay.Text += b.Text;
                }

            }
            else if (state == 3)
            {
                //First key dot
                if (b.Text == ".")
                {
                    lblDisplay.Text = "0.";
                }
                else
                {
                    lblDisplay.Text = b.Text;
                }
                state = 1;
            }
        }

        private void btnOperator_Click(System.Object sender, System.EventArgs e)
        {
            Button b = sender as Button;

            if (lastOperator == "")
            {
                //First op key pressed
                lastOperator = b.Text;
                result = Convert.ToDouble(lblDisplay.Text);
                state = 0;
            }
            else
            {
                switch (lastOperator)
                {
                    case "+":
                        {
                            double tmp = Convert.ToDouble(lblDisplay.Text);
                            result = result + Convert.ToDouble(lblDisplay.Text);
                            lblDisplay.Text = result.ToString();
                            lastOperator = b.Text;
                        }
                        alreadyDot = false;
                        state = 0;
                        break;

                    case "-":
                        {
                            result = result - Convert.ToDouble(lblDisplay.Text);
                            lblDisplay.Text = result.ToString();
                            lastOperator = b.Text;
                        }
                        alreadyDot = false;
                        state = 0;
                        break;

                    case "x":
                        {
                                result = result * Convert.ToDouble(lblDisplay.Text);
                                lblDisplay.Text = result.ToString();
                                lastOperator = b.Text;
                        }
                        alreadyDot = false;
                        state = 0;
                        break;

                    case "/":
                        {
                            if (lblDisplay.Text == "0")
                            {
                                lblDisplay.Text = "Err: Div by zero";
                                state = 3;
                            }
                            else
                            {
                                result = result / Convert.ToDouble(lblDisplay.Text);
                                lblDisplay.Text = result.ToString();
                                lastOperator = b.Text;
                                alreadyDot = false;
                                state = 0;
                            }

                        }
                        break;

                    default:
                        break;
                }

                //Handling = key
                if (b.Text == "=")
                {
                    lastOperator = "";
                    alreadyDot = false;
                    state = 0;
                }                
            }
        }

        void btnAC_Clicked(System.Object sender, System.EventArgs e)
        {
            //Reset Calculator
            lblDisplay.Text = "0";
            result = 0;
            state = 0;
            lastOperator = "";
            alreadyDot = false;
        }

        void btnC_Clicked(System.Object sender, System.EventArgs e)
        {
            //Clear last number
            lblDisplay.Text = "0";
            alreadyDot = false;
            state = 0;
        }

        void btnSign_clicked(System.Object sender, System.EventArgs e)
        {
            //Change sign
            if (lblDisplay.Text.StartsWith("-"))
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        void btnPi_Clicked(System.Object sender, System.EventArgs e)
        {
            lblDisplay.Text = "3.141592653589793";
            alreadyDot = false;
            state = 0;
        }
    }
}
