using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquareEngine
{
    /*
     * Class DebugStringWriter
     * Writes a string at a specified position
     * 
     * This class is intended as a graphical debug output, rather than a text writer
     * 
     * Use Case:
     * create new DebugStringWriter, with specified value.
     * To change string, call changeString in gamescreen Update()
     * 
     * Public Functions 
     * DebugStringWriter(String,int,int,GameScreen)
     * changeString(String,int,int)
     * Draw()
     */
    public class DebugStringWriter : Component
    {
        private int myx, myy = 0;
        private String myvalue = "";

        /*
         * public DebugStringWriter(String,int,int,GameScreen)
         * Args: String,int,int,GameScreen
         * String - value of string
         * int,int - X,Y coordinates of string output
         * GameScreen - Parent screen, required for Component initialization
         */
        public DebugStringWriter(String value, int x, int y, GameScreen parent)
            : base(parent)
        {
            
            myx = x;
            myy = y;
            myvalue = value;
            parent.Visible = true;
           
        }

        /*
         * public void changeString(string,int,int)
         * args: string,int,int
         * String - new value for string
         * int,int - new XY coordinates
         */
        public void changeString(String newStr ,int x, int y)
        {
              myx = x;
            myy = y;
            myvalue = newStr;
        }

        /*
         * public override void Draw()
         * 
         * overrides Component.Draw().
         * Only draws string if engine is in debug mode.
         */
        public override void Draw()
        {
            if(Engine.DebugMode)
            {
                Vector2 textpos = new Vector2(myx, myy);
                Engine.SpriteBatch.DrawString(Engine.DebugFont, myvalue, textpos, Color.White);
            }
            
        }


    }
}
