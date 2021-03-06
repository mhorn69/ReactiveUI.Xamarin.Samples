using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ReactiveUI;
using ReactiveUI.Android;

namespace StockWatch.Advandced
{

    public class SettingsView : ReactiveFragment<SettingsViewModel>, ISettingsView
    {

        public TextView TxtPath { get; private set; }
        public TextView TxtCreationDate { get; private set; }

        public Button BtnNavigateSearch { get; private set; }
        public Button BtnNavigateSearchNoBackStack { get; private set; }

        private static View _view;
        private static IScreen _hostScreen;

        public SettingsView()
        {
            
        }

        public SettingsView(IScreen hostScreen)
        {
            _hostScreen = hostScreen;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            if (_view != null)
            {
                ViewGroup parent = (ViewGroup)_view.Parent;
                if (parent != null)
                    parent.RemoveView(_view);
            }
            try
            {
                _view = inflater.Inflate(Resource.Layout.f_settings, container, false);

                //ViewModel = new StockListViewModel(_hostScreen);

                this.WireUpControls(_view, "settings");

                this.OneWayBind(ViewModel, vm => vm.UrlPathSegment, c => c.TxtPath.Text);
                this.OneWayBind(ViewModel, vm => vm.CreationDate, c => c.TxtCreationDate.Text);

                this.BindCommand(ViewModel, vm => vm.NavigateSearchCommand, c => c.BtnNavigateSearch);
                this.BindCommand(ViewModel, vm => vm.NavigateSearchNoBackStackCommand, c => c.BtnNavigateSearchNoBackStack);

            }
            catch (InflateException e)
            {
                /* view is already there, just return view as it is */
            }


            return _view;
        }

        
    }
}