using System;
using System.Windows;
using MySql.Data.MySqlClient; // Kräver NuGet-paketet MySql.Data

namespace tomtens_verkstad
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            // Glöm inte att lägga till Database=namn!
            string connectionString = "Server=192.168.216.122;Port=3306;Database=tomtens_verkstad;User ID=root;Password=hemligt-losenord;";

            try
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    PersonListBox.Items.Clear();
                    string selectQuery = "SELECT * FROM Land;";

                    using (var cmd = new MySqlCommand(selectQuery, connection))
                    {
                        using (var reader = await cmd.ExecuteReaderAsync())
                        {
                            // Loopa igenom alla rader som hittas i tabellen
                            while (await reader.ReadAsync())
                            {
                                // Ändra "Namn" eller index [0] till kolumnnamnet du vill visa
                                string landNamn = reader["Namn"].ToString();
                                PersonListBox.Items.Add(landNamn);
                            }
                        }
                    }
                }

                MessageBox.Show("datig fungerat!", "BOOM", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ett fel uppstod: {ex.Message}", "Fel", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
