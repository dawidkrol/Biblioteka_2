using Biblioteka_2.Data;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Biblioteka_2
{
    public class BootStrapper : BootstrapperBase
    {
        SimpleContainer _container = new SimpleContainer();
        public BootStrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IEventAggregator, EventAggregator>()
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<ISqlProfile, SqlProfile>()
                .Singleton<IUserProfile, UserProfile>()
                .Singleton<IModule, Module>()
                .Singleton<IConnectionLogin, SqlConnectionLogin>()
                .Singleton<IFileConfig, FileConfig>()
                .Singleton<ILogin, Login>()
                .Singleton<GetDeomoData>();

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<IModule>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
