using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Demo1.udpchat
{
    public class friendInfo
    {
        public string NickName{get;set;}
        
        public int HeadImageIndex { get; set; }
       
        public string Shuoshuo { get; set; }

        public IPAddress IP { get; set; }

        public string chatHistory { get; set; }

        public bool IsOpen { get; set; }

    }
}
