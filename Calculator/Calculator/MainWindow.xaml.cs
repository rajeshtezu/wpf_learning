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

namespace Calculator {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		double lastNumber, result;
		SelectedOperator selectedOperator;

		public MainWindow() {
			InitializeComponent();

			acButton.Click += AcButton_Click;
			negativeButton.Click += NegativeButton_Click;
			percentageButton.Click += PercentageButton_Click;
			equalsButton.Click += EqualsButton_Click;
		}

		private void EqualsButton_Click(object sender, RoutedEventArgs e) {
			double newNumber;
			
			if (double.TryParse(resultLabel.Content.ToString(), out newNumber)) {
				switch (selectedOperator) {
					case SelectedOperator.Addition:
						result = lastNumber + newNumber;
						break;
					case SelectedOperator.Subtration:
						result = lastNumber - newNumber;
						break;
					case SelectedOperator.Multiplication:
						result = lastNumber * newNumber;
						break;
					case SelectedOperator.Division:
						if (newNumber == 0) {
							MessageBox.Show(
								"Division by zero is not supported.",
								"Wrong input",
								MessageBoxButton.OK,
								MessageBoxImage.Error
							);

							return;
						}

						result = lastNumber / newNumber;
						break;
				}

				resultLabel.Content = result.ToString();
			}
		}

		private void PercentageButton_Click(object sender, RoutedEventArgs e) {
			if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) {
				lastNumber = lastNumber / 100;
				resultLabel.Content = lastNumber.ToString();
			}
		}

		private void NegativeButton_Click(object sender, RoutedEventArgs e) {
			if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) {
				lastNumber = lastNumber * -1;
				resultLabel.Content = lastNumber.ToString();
			}
		}

		private void AcButton_Click(object sender, RoutedEventArgs e) {
			resultLabel.Content = "0";
		}

		private void NumberButton_Click(object sender, RoutedEventArgs e) {
			string selectedValue = "0";
			Dictionary<object, string> buttons = new Dictionary<object, string> {
				{ zeroButton, "0" },
				{ oneButton, "1" },
				{ twoButton, "2" },
				{ threeButton, "3" },
				{ fourButton, "4" },
				{ fiveButton, "5" },
				{ sixButton, "6" },
				{ sevenButton, "7" },
				{ eightButton, "8" },
				{ nineButton, "9" }
			};

			buttons.TryGetValue(sender, out selectedValue);

			resultLabel.Content = resultLabel.Content.ToString() == "0" ? selectedValue : $"{resultLabel.Content}{selectedValue}";
		}

		private void dotButton_Click(object sender, RoutedEventArgs e) {
			if (!resultLabel.Content.ToString().Contains(".")) {
				resultLabel.Content = $"{resultLabel.Content}.";
			}
		}

		private void OperationButton_Click (object sender, RoutedEventArgs e) {
			Dictionary<object, SelectedOperator> operators = new Dictionary<object, SelectedOperator> {
				{ plusButton, SelectedOperator.Addition },
				{ minusButton, SelectedOperator.Subtration },
				{ multiplyButton, SelectedOperator.Multiplication },
				{ divisonButton, SelectedOperator.Division }
			};

			if (double.TryParse(resultLabel.Content.ToString(), out lastNumber)) {
				resultLabel.Content = "0";				
			}

			operators.TryGetValue(sender, out selectedOperator);

		}
	}

	public enum SelectedOperator {
		Addition,
		Subtration,
		Multiplication,
		Division
	}
}
