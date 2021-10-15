using Caliburn.Micro;
using ConsultaWPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ConsultaWPF
{
    public class Bootstrapper : BootstrapperBase
    {
        SimpleContainer _container = new SimpleContainer();
        public Bootstrapper()
        {
            Initialize();

        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
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
