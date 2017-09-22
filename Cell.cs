using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace SquareEngine
{
    class Cell 
    {
        public int TileID { get; set; }
        public  Tile tile;
        public Cell(int tileID,  Tile tile)
        {
            this.tile = tile;
            TileID = tileID;
        }
       

    
    }
}
