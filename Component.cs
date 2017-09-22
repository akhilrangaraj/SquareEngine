using Microsoft.Xna.Framework;
using System;

namespace SquareEngine
{
    /*
     * Class Component
     * This class is the base for all drawable objects in the game screen. It should be an abstract class
     * 
     * Global Fields -
     * GameScreen Parent - The Game screen that contains this compnent.
     * bool Initialized - This field represents wether the compnent has been added to the parent gamescreen
     * bool Visible - Sets the visiblity of the compnent
     * 
     * Public Functions
     * Component(GameScreen)
     * Component()
     * Update()
     * Draw()
     * DisableComponent()
     * 
     * Protected Functions
     * InitializeComponent(GameScreen)
     */
    public class Component
    {
        public GameScreen Parent;
        public bool Initialized = false;
        public bool Visible = true;

        /*
         * public Component(GameScreen), Component()
         * args: GameScreen - Parent of the component, none
         * 
         * this function initializes the component. If no parent is specified, it defaults to the
         * engines defaultscreen
         */
        public Component(GameScreen Parent)
        {
            InitializeComponent(Parent);
        }
        public Component()
        {
            InitializeComponent(Engine.DefaultScreen);
        }
        /*
         * protected virtual void InitializeComponent(GameScreen)
         * args: GameScreen - contains the parent game screen
         * 
         * This function adds the component to the parents list of components
         */
        protected virtual void InitializeComponent(GameScreen Parent)
        {
            if (!Engine.IsInitialized)
                throw new Exception("Engine needs to be initid");
            Parent.Components.Add(this);
            Initialized = true;
        }

        /* 
         * public virtual void Update(), Draw()
         * 
         * These functions should be overridden by subclasses
         */
        public virtual void Update() { }
        public virtual void Draw() { }

        /*
         * public virtual void DisableComponent()
         * 
         * Disables the component. Can be overriden
         */
        public virtual void DisableComponent()
        {
            Parent.Components.Remove(this);
        }
    }
}