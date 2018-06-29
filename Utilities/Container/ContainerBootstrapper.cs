using Autofac;
using Caliburn.Micro;
using CodeConcussion.KVL.Utilities.Game;
using CodeConcussion.KVL.Utilities.Messages;
using CodeConcussion.KVL.Utilities.Xaml;
using CodeConcussion.KVL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;

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
        private Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> _previousPropertyBinder;
        private static IContainer _container;

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

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
                .Where(x => x.Namespace != null && x.Namespace.EndsWith("ViewModels"))
                .Where(x => x.GetInterface(_viewModelBaseType.Name, false) != null)
                .AsSelf()
                .InstancePerDependency();

            //views
            builder.RegisterAssemblyTypes(AssemblySource.Instance.ToArray())
                .Where(x => x.Name.EndsWith("View"))
                .Where(x => x.Namespace != null && x.Namespace.EndsWith("Views"))
                .AsSelf()
                .InstancePerDependency();

            //window manager
            builder.Register<IWindowManager>(x => new WindowManager()).InstancePerLifetimeScope();

            //event aggregator
            builder.Register<IEventAggregator>(x => new EventAggregator()).InstancePerLifetimeScope();
            builder.RegisterModule<AutoSubscriptionModule>();

            //kvl
            builder.RegisterType<GameManager>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MessageDispatch>().AsSelf().InstancePerLifetimeScope();

            _container = builder.Build();

            ExtendBinder();
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

            throw new Exception($"Could not locate any instances of {key ?? service.Name}.");
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();

            Application.Current.MainWindow.SizeToContent = SizeToContent.Manual;
            Application.Current.MainWindow.Left = (SystemParameters.PrimaryScreenWidth - 1000) / 2;
            Application.Current.MainWindow.Top = (SystemParameters.PrimaryScreenHeight - 800) / 2;
            Application.Current.MainWindow.Width = 1000;
            Application.Current.MainWindow.Height = 800;
            Application.Current.MainWindow.MinWidth = 500;
            Application.Current.MainWindow.MinHeight = 500;
        }

        private void ExtendBinder()
        {
            _previousPropertyBinder = ViewModelBinder.BindProperties;
            ViewModelBinder.BindProperties = BindFocus;
        }

        private IEnumerable<FrameworkElement> BindFocus(IEnumerable<FrameworkElement> controls, Type modelType)
        {
            var elements = controls.ToList();
            var unmatched = _previousPropertyBinder(elements, modelType);
            var properties = modelType.GetProperties(BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.Public);
            
            foreach (var element in elements)
            {
                var focusPropertyName = "Is" + element.Name + "Focused";
                FocusExtension.CheckForFocusProperty(focusPropertyName, properties, element);
            }

            return unmatched;
        }
    }
}