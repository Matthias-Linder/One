using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UnoGame
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainMenu();

            Route.RegisterRoute("main", typeof(MainPage));
            Route.RegisterRoute("menu", typeof(MainMenu));

            Route.GoToAsync("menu");
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
