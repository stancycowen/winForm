using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Demo1.udpchat
{
    public  class Operation
    {
        public delegate void delAddFriend(friendInfo fi);
        private mainForm _frm;
        public Operation(mainForm frm)
        {
            _frm = frm;
        }
        public static IPAddress getMyIP()
        {
            IPAddress[] ips = Dns.GetHostByName(Dns.GetHostName()).AddressList;
            foreach (IPAddress ip in ips)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    return ip;
                }
            }
            return null;
        }
        public void listen()
        {
            UdpClient uc = new UdpClient(9527);
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9527);
            while (true)
            {
                
                byte[] binfo = uc.Receive(ref ipep);
                string info = Encoding.Default.GetString(binfo);
                string[] s = info.Split('|');
                string msgHead = s[0];
                switch (msgHead)
                {
                    case "LOGIN":
                        if (s.Length != 4)
                        {
                            continue;
                        }
                        int curIndex = Convert.ToInt32(s[2]);
                        if (curIndex < 0 || curIndex > _frm.imgList.Images.Count)
                        {
                            curIndex = 0;
                        }
                        friendInfo fi = new friendInfo();
                        fi.HeadImageIndex = curIndex;
                        fi.NickName = s[1];
                        fi.Shuoshuo = s[3];
                        fi.IP = ipep.Address;
                        object[] pars = new object[1];
                        pars[0] = fi;
                        _frm.Invoke(new delAddFriend(_frm.addUcf), pars);
                        //回发，我也在,ALSOON|呢称|头像|说说
                        UdpClient ucAlso = new UdpClient();
                        IPEndPoint ipepAlso = new IPEndPoint(ipep.Address, 9527);
                        string loginName = Operation.getMyIP().ToString();
                        string infoAlso = "ALSOON|" + loginName + "|15|好烦！";
                        byte[] binfoAlso = Encoding.Default.GetBytes(infoAlso);
                        uc.Send(binfoAlso, binfoAlso.Length, ipepAlso); 
                    break;
                    case "ALSOON":
                        if (s.Length != 4)
                        {
                            continue;
                        }
                        int curIndexAlso = Convert.ToInt32(s[2]);
                        if (curIndexAlso < 0 || curIndexAlso > _frm.imgList.Images.Count)
                        {
                            curIndexAlso = 0;
                        }
                        if (getMyIP().ToString() == ipep.Address.ToString()) 
                        {
                            continue;
                        }
                        friendInfo fiAlso = new friendInfo();
                        fiAlso.HeadImageIndex = curIndexAlso;
                        fiAlso.NickName = s[1];
                        fiAlso.Shuoshuo = s[3];
                        fiAlso.IP = ipep.Address;
                        object[] parsAlso = new object[1];
                        parsAlso[0] = fiAlso;
                        _frm.Invoke(new delAddFriend(_frm.addUcf), parsAlso);
                    break;
                    case "LOGOUT":

                    break;
                    case "MSG":

                    break;
                    default:
                        break;
                }

            }

        }
    }
}
