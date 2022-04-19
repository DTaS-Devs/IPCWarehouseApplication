using Ini;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace IPCWarehouseApplication
{
    /// <summary>
    /// Interaction logic for Location.xaml
    /// </summary>
    public partial class Location : Window
    {
        public Location()
        {
            InitializeComponent();

            PopulateLocationCombo();
        }

        private void PopulateLocationCombo()
        {
            DataSet ds = new DataSet();

            try
            {
                using (SqlConnection con = new SqlConnection(Globals.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "uspGetWarehouseLocations";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();

                        this.cboLocation.ItemsSource = ds.Tables[0].DefaultView;
                        this.cboLocation.SelectedValuePath = "WarehouseLocation";
                        this.cboLocation.DisplayMemberPath = "WarehouseLocation";
                        this.cboLocation.SelectedValue = Globals.Location;                                            
                    }
                }
            }
            catch (SqlException e)
            {
                //con.Close();
                MessageBox.Show("Unable to retrieve location drop-down data." + Environment.NewLine + Environment.NewLine
                    + "Error: " + e.Number + Environment.NewLine + e.Message
                    , Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);

            }

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.cboLocation.Text.ToString() != "")
            {
                Globals.Location = this.cboLocation.Text.ToString();

                IniFile LocalFile = new IniFile(LoadAppVariables.LocalFile);
                LocalFile.IniWriteValue("UserSettings", "Location", Globals.Location);

                MessageBox.Show("Saved.", Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
