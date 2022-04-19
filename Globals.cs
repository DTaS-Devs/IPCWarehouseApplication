using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;

namespace IPCWarehouseApplication
{
    public class Globals
    {
        private static string _connectionstring;
        private static string _applicationnamestring;
        private static string _currentVersion;
        private static string _bdappNo;
        private static string _resolverGroup;
        private static string _installPath;
        private static int _timeOut;
        private static string _pid;
        private static string _fullname;
        private static string _location;
        private static string _bayno;
        private static Int16 _timerdelay;
        private static Int32 _timerdelaymin;
        private static string _bgtheme;
        private static int _ufontsize;

        public static string PID
        {
            get
            {
                return _pid;
            }
            set
            {
                _pid = value;
            }
        }

        public static string FullName
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
            }
        }


        public static int TimeOut
        {
            get
            {
                // Reads are usually simple
                return _timeOut;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _timeOut = value;
            }
        }


        public static string ConnectionString
        {
            get
            {
                // Reads are usually simple
                return _connectionstring;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _connectionstring = value;
            }
        }

        public static string CurrentVersion
        {
            get
            {
                return _currentVersion;
            }
            set
            {
                _currentVersion = value;
            }
        }

        public static string BDAppNo
        {
            get
            {
                return _bdappNo;
            }
            set
            {
                _bdappNo = value;
            }
        }

        public static string ResolverGroup
        {
            get
            {
                return _resolverGroup;
            }
            set
            {
                _resolverGroup = value;
            }
        }

        public static string InstallPath
        {
            get
            {
                return _installPath;
            }
            set
            {
                _installPath = value;
            }
        }

        public static String ApplicationName
        {
            get
            {
                // Reads are usually simple
                return _applicationnamestring;
            }
            set
            {
                // You can add logic here for race conditions,
                // or other measurements
                _applicationnamestring = value;
            }
        }

        public static string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        public static string BayNo
        {
            get
            {
                return _bayno;
            }
            set
            {
                _bayno = value;
            }
        }


        public static Int16 TimerDelay
        {
            get
            {
                return _timerdelay;
            }
            set
            {
                _timerdelay = value;
            }
        }

        public static Int32 TimerDelayMin
        {
            get
            {
                return _timerdelaymin;
            }
            set
            {
                _timerdelaymin = value;
            }
        }

        public static string BGTheme
        {
            get
            {
                return _bgtheme;
            }
            set
            {
                _bgtheme = value;
            }
        }

        public static int UFontSize
        {
            get
            {
                return _ufontsize;
            }
            set
            {
                _ufontsize = value;
            }
        }

        public static void CheckVersions()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

            int _currentVersion = Convert.ToInt16(CurrentVersion);
            int _userVersion = Convert.ToInt16(fvi.FileMajorPart.ToString() + fvi.FileMinorPart.ToString() + fvi.FileBuildPart.ToString() + fvi.FilePrivatePart.ToString());

            if (_currentVersion > _userVersion)
            {
                MessageBoxResult result = MessageBox.Show("There is a new version available.  You will need to update the application", Globals.ApplicationName, MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.OK, MessageBoxOptions.DefaultDesktopOnly);

                if (result == MessageBoxResult.OK)
                {
                    updateApplication();
                }
                else
                {
                    MessageBox.Show("The application will now exit", Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Stop);
                }

                // We need to close the application whether the user updated or not
                Environment.Exit(1);

            }
        }

        public static bool blnmainWindow;

        public static void updateApplication()
        {
            // Update application
            try
            {
                Process.Start(Globals.InstallPath);
                Environment.Exit(1);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(1);
            }
        }

        public static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        public static void GetLocation()
        {

        }

        public static void GetBay()
        {

        }

        public static void GetTimerDelay()
        {

        }
    }
}
