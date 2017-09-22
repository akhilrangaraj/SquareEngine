
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


//a tile map is a special game screen
//it loads the map
//Engine.Camera
namespace SquareEngine
{
    class TileMap : GameScreen
    {
        
        private Boolean Mapinitialized = false;
        private List<Cell> Map;
        private int size = 0;
        public TileMap(String name, String path):base(name)
        {
            //load xml and generate stuff
            if (loadMap(path))
                Mapinitialized = true;
        }

        private Boolean loadMap(String path)
        {
            //Cell c = new Cell(1394, a); ;
            //Map.Add(c);
            //xml voodoo
            return false;
        }

        public  void Update() 
        {
        }
        public  void Draw() 
        { 
        }

    }
}
