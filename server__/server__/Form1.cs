using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;




namespace server__
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            init();
        }
        private void Print(string a1)
        {
            mtxtv1.Text += a1 + "\r\n";
        }

        private void init()
        {
            加密 p1 = new 加密();
            string key = "??}?	-";
            object v1 = new object();
            lock (v1)
            {
                TcpListener server = null;
                try
                {
                    Int32 port = 22500;
                    IPAddress localAddr = IPAddress.Parse("192.168.2.102");

                    server = new TcpListener(localAddr, port);

                    server.Start();

                    Byte[] bytes = new Byte[256];
                    String data = null;

                    while (true)
                    {
                        Print("Waiting for a connection... ");

                        TcpClient client = server.AcceptTcpClient();
                        Print("Connected!");

                        data = null;

                        NetworkStream stream = client.GetStream();

                        int i;

                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            data = System.Text.Encoding.Unicode.GetString(bytes, 0, i);
                            // data = p1.MD5Decrypt(data, key);
                            Print(string.Format("Received: {0}", data));
                            string[] vv1 = new string[2];
                            vv1 = data.Split(',');
                            string vv2 = vv1[0].ToString();
                            string vv3 = vv1[1].ToString();
                            if (vv2 == "123" && vv3 == "123")
                            {
                                data = "登录成功";
                            }
                            else
                            {

                                data = "登录失败";
                            }
                            byte[] msg = System.Text.Encoding.Unicode.GetBytes(data);
                            stream.Write(msg, 0, msg.Length);
                            MessageBox.Show(string.Format("Sent: {0}", data));
                            //Print();
                        }

                        client.Close();
                    }
                }
                catch (SocketException ex)
                {
                    Print(string.Format("SocketException: {0}", ex));
                }
                finally
                {
                    // Stop listening for new clients.
                    server.Stop();
                }


                Print("\nHit enter to continue...");
                Console.Read();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {



        }

        private void mtxtv1_KeyDown(object sender, KeyEventArgs e)
        {
            new Thread(init).Start();
        }
    }
}
