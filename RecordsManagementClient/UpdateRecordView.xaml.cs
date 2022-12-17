using RecordsManagementClient.Model;
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
using System.Windows.Shapes;

namespace RecordsManagementClient
{
    /// <summary>
    /// Interaction logic for UpdateRecordView.xaml
    /// </summary>
    public partial class UpdateRecordView : Window
    {
        Record updateRecord = null!;

        public UpdateRecordView(Record recordToUpdate)
        {
            InitializeComponent();
            this.updateRecord= recordToUpdate;
            InitializeFields();
        }

        private void InitializeFields()
        {
            this.tbNewRecordPerformer.Text = updateRecord.Performer;
            this.tbNewRecordTitle.Text = updateRecord.Title;
            this.tbNewRecordPrice.Text = updateRecord.Price.ToString();
            this.tbNewRecordStock.Text = updateRecord.StockCount.ToString();
        }

        private void updateRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            //TODO
            //semi-heterogeneous Dictionary<string, object> to store the data to update 
            //call the API with the serialized JSON data
            //review the Response
        }
    }
}
