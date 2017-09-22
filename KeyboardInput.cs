using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection;

namespace SquareEngine
{
    /* 
     * Class Util
     * contains a helper function for parsing key events
     */
    public static class Util
    {
        public static List<T> GetEnumValues<T>()
        {
            Type currentEnum = typeof(T);
            List<T> resultSet = new List<T>();
            if (currentEnum.IsEnum)
            {
                FieldInfo[] fields = currentEnum.GetFields(BindingFlags.Static | BindingFlags.Public);
                foreach (FieldInfo field in fields)
                    resultSet.Add((T)field.GetValue(null));
            }
            else
                throw new ArgumentException("bad arg");

            return resultSet;
        }
    }
    /* 
     * Class KeyboardDevice
     * 
     * contains the code to parse keyboard input.
     * Each update causes a the keyboard sate to be saved
     * 
     */
    public class KeyboardDevice : InputDevice<KeyboardState>
    {
        KeyboardState last;
        KeyboardState current;

        Keys[] currentKeys;
        public override KeyboardState State
        {
            get { return current; }
        }
        public Keys[] PressedKeys { get { return currentKeys; } }

        public event InputEventHandler<Keys, KeyboardState> KeyPressed;
        public event InputEventHandler<Keys, KeyboardState> KeyReleased;
        public event InputEventHandler<Keys, KeyboardState> KeyHeld;

        public KeyboardDevice()
        {
            current = Keyboard.GetState();
            Update();
        }

        public override void Update()
        {
            last = current;
            current = Keyboard.GetState();
            currentKeys = current.GetPressedKeys();
            base.Update();
            foreach (Keys key in Util.GetEnumValues<Keys>())
            {
                if (WasKeyPressed(key))
                    if (KeyPressed != null)
                        KeyPressed(this, new InputDeviceEventArgs<Keys, KeyboardState>(key, this));
                if (WasKeyReleased(key))
                    if (KeyReleased != null)
                        KeyReleased(this, new InputDeviceEventArgs<Keys, KeyboardState>(key, this));
                if (WasKeyHeld(key))
                    if (KeyHeld != null)
                        KeyHeld(this, new InputDeviceEventArgs<Keys, KeyboardState>(key, this));
            }
        }
        public bool IsKeyDown(Keys key)
        {
            return current.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return current.IsKeyUp(key);
        }
        public bool WasKeyPressed(Keys key)
        {
            if (last.IsKeyUp(key) && current.IsKeyDown(key))
                return true;
            return false;
        }
        public bool WasKeyReleased (Keys key)
        {
            if (last.IsKeyDown(key) && current.IsKeyUp(key))
                return true;
            return false;
        }
        public bool WasKeyHeld(Keys key)
        {
            if (last.IsKeyDown(key) && current.IsKeyDown(key))
                return true;
            return false;
        }
    }
}