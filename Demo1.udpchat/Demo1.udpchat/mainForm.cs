using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Demo1.udpchat
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        public void addUcf(friendInfo fi)
        {
            UCFriendList ucf = new UCFriendList();
            ucf.Form = this;
            ucf.CurFriend = fi;
            ucf.Top = this.plFriendList.Controls.Count * ucf.Height;
            this.plFriendList.Controls.Add(ucf);           
        }
        private void formFriend_Load(object sender, EventArgs e)
        {
            Operation ope = new Operation(this);           
            mainForm.CheckForIllegalCrossThreadCalls = false; 
            Thread th = new Thread(ope.listen);
            th.IsBackground = true;
            Thread.Sleep(100);
            th.Start();  
            UdpClient uc = new UdpClient();
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 9527);
            string loginName = Operation.getMyIP().ToString();
            string info = "LOGIN|" + loginName + "|15|好烦！";
            byte[] binfo = Encoding.Default.GetBytes(info);
            uc.Send(binfo, binfo.Length, ipep);           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtLogin_Click(object sender, EventArgs e)
        {
            
        }
    }
}
