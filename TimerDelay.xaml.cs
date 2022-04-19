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

namespace IPCWarehouseApplication
{
    /// <summary>
    /// Interaction logic for TimerDelay.xaml
    /// </summary>
    public partial class TimerDelay : Window
    {
        public TimerDelay()
        {
            InitializeComponent();

            PopulateLocationCombo();
        }

        private void PopulateLocationCombo()
        {
            DataSet ds = new DataSet();
            int TimerMin;
            int TimerMax;
            int TimerLoop;

            try
            {
                using (SqlConnection con = new SqlConnection(Globals.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "uspGetTimerDelay";
                        cmd.Connection = con;
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        con.Open();

                        SqlDataReader dr = cmd.ExecuteReader();

                        dr.Read();

                        TimerMin = Convert.ToInt16(dr["TimerDelayMin"].ToString());
                        TimerMax = Convert.ToInt16(dr["TimerDelayMax"].ToString());

                        con.Close();

                        for (TimerLoop = TimerMin; TimerLoop <= TimerMax; TimerLoop++)
                        {
                            this.cboTimerDelay.Items.Add(TimerLoop.ToString());
                        }

                        this.cboTimerDelay.SelectedValue = Globals.TimerDelay.ToString();
                    }
                }
            }
            catch (SqlException e)
            {
                //con.Close();
                MessageBox.Show("Unable to retrieve Timer Delay drop-down data." + Environment.NewLine + Environment.NewLine
                    + "Error: " + e.Number + Environment.NewLine + e.Message
                    , Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);

            }

        }

        private void cmdSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.cboTimerDelay.Text.ToString() != "")
            {
                Globals.TimerDelay = Convert.ToInt16(this.cboTimerDelay.Text.ToString());
                Globals.TimerDelayMin = (int)(Globals.TimerDelay * 1000 * 60);

                IniFile LocalFile = new IniFile(LoadAppVariables.LocalFile);
                LocalFile.IniWriteValue("UserSettings", "TimerDelay", Globals.TimerDelay.ToString());

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
