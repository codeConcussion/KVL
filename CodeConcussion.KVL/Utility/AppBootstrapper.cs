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
        }
    }
}