using System;
using System.Collections.Generic;
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
        public AddRecord()
        {
            InitializeComponent();
        }

        private void addRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            string newRecordPerformer, newRecordTitle;
            double newRecordPrice;
            int newRecordStock = 0;


            if (string.IsNullOrEmpty(tbNewRecordPerformer.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordPerformer.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbNewRecordTitle.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordTitle.Focus();
                return;
            }
            if (string.IsNullOrEmpty(tbNewRecordPrice.Text))
            {
                MessageBox.Show("It seems like there was no Performer typed in.");
                tbNewRecordPrice.Focus();
                return;
            }
            if (_regexPrice.IsMatch(tbNewRecordPrice.Text))
            {
                MessageBox.Show("It seems like you didn't type in a number.\nPlease use local decimal separator!");
                tbNewRecordPrice.Text = "";
                tbNewRecordPrice.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(tbNewRecordStock.Text))
            {
                if (_regexStock.IsMatch(tbNewRecordStock.Text))
                {
                    MessageBox.Show("It seems like you didn't type in a number.");
                    tbNewRecordStock.Text = "";
                    tbNewRecordStock.Focus();
                    return;
                }
                newRecordStock = int.Parse(tbNewRecordStock.Text);
            }
            newRecordPerformer = tbNewRecordPerformer.Text;
            newRecordTitle = tbNewRecordTitle.Text;
            newRecordPrice = int.Parse(tbNewRecordPrice.Text);




            //REST -> Post



            tbNewRecordPerformer.Text = "";
            tbNewRecordTitle.Text = "";
            tbNewRecordPrice.Text = "";
            tbNewRecordStock.Text = "";
        }
    }
}
