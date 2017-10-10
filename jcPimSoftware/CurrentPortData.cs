using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
namespace jcPimSoftware
{
    class CurrentPortData
    {
        DataTable dt;
       public PortElement[] pe;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        public   CurrentPortData(int num)
        {
            pe = new PortElement[num];
            for (int i = 0; i < num; i++)
            {
                pe[i] = new PortElement();
            }
        }

        /// <summary>
        /// 赋值数据
        /// </summary>
        /// <param name="portnum">当前pe</param>
        /// <param name="dt">dbc</param>
        /// <param name="dtm">dbm</param>
        /// <param name="wdt">wdbc</param>
        /// <param name="wdtm">wdbm</param>
        /// <param name="Ystart">开始坐标</param>
        /// <param name="Ystop">结束坐标</param>
        /// <param name="RxStart"></param>
        /// <param name="RxEnd"></param>
        /// <param name="temp">扫描数据</param>
        /// <param name="sweep">扫描类型</param>
        /// <param name="n1">上扫描点数</param>
        public void GetPortData(int portnum, DataTable dt, DataTable dtm, DataTable wdt,DataTable wdtm,float Ystart, float Ystop,
                                float RxStart, float RxEnd, CsvReport_Pim_Entry[] temp,bool sweep,int n1)
        {
            try
            {
                pe[portnum].dt = dt;
                pe[portnum].dtm = dtm;
                pe[portnum].wdt = wdt;
                pe[portnum].wdtm = wdtm;
                pe[portnum].Ystart = Ystart;
                pe[portnum].Ystop = Ystop;
                pe[portnum].RxEnd = RxEnd;
                pe[portnum].RxStart = RxStart;
                pe[portnum].temp = temp;
                pe[portnum].sweep = sweep;
                if (sweep)
                {
                    if (temp.Length >= n1)
                    {
                        pe[portnum].point = new PointF[n1];
                        pe[portnum].point2 = new PointF[temp.Length - n1];
                        pe[portnum].point3 = new PointF[n1];
                        pe[portnum].point4 = new PointF[temp.Length - n1];
                    }
                    else
                    {
                        pe[portnum].point = new PointF[temp.Length];
                        pe[portnum].point2 = new PointF[0];
                        pe[portnum].point3 = new PointF[temp.Length];
                        pe[portnum].point4 = new PointF[0];
                    }
                    for (int i = 0; i < temp.Length; i++)
                    {

                        if (i >= n1)
                        {
                            pe[portnum].point2[i - n1].X = temp[i].Im_F;
                            pe[portnum].point2[i - n1].Y = temp[i].Im_V;
                            pe[portnum].point4[i - n1].X = temp[i].Im_F;
                            pe[portnum].point4[i - n1].Y = temp[i].Im_V - 43;
                        }
                        else
                        {
                            pe[portnum].point[i].X = temp[i].Im_F;
                            pe[portnum].point[i].Y = temp[i].Im_V;
                            pe[portnum].point3[i].X = temp[i].Im_F;
                            pe[portnum].point3[i].Y = temp[i].Im_V - 43;
                        }
                    }
                }
                else
                {
                    pe[portnum].point2 = new PointF[temp.Length];
                    pe[portnum].point4 = new PointF[temp.Length];
                    pe[portnum].point = new PointF[0];
                    pe[portnum].point3 = new PointF[0];
                    for (int i = 0; i < temp.Length; i++)
                    {
                        pe[portnum].point2[i].X = i;
                        pe[portnum].point2[i].Y = temp[i].Im_V;
                        pe[portnum].point4[i].X = i;
                        pe[portnum].point4[i].Y = temp[i].Im_V - 43;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public PortElement GetClass(int num)
        {
            return pe[num];
        }

        /// <summary>
        /// 清空datatable数据
        /// </summary>
        /// <param name="num"></param>
        public void ClearPortDatatable(int num)
        {
            pe[num].dt.Rows.Clear();
            pe[num].dtm.Rows.Clear();
            pe[num].wdt.Rows.Clear();
            pe[num].wdtm.Rows.Clear();
        }
        public void clear(int num)
        {
            pe[num].dt.Clear();
            pe[num].dtm.Clear();
        }
    }
    class PortElement
    {
       public float RxStart = 0;
       public  float RxEnd = 0;
       public  float Ystart = 0;
       public  float Ystop = 0;
       public  DataTable dt;
       public DataTable dtm;
       public  DataTable wdt ;
       public  DataTable wdtm ;
       public CsvReport_Pim_Entry[] temp;
       public bool sweep = true;
       public  PointF[] point;
       public  PointF[] point2 ;
       public PointF[] point3;
       public PointF[] point4;
        /// <summary>
        /// 构造函数
        /// </summary>
        public PortElement()
        {
            dt = new DataTable();
            dtm = new DataTable();
            wdt  = new DataTable();
            wdtm = new DataTable();
            AddColumns(dt);
            AddColumns(dtm);
            AddColumns(wdt);
            AddColumns(wdtm);
        }
        public void AddColumns(DataTable dt)
        {
            dt.Columns.Add("NO.");
            dt.Columns.Add("P1(dBm)");
            dt.Columns.Add("F1(MHz)");
            dt.Columns.Add("P2(dBm)");
            dt.Columns.Add("F2(MHz)");
            dt.Columns.Add("Im_F(MHz)");
            dt.Columns.Add("Im_V(dBm)");
        }
    }
}
