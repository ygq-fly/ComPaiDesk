using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace jcPimSoftware
{
    public partial class FormPimRead : Form
    {
        #region ��������

        /// <summary>
        /// ����ļ��ĸ�·��
        /// </summary>
        private string RootPath = "C:\\";

        /// <summary>
        /// �ļ�·��
        /// </summary>
        private string _FilePath = "";
        public string FilePath
        {
            get { return _FilePath; }
        }

        #endregion


        #region ���幹��
        /// <summary>
        /// ���幹��
        /// </summary>
        public FormPimRead()
        {
            InitializeComponent();
        }

        #endregion


        #region ��ť�¼�

        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.InitialDirectory = App_Configure.Cnfgs.Path_Rpt_Vsw;
            openFile.Filter = "CSV File(*.csv)|*.csv";

            if (openFile.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                txtFilePath.Text = openFile.FileName;
            }
        }

        #endregion

        #region ȷ��
        /// <summary>
        /// ȷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            _FilePath = txtFilePath.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }

        #endregion

        #region ȡ��
        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #endregion
    }
}