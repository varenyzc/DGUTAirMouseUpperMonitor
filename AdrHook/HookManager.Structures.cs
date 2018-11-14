using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace AdrHook
{
    /// <summary>
    /// 功能说明：HookManager分部类，钩子信息定义
    /// 开发人员：王旭（http://www.wxzzz.com）
    /// 开发时间：2014年4月3日
    /// </summary>
    public static partial class HookManager
    {
        /// <summary>
        /// Point结构定义X和Y坐标。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct Point
        {
            /// <summary>
            /// 指定的点的横坐标。
            /// </summary>
            public int X;
            /// <summary>
            /// 指定的点的纵坐标。
            /// </summary>
            public int Y;
        }

        /// <summary>
        /// 该MSLLHOOKSTRUCT结构包含有关一个低级别的键盘输入事件信息。
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct MouseLLHookStruct
        {
            /// <summary>
            /// 指定一个Point结构，它包含的X和Y坐标的光标在屏幕坐标。
            /// </summary>
            public Point Point;

            /// <summary>
            /// 滚轮定义
            /// </summary>
            public int MouseData;

            /// <summary>
            ///指定事件注入标志
            ///指定该事件是否被注入。该值是1，如果该事件被注入;否则，它是0
            ///1-15 保留
            /// </summary>
            public int Flags;

            /// <summary>
            /// 指定此消息的时间戳
            /// </summary>
            public int Time;

            /// <summary>
            /// 指定与该消息相关联的额外信息
            /// </summary>
            public int ExtraInfo;
        }

        /// <summary>
        /// 该KBDLLHOOKSTRUCT结构包含一个低级别的键盘输入事件的信息
        /// </summary>
        /// <remarks>
        /// </remarks>
        [StructLayout(LayoutKind.Sequential)]
        private struct KeyboardHookStruct
        {
            /// <summary>
            /// 指定一个虚拟键码。该代码必须是一个值范围为1〜254
            /// </summary>
            public int VirtualKeyCode;

            /// <summary>
            /// 指定了密钥的硬件扫描码
            /// </summary>
            public int ScanCode;

            /// <summary>
            /// 指定扩展键标志，事件注入标志，上下文代码，以及过渡态的标志
            /// </summary>
            public int Flags;

            /// <summary>
            /// 指定此消息时间戳
            /// </summary>
            public int Time;

            /// <summary>
            /// 指定与消息有关的附加信息
            /// </summary>
            public int ExtraInfo;
        }
    }
}
