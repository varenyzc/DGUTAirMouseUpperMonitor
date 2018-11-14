using AdrHook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Messaging;
namespace AdrHookDemo
{
    
    public partial class FrmMain : Form
    {
        public main b;
        public int r;
        bool flag = false; Graphics g;//变量定义和初始化

        public FrmMain()
        {
            InitializeComponent();
        }
        public void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == "PrintScreen")//左alt按下
            {
                flag = false;//不打开开始标志
                this.TopMost = false;//取消置顶
                this.Hide();
                //this.WindowState = FormWindowState.Minimized;//最小化
            }
        }
        public void HookManager_KeyDown(object sender, KeyEventArgs e)//键盘按下事件，有按键按下执行此函数
        {
            if (e.KeyCode.ToString() == "PrintScreen")//如果左ctrl按下
            {
                flag = true;//开始标志
                
                this.TopMost = true;//置顶界面
                this.Show();
                this.WindowState = FormWindowState.Maximized;//最大化界面
                this.ShowInTaskbar = false;//任务栏不显示
            }
           /* if (e.KeyCode.ToString() == "RMenu")//左alt按下
            {
                flag = false;//不打开开始标志
                this.TopMost = false;//取消置顶
                this.Hide();
                //this.WindowState = FormWindowState.Minimized;//最小化
            }*/
          
        }
        private void FrmMain_MouseMove(object sender, MouseEventArgs e)//鼠标移动事件，有鼠标移动则执行此函数
        {
            if (flag)//开始标志，有开始按键按下才执行，否则不执行
            {
                g = CreateGraphics();//创建界面
                g.Clear(BackColor);//填充背景颜色
                g.FillEllipse(Brushes.CornflowerBlue, MousePosition.X - r/2, MousePosition.Y - r/2, r, r);//绘制透明颜色椭圆随鼠标xy坐标移动

            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            r = b.r1;
        }

        public int message { get; set; }
    }
}
