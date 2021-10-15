using Caliburn.Micro;
using ConsultaDesktop;
using ConsultaDesktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace ConsultaDesktop
{

    public class Bootstrapper : BootstrapperBase
    {
        SimpleContainer _container = new SimpleContainer();
        private IWindowManager _windowManager;
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void Configure()
        {

            _container.Instance(_container);
            _windowManager = new WindowManager();
            //    .PerRequest<IGetFromAPI, GetFromAPI>()
            //    .PerRequest<ICEPSearch, CEPSearch>();

            GetType().Assembly.GetTypes()
                .Where(type => type.IsClass)
                .Where(type => type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                        viewModelType, viewModelType.ToString(), viewModelType));
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }

        //ISSO AQUI TA BUGANDO A APLICAÇÃO...

        protected override object GetInstance(Type service, string key)
        {

            var instance = _container.GetInstance(service, key);
            if (instance != null)
                return instance;
            throw new InvalidOperationException("Could not locate any instances.");

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
