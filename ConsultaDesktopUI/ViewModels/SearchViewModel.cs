using Caliburn.Micro;
using CEPLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaDesktopUI.ViewModels
{
    public class SearchViewModel : Screen
    {
        private readonly IGetFromAPI _getFromAPI;
        private readonly ICEPSearch _search;
        public BindableCollection<Municipio> _AllMunicipios;
        public SearchViewModel(IGetFromAPI getFromAPI, ICEPSearch search)
        {
            _getFromAPI = getFromAPI;
            _search = search;
        }
        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            await LoadAllCEP();
        }
        private async Task LoadAllCEP()
        {
            var cepList = await Task.Run(() => _getFromAPI.GetMunicipios());
            _AllMunicipios = new BindableCollection<Municipio>(cepList);
            NotifyOfPropertyChange(() => IsVisibleAvisoErro);

        }

        private BindableCollection<Municipio> _municipios;
        public BindableCollection<Municipio> Municipios
        {
            get { return _municipios; }
            set
            {
                _municipios = value;
                NotifyOfPropertyChange(() => Municipios);
                NotifyOfPropertyChange(() => CanPesquisar);
            }
        }
        private string _textoPesquisa;

        public string TextoPesquisa
        {
            get { return _textoPesquisa; }
            set
            {
                _textoPesquisa = value;
                NotifyOfPropertyChange(() => TextoPesquisa);
                NotifyOfPropertyChange(() => CanPesquisar);
                NotifyOfPropertyChange(() => IsVisibleAvisoErro);

            }
        }
        private bool _checkPesquisaExata;

        public bool checkPesquisaExata
        {
            get { return _checkPesquisaExata; }
            set
            {
                _checkPesquisaExata = value;
                NotifyOfPropertyChange(() => checkPesquisaExata);
            }
        }

        private string _avisoErroo;

        public string AvisoErro
        {
            get { return _avisoErroo; }
            set
            {
                _avisoErroo = value;
                NotifyOfPropertyChange(() => AvisoErro);
            }
        }



        public void Pesquisar()
        {
            try
            {
                var listPesquisa = _search.Pesquisar(_AllMunicipios, TextoPesquisa, checkPesquisaExata).ToList();
                Municipios = new BindableCollection<Municipio>(listPesquisa);
            }
            catch(Exception ex)
            {
                AvisoErro = ex.Message;
            }

        }
        public bool CanPesquisar
        {
            get
            {
                if (_AllMunicipios?.Count > 0 && TextoPesquisa.Length > 0)
                {

                    return true;
                }
                return false;
            }

        }

        public bool IsVisibleAvisoErro
        {
            get
            {
                if (_AllMunicipios?.Count == 0 && TextoPesquisa.Length > 0)
                {

                    AvisoErro = "Carregando dados...";
                    return true;

                }
                else if (_AllMunicipios == null && TextoPesquisa?.Length > 0)
                {
                    AvisoErro = "Carregando dados...";
                    return true;
                }
                return false;
            }

        }
    }
}
