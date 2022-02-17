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
using System.Data;
using System.Data.SqlClient;
using Ini;

namespace DataRefresh2
{
    /// <summary>
    /// Interaction logic for BayNo.xaml
    /// </summary>
    public partial class BayNo : Window
    {
        public BayNo()
        {
            InitializeComponent();

            PopulateLocationCombo();
        }

        private void PopulateLocationCombo()
        {
            DataSet ds = new DataSet();
            int BayLoopMax;
            int BayLoop;

            try
            {
                using (SqlConnection con = new SqlConnection(Globals.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "uspGetWarehouseLocationsBays";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        SqlParameter prm01 = cmd.Parameters.Add("@nLocation", SqlDbType.NVarChar);
                        prm01.Value = Globals.Location; // "Milton Keynes";

                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();                      
                        
                        dr.Read();

                        BayLoopMax = Convert.ToInt16(dr["NoOfBays"].ToString());

                        con.Close();

                        for (BayLoop = 1; BayLoop <=BayLoopMax; BayLoop++)
                        {
                            this.cboBayNo.Items.Add( BayLoop.ToString());
                        }
                        
                        this.cboBayNo.SelectedValue = Globals.BayNo;
                    }
                }
            }
            catch (SqlException e)
            {
                //con.Close();
                MessageBox.Show("Unable to retrieve Bay Numbers drop-down data." + Environment.NewLine + Environment.NewLine
                    + "Error: " + e.Number + Environment.NewLine + e.Message
                    , Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);

            }

        }
        
        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.cboBayNo.Text.ToString() != "")
            {
                Globals.BayNo = this.cboBayNo.Text.ToString();

                IniFile LocalFile = new IniFile(LoadAppVariables.LocalFile);
                LocalFile.IniWriteValue("UserSettings", "BayNo", Globals.BayNo);

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
