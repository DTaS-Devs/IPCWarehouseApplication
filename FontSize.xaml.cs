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
using Ini;
using System.Data;
using System.Data.SqlClient;

namespace DataRefresh2
{
    /// <summary>
    /// Interaction logic for FontSize.xaml
    /// </summary>
    public partial class FontSize : Window
    {
        public FontSize()
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
                        cmd.CommandText = "uspGetAvailableFontSizes";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;

                        con.Open();

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(ds);
                        con.Close();

                        this.cboFontSize.ItemsSource = ds.Tables[0].DefaultView;
                        this.cboFontSize.SelectedValuePath = "AvailableFontSizes";
                        this.cboFontSize.DisplayMemberPath = "AvailableFontSizes";
                        this.cboFontSize.SelectedValue = Globals.UFontSize;
                    }
                }
            }
            catch (SqlException e)
            {
                //con.Close();
                MessageBox.Show("Unable to retrieve Font Size drop-down data." + Environment.NewLine + Environment.NewLine
                    + "Error: " + e.Number + Environment.NewLine + e.Message
                    , Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);

            }

        }
    

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {

            if (this.cboFontSize.Text.ToString() != "")
            {
                Globals.UFontSize = Convert.ToInt16(this.cboFontSize.Text.ToString());

                IniFile LocalFile = new IniFile(LoadAppVariables.LocalFile);
                LocalFile.IniWriteValue("UserSettings", "FSize", Globals.UFontSize.ToString());

                MessageBox.Show("Saved.", Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }

            this.Close();
        }

        private void cmdCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
