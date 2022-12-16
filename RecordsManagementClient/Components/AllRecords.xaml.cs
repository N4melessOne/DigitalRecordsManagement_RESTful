﻿using RestSharp;
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
                //string responseContent = response.Content!;
                //responseContent = responseContent.Remove(responseContent.IndexOf(']')+1);
                List<Record> allRecords = JsonSerializer.Deserialize<List<Record>>(response.Content!)!;
                dataGrid.ItemsSource = allRecords;
            }
        }

        private void btnUpdateRecord_Click(object sender, RoutedEventArgs e)
        {
            /*
            RestRequest request = new RestRequest();
            request.AddParameter("current_admin_name", ManegementWindow.currentAdmin.AdminName);
            request.AddParameter("current_admin_password", ManegementWindow.currentAdmin.AdminPass);
            request.AddParameter("record_title", )*/
        }

        private void btnDeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            if(ManegementWindow.currentAdmin == null)
            {
                MessageBox.Show("There is no admin currently logged in!");
                return;
            }


            if ((dataGrid.SelectedItem as Record) != null)
            {
                RestRequest request = new RestRequest();
                request.AddParameter("current_admin_name", ManegementWindow.currentAdmin!.AdminName);
                request.AddParameter("current_admin_password", ManegementWindow.currentAdmin!.AdminPass);
                request.AddParameter("recordid_to_delete", (dataGrid.SelectedItem as Record)!.Id);
            }
            else
                MessageBox.Show("There is no record selected!");
        }
    }
}