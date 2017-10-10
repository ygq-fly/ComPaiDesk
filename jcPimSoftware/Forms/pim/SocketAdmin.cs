using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace jcPimSoftware
{
    public class SocketAdmin
    {
        string m_ip;
        int m_port;
        bool m_running;
        public IntPtr intPtr;
        public TcpListener m_server;
        string endCharacter = "\r\n";
        public Dictionary<string, TcpClient> m_client_list;
        public Dictionary<string, StringBuilder> m_readbuff_list;
        public byte[] m_temp_buff = new byte[256];

        public event EventHandler<EventArgs> ErrorEventHandler;
        public event EventHandler<EventArgs> ConnectEventHandler;
        public event EventHandler<EventArgs> AcceptedEventHandler;
        public event EventHandler<EventArgs> CloseEventHandler;
        public event EventHandler<ReachedEventArgs> ReachedEventHandler;

        public SocketAdmin(IntPtr intPtr)
        {
            m_ip = "127.0.0.1";
            m_port = 8888;
            m_running = false;
            this.intPtr = intPtr;
            m_client_list = new Dictionary<string, TcpClient>();
            m_readbuff_list = new Dictionary<string, StringBuilder>();
        }

        public void OnServer(string _ip, int _port)
        {
            if (m_running)
                return;

            m_ip = _ip;
            m_port = _port;
            ServerRun();
        }

        public void OnStop()
        {
            _sckUser.Close();
        }

        public bool OnSend(string Msg, string Addr)
        {
            bool isSuccess = true;
            try
            {
                if (m_client_list[Addr] == null)
                    return false;

                byte[] b = Encoding.UTF8.GetBytes(Msg);
                m_client_list[Addr].GetStream().Write(b, 0, b.Length);
            }
            catch
            {
                CloseClient(Addr);
                isSuccess = false;
            }
            return isSuccess;
        }

        public bool OnSend(string Msg, string Addr, AsyncCallback callback)
        {
            bool isSuccess = true;
            try
            {
                if (m_client_list[Addr] == null)
                    return false;

                byte[] b = Encoding.UTF8.GetBytes(Msg);
                //m_client_list[Addr].GetStream().Write(b, 0, b.Length);
                m_client_list[Addr].GetStream().BeginWrite(b, 0, b.Length, callback, Addr);
            }
            catch
            {
                CloseClient(Addr);
                isSuccess = false;
            }
            return isSuccess;
        }

        public void CloseClient(string addr)
        {
            m_client_list[addr].Close();

            //Log.WriteLog("diaconnect ip:" + addr.ToString(), Log.EFunctionType.PIM);
            if (m_client_list.ContainsKey(addr))
            {
                m_client_list.Remove(addr);
                m_readbuff_list.Remove(addr);
            }
        }

        void ServerRun()
        {
            m_server = new TcpListener(new IPEndPoint(IPAddress.Parse(m_ip), m_port));
            m_server.Start();
            m_server.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), m_server);
            m_running = true;
            if (ConnectEventHandler != null)
            {
                ConnectEventHandler(m_ip, null);
            }
        }
        public TcpClient _sckUser;
        public  void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                _sckUser = m_server.EndAcceptTcpClient(ar);
                string _addr = _sckUser.Client.RemoteEndPoint.ToString();

                if (!m_client_list.ContainsKey(_addr))
                {
                    m_client_list.Add(_sckUser.Client.RemoteEndPoint.ToString(), _sckUser);
                    m_readbuff_list.Add(_sckUser.Client.RemoteEndPoint.ToString(), new StringBuilder());
                    if(m_port!=6307)
                        NativeMessage.PostMessage(intPtr, MessageID.MS_CONNECT, (uint)_sckUser.Client.Handle, 0);
                    string s = "";
                }

                if (AcceptedEventHandler != null)
                    AcceptedEventHandler(_addr, null);

                _sckUser.GetStream().BeginRead(m_temp_buff, 0, 256, new AsyncCallback(ReceiveCallback), _addr);
                m_server.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), m_server);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
        }

        public void ReceiveCallback(IAsyncResult ar)
        {
            string _addr = (string)ar.AsyncState;
            TcpClient _s;
            try
            {
                _s = m_client_list[_addr];
                NetworkStream _ns = _s.GetStream();

                int length = _ns.EndRead(ar);
                if (length > 0)
                {
                    //socket接收date
                    string cmdStr = Encoding.UTF8.GetString(m_temp_buff, 0, length);
                    //推入缓存区
                    m_readbuff_list[_addr].Append(cmdStr);
                    //拆包，不完整的保留
                    cmdStr = Unpack(m_readbuff_list[_addr]);
                    //开始解析包
                    ReachedEventArgs re = new ReachedEventArgs(CmdParse(cmdStr.Replace(endCharacter, "")));
                    if (ReachedEventHandler != null)
                        ReachedEventHandler(_addr, re);

                    _ns.BeginRead(m_temp_buff, 0, 256, new AsyncCallback(ReceiveCallback), _addr);
                }
                else
                {
                    CloseClient(_addr);
                    NativeMessage.PostMessage(intPtr, MessageID.MS_CLOSE, 0, 0);
                    if (CloseEventHandler != null)
                        CloseEventHandler(_addr, null);
                }
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.ToString());
                CloseClient(_addr);
                if (ErrorEventHandler != null)
                    ErrorEventHandler(_addr, null);
            }
        }

        string Unpack(StringBuilder sb)
        {
            StringBuilder temp = new StringBuilder();

            while (true)
            {
                string str = sb.ToString();
                int n = str.IndexOf(endCharacter);
                if (n == -1)
                    break;
                temp.Append(str.Substring(0, n + 2));
                sb.Remove(0, n + 2);
            }

            return temp.ToString();
        }

        public virtual MSGStruct CmdParse(string cmd)
        {
            //SET:......  
            string[] strTemp = cmd.Split(':');
            MSGStruct msg = new MSGStruct();
            msg.key = strTemp[0];
            if (strTemp.Length == 1)
                msg.json = "0";
            else
                msg.json = strTemp[1];

            return msg;
        }

    }

    public class ReachedEventArgs : EventArgs
    {
        public ReachedEventArgs(MSGStruct msg)
        {
            this.msg = msg;
        }

        public string Threshold;
        public MSGStruct msg;
        public DateTime TimeReached;
    }

    public class MSGStruct
    {
        public string key;
        public string json;

        public List<string> cmd_list = new List<string>();
    }
}
