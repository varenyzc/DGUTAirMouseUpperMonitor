using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using AdrHook;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace AdrHookDemo
{
    public partial class main : Form
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);//系统dll导入ini写函数
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);//系统dll导入ini读函数
        string FileName = System.AppDomain.CurrentDomain.BaseDirectory + "data.ini";//ini文件名
        StringBuilder temp = new StringBuilder(255);//存储读出ini内容变量
        public int r1;
        FrmMain a=new FrmMain();
       
        public main()
        {
            InitializeComponent();
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(main_FormClosing);
            a.b = this;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            about form1 = new about();
            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.Show();
            
            HookManager.KeyDown += a.HookManager_KeyDown;
            HookManager.KeyUp += a.HookManager_KeyUp;
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void main_Load(object sender, EventArgs e)
        {
            
            GetPrivateProfileString("焦点半径", "r", "200", temp, 256, FileName);//读取ini值，默认是200
            label1.Text = temp.ToString();
            trackBar1.Value = int.Parse(label1.Text);
            r1=trackBar1.Value;
            button1.Enabled = true;
            button2.Enabled = false;
        }
        private void main_FormClosing(object sender, EventArgs e)
        {
            WritePrivateProfileString("焦点半径", "r", trackBar1.Value.ToString(), FileName);//窗口关闭，保存
        }
        private void button2_Click(object sender, EventArgs e)
        {
            a.Hide();
            HookManager.KeyDown -= a.HookManager_KeyDown;
            HookManager.KeyUp -= a.HookManager_KeyUp;
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            a.r = trackBar1.Value;
            label1.Text = trackBar1.Value.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
