using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;
using CEPLibrary;

namespace ConsultaDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private readonly SearchViewModel _searchViewModel;
        public ShellViewModel(SearchViewModel searchViewModel) 
        {

            _searchViewModel = searchViewModel;
            ActivateItemAsync(_searchViewModel);
        }
    }
}
