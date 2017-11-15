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
using RestSharp;
using Newtonsoft.Json;
using Lapp.Entity;
using Lapp.Framy;
using Lapp.JsonParsee;
using Lapp.Interface;
using System.Diagnostics;
using System.Net;

namespace Lapp
{
    public partial class UList : Page
    {
      //  public int StatusCode { get; set; }
        public UList()
        {
                InitializeComponent();
            try
            {
                RUN();
            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba");
            }
        }

        private async Task RUN()
        {
            try
            {
                var client = new RestClient("https://student.sps-prosek.cz/~sevcima14/4ITB/lap/dotaz.php");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                // if (response.ResponseStatus == RestSharp.ResponseStatus.Completed) {
                    HttpStatusCode stat = response.StatusCode;
                if (stat == HttpStatusCode.OK)
                {
                    IParse parser = new JsonParser();
                    People_list.ItemsSource = await parser.ParseString<List<User>>(response.Content);
                }
                else
                {
                    MessageBox.Show("Špatná stránka!");
                    MessageBox.Show("Nebude to fungovat");
                }
                //}
            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba");
            }
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            BackEnd.frame.Navigate(new NUser());
        }

        private void People_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id = ((User)People_list.SelectedItem).ID;
            BackEnd.frame.Navigate(new UInfo(id));
        }
    }
}
