using System.Windows;
using CodeConcussion.KVL.ViewModels;

namespace CodeConcussion.KVL.Utility
{

    public class AppBootstrapper : AutofacBootstrapper
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