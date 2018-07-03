using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client__
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Print(string a1)
        {
            mtxtv1.Text += a1 + "\r\n";
        }

        private void btnv1_Click(object sender, EventArgs e)
        {
            加密 p1 = new 加密();
            // string key = "??}?	-";
            String server = "192.168.2.102"; String message = txtv1.Text.ToString() + "," + txtv2.Text.ToString();
            // message = p1.MD5Encrypt(message, key);
            try
            {
                Int32 port = 22500;
                TcpClient client = new TcpClient(server, port);
                Byte[] data = System.Text.Encoding.Unicode.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Print(string.Format("Sent: {0}", message));

                data = new Byte[256];

                String responseData = String.Empty;

                Int32 bytes = stream.Read(data, 0, data.Length);
                responseData = System.Text.Encoding.Unicode.GetString(data, 0, bytes);
                Print(string.Format("Received: {0}", responseData));

                stream.Close();
                client.Close();
            }
            catch (ArgumentNullException ex)
            {
                Print(string.Format("ArgumentNullException: {0}", ex));
            }
            catch (SocketException ex)
            {
                Print(string.Format("SocketException: {0}", ex));
            }

            Print("\n Press Enter to continue...");
            Console.Read();
        }
    }
}
