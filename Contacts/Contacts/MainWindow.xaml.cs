using Contacts.Classes;
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

namespace Contacts {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		List<Contact> contacts;

		public MainWindow() {
			InitializeComponent();
			contacts = new List<Contact>();

			ReadDatabase();
		}

		private void newContact_Click(object sender, RoutedEventArgs e) {
			NewContactWindow newContactWindow = new NewContactWindow();
			newContactWindow.ShowDialog();

			ReadDatabase();
		}

		public void ReadDatabase() {
			using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath)) {
				conn.CreateTable<Contact>();
				contacts = conn.Table<Contact>().ToList().OrderBy(contact => contact.Name).ToList();
			}

			if (contacts != null) {
				//foreach(var contact in contacts) {
				//	contactsListView.Items.Add(new ListViewItem() { 
				//		Content = contact
				//	});
				//}

				contactsListView.ItemsSource = contacts;
			}
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
			TextBox searchTextBox = sender as TextBox;
			var filteredList = contacts.Where(contact => contact.Name.ToLower().Contains(searchTextBox.Text.ToLower())).ToList();

			//var filteredList2 = (from contact2 in contacts
			//										 where contact2.Name.ToLower().Contains(searchTextBox.Text.ToLower())
			//										 orderby contact2.Email
			//										 select contact2
 		//											).ToList(); // Using LINQ (Language integrated query)

			contactsListView.ItemsSource = filteredList;
		}

		private void contactsListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Contact selectedContact = (Contact)contactsListView.SelectedItem;

			if (selectedContact != null) {
				ContactDetailsWindow contactDetailsWindow = new ContactDetailsWindow(selectedContact);
				contactDetailsWindow.ShowDialog();
			}

			ReadDatabase();
		}
	}
}
