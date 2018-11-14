using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace AdrHook
{
    /// <summary>
    /// 功能说明：该组件监视所有的鼠标活动在全局范围（同时也是应用程序之外）
    /// 开发人员：王旭（http://www.wxzzz.com）
    /// 开发时间：2014年4月3日
    /// </summary>
    public class GlobalEventProvider : Component
    {
        /// <summary>
        /// 该组件引发事件。该值始终为true
        /// </summary>
        protected override bool CanRaiseEvents
        {
            get
            {
                return true;
            }
        }

        #region 鼠标事件

        private event MouseEventHandler m_MouseMove;

        /// <summary>
        /// 当鼠标指针移动时发生
        /// </summary>
        public event MouseEventHandler MouseMove
        {
            add
            {
                if (m_MouseMove == null)
                {
                    HookManager.MouseMove += HookManager_MouseMove;
                }
                m_MouseMove += value;
            }

            remove
            {
                m_MouseMove -= value;
                if (m_MouseMove == null)
                {
                    HookManager.MouseMove -= HookManager_MouseMove;
                }
            }
        }

        void HookManager_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_MouseMove != null)
            {
                m_MouseMove.Invoke(this, e);
            }
        }

        private event MouseEventHandler m_MouseClick;
        /// <summary>
        /// 当点击由鼠标完成时发生
        /// </summary>
        public event MouseEventHandler MouseClick
        {
            add
            {
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick += HookManager_MouseClick;
                }
                m_MouseClick += value;
            }

            remove
            {
                m_MouseClick -= value;
                if (m_MouseClick == null)
                {
                    HookManager.MouseClick -= HookManager_MouseClick;
                }
            }
        }

        void HookManager_MouseClick(object sender, MouseEventArgs e)
        {
            if (m_MouseClick != null)
            {
                m_MouseClick.Invoke(this, e);
            }
        }

        private event MouseEventHandler m_MouseDown;

        /// <summary>
        /// 当鼠标按下鼠标按钮时发生
        /// </summary>
        public event MouseEventHandler MouseDown
        {
            add
            {
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown += HookManager_MouseDown;
                }
                m_MouseDown += value;
            }

            remove
            {
                m_MouseDown -= value;
                if (m_MouseDown == null)
                {
                    HookManager.MouseDown -= HookManager_MouseDown;
                }
            }
        }

        void HookManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_MouseDown != null)
            {
                m_MouseDown.Invoke(this, e);
            }
        }


        private event MouseEventHandler m_MouseUp;

        /// <summary>
        /// 当松开鼠标按钮时发生
        /// </summary>
        public event MouseEventHandler MouseUp
        {
            add
            {
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp += HookManager_MouseUp;
                }
                m_MouseUp += value;
            }

            remove
            {
                m_MouseUp -= value;
                if (m_MouseUp == null)
                {
                    HookManager.MouseUp -= HookManager_MouseUp;
                }
            }
        }

        void HookManager_MouseUp(object sender, MouseEventArgs e)
        {
            if (m_MouseUp != null)
            {
                m_MouseUp.Invoke(this, e);
            }
        }

        private event MouseEventHandler m_MouseDoubleClick;

        /// <summary>
        /// 当双击时，由鼠标完成时发生 
        /// </summary>
        public event MouseEventHandler MouseDoubleClick
        {
            add
            {
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick += HookManager_MouseDoubleClick;
                }
                m_MouseDoubleClick += value;
            }

            remove
            {
                m_MouseDoubleClick -= value;
                if (m_MouseDoubleClick == null)
                {
                    HookManager.MouseDoubleClick -= HookManager_MouseDoubleClick;
                }
            }
        }

        void HookManager_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (m_MouseDoubleClick != null)
            {
                m_MouseDoubleClick.Invoke(this, e);
            }
        }


        private event EventHandler<MouseEventExtArgs> m_MouseMoveExt;

        /// <summary>
        /// 当鼠标指针移动时发生
        /// </summary>
        public event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt += HookManager_MouseMoveExt;
                }
                m_MouseMoveExt += value;
            }

            remove
            {
                m_MouseMoveExt -= value;
                if (m_MouseMoveExt == null)
                {
                    HookManager.MouseMoveExt -= HookManager_MouseMoveExt;
                }
            }
        }

        void HookManager_MouseMoveExt(object sender, MouseEventExtArgs e)
        {
            if (m_MouseMoveExt != null)
            {
                m_MouseMoveExt.Invoke(this, e);
            }
        }

        private event EventHandler<MouseEventExtArgs> m_MouseClickExt;

        /// <summary>
        /// 当点击由鼠标完成时发生
        /// </summary>
        public event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt += HookManager_MouseClickExt;
                }
                m_MouseClickExt += value;
            }

            remove
            {
                m_MouseClickExt -= value;
                if (m_MouseClickExt == null)
                {
                    HookManager.MouseClickExt -= HookManager_MouseClickExt;
                }
            }
        }

        void HookManager_MouseClickExt(object sender, MouseEventExtArgs e)
        {
            if (m_MouseClickExt != null)
            {
                m_MouseClickExt.Invoke(this, e);
            }
        }


        #endregion


        #region 键盘事件

        private event KeyPressEventHandler m_KeyPress;

        /// <summary>
        /// 当一个键被按下时发生
        /// </summary>
        public event KeyPressEventHandler KeyPress
        {
            add
            {
                if (m_KeyPress == null)
                {
                    HookManager.KeyPress += HookManager_KeyPress;
                }
                m_KeyPress += value;
            }
            remove
            {
                m_KeyPress -= value;
                if (m_KeyPress == null)
                {
                    HookManager.KeyPress -= HookManager_KeyPress;
                }
            }
        }

        void HookManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (m_KeyPress != null)
            {
                m_KeyPress.Invoke(this, e);
            }
        }

        private event KeyEventHandler m_KeyUp;

        /// <summary>
        /// 当释放键时发生
        /// </summary>
        public event KeyEventHandler KeyUp
        {
            add
            {
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp += HookManager_KeyUp;
                }
                m_KeyUp += value;
            }
            remove
            {
                m_KeyUp -= value;
                if (m_KeyUp == null)
                {
                    HookManager.KeyUp -= HookManager_KeyUp;
                }
            }
        }

        private void HookManager_KeyUp(object sender, KeyEventArgs e)
        {
            if (m_KeyUp != null)
            {
                m_KeyUp.Invoke(this, e);
            }
        }

        private event KeyEventHandler m_KeyDown;

        /// <summary>
        /// 当一个键被按下时发生
        /// </summary>
        public event KeyEventHandler KeyDown
        {
            add
            {
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown += HookManager_KeyDown;
                }
                m_KeyDown += value;
            }
            remove
            {
                m_KeyDown -= value;
                if (m_KeyDown == null)
                {
                    HookManager.KeyDown -= HookManager_KeyDown;
                }
            }
        }

        private void HookManager_KeyDown(object sender, KeyEventArgs e)
        {
            m_KeyDown.Invoke(this, e);
        }

        #endregion


    }
}
