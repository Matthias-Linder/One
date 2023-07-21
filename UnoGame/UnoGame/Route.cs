using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace UnoGame
{
    internal class Route
    {
        private static Dictionary<string, Type> dic = new Dictionary<string, Type>();

        public static void RegisterRoute(string path, Type type)
        {
            if (path == null || path.Length == 0) return;

            dic[path] = type;
        }

        public static async void GoToAsync(string page)
        {
            if (dic[page] == null)
            {
                //invalid page  
                return;
            }

            var _p = Activator.CreateInstance(dic[page]) as Page;

            NavigationPage.SetHasNavigationBar(_p, false);  

            if (App.Current.MainPage == null)
            {
                App.Current.MainPage = new NavigationPage(_p);
                return;
            }
            
            await App.Current.MainPage.Navigation.PushAsync(_p, true);
            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack.ElementAt(0));
        }
    }
}
