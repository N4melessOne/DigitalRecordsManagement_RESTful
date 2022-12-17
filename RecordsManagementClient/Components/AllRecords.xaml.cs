using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Printing;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RecordsManagementClient.Model;
using RecordsManagementClient;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecordsManagementClient.Components
{
    /// <summary>
    /// Interaction logic for AllRecords.xaml
    /// </summary>
    public partial class AllRecords : UserControl
    {

        public AllRecords()
        {
            InitializeComponent();
            RefreshRecordsGrid();
        }

        private void RefreshRecordsGrid()
        {
            var request = new RestRequest(ManegementWindow.RestURL, Method.Get);
            var response = ManegementWindow.Client.Get(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                MessageBox.Show(response.StatusDescription);
            else
            {
                List<Record> allRecords = JsonSerializer.Deserialize<List<Record>>(response.Content!)!;
                dataGrid.ItemsSource = allRecords;
            }
        }

        private void btnUpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            //TODO:
            //getting the object through the API
            //Serialize the object gotten from the API, then open UpdateRecordView with the record instance
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if (ManegementWindow.currentAdmin == null)
            {
                MessageBox.Show("There is no admin currently logged in!");
                return;
            }

            if ((dataGrid.SelectedItem as Record) != null)
            {
                var result = MessageBox.Show($"Are you sure to delete {(dataGrid.SelectedItem as Record)!.Performer} - {(dataGrid.SelectedItem as Record)!.Title}?",
                                    "DELETE", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {


                    RestRequest request = new RestRequest(ManegementWindow.RestURL, Method.Delete);
                    Dictionary<string, object> jsonObject = new Dictionary<string, object>();
                    jsonObject.Add("current_admin_name", ManegementWindow.currentAdmin!.AdminName);
                    jsonObject.Add("current_admin_password", ManegementWindow.currentAdmin!.AdminPass);
                    jsonObject.Add("recordid_to_delete", (dataGrid.SelectedItem as Record)!.Id);

                    var json = JsonSerializer.Serialize(jsonObject, typeof(Dictionary<string, object>));
                    request.AddBody(json);

                    var response = ManegementWindow.Client.Delete(request);

                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        MessageBox.Show(response.StatusDescription);
                    else
                    {
                        Response responseFromDelete = ManegementWindow.Client.Deserialize<Response>(response).Data!;
                        if (responseFromDelete.Error == 0 && responseFromDelete.Message == "Deleted successfully!")
                        {
                            MessageBox.Show(responseFromDelete.Message);
                            RefreshRecordsGrid();
                        }
                        else
                        {
                            MessageBox.Show(responseFromDelete.Message);
                        }
                    }
                }
                else
                    return;
            }
            else
                MessageBox.Show("There is no record selected!");
        }
    }
}
