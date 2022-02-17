using System;
using System.Windows;
using System.Windows.Input;

namespace DataRefresh2
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();

            //lblApplicationName.Content = Global.ApplicationName;
            //lblHelp.Content = "hhh";
            lblBDAppName.Content = "BDApp Name : " + Globals.ApplicationName;
            lblBDAppNumber.Content = "BDApp Number : " + Globals.BDAppNo;
            lblResolver.Content = "Resolver Group : " + Globals.ResolverGroup;
            lblVersion.Content = "Version : " + Globals.CurrentVersion;

        }

        private void ImgLogo_MouseEnter(object sender, MouseEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("This will initiate the reinstall process.  Click Ok to proceed", Globals.ApplicationName, MessageBoxButton.OKCancel, MessageBoxImage.Exclamation);

            if (result == MessageBoxResult.OK)
            {
                Globals.updateApplication();
            }
        }
    }
}
