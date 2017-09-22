using System;
using Microsoft.Xna.Framework.Input;

namespace SquareEngine
{
    /*
     * wrapper classes for input  device
     */
    public abstract class InputDevice<T> : Component
    {
        public abstract T State { get; }

    }

    public class InputDeviceEventArgs<O, S> : EventArgs
    {
        public O Object;
        public InputDevice<S> Device;
        public S State;
        public InputDeviceEventArgs(O Object, InputDevice<S> Device)
         {
             this.Object = Object;
              this.Device = Device;
              this.State = ((InputDevice<S>)Device).State;
          }
    }

    public delegate void InputEventHandler<O, S>(object sender, InputDeviceEventArgs<O, S> e);

}