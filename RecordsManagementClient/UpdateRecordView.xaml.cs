using RecordsManagementClient.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RecordsManagementClient
{
    /// <summary>
    /// Interaction logic for UpdateRecordView.xaml
    /// </summary>
    public partial class UpdateRecordView : Window
    {
        Record updateRecord = null!;
        private readonly Regex _regexPrice = new Regex("[^0-9,.-]+");
        private readonly Regex _regexStock = new Regex("[^0-9-]+");
        private readonly Regex _regexEnglishAlphabet = new Regex("[^A-Za-z0-9_.,-]+$");

        public UpdateRecordView(Record recordToUpdate)
        {
            InitializeComponent();
            this.updateRecord = recordToUpdate;
            InitializeFields();
        }

        private void InitializeFields()
        {
            this.tbNewRecordPerformer.Text = updateRecord.Performer;
            this.tbNewRecordTitle.Text = updateRecord.Title;
            this.tbNewRecordPrice.Text = updateRecord.Price.ToString();
            this.tbNewRecordStock.Text = updateRecord.StockCount.ToString();
        }

        private Dictionary<string, object> InitializeJsonObject()
        {
            if (this.updateRecord != null)
            {
                Dictionary<string, object> jsonObject = new Dictionary<string, object>();
                jsonObject.Add("current_admin_name", ManegementWindow.currentAdmin.AdminName);
                jsonObject.Add("current_admin_password", ManegementWindow.currentAdmin.AdminPass);
                jsonObject.Add("recordid", this.updateRecord.Id);
                jsonObject.Add("new_record_performer", this.updateRecord.Performer);
                jsonObject.Add("new_record_title", this.updateRecord.Title);
                jsonObject.Add("new_record_price", this.updateRecord.Price);
                jsonObject.Add("new_record_stock", this.updateRecord.StockCount);

                return jsonObject;
            }
            else
                return null!;
        }

        private void updateRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ManegementWindow.currentAdmin == null)
            {
                MessageBox.Show("You have to log in to update a record!");
                return;
            }

            Dictionary<string, object> jsonObject = InitializeJsonObject();

            //New Performer
            if (string.IsNullOrEmpty(tbNewRecordPerformer.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordPerformer.Focus();
                return;
            }
            if (this.updateRecord.Performer != tbNewRecordPerformer.Text)
            {
                if (_regexEnglishAlphabet.IsMatch(tbNewRecordPerformer.Text))
                {
                    MessageBox.Show("Please use only letters from the english alphabet!");
                    tbNewRecordPerformer.Focus();
                    return;
                }

                jsonObject["new_record_performer"] = tbNewRecordPerformer.Text;
            }

            //New Title
            if (string.IsNullOrEmpty(tbNewRecordTitle.Text))
            {
                MessageBox.Show("It seems like there was no Title typed in.");
                tbNewRecordPerformer.Focus();
                return;
            }
            if (this.updateRecord.Title != tbNewRecordTitle.Text)
            {
                if (_regexEnglishAlphabet.IsMatch(tbNewRecordTitle.Text))
                {
                    MessageBox.Show("Please use only letters from the english alphabet!");
                    tbNewRecordTitle.Focus();
                    return;
                }

                jsonObject["new_record_title"] = tbNewRecordTitle.Text;
            }

            //New Price
            if (string.IsNullOrEmpty(tbNewRecordPrice.Text))
            {
                MessageBox.Show("It seems like there was no Price typed in.");
                tbNewRecordPrice.Focus();
                return;
            }
            if (this.updateRecord.Price != double.Parse(tbNewRecordPrice.Text))
            {
                if (_regexPrice.IsMatch(tbNewRecordPrice.Text))
                {
                    MessageBox.Show("It seems like you didn't type in a number.\nPlease use local decimal separators!");
                    tbNewRecordPrice.Text = "";
                    tbNewRecordPrice.Focus();
                    return;
                }
                //because of the thing mentioned in the AddRecords file, I have to pass a wrongly
                //parsed number. But this also will be handled by the API
                jsonObject["new_record_price"] = double.Parse(tbNewRecordPrice.Text);
            }

            //New Stock count
            if (this.updateRecord.StockCount != int.Parse(tbNewRecordStock.Text))
            {
                if (string.IsNullOrEmpty(tbNewRecordStock.Text))
                    jsonObject["new_record_stock"] = 0;

                if (_regexStock.IsMatch(tbNewRecordStock.Text))
                {
                    MessageBox.Show("It seems like you didn't type in a number.\nPlease use local decimal separators!");
                    tbNewRecordStock.Text = "";
                    tbNewRecordStock.Focus();
                    return;
                }

                jsonObject["new_record_stock"] = int.Parse(tbNewRecordStock.Text);
            }

            if (jsonObject.Count != 7)
            {
                MessageBox.Show("Missing parameters!");
                return;
            }

            PutNewRecord(jsonObject);

            this.Close();
        }


        private void PutNewRecord(Dictionary<string, object> jsonObject)
        {
            var request = new RestRequest(ManegementWindow.RestURL, Method.Put);
            var json = JsonSerializer.Serialize(jsonObject, typeof(Dictionary<string, object>));
            request.AddBody(json);

            var response = ManegementWindow.Client.Put(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                MessageBox.Show(response.StatusDescription);
            else
            {
                Response responseFromPut = ManegementWindow.Client.Deserialize<Response>(response).Data!;
                if (responseFromPut.Error == 0 && responseFromPut.Message == "Updated succsessfully!")
                {
                    MessageBox.Show(responseFromPut.Message);
                }
                else
                {
                    MessageBox.Show(responseFromPut.Message);
                }
            }
        }
    }
}
