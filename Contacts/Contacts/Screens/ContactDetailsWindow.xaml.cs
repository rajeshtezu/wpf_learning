using Contacts.Classes;
using SQLite;
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
using System.Windows.Shapes;

namespace Contacts {
	/// <summary>
	/// Interaction logic for ContactDetailsWindos.xaml
	/// </summary>
	public partial class ContactDetailsWindow : Window {
		Contact contact;
		public ContactDetailsWindow(Contact contact) {
			InitializeComponent();

			this.contact = contact;
			nameTextInput.Text = contact.Name;
			emailTextInput.Text = contact.Email;
			phoneTextInput.Text = contact.Phone;
		}

		private void deleteButton_Click(object sender, RoutedEventArgs e) {
			MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
			if (messageBoxResult == MessageBoxResult.Yes) {
				using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) {
					connection.CreateTable<Contact>();
					connection.Delete(contact);
				}
			}
			
			Close();
		}

		private void updateButton_Click(object sender, RoutedEventArgs e) {
			contact.Name = nameTextInput.Text;
			contact.Email = emailTextInput.Text;
			contact.Phone = phoneTextInput.Text;

			using (SQLiteConnection connection = new SQLiteConnection(App.databasePath)) {
				connection.CreateTable<Contact>();
				connection.Update(contact);
			}

			Close();
		}
	}
}
