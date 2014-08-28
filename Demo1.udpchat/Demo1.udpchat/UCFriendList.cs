using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Demo1.udpchat
{
    public partial class UCFriendList : UserControl
    {
        private mainForm form;

        public mainForm Form
        {
            get { return form; }
            set { form = value; }
        }
        private friendInfo curFriend;
        public friendInfo CurFriend
        {
            get { return curFriend; }
            set
            {
                curFriend = value;
                this.lblNickName.Text = value.NickName;
                this.lblShuoShuo.Text = value.Shuoshuo;
                this.picHeadImage.Image = this.form.imgList.Images[value.HeadImageIndex];
                
            }
        }
        public UCFriendList()
        {
            InitializeComponent();
        }

        private void UCFriendList_Load(object sender, EventArgs e)
        {

        }

        private void UCFriendList_DoubleClick(object sender, EventArgs e)
        {
            chat ch = new chat();
            ch.CurFriend = this.curFriend;
            ch.Show();
                                                   
        }
    }
}
