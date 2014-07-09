using System.Windows;
using Caliburn.Micro;
using CodeConcussion.KVL.ViewModels;

namespace CodeConcussion.KVL.Utility
{
    public class AppBootstrapper : BootstrapperBase
    {
        public AppBootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
