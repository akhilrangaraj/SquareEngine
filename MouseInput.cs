using System;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Reflection;

namespace SquareEngine
{
    /*
     * Class MouseDevice
     * 
     * Input class for mice.
     */
    public class MouseDevice : InputDevice<MouseState>
    {
        MouseState last;
        MouseState current;
        
        public override MouseState State
        {
            get { return current; }
        }
 


        public MouseDevice()
        {
            current = Mouse.GetState();
            Update();
        }

        public override void Update()
        {
            last = current;
            current = Mouse.GetState();
            base.Update();
        }
    }
}