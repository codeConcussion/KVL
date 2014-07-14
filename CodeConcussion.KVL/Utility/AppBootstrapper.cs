using System.Windows;
using Caliburn.Micro.Autofac;
using CodeConcussion.KVL.ViewModels;

namespace CodeConcussion.KVL.Utility
{
    public class AppBootstrapper : AutofacBootstrapper<ShellViewModel>
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            //Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            //Application.Current.MainWindow.Width = (SystemParameters.PrimaryScreenWidth / 3d) * 2;
            //Application.Current.MainWindow.Height = (SystemParameters.PrimaryScreenHeight / 3d) * 2;
            //Application.Current.MainWindow.Left = SystemParameters.PrimaryScreenWidth / 6d;
            //Application.Current.MainWindow.Top = SystemParameters.PrimaryScreenHeight / 6d;

            //Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            //Application.Current.MainWindow.Width = 500;
            //Application.Current.MainWindow.Height = 350;
            //Application.Current.MainWindow.Left = 10;
            //Application.Current.MainWindow.Top = 10;

            //ClearValue(SizeToContentProperty);
            //LayoutRoot.ClearValue(WidthProperty);
            //LayoutRoot.ClearValue(HeightProperty);
        }
    }
}