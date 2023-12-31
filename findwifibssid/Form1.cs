using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace findwifibssid
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(wifiNameTextBox.Text.Trim()))
                {
                    MessageBox.Show("Please enter wifi name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string wifiName=wifiNameTextBox.Text;                
                string bssId=FindBssid(wifiName);
                if (string.IsNullOrEmpty(bssId))
                {
                    MessageBox.Show("BSSID not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    bssidTextbox.Clear();
                }
                bssidTextbox.Text = bssId;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static string FindBssid(string ssid)
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 && nic.Description.ToLower().Contains("wireless"))
                {
                    foreach (var unicastAddress in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (unicastAddress.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {                            
                            if (nic.GetPhysicalAddress() != null && nic.GetPhysicalAddress().ToString() != "")
                            {
                                return nic.GetPhysicalAddress().ToString();
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
