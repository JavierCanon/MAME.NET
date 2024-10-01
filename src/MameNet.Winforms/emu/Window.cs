using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ui;

namespace mame
{
    public class Window
    {
        private static mainForm _myParentForm;
        [DllImport("kernel32.dll ")]
        private static extern uint GetTickCount();
        [DllImport("user32.dll", EntryPoint="GetWindowRect")]
        private static extern int GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", EntryPoint="GetCursorPos")]
        public static extern bool GetCursorPos(ref Point lpPoint);
        [DllImport("user32.dll", EntryPoint = "ClipCursor")]
        private static extern bool ClipCursor(ref RECT lpRect);
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern int SetCursorPos(int x, int y);
        public static bool input_enabled,input_paused, mouse_enabled, lightgun_enabled;
        public static uint last_poll, last_event_check;
        private static bool _CursorShown = true;
        public static bool CursorShown
        {
            get
            {
                return _CursorShown;
            }
            set
            {
                if (value == _CursorShown)
                {
                    return;
                }
                if (value)
                {
                    _myParentForm.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Show();
                    });
                }
                else
                {
                    _myParentForm.Invoke((MethodInvoker)delegate
                    {
                        Cursor.Hide();
                    });
                }
                _CursorShown = value;
            }
        }
        public static void osd_update(bool skip_redraw)
        {
            if (!skip_redraw)
            {
                //winwindow_video_window_update();
            }
            winwindow_process_events(true);
            wininput_poll();
            //check_osd_inputs(machine);
        }
        public static void winwindow_process_events(bool ingame)
        {
            last_event_check = GetTickCount();
            winwindow_update_cursor_state();
        }
        public static void wininput_poll()
        {
            if (input_enabled)
            {
                last_poll = GetTickCount();
                winwindow_process_events_periodic();
            }
        }
        public static void winwindow_process_events_periodic()
        {
            uint currticks = GetTickCount();
            if (currticks - last_event_check < 1000 / 8)
            {
                return;
            }
            winwindow_process_events(true);
        }
        public static void winwindow_update_cursor_state()
        {
            Point saved_cursor_pos = new Point(-1, -1);
            RECT bounds4;
            Mame.handle2 = GetForegroundWindow();
            if (Mame.handle1 == Mame.handle2 && (!Mame.mame_is_paused() && wininput_should_hide_mouse()))
            {
                RECT bounds;
                CursorShown = false;
                GetCursorPos(ref saved_cursor_pos);
                GetWindowRect(Mame.handle3, out bounds);
                ClipCursor(ref bounds);
            }
            else
            {
                CursorShown = true;
                Mame.handle4 = GetDesktopWindow();
                GetWindowRect(Mame.handle4, out bounds4);
                ClipCursor(ref bounds4);
                if (saved_cursor_pos.X != -1 || saved_cursor_pos.Y != -1)
                {
                    SetCursorPos(saved_cursor_pos.X, saved_cursor_pos.Y);
                    saved_cursor_pos.X = saved_cursor_pos.Y = -1;
                }
            }
        }
        public static void osd_init(mainForm form)
        {
            _myParentForm = form;
            wininput_init();
        }
        public static void wininput_init()
        {
            input_enabled = true;
            switch (Machine.sName)
            {
                case "opwolf":
                case "opwolfa":
                case "opwolfj":
                case "opwolfu":
                case "opwolfb":
                case "opwolfp":
                    mouse_enabled = true;
                    lightgun_enabled = false;
                    break;
                default:
                    mouse_enabled = false;
                    lightgun_enabled = false;
                    break;
            }
            wininput_poll();
        }        
        public static void wininput_pause(bool paused)
        {
            input_paused = paused;
        }
        public static bool wininput_should_hide_mouse()
        {
            if (input_paused || !input_enabled)
                return false;
            if (!mouse_enabled && !lightgun_enabled)
                return false;
            //if (win_window_list != NULL && win_has_menu(win_window_list))
            //    return false;
            return true;
        }
    }
}
