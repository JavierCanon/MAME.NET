using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.DirectX.DirectInput;
using DIDevice = Microsoft.DirectX.DirectInput.Device;
using ui;

namespace mame
{
    public class Keyboard
    {
        public static bool bF10;
        public static DIDevice dIDevice;
        public static void InitializeInput(mainForm form1)
        {
            dIDevice = new DIDevice(SystemGuid.Keyboard);
            dIDevice.SetCooperativeLevel(form1, CooperativeLevelFlags.Background | CooperativeLevelFlags.NonExclusive);
            dIDevice.Acquire();
        }
        struct KeyState
        {
            public bool IsPressed;
            public bool IsTriggered;
            public bool WasPressed;
        };
        private static KeyState[] m_KeyStates = new KeyState[256];
        public static bool IsPressed(Key key)
        {
            return m_KeyStates[(int)key].IsPressed;
        }
        public static bool IsTriggered(Key key)
        {
            return m_KeyStates[(int)key].IsTriggered;
        }
        public static void Update()
        {
            for (int i = 0; i < 256; i++)
            {
                m_KeyStates[i].IsPressed = false;
            }
            foreach (Key key in dIDevice.GetPressedKeys())
            {
                m_KeyStates[(int)key].IsPressed = true;
            }
            for (int i = 0; i < 256; i++)
            {
                if (m_KeyStates[i].IsPressed)
                {
                    if (m_KeyStates[i].WasPressed)
                    {
                        m_KeyStates[i].IsTriggered = false;
                    }
                    else
                    {
                        m_KeyStates[i].WasPressed = true;
                        m_KeyStates[i].IsTriggered = true;
                    }
                }
                else
                {
                    m_KeyStates[i].WasPressed = false;
                    m_KeyStates[i].IsTriggered = false;
                }
            }
        }
    }
}