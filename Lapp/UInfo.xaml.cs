using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lapp.Entity;
using Lapp.Interface;
using Lapp.JsonParsee;
using Lapp.Framy;
using RestSharp;
using Newtonsoft.Json;
using System.Net;

namespace Lapp
{
    /// <summary>
    /// Interakční logika pro UInfo.xaml
    /// </summary>
    public partial class UInfo : Page
    {
        User u;
        int ID;

        public UInfo(int id)
        {
            InitializeComponent();
            ID = id;
            GetUser();
            name.Content = u.name + " " + u.ID;
        }

        private void GetUser()
        {
            try
            {
                var client = new RestClient("https://student.sps-prosek.cz/~sevcima14/4ITB/lapp/dotaz.php?ID=" + ID);
                //var client = new RestClient("https://requestb.in/19vwv091");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                IRestResponse response = client.Execute(request);
                HttpStatusCode stat = response.StatusCode;
                if (stat == HttpStatusCode.OK)
                {
                    IParse parser = new JsonParser();
                    string result = response.Content.Replace(@"[", "");
                    result = result.Replace(@"]", "");
                    u = JsonConvert.DeserializeObject<User>(result);
                }
                else
                {
                    MessageBox.Show("Špatná stránka!");
                    MessageBox.Show("Nebude to fungovat");
                }
            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            BackEnd.frame.Navigate(new UList());
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            BackEnd.frame.Navigate(new NUser(u));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Smazat " + u.name + " ?", "Deleting item", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    var client = new RestClient("https://student.sps-prosek.cz/~sevcima14/4ITB/lapp/Delete.php?ID=" + ID);
                    var request = new RestRequest(Method.GET);
                    IRestResponse response = client.Execute(request);
                    BackEnd.frame.Navigate(new UList());
                }
            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba");
            }
        }
    }
}
