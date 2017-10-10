using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace jcPimSoftware
{
    internal  class MsSwithc
    {
       public static   com_io_ctl.com_io_ctl cic;
        
        public static  bool  ClientCom()
        {
            try
            {
                cic = new com_io_ctl.com_io_ctl(Application.StartupPath + "\\io_mobi2_6.ini");
                cic.OpenCom("COM" + App_Configure.Cnfgs.Comaddr_switch);
                return true;
            }
            catch
            {
                return false;
            }
          
        }
        public static  bool CheckPort(int num)
        {
            bool result = cic.BaseStateWrite(num);
            Thread.Sleep(100);
            return result;
        }
    }
}
