using System.Windows;
using Caliburn.Micro.Autofac;
using CodeConcussion.KVL.ViewModels;

namespace CodeConcussion.KVL.Utilities
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            AutoSubscribeEventAggegatorHandlers = true;
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            Application.Current.MainWindow.Width = (SystemParameters.PrimaryScreenWidth * .75);
            Application.Current.MainWindow.Height = (SystemParameters.PrimaryScreenHeight * .75);
            Application.Current.MainWindow.Left = (SystemParameters.PrimaryScreenWidth * .125);
            Application.Current.MainWindow.Top = (SystemParameters.PrimaryScreenHeight * .125);
            //Application.Current.MainWindow.MinWidth = 352;
            //Application.Current.MainWindow.MinHeight = 320;
            Application.Current.MainWindow.MinWidth = 458;
            Application.Current.MainWindow.MinHeight = 334;
        }
    }
}