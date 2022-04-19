using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
using Ini;

namespace IPCWarehouseApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker bw1 = new BackgroundWorker();

        Theme theme = new Theme();

        public MainWindow()
        {
            try
            {
                // Populate global variables
                LoadAppVariables gen = new LoadAppVariables();
                gen.LoadApplication();            

                Globals.CheckVersions();

                // Get user details            
                User user = new User();
                Globals.FullName = user.FullName;
                Globals.PID = user.PID;

                Globals.TimerDelayMin = (int)(Globals.TimerDelay * 1000 * 60); //Minutes;

                InitializeComponent();

                //IniFile LocalFile = new IniFile(LoadAppVariables.LocalFile);
                //Globals.UFontSize = Convert.ToInt16( LocalFile.IniReadValue("UserSettings", "FSize"));
                ChangeTheme(Globals.BGTheme, Globals.UFontSize);

                this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;

                //Initial Data Load
                //refreshDataset();
                //string strTime = DateTime.Now.ToLongTimeString();
                //lblLastUpdated.Content = "Last Updated : " + strTime;

                //initialize background worker.
                bw1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bw1_DoWork);
                bw1.WorkerSupportsCancellation = true;
                bw1.RunWorkerAsync(Globals.TimerDelayMin);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            do
            {
                //Thread.Sleep(20000);
                if(bw1.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                this.Dispatcher.Invoke((Action)(() =>
                {
                    tabControl.SelectedIndex = 0;

                    if (tabControl.SelectedItem.ToString() != null) 
                    {
                        strControl = tabControl.SelectedItem.ToString();
                    }
                    else
                    {
                        strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
                    }
                }));
                refreshDataset(strControl);
                string strTime = DateTime.Now.ToLongTimeString();
                this.Dispatcher.Invoke((Action)(() =>
                { lblLastUpdated.Content = "Last Updated : " + strTime; 
                this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;
                }));
                i++;
                Thread.Sleep(Globals.TimerDelayMin);
            } while (i < 4);
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            this.Dispatcher.Invoke((Action)(() =>
            { lblLastUpdated.Content = "Last Updated : " + DateTime.Now.ToLongTimeString(); }));
            MessageBox.Show("Finished");
        }

        private void refreshDataset(string strControl)
        {
            //message hidden because label update shows the action
            if (strControl == "System.Windows.Controls.TabItem Header:Summary Content:") //Summary Content
            {
                //MessageBox.Show(strControl.ToString());
                try
                {
                    // get search results
                    DataTable dt = new DataTable("ks");
                    SqlConnection con = new SqlConnection(Globals.ConnectionString);//("Server=N074CASSVRBDA01;Database=IPC_Casetracker_111092;TRUSTED_CONNECTION=Yes;Connection Timeout=50");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("tmpWarehouseDataView", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                    // list cases
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        dgSummary.ItemsSource = dt.DefaultView;
                    }));
                }
                catch
                {
                    //MessageBox.Show("Invalid .", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else //Bay Information
            {
                //MessageBox.Show(strControl.ToString());
                SqlConnection con = new SqlConnection(Globals.ConnectionString);// ("Server=N074CASSVRBDA01;Database=IPC_Casetracker_111092;TRUSTED_CONNECTION=Yes;Connection Timeout=50");
                try
                {
                    // get search results
                    //BayData.Bay_Data bd = new BayData.Bay_Data();                    
                    //DataTable dt = new DataTable("ks");
                    //con.Open();
                    SqlCommand cmd = new SqlCommand("tmpBayView", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //SqlDataAdapter da = new SqlDataAdapter(cmd);
                    //da.Fill(dt);
                    cmd.Parameters.Clear();
                    SqlParameter prm01 = cmd.Parameters.Add("@nLocation", SqlDbType.NVarChar);
                    prm01.Value = Globals.Location; // "Milton Keynes";
                    SqlParameter prm02 = cmd.Parameters.Add("@nBayNo", SqlDbType.Int);
                    prm02.Value = Convert.ToInt16(Globals.BayNo);  //1;  // will need to update with Bay Number
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();


                    // list cases
                    //this.Dispatcher.Invoke((Action)(() =>
                    //    {
                    //        dgSummary.ItemsSource = dt.DefaultView;
                    //    }));
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        dr.Read();
                        //bd.BIID = dr["BIID"].ToString();
                        //bd.BIReference = dr["BIReference"].ToString();
                        //bd.BIStartTime = dr["BIStartTime"].ToString();
                        //bd.BIEndTime = dr["BIEndTime"].ToString();
                        //bd.BIAWB = dr["BIAWB"].ToString();
                        //bd.BIItemLines = dr["BIItemLines"].ToString();
                        //bd.BIPallets = dr["BIPallets"].ToString();
                        //bd.BICartons = dr["BICartons"].ToString();
                        //bd.CIContainerNo = dr["CIContainerNo"].ToString();
                        //bd.CIComments = dr["CIComments"].ToString();
                        //bd.BFDM = dr["BFDM"].ToString();
                        //bd.BFTL = dr["BFTL"].ToString();
                        //bd.BFExamTeam = dr["BFExamTeam"].ToString(); 
                        //bd.ELead = dr["ELead"].ToString();
                        //bd.EExamTeam = dr["EExamTeam"].ToString();
                        //bd.AIInfo = dr["AIInfo"].ToString(); 
                        //bd.SNotebook = dr["SNotebook"].ToString();
                        //bd.SPhoto = dr["SPhoto"].ToString();
                        //bd.SCage = dr["SCage"].ToString();                        
                        //this.DataContext = bd;
                        this.txtBay.Text = dr["BIID"].ToString();
                        this.txtReference.Text = dr["BIReference"].ToString();
                        this.txtStartTime.Text = dr["BIStartTime"].ToString();
                        this.txtEndTime.Text = dr["BIEndTime"].ToString();
                        this.txtAWB.Text = dr["BIAWB"].ToString();
                        this.txtItemLines.Text = dr["BIItemLines"].ToString();
                        this.txtPallets.Text = dr["BIPallets"].ToString();
                        this.txtCartons.Text = dr["BICartons"].ToString();
                        this.txtContainerNo.Text = dr["CIContainerNo"].ToString();
                        this.txtComments.Text = dr["CIComments"].ToString();
                        this.txtDM.Text = dr["BFDM"].ToString();
                        this.txtTL.Text = dr["BFTL"].ToString();
                        this.txtExamTeam.Text = dr["BFExamTeam"].ToString();
                        this.txtELead.Text = dr["ELead"].ToString();
                        this.txtExamTeam.Text = dr["EExamTeam"].ToString();
                        this.txtInfo.Text = dr["AIInfo"].ToString();
                        this.txtNotebook.Text = dr["SNotebook"].ToString();
                        this.txtPhoto.Text = dr["SPhoto"].ToString();
                        this.txtCage.Text = dr["SCage"].ToString();
                        this.DataContext = theme;
                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Invalid ." + ex, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
        }

        public void mnuRefresh_Click(object sender, RoutedEventArgs e)
        {
            //bw1.CancelAsync();
            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            string strTime = DateTime.Now.ToLongTimeString();
            lblLastUpdated.Content = "Last Updated : " + strTime;
            this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;
        }

        private void mnuClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Close();
        }

        private void mnuLocation_Click(object sender, RoutedEventArgs e)
        {
            Location location = new Location();
            location.ShowDialog();

            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            string strTime = DateTime.Now.ToLongTimeString();
            lblLastUpdated.Content = "Last Updated : " + strTime;
            this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;
        }

        private void mnuBay_Click(object sender, RoutedEventArgs e)
        {
            BayNo bayno = new BayNo();
            bayno.ShowDialog();

            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            string strTime = DateTime.Now.ToLongTimeString();
            lblLastUpdated.Content = "Last Updated : " + strTime;
            this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;
        }

        private void mnuStop_Click(object sender, RoutedEventArgs e)
        {
            bw1.CancelAsync();
        }

        private void mnuStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bw1.RunWorkerAsync(Globals.TimerDelayMin); 
            }
            catch
            {
                //already running
                string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
                this.Dispatcher.Invoke((Action)(() =>
                { strControl = tabControl.SelectedItem.ToString(); }));
                refreshDataset(strControl);
                string strTime = DateTime.Now.ToLongTimeString();
                lblLastUpdated.Content = "Last Updated : " + strTime;
            }
        }

        private void mnuAdjust_Click(object sender, RoutedEventArgs e)
        {
            TimerDelay timerdelay = new TimerDelay();
            timerdelay.ShowDialog();

            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            string strTime = DateTime.Now.ToLongTimeString();
            lblLastUpdated.Content = "Last Updated : " + strTime;
            this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;
        }

        private void mnuAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.Show();
        }

        private void dgSummary_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgSummary.UnselectAll();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            string strTime = DateTime.Now.ToLongTimeString();
            lblLastUpdated.Content = "Last Updated : " + strTime;
        }

        private void mnuGreyTheme_Click(object sender, RoutedEventArgs e)
        {
            Globals.BGTheme = "grey";
            ChangeTheme(Globals.BGTheme, Globals.UFontSize);
        }

        private void mnuRedTheme_Click(object sender, RoutedEventArgs e)
        {
            Globals.BGTheme = "red";
            ChangeTheme(Globals.BGTheme, Globals.UFontSize);
        }

        private void mnuGreenTheme_Click(object sender, RoutedEventArgs e)
        {
            Globals.BGTheme = "green";
            ChangeTheme(Globals.BGTheme, Globals.UFontSize);
        }
        private void mnuBlueTheme_Click(object sender, RoutedEventArgs e)
        {
                Globals.BGTheme = "blue";
                ChangeTheme(Globals.BGTheme, Globals.UFontSize);
            }

        private void mnuWhiteTheme_Click(object sender, RoutedEventArgs e)
        {
            Globals.BGTheme = "white";
            ChangeTheme(Globals.BGTheme, Globals.UFontSize);
        }

        private void ChangeTheme(string strTheme, int FSize)
        {
            IniFile LocalFile = new IniFile(LoadAppVariables.LocalFile);
            LocalFile.IniWriteValue("UserSettings", "BGColour", strTheme);
            LocalFile.IniWriteValue("UserSettings", "FSize", FSize.ToString());

            DataContext = "";
            switch (strTheme)
            {
                case "blue":
                    theme.Color1 = "#C9DBEA";
                    theme.Color2 = "#B6D2EA";
                    theme.Color3 = "#83BAEA";
                    break;

                case "grey":
                    theme.Color1 = "#DEDEDE";
                    theme.Color2 = "#9E9E9E";
                    theme.Color3 = "#5D5D5D";
                    break;

                case "red":
                    theme.Color1 = "#E73508";
                    theme.Color2 = "#E73508";
                    theme.Color3 = "#D81d08";
                    break;

                case "green":
                    theme.Color1 = "#6BD249";
                    theme.Color2 = "#5CC739";
                    theme.Color3 = "#43B020";
                    break;

                default:
                    theme.Color1 = "#FFFFFF";
                    theme.Color2 = "#FFFFFF";
                    theme.Color3 = "#FFFFFF";
                    break;
            }

            DataContext = "";

            theme.FSizeValue = FSize;

            DataContext = theme;

            //MessageBox.Show("Background switched to a " + strTheme + " Background.", "Background", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void mnuFontSize_Click(object sender, RoutedEventArgs e)
        {
            FontSize fontsize = new FontSize();
            fontsize.ShowDialog();

            ChangeTheme(Globals.BGTheme, Globals.UFontSize);

            string strControl = "System.Windows.Controls.TabItem Header:Summary Content:";
            this.Dispatcher.Invoke((Action)(() =>
            { strControl = tabControl.SelectedItem.ToString(); }));
            refreshDataset(strControl);
            string strTime = DateTime.Now.ToLongTimeString();
            lblLastUpdated.Content = "Last Updated : " + strTime;
            this.lblLocation.Content = "Location : " + Globals.Location + " - Bay No. " + Globals.BayNo;
        }
    }
}
