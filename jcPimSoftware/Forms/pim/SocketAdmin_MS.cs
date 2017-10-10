using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace jcPimSoftware
{
   public class SocketAdmin_MS:SocketAdmin
    {
       public SocketAdmin_MS(IntPtr intPter):base(intPter)
       {
       
       }
      public  override  MSGStruct CmdParse(string cmd)
        {
            //SET:......  
            MSGStruct msg = new MSGStruct();
            msg.key = cmd;
            return msg;
        }
     //public override void AcceptCallback(IAsyncResult ar)
     // {
     //     try
     //     {
     //         _sckUser = m_server.EndAcceptTcpClient(ar);
     //         string _addr = _sckUser.Client.RemoteEndPoint.ToString();

     //         if (!m_client_list.ContainsKey(_addr))
     //         {
     //             m_client_list.Add(_sckUser.Client.RemoteEndPoint.ToString(), _sckUser);
     //             m_readbuff_list.Add(_sckUser.Client.RemoteEndPoint.ToString(), new StringBuilder());
     //             NativeMessage.PostMessage(intPtr, MessageID.MS_CONNECT, (uint)_sckUser.Client.Handle, 0);
     //             string s = "";
     //         }

     //         //if (AcceptedEventHandler != null)
     //         //    AcceptedEventHandler(_addr, null);

     //         _sckUser.GetStream().BeginRead(m_temp_buff, 0, 256, new AsyncCallback(ReceiveCallback), _addr);
     //         m_server.BeginAcceptTcpClient(new AsyncCallback(AcceptCallback), m_server);
     //     }
     //     catch (Exception ex)
     //     {
     //         string s = ex.Message;
     //     }
     // }
      
    }

  
}
