using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using DIDevice = Microsoft.DirectX.DirectInput.Device;
using ui;

namespace mame
{
    public class Mouse
    {
        public static DIDevice mouseDevice;
        public static int deltaX, deltaY, oldX, oldY;
        public static byte[] buttons;
        public static void InitialMouse(mainForm form1)
        {
            mouseDevice = new Microsoft.DirectX.DirectInput.Device(SystemGuid.Mouse);
            mouseDevice.Properties.AxisModeAbsolute = true;
            mouseDevice.SetCooperativeLevel(form1, CooperativeLevelFlags.NonExclusive | CooperativeLevelFlags.Background);
            mouseDevice.Acquire();
        }
        public static void Update()
        {
            MouseState mouseState = mouseDevice.CurrentMouseState;
            deltaX = mouseState.X - oldX;
            deltaY = mouseState.Y - oldY;
            oldX = mouseState.X;
            oldY = mouseState.Y;
            buttons = mouseState.GetMouseButtons();
        }
    }
}
