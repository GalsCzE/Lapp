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

namespace Lapp
{
    public partial class UList : Page
    {
        public UList()
        {
            InitializeComponent();

            try
            {
                RUN();
            }
            catch
            {
                Debug.WriteLine("Neplatná hodnota v databázi");
            }
        }

        private async Task RUN()
        {
            var client = new RestClient("https://student.sps-prosek.cz/~sevcima14/4ITB/lapp/dotaz.php");
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            IParse parser = new JsonParser();
            People_list.ItemsSource = await parser.ParseString<List<User>>(response.Content);
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void People_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
