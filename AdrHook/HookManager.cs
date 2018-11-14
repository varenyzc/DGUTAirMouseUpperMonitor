using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace AdrHook
{
    /// <summary>
    /// 功能说明：这个类提供全局键盘与鼠标的信息监视（同时在程序之外的），并且提供事件响应
    /// 开发人员：王旭（http://www.wxzzz.com）
    /// 开发时间：2014年4月3日
    /// </summary>
    public partial class HookManager
    {
        #region 鼠标事件

        private static event MouseEventHandler s_MouseMove;

        /// <summary>
        /// 当鼠标指针移动时触发
        /// </summary>
        public static event MouseEventHandler MouseMove
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseMove += value;
            }

            remove
            {
                s_MouseMove -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event EventHandler<MouseEventExtArgs> s_MouseMoveExt;

        /// <summary>
        /// 当鼠标指针移动时触发
        /// </summary>
        /// <remarks>
        /// 此事件提供类型的扩展参数 <see cref="MouseEventArgs"/> 使您可以 
        /// 在其他程序中对鼠标移动做进一步的处理
        /// </remarks>
        public static event EventHandler<MouseEventExtArgs> MouseMoveExt
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseMoveExt += value;
            }

            remove
            {

                s_MouseMoveExt -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseClick;

        /// <summary>
        /// 当点击由鼠标完成时触发
        /// </summary>
        public static event MouseEventHandler MouseClick
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseClick += value;
            }
            remove
            {
                s_MouseClick -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event EventHandler<MouseEventExtArgs> s_MouseClickExt;

        /// <summary>
        /// 当点击由鼠标完成时触发
        /// </summary>
        /// <remarks>
        /// 此事件提供类型的扩展参数 <see cref="MouseEventArgs"/> 使您可以 
        /// 在其他程序中对鼠标点击做进一步的操作
        /// </remarks>
        public static event EventHandler<MouseEventExtArgs> MouseClickExt
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseClickExt += value;
            }
            remove
            {
                s_MouseClickExt -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseDown;

        /// <summary>
        /// 当鼠标按下鼠标按钮时触发
        /// </summary>
        public static event MouseEventHandler MouseDown
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseDown += value;
            }
            remove
            {
                s_MouseDown -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseUp;

        /// <summary>
        /// 当松开鼠标按钮时触发
        /// </summary>
        public static event MouseEventHandler MouseUp
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseUp += value;
            }
            remove
            {
                s_MouseUp -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        private static event MouseEventHandler s_MouseWheel;

        /// <summary>
        /// 当滑动鼠标滚轮时发生
        /// </summary>
        public static event MouseEventHandler MouseWheel
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                s_MouseWheel += value;
            }
            remove
            {
                s_MouseWheel -= value;
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }


        private static event MouseEventHandler s_MouseDoubleClick;

        //该双击事件不会直接从Hook钩子提供。触发的双击事件需要监测鼠标事件
        //当它是在Windows中定义多次点击时间的时间间隔来决定触发事件

        /// <summary>
        /// 当双击时，由鼠标完成时触发
        /// </summary>
        public static event MouseEventHandler MouseDoubleClick
        {
            add
            {
                EnsureSubscribedToGlobalMouseEvents();
                if (s_MouseDoubleClick == null)
                {
                    //我们创建了一个计时器，用于监测两次点击之间的时间间隔
                    s_DoubleClickTimer = new Timer
                    {
                        //这个时间间隔将被设置为我们从窗户中检索该值。这是一个从窗户控制面板设置。
                        Interval = GetDoubleClickTime(),
                        //如果我们不开始计时。当点击时它会被启动
                        Enabled = false
                    };
                    //我们定义回调函数，定时器
                    s_DoubleClickTimer.Tick += DoubleClickTimeElapsed;
                    //我们先来监听鼠标事件。
                    MouseUp += OnMouseUp;
                }
                s_MouseDoubleClick += value;
            }
            remove
            {
                if (s_MouseDoubleClick != null)
                {
                    s_MouseDoubleClick -= value;
                    if (s_MouseDoubleClick == null)
                    {
                        //停止监听鼠标按键弹起
                        MouseUp -= OnMouseUp;
                        //配置定时器
                        s_DoubleClickTimer.Tick -= DoubleClickTimeElapsed;
                        s_DoubleClickTimer = null;
                    }
                }
                TryUnsubscribeFromGlobalMouseEvents();
            }
        }

        //此字段记录鼠标按钮，因为除了在短的时间间隔必须是也相同的按钮点击。
        private static MouseButtons s_PrevClickedButton;
        //定时监测两次点击之间的时间间隔。
        private static Timer s_DoubleClickTimer;

        private static void DoubleClickTimeElapsed(object sender, EventArgs e)
        {
            //定时器过去了，没有第二次点击发生
            s_DoubleClickTimer.Enabled = false;
            s_PrevClickedButton = MouseButtons.None;
        }

        /// <summary>
        /// 这种方法的目的是监视鼠标点击才能触发一个双击事件，如果点击之间的时间间隔足够短。
        /// </summary>
        /// <param name="sender">始终为空</param>
        /// <param name="e">关于点击的一些信息发生.</param>
        private static void OnMouseUp(object sender, MouseEventArgs e)
        {
            //这不应该发生
            if (e.Clicks < 1) { return; }
            //如果第二次单击发生在同一个按钮
            if (e.Button.Equals(s_PrevClickedButton))
            {
                if (s_MouseDoubleClick != null)
                {
                    //触发双击事件
                    s_MouseDoubleClick.Invoke(null, e);
                }
                //停止计时器
                s_DoubleClickTimer.Enabled = false;
                s_PrevClickedButton = MouseButtons.None;
            }
            else
            {
                //如果是第一次点击开始计时
                s_DoubleClickTimer.Enabled = true;
                s_PrevClickedButton = e.Button;
            }
        }
        #endregion

        #region 键盘事件

        private static event KeyPressEventHandler s_KeyPress;

        /// <summary>
        /// 当一个键被按下时触发
        /// </summary>
        /// <remarks>
        /// 发生下列顺序按键事件：
        /// <list type="number">
        /// <item>KeyDown</item>
        /// <item>KeyPress</item>
        /// <item>KeyUp</item>
        /// </list>
        /// </remarks>
        public static event KeyPressEventHandler KeyPress
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyPress += value;
            }
            remove
            {
                s_KeyPress -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        private static event KeyEventHandler s_KeyUp;

        /// <summary>
        /// 当释放键时触发
        /// </summary>
        public static event KeyEventHandler KeyUp
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyUp += value;
            }
            remove
            {
                s_KeyUp -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }

        private static event KeyEventHandler s_KeyDown;

        /// <summary>
        /// 当一个键被按下时触发
        /// </summary>
        public static event KeyEventHandler KeyDown
        {
            add
            {
                EnsureSubscribedToGlobalKeyboardEvents();
                s_KeyDown += value;
            }
            remove
            {
                s_KeyDown -= value;
                TryUnsubscribeFromGlobalKeyboardEvents();
            }
        }


        #endregion
    }
}
