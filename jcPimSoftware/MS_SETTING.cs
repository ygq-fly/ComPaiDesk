using SpectrumLib.Models;
using SpectrumLib.Defines;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace jcPimSoftware
{
    public partial class MS_SETTING : Form
    {
        #region 变量定义
        //bool isgetpim = false;
        decimal pow1 ;
        decimal pow2;
        decimal f1;
        decimal f2;
        bool isrf1_rest = false;
        bool isrf2_rest = false;
        string err_string = "";
        PowerStatus status1;
        PowerStatus status2;
        private SweepCtrl ctrl;
        decimal pimval;
        /// <summary>
        /// 执行扫描参数，在扫描函数中，应该使用此参数
        /// 它从usr_sweeps复制过来
        /// </summary>
        private SweepParams exe_params;

        /// <summary>
        /// 频谱分析线程
        /// </summary>
        private Thread thdAnalysis;

        /// <summary>
        /// 功放1测试线程
        /// </summary>
        private Thread thdRF1;

        /// <summary>
        /// 功放2测试线程
        /// </summary>
        private Thread thdRF2;

        /// <summary>
        /// 频谱仪接口对象
        /// </summary>
        private SpectrumLib.ISpectrum ISpectrumObj;

        /// <summary>
        /// 频谱仪类型 0 NEC 1 Bird
        /// </summary>
        int type = 0;

        /// <summary>
        /// 功放1等待句柄
        /// </summary>
        private ManualResetEvent power1_Handle;

        /// <summary>
        /// 功放2等待句柄
        /// </summary>
        private ManualResetEvent power2_Handle;

        /// <summary>
        /// 功放1异常信息对象
        /// </summary>
        private RFErrors rfErrors_1;

        /// <summary>
        /// 功放2异常信息对象
        /// </summary>
        private RFErrors rfErrors_2;

        /// <summary>
        /// 功放1状态信息对象
        /// </summary>
        private PowerStatus rfStatus_1;

        /// <summary>
        /// 功放2状态信息对象
        /// </summary>
        private PowerStatus rfStatus_2;

        /// <summary>
        /// 窗体句柄
        /// </summary>
        private IntPtr _handle = IntPtr.Zero;

        /// <summary>
        /// 功放协议类型(0紫光功放，1韩国功放)
        /// </summary>
        private readonly int RF_Type = 0;

        /// <summary>
        /// 开启功放1等待稳定输出的时间
        /// </summary>
        private int Wait_time1 = 1000;

        /// <summary>
        /// 开启功放2等待稳定输出的时间
        /// </summary>
        private int Wait_time2 = 1000;

        #endregion

        string[] values;
        string _addr;
        string add;
       public SocketAdmin_MS sa_ms;
       int  isFirstConnect=0;
       StringBuilder sb = new StringBuilder();
       IntPtr handle_;
       public MS_SETTING(string add, int port, SocketAdmin_MS sa_ms, string txt,IntPtr han)
        {
            InitializeComponent();
           //
           //
            this.handle_ = han;
            this.sa_ms = sa_ms;
            label7.Text = txt;
            ctrl = new SweepCtrl();
            Monitor.Enter(ctrl);
            ctrl.Quit = false;
            Monitor.Exit(ctrl);
            //this.power1_Handle = power1_Handle;
            //this.power2_Handle = power2_Handle;
            //this.add = add;
            //sa_ms = new SocketAdmin_MS();
            //sa_ms.OnServer(add, port);
            sa_ms.ReachedEventHandler += sa_ReachedEventHandler2_MS;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        bool rf1_first = true;
        bool rf2_first = true;

        private void sa_ReachedEventHandler2_MS(object sender, ReachedEventArgs e)
        {
            try
            {
                while (!IsHandleCreated)
                {
                    Thread.Sleep(200);
                }
                //Stopwatch stss = new Stopwatch();
                //stss.Start();
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                _addr = sender as string;
                //this.Invoke(new ThreadStart(delegate()
                //     {
                //         button2.Enabled = false;
                //     }));

                values = e.msg.key.Split(';');
                List<string> vals = new List<string>();
               
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i].Contains("?"))
                        vals.Add(GetMesss(values[i], _addr));
                    else
                        SetMesss(values[i]);
                }
                         
                analyzeSpecialProcess(ref f1, ref f2, ref pimval);
                //if (rf1_)
                //{
                    //bool res = false;
                    //bool res2 = false;
                   // this.Invoke(new ThreadStart(delegate()
                   //{
                   //    //res = RF_Set_Sample(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1), ref  status1);
                   //    do
                   //    {
                   //        res = RF_Set_Sample2(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1 / 1000), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1 / 1000), ref  status1);
                   //        res2 = RF_Set_Sample2(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, OffsetPower(Convert.ToSingle(f2 / 1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2 / 1000), ref  status2);
                   //        RF_Set_Sample2_Wait(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1 / 1000), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1 / 1000), ref  status1);
                   //        RF_Set_Sample2_Wait(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, OffsetPower(Convert.ToSingle(f2 / 1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2 / 1000), ref  status2);
                   //        isFirstConnect++;
                   //    }
                   //    while (isFirstConnect == 1);
                      
                   //}));
                   // string vvvv = label11.Text;
                   // if (res)
                   // {
                   //     vvvv = "-10000";
                   //     err_string += "rf1 timeout;";
                   // }

                   // int n = vals.IndexOf("aaa", 0, vals.Count);
                   // if (n >= 0)
                   //     vals[n] = vvvv;

                   // string vvvv2 = label12.Text;
                   // if (res2)
                   // {
                   //     vvvv2 = "-10000";
                   //     err_string += "rf2 timeout;";
                   // }
                   // int n2= vals.IndexOf("bbb", 0, vals.Count);
                   // if (n2 >= 0)
                   //     vals[n2] = vvvv2;
                //}


                if (rf1_)
                {
                    bool res = false;
                    this.Invoke(new ThreadStart(delegate()
                   {
                       do
                       {
                           res = RF_Set_Sample(App_Configure.Cnfgs.ComAddr1, RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1 / 1000), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1 / 1000), ref  status1);
                           isFirstConnect++;
                       }
                       while (isFirstConnect == 1);
                   }));
                    string vvvv = label11.Text;

                    //while (true&&rf1_first)
                    //{
                    //    if (Math.Abs(Convert.ToSingle(vvvv)) >= 43.5 || Math.Abs(Convert.ToSingle(vvvv)) <= 42.5)
                    //    {
                    //        NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET1, 0, 0);

                    //        NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET1, 0, 1);
                    //        res = RF_Set_Sample(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1 / 1000), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1 / 1000), ref  status1);
                    //        vvvv = label11.Text;
                    //    }
                    //    else
                    //    {
                    //        rf1_first = false;
                    //        break;
                    //    }
                    //    Thread.Sleep(100);
                    //}
                        if (res)
                        {
                            vvvv = "-10000";
                            err_string += "rf1 timeout;";
                        }

                        int n = vals.IndexOf("aaa", 0, vals.Count);
                        if (n >= 0)
                            vals[n] = vvvv;
                    
                    
                }
            
                if (rf2_)
                {
                    if (rf1_) isFirstConnect--;
                    bool res = false;
                    this.Invoke(new ThreadStart(delegate()
                   {
                       do
                       {
                           res = RF_Set_Sample(App_Configure.Cnfgs.ComAddr2, RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f2 / 1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2 / 1000), ref  status2);
                           isFirstConnect++;
                       }
                       while (isFirstConnect == 1);
                   }));
                    string vvvv = label12.Text;
                    //while (true&&rf2_first)
                    //{
                    //    if (Math.Abs(Convert.ToSingle(vvvv)) >= 43.5 || Math.Abs(Convert.ToSingle(vvvv)) <= 42.5)
                    //    {
                    //        NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET2, 0, 0);

                    //        NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET2, 0, 1);
                    //        res = RF_Set_Sample(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, OffsetPower(Convert.ToSingle(f2 / 1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2 / 1000), ref  status2);
                    //        vvvv = label12.Text;
                    //    }
                    //    else
                    //    {
                    //        rf2_first = false;
                    //        break;
                    //    }
                    //    Thread.Sleep(100);
                    //}
                    if (res)
                    {
                        vvvv = "-10000";
                        err_string += "rf2 timeout;";
                    }
                    int n = vals.IndexOf("bbb", 0, vals.Count);
                    if (n >= 0)
                        vals[n] = vvvv;
                }

                if (pim_)
                {
                    Stopwatch st = new Stopwatch();
                    st.Start();
                    StartScan();
                    int i = 0;
                    int n = vals.IndexOf("abc", 0, vals.Count);
                    while (true)
                    {

                        Monitor.Enter(ctrl);
                        bool isgetpim = ctrl.Quit;
                        Monitor.Exit(ctrl);
                        if (isgetpim)
                        {

                            if (n >= 0)
                                vals[n] = label9.Text;
                            //sa_ms.OnSend(label9.Text +"\r\n", _addr);
                            break;
                        }
                        Thread.Sleep(20);
                        i++;
                        if (i > 100)
                        {
                            vals[n] = "-10000";
                            err_string += "pim timeout;";
                            break;
                        }
                    }
                    st.Stop();
                    this.Invoke(new ThreadStart(delegate()
                 {
                     label14.Text = "分析时间：" + st.ElapsedMilliseconds.ToString();
                 }));
                }

                string allMess = "";
                for (int i = 0; i < vals.Count; i++)
                {
                    allMess += vals[i];
                    if (i == vals.Count - 1)
                        allMess += "\r\n";
                    else
                        allMess += ";";

                }
                if (allMess != "") 
                sa_ms.OnSend(allMess, _addr);
                vals.Clear();
                pim_ = false;
                rf1_ = false;
                rf2_ = false;
                rf2_first = true;
                rf1_first = true;
                //stss.Stop();
                //this.Invoke(new ThreadStart(delegate()
                // {
                //     label15.Text = (stss.ElapsedMilliseconds).ToString();
                // }));
            }
            catch (Exception ex)
            {
                RF_Dispose();
                RF_Rest(handle_);
                sa_ms.OnStop();
                StopScan();
                this.Hide();
                //MessageBox.Show(ex.Message);
            }

        }

       string  GetMesss(string value,string addr)
        {
            string result = "";
            string enters = "";
            switch (value.ToUpper())
            { 
                case "JC:PIM:DATA?":
                    pim_ = true;
                    result = "abc";
                    break;
                case "JC:SIG1:FREQ?":
                    result = numericUpDown4.Value.ToString() + enters;
                    rf1_ = true;
                    break;
                case "JC:SIG2:FREQ?":
                    result = numericUpDown3.Value.ToString() + enters;
                    rf2_ = true;
                    break;
                case "JC:SIG1:POW?":
                    result = numericUpDown1.Value.ToString()+enters;
                    rf1_ = true;
                    break;
                case "JC:SIG2:POW?":
                    result =numericUpDown2.Value.ToString()+enters;
                    rf2_ = true;
                    break;
                case "JC:SIG1:OUTP?":
                    if (radioButton2.Checked) result ="OFF"+enters;
                    else result ="ON"+enters;
                    break;
                case "JC:SIG2:OUTP?":
                    if (radioButton3.Checked) result ="OFF"+enters;
                    else result ="ON"+enters;
                    break;
                case "JC:PIM:CH?":
                    if (radioButton6.Checked) result ="REV1"+enters;
                    else result ="FWD1"+enters;
                    break;
                case "*IDN?":
                    result ="Jointcom," + App_Configure.Cnfgs.Mac_Desc + "," + App_Configure.Cnfgs.SN + "," + "V4.1.5.6100"+enters;
                    break;
                case "JC:DEV:INFO?":
                    result ="TX:" + (App_Settings.sgn_1.Min_Freq*1000).ToString() + "-" + (App_Settings.sgn_2.Max_Freq*1000).ToString() +
                     "KHz RX:" + (App_Settings.spfc.cbn.RxS*1000).ToString() + "-" + (App_Settings.spfc.cbn.RxE*1000).ToString() + "KHz"+enters;
                    break;
                case "JC:SIG1:DET?":
                    result =  "aaa" ;
                    rf1_ = true;
                    break;
                case "JC:SIG2:DET?":
                    result =  "bbb" ;
                    rf2_ = true;
                    break;
                case "JC:PIM:FREQ?":
                    this.Invoke(new ThreadStart(delegate()
                    {
                        result =(Convert.ToSingle(textBox5.Text)*1000).ToString();
                    }));
                    break;
                case "*ERR?":
                    result=err_string;
                    break;
                case "JC:PIM:RXREF?":
                    result = App_Settings.spc.RxRef.ToString();
                    break;
             
            }
            return result;
        }

        bool rf1_ = false;
        bool rf2_ = false;
        bool pim_ = false;

        void SetMesss(string value)
        {
           
            try
            {
                //value=value.Remove(value.Length-2,2);
                string[] data = value.Split(' ');
                if(data.Length>=2)
                data[1] = data[1].ToUpper();
                switch (data[0].ToUpper())
                {
                    case "JC:SIG1:FREQ":
                        this.Invoke(new ThreadStart(delegate()
                        {                           
                           f1 = Convert.ToDecimal(Convert.ToSingle(data[1]) );
                           if (f1 / 1000 >= numericUpDown4.Minimum && f1 / 1000 <= numericUpDown4.Maximum)
                           {
                               numericUpDown4.Value = f1 / 1000;
                               rf1_ = true;
                           }
                        }));
                       break;
                    case "JC:SIG2:FREQ":
                       this.Invoke(new ThreadStart(delegate()
                       {                           
                          f2 = Convert.ToDecimal(Convert.ToSingle(data[1]));
                          if (f2 / 1000 >= numericUpDown3.Minimum && f2 / 1000 <= numericUpDown3.Maximum)
                          {
                              numericUpDown3.Value = f2 / 1000;
                              rf2_ = true;
                          }
                       })); 
                       break;
                    case "JC:SIG1:POW":
                       this.Invoke(new ThreadStart(delegate()
                       {
                           pow1=Convert.ToDecimal(data[1]);
                           if (pow1 >= numericUpDown1.Minimum && pow1 <= numericUpDown1.Maximum)
                           {
                               numericUpDown1.Value = pow1;
                               rf1_ = true;
                           }
                       }));
                       break;
                    case "JC:SIG2:POW":
                       this.Invoke(new ThreadStart(delegate()
                       {
                            pow2 =Convert.ToDecimal(data[1]);
                            if (pow2 >= numericUpDown2.Minimum && pow2 <= numericUpDown2.Maximum)
                            {
                                numericUpDown2.Value = pow2;
                                rf2_ = true;
                            }
                       })); 
                       break;
                    case "JC:SIG1:OUTP":
                        //   this.Invoke(new ThreadStart(delegate(){
                        //if (data[1] == "ON") radioButton1.Checked = true;
                        //else radioButton2.Checked = true;
                        //   }));
                       //if (data[1] == "ON")
                       //    NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET1, 0, 1);
                       //else
                       //    NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET1, 0, 0);

                        RF_Switch(App_Configure.Cnfgs.ComAddr1,RFPriority.LvlTwo,(data[1]=="ON"));
                        this.Invoke(new ThreadStart(delegate(){
                            radioButton1.Checked = (data[1] == "ON");
                            if (data[1] != "ON") label1.Text = "--.--";
                        }));                        
                        break;
                    case "JC:SIG2:OUTP":
                        //if (data[1] == "ON")
                        //{

                        //    NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET2, 0, 1);
                        //    //while (!isrf1_rest)
                        //    //{
                        //    //    Thread.Sleep(100);

                        //    //}
                        //    //isrf1_rest = false;
                        //}
                        //else
                        //{
                        //    isrf1_rest = true;
                        //    NativeMessage.PostMessage(this.Handle, MessageID.MS_RFSET2, 0, 0);
                        //    //while (!isrf2_rest)
                        //    //{
                        //    //    Thread.Sleep(100);

                        //    //}
                        //    //isrf2_rest = false;
                        //}
                        RF_Switch(App_Configure.Cnfgs.ComAddr2, RFPriority.LvlTwo, (data[1] == "ON"));
                        this.Invoke(new ThreadStart(delegate()
                        {
                            radioButton4.Checked = (data[1] == "ON");
                            if (data[1] != "ON") label1.Text = "--.--";
                        }));                                          
                        break;
                    case "JC:PIM:FREQ":
                        this.Invoke(new ThreadStart(delegate()
                        {
                            float va = (Convert.ToSingle(data[1]) / 1000);
                            pimval = Convert.ToDecimal(va) * 1000;
                            textBox5.Text = Convert.ToString(va);
                        }));
                        break;
                    case "JC:PIM:CH":
                        this.Invoke(new ThreadStart(delegate()
                        {
                            if (data[1] == "REV1") radioButton6.Checked = true;
                            else radioButton5.Checked = true;
                        }));
                        break;
                    case "JC:PIM:RXREF":
                        this.Invoke(new ThreadStart(delegate()
                        {
                            numericUpDown6.Value = Convert.ToDecimal(data[1]);
                            IniFile.SetFileName("d:\\settings\\Settings_Spc.ini");
                            App_Settings.spc.RxRef = Convert.ToSingle(numericUpDown6.Value);
                            IniFile.SetString("spectrum", "rxRef", App_Settings.spc.RxRef.ToString());                            
                        }));
                        break;
                    case "*CLS":
                        err_string = "";
                        break;
                    case  "*RST":
                        this.Invoke(new ThreadStart(delegate()
                       {
                           RF1_Close();
                           RF2_Close();
                       }));
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this,ex.Message);
            }
        }

        public void ShowS()
        {
            type = App_Configure.Cnfgs.Spectrum;
            switch (type)
            {
                case 0:
                    ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 1:
                    ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 2:
                    ISpectrumObj = new SpectrumLib.Spectrums.Deli(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 3:
                    ISpectrumObj = new SpectrumLib.Spectrums.FanShuang(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
            }
        }
        private void MS_SETTING_Load(object sender, EventArgs e)
        {
           

            //频谱类型
            type = App_Configure.Cnfgs.Spectrum;
            switch (type)
            {
                case 0:
                    ISpectrumObj = new SpectrumLib.Spectrums.SpeCat2(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 1:
                    ISpectrumObj = new SpectrumLib.Spectrums.BirdSh(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 2:
                    ISpectrumObj = new SpectrumLib.Spectrums.Deli(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
                case 3:
                    ISpectrumObj = new SpectrumLib.Spectrums.FanShuang(this.Handle, MessageID.SPECTRUEME_SUCCED, MessageID.SPECTRUM_ERROR);
                    break;
            }

            //ISpectrumObj.EnableLog();

            //绑定RBW
            //Bind(comboBoxRbw);

            //if (type == 0)
            //{
            //    comboBoxVbw.Enabled = false;
            //    comboBoxVbw.Visible = false;
            //    label19.Visible = false;
            //}
            //else if (type == 1)
            //{
            //    //绑定VBW
            //    Bind(comboBoxVbw);
            //}
            //else
            //{
            //    comboBoxVbw.Enabled = false;
            //    comboBoxVbw.Visible = false;
            //    label19.Visible = false;
            //}

            //绑定Fun
            //BindFun();

            //扫描对象
            //exe_params = new SweepParams();
            //exe_params.SweepType = SweepType.Time_Sweep;
            //exe_params.WndHandle = this.Handle;
            //exe_params.DevInfo = new DeviceInfo();
            //exe_params.TmeParam = new TimeSweepParam();
            //exe_params.WndHandle = _handle;
            power1_Handle = new ManualResetEvent(false);
            power2_Handle = new ManualResetEvent(false);
            //建立射频功放层
            //RFSignal.SetWndHandle(exe_params.WndHandle);

          

            //范围
            numericUpDown4.Minimum = (decimal)App_Settings.sgn_1.Min_Freq;
            numericUpDown4.Maximum = (decimal)App_Settings.sgn_1.Max_Freq;
            numericUpDown3.Minimum = (decimal)App_Settings.sgn_2.Min_Freq;
            numericUpDown3.Maximum = (decimal)App_Settings.sgn_2.Max_Freq;
            numericUpDown1.Minimum = (decimal)App_Settings.sgn_1.Min_Power;
            numericUpDown1.Maximum = (decimal)App_Settings.sgn_1.Max_Power;
            numericUpDown2.Minimum = (decimal)App_Settings.sgn_2.Min_Power;
            numericUpDown2.Maximum = (decimal)App_Settings.sgn_2.Max_Power;
            
            //初值
            numericUpDown3.Value =Convert.ToDecimal(App_Settings.sgn_2.Min_Freq);
            numericUpDown4.Value =Convert.ToDecimal( App_Settings.sgn_1.Min_Freq);
            numericUpDown2.Value =Convert.ToDecimal( App_Settings.sgn_2.Min_Power);
            numericUpDown1.Value =Convert.ToDecimal( App_Settings.sgn_1.Min_Power);
            numericUpDown6.Value = Convert.ToDecimal(App_Settings.spc.RxRef);
            pow1 = numericUpDown1.Value;
            pow2 = numericUpDown2.Value;
            f1 = numericUpDown4.Value*1000;
            f2 = numericUpDown3.Value*1000;

            textBox1.Text += "机型：" + App_Configure.Cnfgs.Mac_Desc+"\r\n";
            textBox1.Text += "TX频段范围：" + App_Settings.sgn_1.Min_Freq.ToString() + "-" + App_Settings.sgn_2.Max_Freq.ToString()+"MHz\r\n";
            textBox1.Text += "RX频段范围：" + App_Settings.spfc.cbn.RxS.ToString() + "-" + App_Settings.spfc.cbn.RxE.ToString()+"MHz";



        }
        private void Bind(ComboBox cmbx)
        {
            if (type == 0)
            {
                DataTable dt_nec = new DataTable();
                dt_nec.Columns.Add("rbw", typeof(string));
                dt_nec.Columns.Add("value", typeof(int));

                DataRow dr = dt_nec.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = 1;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "4 KHz";
                dr["value"] = 4;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "8 KHz";
                dr["value"] = 8;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "20 KHz";
                dr["value"] = 20;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "40 KHz";
                dr["value"] = 40;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "250 KHz";
                dr["value"] = 250;
                dt_nec.Rows.Add(dr);

                cmbx.DataSource = dt_nec;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
            else if (type == 1)
            {
                DataTable dt_bird = new DataTable();
                dt_bird.Columns.Add("rbw", typeof(string));
                dt_bird.Columns.Add("value", typeof(int));

                DataRow dr = dt_bird.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = 1000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "3 KHz";
                dr["value"] = 3000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "10 KHz";
                dr["value"] = 10000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "30 KHz";
                dr["value"] = 30000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "300 KHz";
                dr["value"] = 300000;
                dt_bird.Rows.Add(dr);

                dr = dt_bird.NewRow();
                dr["rbw"] = "1000 KHz";
                dr["value"] = 1000000;
                dt_bird.Rows.Add(dr);

                cmbx.DataSource = dt_bird;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
            else if (type == 2)
            {
                DataTable dt_nec = new DataTable();
                dt_nec.Columns.Add("rbw", typeof(string));
                dt_nec.Columns.Add("value", typeof(int));

                DataRow dr = dt_nec.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = 1;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "10 KHz";
                dr["value"] = 10;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "1000 KHz";
                dr["value"] = 1000;
                dt_nec.Rows.Add(dr);

                cmbx.DataSource = dt_nec;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
            else if (type == 3)
            {
                DataTable dt_nec = new DataTable();
                dt_nec.Columns.Add("rbw", typeof(string));
                dt_nec.Columns.Add("value", typeof(int));

                DataRow dr = dt_nec.NewRow();
                dr["rbw"] = "1 KHz";
                dr["value"] = -1000;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "10 KHz";
                dr["value"] = 10;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "100 KHz";
                dr["value"] = 100;
                dt_nec.Rows.Add(dr);

                dr = dt_nec.NewRow();
                dr["rbw"] = "1000 KHz";
                dr["value"] = 1000;
                dt_nec.Rows.Add(dr);

                cmbx.DataSource = dt_nec;
                cmbx.DisplayMember = "rbw";
                cmbx.ValueMember = "value";
            }
        }

        void RF_set1(object  isOpen)
        {
            bool succ= RF_SetFirst(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, (bool)isOpen);
            
            //RF_Sample(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo,ref status1);
            this.Invoke(new ThreadStart(delegate()
            {
                groupBox3.Enabled = true;
            }));
        }

        void RF_set2(object isOpen)
        {
            RF_SetFirst(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, (bool)isOpen);
            //RF_Sample(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2,ref status2);
            this.Invoke(new ThreadStart(delegate()
            {
                groupBox4.Enabled = true;
            }));
        }

        #region 功放开关
        internal bool RF_Switch(int Addr, int Lvl,bool RFon)
        {
            RFSignal.RFClear(Addr, Lvl);

            if (RFon)
            {
                RFSignal.RFOn(Addr, Lvl);//打开功放
            }
            else
            {
                RFSignal.RFOff(Addr, Lvl);//关闭功放
            }

            RFSignal.RFStart(Addr);

            return RFSignal.RFWait(Addr);
        }

        #endregion


        #region RF_SetFirst 打开功放，设置功率；关闭功放，设置功率
        /// <summary>设置功率，打开功放；设置功率，关闭功放
        /// 
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令级别</param>
        /// <param name="P">功率</param>
        /// <param name="RFon">true=打开功放，false=关闭功放</param>
        /// <returns></returns>
        internal bool RF_SetFirst(int Addr,
                           int Lvl, bool RFon)
        {
            //return false;
            bool RF_Succ = true;

            RFSignal.RFClear(Addr, Lvl);

            if (RFon)
            {
                RFSignal.RFOn(Addr, Lvl);//打开功放
                Log.WriteLog("succ =" + Addr.ToString(), Log.EFunctionType.PIM);
            }
            else
            {
                RFSignal.RFOff(Addr, Lvl);//关闭功放
            }
            RFSignal.RFStart(Addr);
            if (Addr == App_Configure.Cnfgs.ComAddr1)
            {
                RF_Succ = power1_Handle.WaitOne(500, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(500, true);
                power2_Handle.Reset();
            
            }
            Thread.Sleep(100);
            //RF_Succ= RFSignal.RFWait(Addr);
            //返回通信超时的情况
            return (!RF_Succ);
        }
        #endregion

        #region RF_Set_Sample 设置频率，功率 , 并执行Sample
        /// <summary>设置频率，功率
        /// 
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令级别</param>
        /// <param name="P">功率</param>
        /// <param name="F">频率</param>
        /// <returns></returns>
        private bool RF_Set_Sample(int Addr,
                           int Lvl,
                           float P, float F, ref PowerStatus status)
        {
            // return false;
            bool RF_Succ = true;
            RFSignal.RFClear(Addr, Lvl);

            //紫光功放改变频率会影响功率，需先设置频率；韩国功放改变功率会影响频率，需先设置功率
            if (RF_Type == 0)
            {
                //if (App_Configure.Cnfgs.RFFormula == 0)//对数LOG
                //{
                //    RFSignal.RFPowerFreq(Addr, Lvl, P, F);
                //}
                //else //线性Linear
                //{
                //    RFSignal.RFFreq(Addr, Lvl, F);
                //    RFSignal.RFPower(Addr, Lvl, P);
                //}
                RFSignal.RFPowerFreqSample(Addr, Lvl, P, F);
            }
            else
            {
                RFSignal.RFPowerFreqSample(Addr, Lvl, P, F);
            }

            RFSignal.RFStart(Addr);

            //等待功放
            //if (Addr == exe_params.DevInfo.RF_Addr1)
            //{
            //    //RF_Succ = power1_Handle.WaitOne(5000, true);
            //    RF_Succ = RFSignal.RFWait(Addr);
            //    //power1_Handle.Reset();
            //}
            //else
            //{
            RF_Succ = RFSignal.RFWait(Addr);
                //RF_Succ = power2_Handle.WaitOne(5000, true);
                //power2_Handle.Reset();
            //}

            //没有发生功放通信超时，则获取功放状态信息
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            //if (status.Status1.Ver[1] >= 6)
            //{
            //    //nozuo
            //}
            else if (RF_Type == 0)
                Thread.Sleep(50);
            else
                Thread.Sleep(150);
            //status.Status2.OutP = ps1.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(ps1.Status2.Freq, settings.Tx, Tx_Tables.pim_rev_offset1_disp);
            //ps2.Status2.OutP = ps2.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(ps2.Status2.Freq, settings.Tx2, Tx_Tables.pim_rev_offset2_disp);

            if (Addr == App_Configure.Cnfgs.ComAddr1)
            {
                string val=(status.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(status.Status2.Freq, (float)pow1, Tx_Tables.pim_rev_offset1_disp)).ToString("0.0");
                int n=status.Status2.Locked;
                //this.Invoke(new ThreadStart(delegate()
                //{
                float value_pim = Convert.ToSingle(val);
                if (Math.Abs(value_pim - Convert.ToSingle(pow1)) > 0.2f && Math.Abs(value_pim - Convert.ToSingle(pow1)) <= 0.5f)
                    value_pim = Convert.ToSingle(pow1) - 0.1f;
                else if (Math.Abs(value_pim - Convert.ToSingle(pow1)) > 0.5f && Math.Abs(value_pim - Convert.ToSingle(pow1)) <= 2f)
                    value_pim = Convert.ToSingle(pow1) + 0.1f;
                
                label11.Text = value_pim.ToString("F2");
                //if (n == 1)
                //    label16.ForeColor = Color.Red;
                //else
                //    label16.ForeColor = Color.Green;
                //}));
            }
            else
            {
                string val=(status.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(status.Status2.Freq, (float)pow2, Tx_Tables.pim_rev_offset2_disp)).ToString("0.0");

                int n = status.Status2.Locked;
               // this.Invoke(new ThreadStart(delegate()
               //{

                float value_pim = Convert.ToSingle(val);
                if (Math.Abs(value_pim - Convert.ToSingle(pow2)) > 0.2f && Math.Abs(value_pim - Convert.ToSingle(pow2)) <= 0.5f)
                    value_pim = Convert.ToSingle(pow1) - 0.1f;
                else if (Math.Abs(value_pim - Convert.ToSingle(pow2)) > 0.5f && Math.Abs(value_pim - Convert.ToSingle(pow2)) <= 2f)
                    value_pim = Convert.ToSingle(pow1) + 0.1f;
                
                label12.Text = value_pim.ToString("F2");
                //if (n == 1)
                //    label17.ForeColor = Color.Red;
                //else
                //    label17.ForeColor = Color.Green;
               //}));
            }

            //返回通信超时的情况
            return (!RF_Succ);
        }

        private bool RF_Set_Sample2(int Addr,
                        int Lvl,
                        float P, float F, ref PowerStatus status)
        {
            // return false;
            bool RF_Succ = true;
            RFSignal.RFClear(Addr, Lvl);

            //紫光功放改变频率会影响功率，需先设置频率；韩国功放改变功率会影响频率，需先设置功率
            if (RF_Type == 0)
            {

                RFSignal.RFPowerFreqSample(Addr, Lvl, P, F);
            }
            else
            {
                RFSignal.RFPowerFreqSample(Addr, Lvl, P, F);
            }

            RFSignal.RFStart(Addr);

         
            return (!RF_Succ);
        }

        private bool RF_Set_Sample2_Wait(int Addr,
                        int Lvl,
                        float P, float F, ref PowerStatus status)
        {
            // return false;
            bool RF_Succ = true;
         
            RF_Succ = RFSignal.RFWait(Addr);
            //RF_Succ = power2_Handle.WaitOne(5000, true);
            //power2_Handle.Reset();
            //}

            //没有发生功放通信超时，则获取功放状态信息
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);

            else if (RF_Type == 0)
                Thread.Sleep(50);
            else
                Thread.Sleep(150);


            if (Addr == App_Configure.Cnfgs.ComAddr1)
            {
                string val = (status.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(status.Status2.Freq, (float)pow1, Tx_Tables.pim_rev_offset1_disp)).ToString("0.0");
                int n = status.Status2.Locked;
                label11.Text = val;
            }
            else
            {
                string val = (status.Status2.OutP + Tx_Tables.pim_rev_tx2disp.Offset(status.Status2.Freq, (float)pow2, Tx_Tables.pim_rev_offset2_disp)).ToString("0.0");

                int n = status.Status2.Locked;
                label12.Text = val;

            }

            //返回通信超时的情况
            return (!RF_Succ);
        }
        #endregion

        #region RF_Set 设置频率，功率
        /// <summary>设置频率，功率
        /// 
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令级别</param>
        /// <param name="P">功率</param>
        /// <param name="F">频率</param>
        /// <returns></returns>
        private bool RF_Set(int Addr,
                           int Lvl,
                           float P, float F)
        {
            // return false;
            bool RF_Succ = true;

            PowerStatus status = new PowerStatus();
            RFSignal.RFStatus(Addr, ref status);

            RFSignal.RFClear(Addr, Lvl);

            //紫光功放改变频率会影响功率，需先设置频率；韩国功放改变功率会影响频率，需先设置功率
            if (status.Status1.Ver[1] >= 6)
            {
                RFSignal.RFPowerFreq(Addr, Lvl, P, F);
            }
            else if (RF_Type == 0)
            {
                if (App_Configure.Cnfgs.RFFormula == 0)//对数LOG
                {
                    RFSignal.RFPowerFreq(Addr, Lvl, P, F);
                }
                else //线性Linear
                {
                    RFSignal.RFFreq(Addr, Lvl, F);
                    RFSignal.RFPower(Addr, Lvl, P);
                }
            }
            else
            {
                RFSignal.RFPowerFreq(Addr, Lvl, P, F);
            }

            RFSignal.RFStart(Addr);

            //等待功放
            //if (Addr == exe_params.DevInfo.RF_Addr1)
            //{
            //    RF_Succ = power1_Handle.WaitOne(5000, true);
            //    power1_Handle.Reset();
            //}
            //else
            //{
            //    RF_Succ = power2_Handle.WaitOne(5000, true);
            //    power2_Handle.Reset();
            //}

            if (status.Status1.Ver[1] >= 6)
            {
                //nozuo
            }
            else if (RF_Type == 0)
                Thread.Sleep(50);
            else
                Thread.Sleep(150);

            //返回通信超时的情况
            return (!RF_Succ);
        }


        #endregion

        #region RF_Sample 采样
        /// <summary>功放采样
        /// 
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令等级</param>
        /// <param name="status">功放返回的状态</param>
        /// <returns></returns>
        private bool RF_Sample(int Addr,
                              int Lvl,
                              ref PowerStatus status)
        {
            // return false;
            bool RF_Succ = true;

            RFSignal.RFStatus(Addr, ref status);

            RFSignal.RFClear(Addr, Lvl);

            //以功放固件版本确定是否需要延时
            if (status.Status1.Ver[1] < 6)
                Thread.Sleep(300);

            RFSignal.RFSample(Addr, Lvl);

            RFSignal.RFStart(Addr);

            //if (Addr == exe_params.DevInfo.RF_Addr1)
            //{
            //    RF_Succ = power1_Handle.WaitOne(5000, true);
            //    power1_Handle.Reset();
            //}
            //else
            //{
            //    RF_Succ = power2_Handle.WaitOne(5000, true);
            //    power2_Handle.Reset();
            //}

            //没有发生功放通信超时，则获取功放状态信息
            if (RF_Succ)
                RFSignal.RFStatus(Addr, ref status);


            if (Addr == App_Configure.Cnfgs.ComAddr1)
            {
                label11.Text = (status.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(status.Status2.Freq, (float)pow1, Tx_Tables.pim_rev_offset1_disp)).ToString("F2");
                //if (status.Status2.Locked == 1)
                //    label16.ForeColor = Color.Red;
                //else
                //    label16.ForeColor = Color.Green;
            }
            else
            {
                label12.Text = (status.Status2.OutP + Tx_Tables.pim_rev_tx1disp.Offset(status.Status2.Freq, (float)pow2, Tx_Tables.pim_rev_offset2_disp)).ToString("F2");
                //if (status.Status2.Locked == 1)
                //    label17.ForeColor = Color.Red;
                //else
                //    label17.ForeColor = Color.Green;
            }
            //返回通信超时的情况
            return (!RF_Succ);
        }

        #endregion

        #region 计算功放补偿
        /// <summary>
        /// 计算功放补偿
        /// </summary>
        /// <param name="freq">功放频率</param>
        /// <param name="p">输出功率</param>
        /// <param name="num">功放编号</param>
        /// <returns>补偿后的输出功率</returns>
        private float OffsetPower(float freq, float p, int num)
        {
            float revP = p;

            
                        if (num == 1)
                        {

                            //revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p;
                            if (App_Configure.Cnfgs.Mode >= 2)
                            {
                                if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                                    revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p - App_Settings.spc.TxRef;
                                else
                                    revP = Tx_Tables.pim_rev2_tx1.Offset(freq, p, Tx_Tables.pim_rev2_offset1) + p - App_Settings.spc.TxRef;
                            }
                            else
                                revP = Tx_Tables.pim_rev_tx1.Offset(freq, p, Tx_Tables.pim_rev_offset1) + p - App_Settings.spc.TxRef;
                        }
                        else
                        {
                            //revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p;
                            if (App_Configure.Cnfgs.Mode >= 2)
                            {
                                if (PimForm.port1_rev_fwd == 1 || PimForm.port1_rev_fwd == 2)
                                    revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p - App_Settings.spc.TxRef;
                                else
                                    revP = Tx_Tables.pim_rev2_tx2.Offset(freq, p, Tx_Tables.pim_rev2_offset2) + p - App_Settings.spc.TxRef;
                            }
                            else
                                revP = Tx_Tables.pim_rev_tx2.Offset(freq, p, Tx_Tables.pim_rev_offset2) + p - App_Settings.spc.TxRef;
                        }
                   
                 

            return revP;
        }

        #endregion


        #region 启动功放测试
        /// <summary>
        /// 启动功放测试
        /// </summary>
        /// <param name="Num">功放编号</param>
        //private void StartRF(object RF)
        //{
        //    RFInvolved Num;
        //    Num = (RFInvolved)RF;
        //    bool bErrors = false;

        //    if (Num == RFPriority.LvlTwo)
        //    {
        //        bErrors = RF_Set(exe_params.DevInfo.RF_Addr1,
        //              exe_params.RFPriority,
        //              exe_params.TmeParam.P1, exe_params.TmeParam.F1,
        //              true, false, true, true);

        //        //if(bErrors)
        //        //    NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);

        //        for (int i = 0; i < 60; i++)
        //        {
        //            bErrors |= RF_Sample(exe_params.DevInfo.RF_Addr1,
        //                              exe_params.RFPriority,
        //                              ref rfStatus_1);

        //            //检查功放异常现象，包括功放通信超时
        //            bErrors = CheckRF_1(bErrors);
        //            if (bErrors)
        //            {
        //                NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr1, 0);
        //                return;
        //            }

        //            Thread.Sleep(1000);
        //        }
        //    }
        //    else
        //    {
        //        bErrors = RF_Set(exe_params.DevInfo.RF_Addr2,
        //             exe_params.RFPriority,
        //             exe_params.TmeParam.P2, exe_params.TmeParam.F2,
        //             true, false, true, true);

        //        //if (bErrors)
        //        //    NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);

        //        for (int i = 0; i < 60; i++)
        //        {
        //            bErrors |= RF_Sample(exe_params.DevInfo.RF_Addr2,
        //                              exe_params.RFPriority,
        //                              ref rfStatus_2);

        //            //检查功放异常现象，包括功放通信超时
        //            bErrors = CheckRF_2(bErrors);
        //            if (bErrors)
        //            {
        //                NativeMessage.PostMessage(_handle, MessageID.RF_ERROR, (uint)exe_params.DevInfo.RF_Addr2, 0);
        //                return;
        //            }

        //            Thread.Sleep(1000);
        //        }
        //    }
        //}

        #endregion


        #region 停止功放测试
        /// <summary>
        /// 停止功放测试
        /// </summary>
        /// <param name="Num">功放编号</param>
        //private void StopRF(RFInvolved Num)
        //{
        //    if (Num == RFPriority.LvlTwo)
        //    {
        //        RF_Set(exe_params.DevInfo.RF_Addr1,
        //              exe_params.RFPriority,
        //              exe_params.TmeParam.P1, exe_params.TmeParam.F1,
        //              false, false, false, false);


        //    }
        //    else
        //    {
        //        RF_Set(exe_params.DevInfo.RF_Addr2,
        //             exe_params.RFPriority,
        //             exe_params.TmeParam.P2, exe_params.TmeParam.F2,
        //             false, false, false, false);


        //    }
        //}

        #endregion

        #region 功放异常检测
        /// <summary>
        /// 依据全局静态的功放设备限制条件进行异常检查
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_1(bool timeOut)
        {
            bool errors = false;

            rfErrors_1.RF_TimeOut = timeOut;
            if (timeOut)
                Log.WriteLog("RF1通讯超时!", Log.EFunctionType.TestMode);

            else
            {
                if ((rfStatus_1.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                    (rfStatus_1.Status2.Current > App_Configure.Cnfgs.Max_Curr))
                {
                    errors = true;
                    rfErrors_1.RF_CurrError = true;
                    rfErrors_1.RF_CurrValue = rfStatus_1.Status2.Current;
                    Log.WriteLog("RF1电流异常!  I1=" + rfStatus_1.Status2.Current.ToString(), Log.EFunctionType.TestMode);
                }

                if ((rfStatus_1.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                    (rfStatus_1.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
                {
                    errors = true;
                    rfErrors_1.RF_TempError = true;
                    rfErrors_1.RF_TempValue = rfStatus_1.Status2.Temp;
                    Log.WriteLog("RF1温度异常!  Temp1=" + rfStatus_1.Status2.Temp.ToString(), Log.EFunctionType.TestMode);
                }

                if ((rfStatus_1.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr))
                {
                    errors = true;
                    rfErrors_1.RF_VswrError = true;
                    rfErrors_1.RF_VswrValue = rfStatus_1.Status2.Vswr;
                    Log.WriteLog("RF1驻波异常!  VSWR1=" + rfStatus_1.Status2.Vswr.ToString(), Log.EFunctionType.TestMode);
                }
            }

            return (errors || timeOut);
        }

        /// <summary>
        /// 依据全局静态的功放设备限制条件进行异常检查
        /// </summary>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        private bool CheckRF_2(bool timeOut)
        {
            bool errors = false;

            rfErrors_2.RF_TimeOut = timeOut;
            if (timeOut)
                Log.WriteLog("RF2通讯超时!", Log.EFunctionType.TestMode);

            else
            {
                if ((rfStatus_2.Status2.Current < App_Configure.Cnfgs.Min_Curr) ||
                    (rfStatus_2.Status2.Current > App_Configure.Cnfgs.Max_Curr))
                {
                    errors = true;
                    rfErrors_2.RF_CurrError = true;
                    rfErrors_2.RF_CurrValue = rfStatus_2.Status2.Current;
                    Log.WriteLog("RF2电流异常!  I2=" + rfStatus_2.Status2.Current.ToString(), Log.EFunctionType.TestMode);
                }

                if ((rfStatus_2.Status2.Temp < App_Configure.Cnfgs.Min_Temp) ||
                    (rfStatus_2.Status2.Temp > App_Configure.Cnfgs.Max_Temp))
                {
                    errors = true;
                    rfErrors_2.RF_TempError = true;
                    rfErrors_2.RF_TempValue = rfStatus_2.Status2.Temp;
                    Log.WriteLog("RF2温度异常!  Temp1=" + rfStatus_2.Status2.Temp.ToString(), Log.EFunctionType.TestMode);
                }

                if (rfStatus_2.Status2.Vswr > App_Configure.Cnfgs.Max_Vswr)
                {
                    errors = true;
                    rfErrors_2.RF_VswrError = true;
                    rfErrors_2.RF_VswrValue = rfStatus_2.Status2.Vswr;
                    Log.WriteLog("RF2驻波异常!  VSWR2=" + rfStatus_2.Status2.Vswr.ToString(), Log.EFunctionType.TestMode);
                }
            }

            return (errors || timeOut);
        }

        #endregion


       public static  bool analyzeSpecialProcess(ref decimal f1, ref decimal f2, ref decimal fpim)
        {
            //return false;
            const int RES_BANDWITDH = 70000;
            float f_f1 = Convert.ToSingle(f1);
            float f_f2 = Convert.ToSingle(f2);
            float f_pim = Convert.ToSingle(fpim);
            int n = 0, m = 0;
            int offset = 100;
            if (App_Configure.Cnfgs.NoFilter == 1)
                offset = 1000;
            if ((Math.Abs(f1 - fpim) == RES_BANDWITDH || Math.Abs(f2 - fpim) == RES_BANDWITDH) ||
                ((Math.Abs(f_f1 - f_pim) <= 140.7f && Math.Abs(f_f1 - f_pim) >= 139.3f) || (Math.Abs(f_f2 - f_pim) <= 140.7f && Math.Abs(f_f2 - f_pim) >= 139.3f)))
            {
                //n=m+1;
                //fpim=n*f1-m*f2
                //m=(fpim-f1)/(f1-f2)
                if (fpim > f1)
                {
                    m = (int)((fpim - (f1 > f2 ? f1 : f2)) / Math.Abs(f1 - f2));

                    if (f1 > f2)
                    {
                        f1 -= offset;
                        fpim = m * (f1 - f2) + f1;
                    }
                    else
                    {
                        f2 -= offset;
                        fpim = m * (f2 - f1) + f2;
                    }
                }
                else
                {
                    m = (int)(((f1 < f2 ? f1 : f2) - fpim) / Math.Abs(f1 - f2));

                    if (f1 < f2)
                    {
                        f1 += offset;
                        fpim = f1 - m * (f2 - f1);
                    }
                    else
                    {
                        f2 += offset;
                        fpim = f2 - m * (f1 - f2);
                    }
                }
            }
            else
                return false;

            return true;
        }


        #region 发送功放命令
        //#region RF_Set
        ///// <summary>
        ///// RF_Set
        ///// </summary>
        ///// <param name="Addr">功放地址</param>
        ///// <param name="Lvl">命令等级</param>
        ///// <param name="P">功率</param>
        ///// <param name="F">频率</param>
        ///// <param name="RFon">开启功放标识</param>
        ///// <param name="ignoreRFon">忽略开功放标识</param>
        ///// <param name="useP">设置功率标识</param>
        ///// <param name="useF">设置频率标识</param>
        ///// <returns>true成功 false超时</returns>
        //private bool RF_Set(int Addr,
        //                   int Lvl,
        //                   float P,
        //                   float F,
        //                   bool RFon,
        //                   bool ignoreRFon,
        //                   bool useP,
        //                   bool useF)
        //{
        //    bool RF_Succ = true;

        //    RFSignal.RFClear(Addr, Lvl);

        //    //紫光功放改变频率会影响功率，需先设置频率；韩国功放改变功率会影响频率，需先设置功率
        //    if (RF_Type == 0)
        //    {
        //        if (useF)
        //            RFSignal.RFFreq(Addr, Lvl, F);

        //        if (useP)
        //            RFSignal.RFPower(Addr, Lvl, P);
        //    }
        //    else
        //    {
        //        if (useP)
        //            RFSignal.RFPower(Addr, Lvl, P);

        //        if (useF)
        //            RFSignal.RFFreq(Addr, Lvl, F);
        //    }

        //    if (!ignoreRFon)
        //    {
        //        if (RFon)
        //            RFSignal.RFOn(Addr, Lvl);
        //        else
        //            RFSignal.RFOff(Addr, Lvl);
        //    }

        //    RFSignal.RFStart(Addr);


        //    //等待功放
        //    if (Addr == exe_params.DevInfo.RF_Addr1)
        //    {
        //        RF_Succ = power1_Handle.WaitOne(1000, true);
        //        power1_Handle.Reset();
        //    }
        //    else
        //    {
        //        RF_Succ = power2_Handle.WaitOne(1000, true);
        //        power2_Handle.Reset();
        //    }

        //    if (RFon == true && ignoreRFon == false)
        //    {
        //        if (RF_Type == 0)
        //        {
        //            if (Addr == exe_params.DevInfo.RF_Addr1)
        //                Thread.Sleep(Wait_time1);
        //            else
        //                Thread.Sleep(Wait_time2);
        //        }
        //    }
        //    else
        //    {
        //        if (RF_Type == 0)
        //            Thread.Sleep(50);
        //        else
        //            Thread.Sleep(150);
        //    }

        //    //返回通信超时的情况
        //    return (!RF_Succ);
        //}
        //#endregion
        //#region RF_Sample
        ///// <summary>
        ///// RF_Sample
        ///// </summary>
        ///// <param name="Addr">功放地址</param>
        ///// <param name="Lvl">命令等级</param>
        ///// <param name="status">功放状态对象</param>
        ///// <returns>true成功 false超时</returns>
        //private bool RF_Sample(int Addr,
        //                      int Lvl,
        //                      ref PowerStatus status)
        //{
        //    bool RF_Succ = true;

        //    RFSignal.RFClear(Addr, Lvl);

        //    RFSignal.RFSample(Addr, Lvl);

        //    RFSignal.RFStart(Addr);

        //    if (Addr == exe_params.DevInfo.RF_Addr1)
        //    {
        //        RF_Succ = power1_Handle.WaitOne(1000, true);
        //        power1_Handle.Reset();
        //    }
        //    else
        //    {
        //        RF_Succ = power2_Handle.WaitOne(1000, true);
        //        power2_Handle.Reset();
        //    }

        //    //没有发生功放通信超时，则获取功放状态信息
        //    if (RF_Succ)
        //        RFSignal.RFStatus(Addr, ref status);

        //    //返回通信超时的情况
        //    return (!RF_Succ);
        //}

        //#endregion 

       void RF1_Close()
       {
           Thread th = new Thread(RF_set1);
           th.IsBackground = true;
           th.Start(false);
       }
       void RF2_Close()
       {
           Thread th = new Thread(RF_set2);
           th.IsBackground = true;
           th.Start(false);
       }

       #region radio
       private void radioButton1_CheckedChanged(object sender, EventArgs e)
       {
           RF_Switch(App_Configure.Cnfgs.ComAddr1, RFPriority.LvlTwo, radioButton1.Checked);

           if (!radioButton1.Checked)
           {
               radioButton2.Checked = true;
               this.label1.Text = "--.--";
               //groupBox3.Enabled = false;
               //float freq_1 = Convert.ToSingle(numericUpDown4.Value);
               //float power_1 = Convert.ToSingle(numericUpDown1.Value);
               //exe_params.TmeParam.F1 = freq_1;
               //exe_params.TmeParam.P1 = OffsetPower(freq_1, power_1, 1);

               //exe_params.DevInfo.RF_Addr1 = App_Configure.Cnfgs.ComAddr1;
               //exe_params.RFInvolved = RFPriority.LvlTwo;
               //rfStatus_1 = new PowerStatus();
               //rfErrors_1 = new RFErrors();

               //Thread th = new Thread(StartRF1);
               //Thread th = new Thread(StartRF1);
               //th.IsBackground = true;
               //th.Start(true);
               //StartRF1(true);
           }
       }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                //groupBox3.Enabled = false;
                //if (thdRF1 != null && thdRF1.IsAlive)
                //{
                //    thdRF1.Abort();
                //}

                //StopRF(RFPriority.LvlTwo);
                //Thread th = new Thread(RF_set1);
                //Thread th = new Thread(StartRF1);
                //th.IsBackground = true;
                //th.Start(false);
                //StartRF1(false);

            }
        }
        #endregion

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            RF_Switch(App_Configure.Cnfgs.ComAddr2, RFPriority.LvlTwo, radioButton4.Checked);

            if (!radioButton4.Checked)
            {
                radioButton3.Checked = true;
                this.label2.Text = "--.--";
                //groupBox4.Enabled = false;
                //float freq_2 = Convert.ToSingle(numericUpDown3.Value);
                //float power_2 = Convert.ToSingle(numericUpDown2.Value);
                //exe_params.TmeParam.F2 = freq_2;
                //exe_params.TmeParam.P2 = OffsetPower(freq_2, power_2, 2);

                //exe_params.DevInfo.RF_Addr2 = App_Configure.Cnfgs.ComAddr2;
                //exe_params.RFInvolved = RFInvolved.Rf_2;
                //rfStatus_2 = new PowerStatus();
                //rfErrors_2 = new RFErrors();

                //thdRF2 = new Thread(StartRF);
                //thdRF2.IsBackground = true;
                //thdRF2.Start(RFInvolved.Rf_2);
                //Thread th = new Thread(RF_set2);
                //Thread th = new Thread(StartRF2);
                //th.IsBackground = true;
                //th.Start(true);
                //StartRF2(true);
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                //groupBox4.Enabled = false;
                //if (thdRF2 != null && thdRF2.IsAlive)
                //{
                //    thdRF2.Abort();
                //}

                //StopRF(RFInvolved.Rf_2);
                //Thread th = new Thread(RF_set2);
                //Thread th = new Thread(StartRF2);
                //th.IsBackground = true;
                //th.Start(false);
                //StartRF2(false);
            }
        }
        #endregion

        #region 计算频谱补偿
        /// <summary>
        /// 计算频谱补偿
        /// </summary>
        /// <param name="freq">频率</param>
        /// <param name="p">接收到的功率</param>
        /// <returns>补偿后的功率</returns>
        private float OffsetSpec(float freq, float p)
        {
            //MessageBox.Show("2");
            float revP = p;
                if (radioButton6.Checked)
                {
                    revP = Rx_Tables.Offset(freq, FuncModule.PIM, true) + p;
                //Log.WriteLog("revp="+revP.ToString()+" offset="+Rx_Tables.Offset(freq, FuncModule.PIM, true).ToString()+" rxref=" + App_Settings.spc.RxRef.ToString() + "  freq=" + freq.ToString() + "  pim=" + p.ToString(), Log.EFunctionType.PIM);
                }
                else
                {
                    revP = Rx_Tables.Offset(freq, FuncModule.PIM, false) + p ;
                    //Log.WriteLog("revp=" + revP.ToString() + " offset=" + Rx_Tables.Offset(freq, FuncModule.PIM, true).ToString() + " rxref=" + App_Settings.spc.RxRef.ToString() + "  freq=" + freq.ToString() + "  pim=" + p.ToString(), Log.EFunctionType.PIM);
                }
          

            return (float)Math.Round(revP, 2);
        }

        #endregion

        #region 启动频谱分析
        /// <summary>
        /// 启动频谱分析
        /// </summary>
        private void StartScan()
        {
            Monitor.Enter(ctrl);
            ctrl.Quit = false;
            Monitor.Exit(ctrl);
            //float centerFreq = Convert.ToSingle(textBox5.Text);
            float centerFreq = Convert.ToSingle(pimval/1000);
            object o;

            SpectrumLib.Models.ScanModel ScanModel;
            ScanModel = new ScanModel();
            ScanModel.StartFreq = centerFreq - App_Settings.pim.Scanband;
            ScanModel.EndFreq = centerFreq + App_Settings.pim.Scanband;
            ScanModel.Unit = CommonDef.EFreqUnit.MHz;
            ScanModel.Att = 0;
            //ScanModel.Rbw = -1000;
            ScanModel.EnableTimer = true;
            ScanModel.Continued = false;
            ScanModel.TimeSpan = App_Settings.spc.SampleSpan;
            //Log.WriteLog("center"+centerFreq.ToString(),Log.EFunctionType.PIM);
            if (type == 1)
            {
                ScanModel.FullPoints = true;
            }
            else
            {
                ScanModel.FullPoints = false;
            }

            ScanModel.Deli_averagecount = 6;
            ScanModel.Deli_detector = "AVERage";
            ScanModel.Deli_ref = -50;//REF
            ScanModel.Deli_refoffset = 0;
            ScanModel.Deli_startspe = 1;
            ScanModel.DeliSpe = CommonDef.SpectrumType.Deli_SPECTRUM;
            ScanModel.Deli_isSpectrum = true;

            o = ScanModel;

            //thdAnalysis = new Thread(ISpectrumObj.StartAnalysis);
            //thdAnalysis.IsBackground = true;
            //thdAnalysis.Start(o);
            Thread th = new Thread(ISpectrumObj.StartAnalysis);
            th.IsBackground = true;
            th.Start(o);
        }

        void StartPim()
        { 
        
        }

        #endregion

        #region 停止频谱分析
        /// <summary>
        /// 停止频谱分析
        /// </summary>
       public void StopScan()
        {
            RF_Switch(App_Configure.Cnfgs.ComAddr1, RFPriority.LvlTwo, false);
            RF_Switch(App_Configure.Cnfgs.ComAddr2, RFPriority.LvlTwo, false);
            this.Invoke(new ThreadStart(delegate()
            {
                radioButton2.Checked = true;
                radioButton3.Checked = true;
                label11.Text = "--.--";
                label12.Text = "--.--";
            }));                        
            ISpectrumObj.StopAnalysis();        
        }

        #endregion

        #region 获取频谱数据
        /// <summary>
        /// 获取频谱数据
        /// </summary>
        /// <returns></returns>
        private float GetSpeRev()
        {
            int maxIndex = 0;
            float max = float.MinValue;
            PointF[] PaintPointFs = (PointF[])ISpectrumObj.GetSpectrumData();
            for (int i = 0; i < PaintPointFs.Length; i++)
            {
                if (PaintPointFs[i].Y > max)
                {
                    max = PaintPointFs[i].Y;
                    maxIndex = i;
                }
            }
            //MessageBox.Show("1");
            max = OffsetSpec(PaintPointFs[maxIndex].X, PaintPointFs[maxIndex].Y);
            return max;
        }
        private float GetSpeRev2()
        {
            int maxIndex = 0;
            float max = float.MinValue;
            PointF[] PaintPointFs = (PointF[])ISpectrumObj.GetSpectrumData();
            for (int i = 0; i < PaintPointFs.Length; i++)
            {
                if (PaintPointFs[i].Y > max)
                {
                    max = PaintPointFs[i].Y;
                    maxIndex = i;
                }
            }

            max = OffsetSpec(PaintPointFs[maxIndex].X, PaintPointFs[maxIndex].Y);
            return max;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
           
            pimval = decimal.Parse(textBox5.Text) * 1000;

            analyzeSpecialProcess(ref f1, ref f2, ref pimval);

            StartScan();
        
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopScan();
        }
        #region 窗体消息

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MessageID.SPECTRUEME_SUCCED:
                    //label9.Text = (GetSpeRev()+App_Configure.Cnfgs.RxRef).ToString("0.0");
                    label9.Text = GetSpeRev().ToString("F1");
                    Monitor.Enter(ctrl);
                    ctrl.Quit = true;
                    Monitor.Exit(ctrl);
                    break;
                case MessageID.SPECTRUM_ERROR:
                    MessageBox.Show(this, "Spectrum analysis failed. It may be caused by the spectrum device does not connect or scanning failed!");
                    StopScan();
                    break;
                case MessageID.RF_SUCCED_ALL:
                    //if (m.WParam.ToInt32() == App_Configure.Cnfgs.ComAddr1)
                    //{
                    //    //MessageBox.Show("rf1_set");
                    //    power1_Handle.Set();
                    //}
                    //if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr2)
                    //{
                    //    //MessageBox.Show("rf2_set");
                    //    power2_Handle.Set();
                    //}
                    break;
                case MessageID.RF_ERROR:
                    //if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr1)
                    //{
                    //    if (thdRF1 != null && thdRF1.IsAlive)
                    //    {
                    //        thdRF1.Abort();
                    //    }
                    //    StopRF(RFPriority.LvlTwo);
                    //    MessageBox.Show(this, "PA1 operation failed!");
                    //}
                    //if (m.WParam.ToInt32() == exe_params.DevInfo.RF_Addr2)
                    //{
                    //    if (thdRF2 != null && thdRF2.IsAlive)
                    //    {
                    //        thdRF2.Abort();
                    //    }
                    //    StopRF(RFInvolved.Rf_2);
                    //    MessageBox.Show(this, "PA2 operation failed!");
                    //}
                    break;
                case MessageID.MS_RFSET1:
                    if ((int)m.LParam == 1)
                    {
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Checked = true;
                    }
                    break;
                case MessageID.MS_RFSET2:
                    if ((int)m.LParam == 1)
                    {
                        radioButton4.Checked = true;
                    }
                    else
                    {
                        radioButton3.Checked = true;
                    }
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        #endregion

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton6.Checked)
                GPIO.Rev();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
                GPIO.Fwd();
        }

        public void MS_SETTING_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                //    if (thdRF1 != null && thdRF1.IsAlive)
                //    {
                //        thdRF1.Abort();
                //    }
                //    StopRF(RFPriority.LvlTwo);
                //    if (thdRF2 != null && thdRF2.IsAlive)
                //    {
                //        thdRF2.Abort();
                //    }
                //StopRF(RFInvolved.Rf_2);
                //RF_SetFirst(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, false);
                //RF_SetFirst(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, false);
                 RF_Set(App_Configure.Cnfgs.ComAddr1,
                     (int)RFPriority.LvlTwo,
                     false, false, true, true);
                 RF_Set(App_Configure.Cnfgs.ComAddr1,
                     (int)RFPriority.LvlTwo,
                     false, false, true, true);
                StopScan();
                //sa_ms.CloseClient(add);
                sa_ms.OnStop();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
           
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
           
        }
        void set_RF1start()
        {
            Thread th = new Thread(set_F1pow);
            th.IsBackground = true;
            th.Start();
        }

        void set_RF2start()
        {
            Thread th = new Thread(set_F2pow);
            th.IsBackground = true;
            th.Start();
        }
        void set_F1pow()
        {
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
                RF_Set_Sample(App_Configure.Cnfgs.ComAddr1, RFPriority.LvlTwo,OffsetPower( Convert.ToSingle(f1/1000),Convert.ToSingle(pow1),1), Convert.ToSingle(f1/1000), ref  status1);
            else
            {
                RF_Set(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1/1000), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1/1000));
                //RF_Sample(App_Configure.Cnfgs.ComAddr1, (int)RFPriority.LvlTwo, ref  status1);
            }
            this.Invoke(new ThreadStart(delegate()
            {
                button4.Enabled = true;
                button5.Enabled = true;
            }));
        }
        void set_F2pow()
        {
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
                RF_Set_Sample(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, OffsetPower(Convert.ToSingle(f2/1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2/1000), ref status2);
            else
            {
                RF_Set(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, OffsetPower(Convert.ToSingle(f2/1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2/1000));
                //RF_Sample(App_Configure.Cnfgs.ComAddr2, (int)RFInvolved.Rf_2, ref  status2);
            }
            this.Invoke(new ThreadStart(delegate()
            {
                button6.Enabled = true;
                button7.Enabled = true;
            }));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = false;
            pow1 = numericUpDown1.Value;
            set_RF1start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            button5.Enabled = false;
            f1 = numericUpDown4.Value*1000;
            set_RF1start();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button7.Enabled = false;
            pow2 = numericUpDown2.Value;
            set_RF2start();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button7.Enabled = false;
            f2 = numericUpDown3.Value*1000;
            set_RF2start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Enabled = false;
            Thread th = new Thread(SingleSample);
            th.IsBackground = true;
            th.Start();
        }

        void SingleSample()
        {
            if (App_Configure.Cnfgs.Spectrum == SpectrumType.FanShuang)
            {
                RF_Set_Sample(App_Configure.Cnfgs.ComAddr1, RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f1/1000), Convert.ToSingle(pow1), 1), Convert.ToSingle(f1/1000), ref  status1);
                RF_Set_Sample(App_Configure.Cnfgs.ComAddr2, RFPriority.LvlTwo, OffsetPower(Convert.ToSingle(f2/1000), Convert.ToSingle(pow2), 2), Convert.ToSingle(f2 / 1000), ref  status2);
            }
            else
            {
                RF_Sample(App_Configure.Cnfgs.ComAddr2, RFPriority.LvlTwo, ref  status2);
                RF_Sample(App_Configure.Cnfgs.ComAddr1, RFPriority.LvlTwo, ref  status1);
            }

            this.Invoke(new ThreadStart(delegate()
            {
                button8.Enabled = true;
            }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Enabled = true;
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
            groupBox4.Enabled = true;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            RF_Dispose();
            RF_Rest(handle_);
            sa_ms.OnStop();
            StopScan();
            this.Hide();
            
        }

        public void RF_Rest(IntPtr handle)
        {
            //RFSignal.InitRFSignal(handle);
            //bool flag = false;
            //int adrr1 = App_Configure.Cnfgs.ComAddr1;
            //int adrr2 = App_Configure.Cnfgs.ComAddr2;
            //int rfClass = App_Configure.Cnfgs.RFClass;
            //int rFFormula = App_Configure.Cnfgs.RFFormula;

            //flag = RFSignal.NewRFSignal(adrr1, rfClass, rFFormula);           
            //flag = RFSignal.NewRFSignal(adrr2, rfClass, rFFormula);
        }
        public void RF_Dispose()
        {
            isFirstConnect = 0;
            //RFSignal.FinaRFSignal();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            IniFile.SetFileName("d:\\settings\\Settings_Spc.ini");
            App_Settings.spc.RxRef = Convert.ToSingle(numericUpDown6.Value);
            IniFile.SetString("spectrum", "rxRef", App_Settings.spc.RxRef.ToString());
            //App_Settings.spc.TxRef = Convert.ToSingle(numericUpDown2.Value);
            //IniFile.SetString("spectrum", "txRef", App_Settings.spc.TxRef.ToString());
        }
        #region 启动功放测试
        /// <summary>
        /// 启动功放测试
        /// </summary>
        /// <param name="Num">功放编号</param>
        private void StartRF1(object RF)
        {
            bool opens = (bool)RF;
            bool bErrors = false;

                bErrors = RF_Set(App_Configure.Cnfgs.ComAddr1,
                      RFPriority.LvlTwo,
                      opens, false, true, true);
                isrf1_rest = true;
        }
        private void StartRF2(object RF)
        {
            bool opens = (bool)RF;
            bool bErrors = false;

            bErrors = RF_Set(App_Configure.Cnfgs.ComAddr2,
                  RFPriority.LvlTwo,
                  opens, false, true, true);
            isrf2_rest = true;

        }

        #endregion

        #region RF_Set
        /// <summary>
        /// RF_Set
        /// </summary>
        /// <param name="Addr">功放地址</param>
        /// <param name="Lvl">命令等级</param>
        /// <param name="P">功率</param>
        /// <param name="F">频率</param>
        /// <param name="RFon">开启功放标识</param>
        /// <param name="ignoreRFon">忽略开功放标识</param>
        /// <param name="useP">设置功率标识</param>
        /// <param name="useF">设置频率标识</param>
        /// <returns>true成功 false超时</returns>
        private bool RF_Set(int Addr,
                           int Lvl,
                           bool RFon,
                           bool ignoreRFon,
                           bool useP,
                           bool useF)
        {
            bool RF_Succ = true;

            RFSignal.RFClear(Addr, Lvl);

                if (RFon)
                    RFSignal.RFOn(Addr, Lvl);
                else
                    RFSignal.RFOff(Addr, Lvl);
          
            
            RFSignal.RFStart(Addr);


            //等待功放
            if (Addr == App_Configure.Cnfgs.ComAddr1)
            {
                RF_Succ = power1_Handle.WaitOne(1000, true);
                power1_Handle.Reset();
            }
            else
            {
                RF_Succ = power2_Handle.WaitOne(1000, true);
                power2_Handle.Reset();
            }

            if (RFon == true && ignoreRFon == false)
            {
                if (RF_Type == 0)
                {
                    if (Addr == App_Configure.Cnfgs.ComAddr1)
                        Thread.Sleep(Wait_time1);
                    else
                        Thread.Sleep(Wait_time2);
                }
            }
            else
            {
                if (RF_Type == 0)
                    Thread.Sleep(50);
                else
                    Thread.Sleep(150);
            }

            //返回通信超时的情况
            return (!RF_Succ);
        }


        #endregion

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

    }
}
