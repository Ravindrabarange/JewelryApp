using Microsoft.Win32;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;

namespace JewelryApp
{
    /// <summary>
    /// Interaction logic for EstimationScreen.xaml
    /// </summary>
    public partial class EstimationScreen : Window
    {
        decimal goldPricePerGram = 0;
        decimal weight = 0;
        decimal totalPrice = 0;
        int discount = 0;
        bool IsEnable = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public EstimationScreen()
        {
            InitializeComponent();
            btn_PrintToFile.IsEnabled = false;
            btn_PrintToPaper.IsEnabled = false;
            btn_printToScreen.IsEnabled = false;
        }

        /// <summary>
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Environment.Exit(0);
        }

        /// <summary>
        /// Calculates the gold price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Calculate_Click(object sender, RoutedEventArgs e)
        {

            if (ValidateInputValues())
            {
                IsEnable = true;
                Calculate();
                ResetButtons();
            }
            else
            {
                IsEnable = false;
                ResetButtons();
                MessageBox.Show("inputs are not in correct format.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Calculate()
        {
            var DiscountPrice = (goldPricePerGram * weight) * (discount) / 100;
            totalPrice = (goldPricePerGram * weight) - DiscountPrice;
            txt_Price.Content = totalPrice.ToString();
        }

        private bool ValidateInputValues()
        {
            if (decimal.TryParse(txt_GoldPrice.Text, out goldPricePerGram)
            && decimal.TryParse(txt_Weight.Text, out weight)
            && int.TryParse(txt_Discount.Text, out discount))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reset all the buttons
        /// </summary>
        private void ResetButtons()
        {
            btn_PrintToFile.IsEnabled = IsEnable;
            btn_PrintToPaper.IsEnabled = IsEnable;
            btn_printToScreen.IsEnabled = IsEnable;
        }

        /// <summary>
        /// Print to screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_printToScreen_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputValues())
            {
                Calculate();
                string Info = "Hi " + lbl_UserName.Content + "," + "\n\nGold Price (per gram): " + goldPricePerGram +
                  "\n\nTotal price for " + weight + " gram Gold is: " + totalPrice + "  (Discount: " + discount + "%)";
                MessageBox.Show(Info, "Total Price", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ResetButtons();
                MessageBox.Show("inputs are not in correct format.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Saves data in a text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintToFile_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputValues())
            {
                Calculate();
                string info = "Total price for " + weight + " gram Gold is " + totalPrice;
                SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
                SaveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (SaveFileDialog1.ShowDialog() == true)
                {
                    using (StreamWriter sw = new StreamWriter(SaveFileDialog1.FileName))
                    {
                        sw.WriteLine("--------- Jewelry -------\n");
                        sw.WriteLine("User: " + lbl_UserName.Content);
                        sw.WriteLine("--------- ------ -------\n\n");
                        sw.WriteLine("      Gold Price Per Gram: " + goldPricePerGram);
                        sw.WriteLine("      Weight : " + weight);
                        sw.WriteLine("    - Discount: " + discount + "%");
                        sw.WriteLine("-------------------------------");
                        sw.WriteLine("Total Price: " + totalPrice);
                        sw.WriteLine("-------------------------------");
                    }
                }
            }
            else
            {
                ResetButtons();
                MessageBox.Show("inputs are not in correct format.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Print to a paper
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintToPaper_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputValues())
            {
                Calculate();
                ResetButtons();
                MessageBox.Show("inputs are not in correct format.", "Validation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                throw new NotImplementedException();
        }
    }
}
