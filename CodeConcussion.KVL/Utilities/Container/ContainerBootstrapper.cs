using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Autofac;
using Caliburn.Micro;
using CodeConcussion.KVL.ViewModels;

namespace CodeConcussion.KVL.Utilities.Container
{
    ///modeled after https://github.com/brendankowitz/Caliburn.Micro.Autofac
    public sealed class ContainerBootstrapper : BootstrapperBase
    {
        public ContainerBootstrapper()
        {
            Initialize();
        }
        
        private readonly Type _viewModelBaseType = typeof(System.ComponentModel.INotifyPropertyChanged);
        private static IContainer _container;

        protected override void BuildUp(object instance)
        {
            _container.InjectProperties(instance);
        }

        protected override void Configure()
        {
            var builder = new ContainerBuilder();

            //view models
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(x => x.Name.EndsWith("ViewModel"))
                .Where(x => !(string.IsNullOrWhiteSpace(x.Namespace)) && x.Namespace.EndsWith("ViewModels"))
                .Where(x => x.GetInterface(_viewModelBaseType.Name, false) != null)
                .AsSelf()
                .InstancePerDependency();

            //views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(x => x.Name.EndsWith("View"))
                .Where(x => !(string.IsNullOrWhiteSpace(x.Namespace)) && x.Namespace.EndsWith("Views"))
                .AsSelf()
                .InstancePerDependency();

            //window manager
            builder.Register<IWindowManager>(x => new WindowManager()).InstancePerLifetimeScope();

            //event aggregator
            builder.Register<IEventAggregator>(x => new EventAggregator()).InstancePerLifetimeScope();
            builder.RegisterModule<AutoSubscriptionModule>();

            _container = builder.Build();
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.Resolve(typeof(IEnumerable<>).MakeGenericType(service)) as IEnumerable<object>;
        }

        protected override object GetInstance(Type service, string key)
        {
            object instance;
            if (string.IsNullOrWhiteSpace(key))
            {
                if (_container.TryResolve(service, out instance)) return instance;
            }
            else
            {
                if (_container.TryResolveNamed(key, service, out instance)) return instance;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", key ?? service.Name));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            //Application.Current.MainWindow.Width = (SystemParameters.PrimaryScreenWidth * .75);
            //Application.Current.MainWindow.Height = (SystemParameters.PrimaryScreenHeight * .75);
            //Application.Current.MainWindow.Left = (SystemParameters.PrimaryScreenWidth * .125);
            //Application.Current.MainWindow.Top = (SystemParameters.PrimaryScreenHeight * .125);
            Application.Current.MainWindow.Width = 1024;
            Application.Current.MainWindow.Height = 768;
            Application.Current.MainWindow.MinWidth = 458;
            Application.Current.MainWindow.MinHeight = 334;
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}