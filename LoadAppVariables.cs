using System;
using System.IO;
using System.Windows;
using System.Threading.Tasks;
using Ini;

namespace DataRefresh2
{
    class LoadAppVariables
    {
        public static string GlobalFile;
        public static string LocalFile;
        public static string Location;
        public static string BayNo;
        public static string TimerDelay;
        public static int MainPageSize;
        public static string BGColour;
        public static string Colour1 = "#C9DBEA";
        public static string Colour2 = "#B6D2EA";
        public static string Colour3 = "#83BAEA";
        public static int FSize;
        public void LoadApplication()
        {

            // get path to local file
            #region Local FIle
            LocalFile = AppDomain.CurrentDomain.BaseDirectory + "lclIPCWarehouseApplication.ini";
            IniFile iniLocal = new IniFile(LocalFile);
            Location = iniLocal.IniReadValue("UserSettings", "Location");
            BayNo = iniLocal.IniReadValue("UserSettings", "BayNo");
            TimerDelay = iniLocal.IniReadValue("UserSettings", "TimerDelay");
            GlobalFile = iniLocal.IniReadValue("Paths", "GlobalFile");
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "lclIPCWarehouseApplication.ini"))
            {
                try
                {
                    MainPageSize = Convert.ToInt16(iniLocal.IniReadValue("UserSettings", "MainPageSize"));
                }
                catch
                {
                    MainPageSize = 100;
                }
                try
                {
                    BGColour = Convert.ToString(iniLocal.IniReadValue("UserSettings", "BGColour"));                    
                }
                catch
                {
                    BGColour = "White";
                }
                Globals.BGTheme = BGColour;
                try
                {
                    FSize = Convert.ToInt16(iniLocal.IniReadValue("UserSettings", "FSize"));
                }
                catch
                {
                    FSize = 24;
                }
                Globals.UFontSize = FSize;
            }
            else
            {
                MessageBox.Show("Unable to find the Local settings file.  The application will now close.", Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
                Environment.Exit(1);
            }
            #endregion

            // Check global file exists
            #region Global File
            if (File.Exists(GlobalFile))
            {
                // Do nothing;
            }
            else
            {
                MessageBox.Show("Unable to find the Global settings file.  The application will now close.", Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
                Environment.Exit(1);
            }
            #endregion

            Globals.Location = Location;
            Globals.BayNo = BayNo;
            try
            {
                Globals.TimerDelay = Convert.ToInt16(TimerDelay);
                if(Globals.TimerDelay>5 || Globals.TimerDelay<2)
                {
                    Globals.TimerDelay = 2;
                }
            }
            catch
            {
                Globals.TimerDelay = 2;
            }

            Globals.TimerDelayMin = (int)(Globals.TimerDelay * 1000 * 60);

            IniFile iniGlobal = new IniFile(GlobalFile);
            int iTimeOut;

            // read info from Global file
            Globals.ConnectionString = iniGlobal.IniReadValue("System", "ConnectString");
            Globals.CurrentVersion = iniGlobal.IniReadValue("AppDetails", "CurrentVersion");
            Globals.InstallPath = iniGlobal.IniReadValue("AppDetails", "InstallPath");

            // Application Name, default to Assembly Name
            Globals.ApplicationName = iniGlobal.IniReadValue("AppDetails", "ApplicationName");
            Globals.ApplicationName = Globals.ApplicationName == "" ? System.Reflection.Assembly.GetCallingAssembly().GetName().Name : Globals.ApplicationName;

            // Application Name, default to Assembly Name
            Globals.BDAppNo = iniGlobal.IniReadValue("AppDetails", "BDAppNo");
            Globals.BDAppNo = Globals.BDAppNo == "" ? "000000" : Globals.BDAppNo;

            // Application Name, default to Assembly Name
            Globals.ResolverGroup = iniGlobal.IniReadValue("AppDetails", "ResolverGroup");
            Globals.ResolverGroup = Globals.ApplicationName == "" ? "HMRC B Local Compliance" : Globals.ResolverGroup;


            // External option to adjust connection timeout.
            try
            {
                iTimeOut = int.Parse(iniGlobal.IniReadValue("System", "TimeOut"));
            }
            catch
            {
                iTimeOut = 30;  // default
            }
            Globals.TimeOut = iTimeOut < 30 ? 30 : iTimeOut; // avoid any possibility of zero by setting minimum at 10

        }


        public string getAppVersion(string strLocalIniFile)
        {
            IniFile iniLocal = new IniFile(strLocalIniFile);
            string strLocalVersionNo = iniLocal.IniReadValue("Version", "CurrentVersion");

            return strLocalVersionNo;
        }

    }
}
