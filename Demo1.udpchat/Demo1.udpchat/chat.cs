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
    public partial class chat : Form
    {
        public chat()
        {
            InitializeComponent();
        }
        public chat(friendInfo fi)
        {
            
        }
        private friendInfo curFriend;

        public friendInfo CurFriend
        {
            get { return curFriend; }
            set { curFriend = value; }
        }
        private void btnSend_Click(object sender, EventArgs e)
        {

            UdpClient uc = new UdpClient();
            IPEndPoint ipep = new IPEndPoint(curFriend.IP, 9527);
            string chat = this.txtChat.Text;
            string info = "MSG|" + chat;
            byte[] binfo = Encoding.Default.GetBytes(info);
            uc.Send(binfo, binfo.Length, ipep);
            this.txtHistory.Text += this.txtChat.Text + "\r\n";
            
        }

        private void chat_Load(object sender, EventArgs e)
        {
            this.Text = "与"+curFriend.NickName+"聊天中...";



            //this.txtHistory.Text += curFriend.chatHistory;

        }
    }
}
