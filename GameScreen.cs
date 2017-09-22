using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SquareEngine
{
    /* Class GameScreen
     * this is the main heart of the graphics engine.
     * Think of a gamescreen like a layer in photoshop -  components inside the game screen are on the same layer
     * when the engine iterates over all Gamescreens, it builds the complete image
     * 
     * Use - Game creates new game screen and registers it with Engine, then creates components with the
     * said game screen as a parent.
     * 
     * TODO: - Support reordering components
     * 
     * Public Fields
     * Visible - is the game screen viewable?
     * blocksupdate - game screen stops subsequent game screens from updating (used for pause screen)
     * overrideupdateblocked - gamescreen updates no matter what
     * blocksdraw - game screen stops subsequent game screens from drawing
     * overridedrawblocked - gamescreen draws no matter what
     * blocksinput - game screen stops subsequent game screens from processing input
     * override input blocked - game screen processes input no matter what
     * inputdisabled - game screen does not process input
     * isinputallowed - does the game screen have the option of using input?
     * name - name of gamescreen
     * OnInitialized - event handler for ????
     * initialized - is the game screen registered with the engine?
     * 
     * public functions
     * GameScreen(String)
     * Intialize()
     * Update()
     * Draw()
     * Disable()
     * ToString()
     */
    public class GameScreen
    {
        public ComponentCollection Components;
        public bool Visible = true;
        public bool BlocksUpdate = false;
        public bool OverrideUpdateBlocked = false;
        public bool BlocksDraw = false;
        public bool OverrideDrawBlocked = false;
        public bool BlocksInput = false;
        public bool OverrideInputBlocked = false;

        public bool InputDisabled = false;
        public bool IsInputAllowed = true;

        public string Name;

        public event EventHandler OnInitialized;

        bool initialized = false;

        public bool Initialized
        {
            get { return initialized; }
            set
            {
                initialized = value;
                if (OnInitialized != null)
                {
                    OnInitialized(this, new EventArgs());
                }
            }
        }

        /*
         * public GameScreen(String)
         * String - Name of the GameScreen
         * 
         * Sets up the Gamescreen by creating a new collection of components, and registering the screen with the engine
         */
        public GameScreen(string Name)
        {
            Components = new ComponentCollection(this);
            this.Name = Name;
            Engine.GameScreens.Add(this);


            if (!this.Initialized)
                Initialize();
        }
        public virtual void Initialize()
        {
            this.Initialized = true;
        }

        /*
         * public virtual void Update()
         * 
         * calls all registered components to update
         */
        public virtual void Update()
        {
            ComponentCollection cctemp = new ComponentCollection(this);
            foreach (Component c in Components)
            {

                if (c.Initialized)
                    cctemp.Add(c);
            }
            foreach (Component c in cctemp)
                c.Update();
        }
        
        /*
         * public virtual void Draw()
         * 
         * calls all registered components to draw
         * 
         * Todo - Investigate if each gamescreen needs its own spritebach
         */
        public virtual void Draw()
        {
            Engine.SpriteBatch.Begin();

            ComponentCollection cctemp = new ComponentCollection(this);
            foreach (Component c in Components)
            {

                if (c.Initialized)
                    cctemp.Add(c);
            }
            foreach (Component c in cctemp)
                c.Draw(); 
            Engine.SpriteBatch.End();
        }
        /*
         * public virtual void Disable()
         * 
         * deregisters the game screen
         */
        public virtual void Disable()
        {
            Components.Clear();
            Engine.GameScreens.Remove(this);
            if (Engine.DefaultScreen == this)
                Engine.DefaultScreen = Engine.BackgroundScreen;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
