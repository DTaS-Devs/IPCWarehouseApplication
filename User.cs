using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;


namespace IPCWarehouseApplication
{
    public class User
    {
        public String Input;
        public User()
        {
            try
            {
                PID = Convert.ToInt32(Environment.UserName).ToString();

            }
            catch
            {
                PID = "1234567";
            }

            try
            {
                var varUserName = Regex.Match(Environment.UserName, @"\d+", RegexOptions.RightToLeft).Value;
                PID = Convert.ToInt32(varUserName).ToString();

            }
            catch
            {
                PID = "1234567";
            }


            try
            {
                FullName = Environment.UserDomainName.ToString();

            }
            catch
            {
                FullName = "Unknown";
            }

            //PID for dev purposes only
            if ((System.Diagnostics.Debugger.IsAttached == true) && (PID == "0000000"))
                {
                    PID = "1234567";
                }


            GetUserInfo();

        }


        private string _pid = "1234567";
        private string _fullname = "";
        private bool _active = false;

        public string PID
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

        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }

        public string FullName
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

        public void GetUserInfo()
        {
            try
            {
            SqlConnection con = new SqlConnection(Globals.ConnectionString);
            SqlCommand cmd = new SqlCommand("tmpCheckWarehouseUser", con);
            cmd.CommandTimeout = Globals.TimeOut;
            cmd.CommandType = CommandType.StoredProcedure;


            SqlParameter prmPID = cmd.Parameters.Add("@nPID", SqlDbType.Int);

            prmPID.Value = PID;

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            #region Recordset
            if (reader.HasRows)
            {
                reader.Read();
                {
                    FullName = reader["Name"].ToString();
                }
            }
            else
            {
                {
                    MessageBox.Show("You are not know by this application." + "\n" + "\n" + "The application will now close.", Globals.ApplicationName, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    Environment.Exit(0);
                }
            }

            #endregion
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
