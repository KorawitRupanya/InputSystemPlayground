﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Devices
{
    GenericDevice m_Device;
    Vector2 m_DeviceScrollView;

    public void DoGUI()
    {
        if (InputSystem.devices.Count == 0)
        {
            GUILayout.Button("No devices", Styles.BoldButton);
            return;
        }
        GUILayout.BeginHorizontal();
        m_DeviceScrollView = GUILayout.BeginScrollView(m_DeviceScrollView, GUILayout.Height(10 * Styles.FontSize));
        foreach (var d in InputSystem.devices)
        {
            var name = string.Format("{0} (Type = {1})", d.displayName, d.GetType().Name);
            if (GUILayout.Button(name, m_Device != null && m_Device.Device == d ? Styles.BoldButtonSelecetd : Styles.BoldButton))
            {
                if (m_Device != null)
                    m_Device.Dispose();

                if (d as Keyboard != null)
                    m_Device = new KeyboardDevice(d);
                else if (d as Mouse != null)
                    m_Device = new MouseDevice(d);
                else if (d as Sensor != null)
                    m_Device = new SensorDevice(d);
                else
                    m_Device = new GenericDevice(d);
            }
        }

        for (int i = 0; i < 30; i++)
        {
         //   GUILayout.Button("Device 1", Styles.BoldButton);
        }

        GUILayout.EndScrollView();
        GUILayout.EndHorizontal();

        if (m_Device != null)
            m_Device.DoGUI();
    }
}
