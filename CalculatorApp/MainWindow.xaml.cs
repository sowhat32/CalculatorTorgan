using System;
using System.Windows;
using System.Windows.Controls;
using CalculatorLibrary;

namespace CalculatorApp
{
    public partial class MainWindow : Window
    {
       
        private CalculatorEngine calculator = new CalculatorEngine();

        public MainWindow()
        {
            InitializeComponent();
            txtResult.Text = "0";
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string number = btn.Content.ToString();

            if (txtResult.Text == "0")
                txtResult.Text = number;
            else
                txtResult.Text += number;
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            if (!txtResult.Text.Contains("."))
                txtResult.Text += ".";
        }

        private void Bracket_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string bracket = btn.Content.ToString();

            if (txtResult.Text == "0")
                txtResult.Text = bracket;
            else
                txtResult.Text += bracket;
        }

        private void Operation_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string operation = btn.Content.ToString();

            if (txtResult.Text == "0")
            {
                txtResult.Text = operation;
            }
            else
            {
                txtResult.Text += $" {operation} ";
            }
        }

        private void Equal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // вызов библиотеки
                double result = calculator.Evaluate(txtResult.Text);
                txtResult.Text = result.ToString(System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("на ноль делить нельзя!", "ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtResult.Text = "0";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ошибка в выражении: {ex.Message}", "ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                txtResult.Text = "0";
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            txtResult.Text = "0";
        }
    }
}