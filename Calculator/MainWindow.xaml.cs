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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, currentNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow() // konstruktor koji mora da se zove kao klasa. Ovo se izvrsava kada se kreira prozor
        {
            InitializeComponent();

            // Event Handlers kreiramo tako sto ukucamo acButton.Click += pa pritisnemo TAB pa ENTER
            // Ovo ce automatski kreirati private void AcButton_Click metodu
            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        result = SimpleMath.Substraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;

                /*
                Zelim da, ako unesem : 
                50 + 5% (2.5) = (52.5)
                80 + 10% (8) = (88)
                */
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }

                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString() , out currentNumber))
            {
                currentNumber = currentNumber * -1;
                resultLabel.Content = currentNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if(sender == multiplicationButton) selectedOperator = SelectedOperator.Multiplication;
            if(sender == divisionButton) selectedOperator = SelectedOperator.Division;
            if(sender == plusButton) selectedOperator = SelectedOperator.Addition;
            if(sender == minusButton) selectedOperator = SelectedOperator.Substraction;
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            // Potrebno je proveriti da li vec ima decimale, ako nema unesi jednu
            if (resultLabel.Content.ToString().Contains("."))
            {
                // da ne radi nista
            }
            else resultLabel.Content = $"{resultLabel.Content}.";
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            int selectedValue = 0;

            // Mozemo da radimo na skolski duuuuuugacak nacin ili na kraci
            
            if (sender == zeroButton) selectedValue = 0;
            if (sender == oneButton) selectedValue = 1;
            if (sender == twoButton) selectedValue = 2;
            if (sender == threeButton) selectedValue = 3;
            if (sender == fourButton) selectedValue = 4;
            if (sender == fiveButton) selectedValue = 5;
            if (sender == sixButton) selectedValue = 6;
            if (sender == sevenButton) selectedValue = 7;
            if (sender == eightButton) selectedValue = 8;
            if (sender == nineButton) selectedValue = 9;

            if (resultLabel.Content.ToString() == "0")   // content je objekat, treba nam ToString
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";  // append-ujemo selected value
            }
            */

            /*
            // refaktorisanje koda iznad
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")   // content je objekat, treba nam ToString
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";  // append-ujemo selected value
            }
            */

            
            // refaktorisanje 2
            // I just cast the sender to a button... and then use button.Content
            Button button = (Button)sender;
            if (resultLabel.Content.ToString() == "0")  // content je objekat, treba nam ToString
            {
                resultLabel.Content = button.Content;
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{button.Content}"; // append-ujemo selected value
            }
            
        }
    }

    // kada ukucas public enum pa pritisnes TAB sam kreira viticaste zagrade
    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double n1, double n2)
        {
            return n1 + n2;
        }
        public static double Substraction(double n1, double n2)
        {
            return n1 - n2;
        }
        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }
        public static double Divide(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;   // Vracam vrednost koja ce se prikazati, izabrao sam nulu
            }
            return n1 / n2;
        }
    }
}
