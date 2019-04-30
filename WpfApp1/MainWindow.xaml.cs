using Microsoft.AspNetCore.SignalR.Client;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HubConnection _connection;

        public MainWindow()
        {
            InitializeComponent();
            _connection = new HubConnectionBuilder().WithUrl("https://localhost:44320/testHub").Build();

            /*
             * If connection is closed, this will delay a bit and restart the connection
             */
            _connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await _connection.StartAsync();
            };
        }
        private async void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            /*
              Start Listener
             */
            _connection.On<string>("Connected",
                                   (connectionid) =>
                                   {
                                       tbMain.Text = connectionid;
                                   });

            _connection.On<string>("TestBrocast",
                                   (value) =>
                                   {
                                       Dispatcher.BeginInvoke((Action)(() =>
                                       {
                                           messagesList.Items.Add(value);
                                       }));
                                   });
            /*
             End Listener
             */

            try
            {
                await _connection.StartAsync();
                messagesList.Items.Add("Connection started");
                btnConnect.IsEnabled = false;
            }
            catch (Exception ex)
            {
                messagesList.Items.Add(ex.Message);
            }
        }
    }
}
