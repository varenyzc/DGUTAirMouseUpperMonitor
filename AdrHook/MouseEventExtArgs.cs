using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AdrHook
{
    /// 功能说明：提供数据的MouseClickExt和MouseMoveExt事件。它还提供处理的属性。
    /// 开发人员：王旭（http://www.wxzzz.com）
    /// 开发时间：2014年4月3日
    /// </summary>
    public class MouseEventExtArgs : MouseEventArgs
    {
        /// <summary>
        /// 在初始化MouseEventArgs的类的新实例。
        /// </summary>
        public MouseEventExtArgs(MouseButtons buttons, int clicks, int x, int y, int delta)
            : base(buttons, clicks, x, y, delta)
        { }

        /// <summary>
        /// 在初始化MouseEventArgs的类的新实例。
        /// </summary>
        internal MouseEventExtArgs(MouseEventArgs e)
            : base(e.Button, e.Clicks, e.X, e.Y, e.Delta)
        { }

        private bool m_Handled;

        /// <summary>
        /// 将此属性设置为<b>true</b>您的事件处理程序中，以防止其它应用程序事件的进一步处理。
        /// </summary>
        public bool Handled
        {
            get { return m_Handled; }
            set { m_Handled = value; }
        }
    }
}
