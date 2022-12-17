using RecordsManagementClient.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RecordsManagementClient.Components
{
    /// <summary>
    /// Interaction logic for AddRecord.xaml
    /// </summary>
    public partial class AddRecord : UserControl
    {
        private readonly Regex _regexPrice = new Regex("[^0-9,.-]+");
        private readonly Regex _regexStock = new Regex("[^0-9-]+");
        private readonly Regex _regexEnglishAlphabet = new Regex("[^A-Za-z0-9_.,-]+$");
        public AddRecord()
        {
            InitializeComponent();
        }

        private void addRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ManegementWindow.currentAdmin == null)
            {
                MessageBox.Show("You have to log in to add a new record!");
                return;
            }

            Dictionary<string, object> jsonObject = new Dictionary<string, object>();
            jsonObject.Add("current_admin_name", ManegementWindow.currentAdmin.AdminName);
            jsonObject.Add("current_admin_password", ManegementWindow.currentAdmin.AdminPass);

            //New Performer
            if (string.IsNullOrEmpty(tbNewRecordPerformer.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordPerformer.Focus();
                return;
            }
            if (_regexEnglishAlphabet.IsMatch(tbNewRecordPerformer.Text))
            {
                MessageBox.Show("Please use only letters from the englis alphabet!");
                tbNewRecordPerformer.Focus();
                return;
            }
            jsonObject.Add("new_record_performer", tbNewRecordPerformer.Text);

            //New Title
            if (string.IsNullOrEmpty(tbNewRecordTitle.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordTitle.Focus();
                return;
            }
            if (_regexEnglishAlphabet.IsMatch(tbNewRecordTitle.Text))
            {
                MessageBox.Show("Please use only letters from the englis alphabet!");
                tbNewRecordPerformer.Focus();
                return;
            }
            jsonObject.Add("new_record_title", tbNewRecordTitle.Text);

            //New Price
            if (string.IsNullOrEmpty(tbNewRecordPrice.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordPrice.Focus();
                return;
            }
            if (_regexPrice.IsMatch(tbNewRecordPrice.Text))
            {
                MessageBox.Show("It seems like you didn't type in a number.\nPlease use local decimal separators!");
                tbNewRecordPrice.Text = "";
                tbNewRecordPrice.Focus();
                return;
            }
            //for some odd reason double.Parse(tbNewRecordPrice.Text) stores xx,yy in the Dictionary
            //So Insert doesn't work just because of this, only if the user types in an integer
            //I have to fix the input in the API...

            jsonObject.Add("new_record_price", double.Parse(tbNewRecordPrice.Text));

            //New Stock(optional)
            if (!string.IsNullOrEmpty(tbNewRecordStock.Text))
            {
                if (_regexStock.IsMatch(tbNewRecordStock.Text))
                {
                    MessageBox.Show("It seems like you didn't type in a number.");
                    tbNewRecordStock.Text = "";
                    tbNewRecordStock.Focus();
                    return;
                }
                jsonObject.Add("new_record_stock", int.Parse(tbNewRecordStock.Text));
            }
            else
                jsonObject.Add("new_record_stock", 0);

            //check if all parameters are fulfilled
            if (jsonObject.Count != 6) //6 rows should be in the Dictionary!
            {
                MessageBox.Show("Missing Parameters!");
                return;
            }


            PostNewRecord(jsonObject);


            tbNewRecordPerformer.Text = "";
            tbNewRecordTitle.Text = "";
            tbNewRecordPrice.Text = "";
            tbNewRecordStock.Text = "";
        }


        private void PostNewRecord(Dictionary<string, object> jsonObject)
        {
            var request = new RestRequest(ManegementWindow.RestURL, Method.Post);
            request.AddParameter(jsonObject.ElementAt(0).Key, jsonObject.ElementAt(0).Value, ParameterType.GetOrPost);
            request.AddParameter(jsonObject.ElementAt(1).Key, jsonObject.ElementAt(1).Value, ParameterType.GetOrPost);
            request.AddParameter(jsonObject.ElementAt(2).Key, jsonObject.ElementAt(2).Value, ParameterType.GetOrPost);
            request.AddParameter(jsonObject.ElementAt(3).Key, jsonObject.ElementAt(3).Value, ParameterType.GetOrPost);
            request.AddParameter(jsonObject.ElementAt(4).Key, jsonObject.ElementAt(4).Value, ParameterType.GetOrPost);
            request.AddParameter(jsonObject.ElementAt(5).Key, jsonObject.ElementAt(5).Value, ParameterType.GetOrPost);

            var response = ManegementWindow.Client.Post(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                MessageBox.Show(response.StatusDescription);
            else
            {
                Response responseFromPost = ManegementWindow.Client.Deserialize<Response>(response).Data!;
                if (responseFromPost.Error == 0 && responseFromPost.Message == "Inserted succsessfully!")
                {
                    MessageBox.Show(responseFromPost.Message);
                }
                else
                {
                    MessageBox.Show(responseFromPost.Message);
                }
            }
        }
    }
}
