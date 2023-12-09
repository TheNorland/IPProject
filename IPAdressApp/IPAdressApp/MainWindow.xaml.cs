using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows;

namespace IPAddressApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetIPAddress();
            SendIPAddressToServer(); // Add this line to send the IP address on app startup
        }

        private void GetIPAddress()
        {
            try
            {
                string ipAddress = GetLocalIPAddress();
                ipAddressLabel.Content = $"Your IP Address: {ipAddress}";
            }
            catch (Exception ex)
            {
                ipAddressLabel.Content = $"Error: {ex.Message}";
            }
        }

        private string GetLocalIPAddress()
        {
            string localIP = "?";
            try
            {
                // Get the local machine's IP addresses
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                // Find an IPv4 address
                foreach (IPAddress ip in localIPs)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exceptions if any
                throw;
            }
            return localIP;
        }

        private async void SendIPAddressToServer()
        {
            try
            {
                string serverUrl = "ipproject-eta.vercel.app"; // Replace with your Vercel app URL

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent($"{{\"ipAddress\": \"{ipAddressLabel.Content}\"}}", Encoding.UTF8, "application/json");

                    // Send the IP address to the server using a POST request
                    HttpResponseMessage response = await client.PostAsync(serverUrl, content);

                    // Check the response status if needed
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("IP address sent successfully!");
                    }
                    else
                    {
                        MessageBox.Show($"Failed to send IP address. Status Code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending IP address: {ex.Message}");
            }
        }
    }
}
