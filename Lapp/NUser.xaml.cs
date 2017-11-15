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
using Lapp.Framy;
using Lapp.Entity;

namespace Lapp
{
    /// <summary>
    /// Interakční logika pro NUser.xaml
    /// </summary>
    public partial class NUser : Page
    {
        User u;
        bool edit;
        string url;

        public NUser()
        {
            InitializeComponent();
        }

        public NUser(User us)
        {
            InitializeComponent();
            u = us;
            edit = true;
            name.Text = u.name;
        }

        private void Create()
        {
            try
            {
                User uc = new User();
                uc.name = name.Text;
                if (edit)
                {
                    url = "https://student.sps-prosek.cz/~sevcima14/4ITB/lapp/Insert.php?ID=" + u.ID;
                    MessageBox.Show(url);
                }
                else
                {
                    url = "https://student.sps-prosek.cz/~sevcima14/4ITB/lapp/Insert.php";
                }
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", Newtonsoft.Json.JsonConvert.SerializeObject(uc), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
            }
            catch
            {
                MessageBox.Show("Vyskytla se chyba");
            }
        }

        private void back_btn_Click(object sender, RoutedEventArgs e)
        {
            if (edit)
            {
                BackEnd.frame.Navigate(new UInfo(u.ID));
            }
            else
            {
                BackEnd.frame.Navigate(new UList());
            }
        }

        private void created_Click(object sender, RoutedEventArgs e)
        {
            Create();
            if (edit)
            {
                BackEnd.frame.Navigate(new UInfo(u.ID));
            }
            else
            {
                BackEnd.frame.Navigate(new UList());
            }
        }

        private void name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (edit)
            {
                created.Content = "Editovat uživatele " + name.Text;
            }
            else
            {
                created.Content = "Vytvořit uživatele " + name.Text;
            }
        }
    }
}
