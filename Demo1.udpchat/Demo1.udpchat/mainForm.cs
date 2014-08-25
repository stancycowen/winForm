using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Demo1.udpchat
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            UdpClient uc = new UdpClient();
            string txtIP = this.txtIP.Text;
            string chat = "PUBLIC|" + this.txtChat.Text + "|陈宇川";
            byte[] bchat = Encoding.Default.GetBytes(chat);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(txtIP), 9527);
            uc.Send(bchat,bchat.Length,ipep);
            this.txtChat.Text = "";

        }
        private void listen()
        {
            UdpClient uc = new UdpClient(9527);
            while (true)
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Any,9527);
                byte[] bchat = uc.Receive(ref ipep);
                string chat = Encoding.Default.GetString(bchat);
                string[] s = chat.Split('|');
                if (s.Length == 3 && s[0] =="PUBLIC")
                {
                    string schat = s[1];
                    string sname = s[2];
                    this.txtHistory.Text += sname + ":" + "\r\n";
                    this.txtHistory.Text += schat + "\r\n";
                }
                else if (s[0] == "INROOM" && s.Length == 2)
                {
                    string sname = s[1];
                    this.txtHistory.Text += sname + "上线了" + "\r\n";
                }
                else
                {
                    return;
                }
                
            }
        }
        private void mainForm_Load(object sender, EventArgs e)
        {
            UdpClient uc = new UdpClient();
            string txtIP = this.txtIP.Text;
            string chat = "INROOM|陈宇川";
            byte[] bchat = Encoding.Default.GetBytes(chat);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(txtIP), 9527);
            uc.Send(bchat, bchat.Length, ipep);
            mainForm.CheckForIllegalCrossThreadCalls = false;
            Thread th = new Thread(new ThreadStart(listen));
            th.Start();

        }
    }
}
