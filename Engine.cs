using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
namespace SquareEngine
{
    /*
     * Engine Class
     * Contains all the functions needed to display graphics, text, and play audio
     * Game shoud create a new Engine and then set it up, before loading content.
     * 
     * Todo:
     *  -Allow for reordering of gamescreens to change draw order
     */
    public static class Engine
    {
        public static GraphicsDevice GraphicsDevice;
        public static SpriteBatch SpriteBatch;
        /*
         * GameScreenCollection GameScreens - 
         * Contains the list of Game Screens
         * See GameScreen.cs for details on GameScreen Class;
         */
        public static GameScreenCollection GameScreens = new GameScreenCollection();
        public static GameTime GameTime;
        public static bool IsInitialized = false;
        public static IEServiceContainer Services;
        public static IEContentManager Content;
        public static AudioEngine Audio;
        public static GameScreen BackgroundScreen;
        public static GameScreen DefaultScreen;
        public static bool DebugMode = false;
        public static SpriteFont DebugFont;

        /*
         *  public static void SetupEngine
         *  args: IGraphicsDeviceService, String
         *
         *  This function sets up the basic functions of the engine - audio/input/content manager
         *  as well as setting up a default game screen.
         *  This funtion also sets up a global sprite batch which is called by all game screens.
         *  Probably, this global sprite batch should be removed, and each GameScreen have its own,
         *  but we'll see.
         */
        public static void SetupEngine(IGraphicsDeviceService GraphicsDeviceService, string ContentDir)
        {
            Engine.GraphicsDevice = GraphicsDeviceService.GraphicsDevice;
            if (Engine.GraphicsDevice == null)
                throw new Exception("Oh shit, something went wrong");
            Engine.SpriteBatch = new SpriteBatch(Engine.GraphicsDevice);
            Engine.IsInitialized = true;
            Engine.Services = new IEServiceContainer();
            Engine.Services.AddService(typeof(IGraphicsDeviceService), GraphicsDeviceService);
            Engine.Content = new IEContentManager(Services);
            Engine.Content.RootDirectory = ContentDir;
            Engine.Audio = new AudioEngine();
            Engine.Services.AddService(typeof(AudioEngine), Audio);
          
            BackgroundScreen = new GameScreen("Engine.Background");

            BackgroundScreen.OverrideDrawBlocked = true;
            BackgroundScreen.OverrideInputBlocked = true;
            BackgroundScreen.OverrideUpdateBlocked = true;
            DefaultScreen = BackgroundScreen;
            KeyboardDevice kbd = new KeyboardDevice();
            Engine.Services.AddService(typeof(KeyboardDevice), kbd);
            MouseDevice mouse = new MouseDevice();
            Engine.Services.AddService(typeof(MouseDevice), mouse);
        }
        /*
         * public static void Update
         * args: GameTime
         * 
         * This function calls the update on all GameScreens, causing them to update
         * all components in said gamescreen.
         * 
         * Todo: check all the O(n^2) loops
         */
        public static void Update(GameTime gameTime)
        {
            Engine.GameTime = gameTime;
            List<GameScreen> updating = new List<GameScreen>();
            bool blocked = false;
            foreach (GameScreen screen in GameScreens) {
                if (screen.BlocksUpdate && !blocked)
                {
                    blocked = true;

                    updating.Add(screen);
                }
                else if (screen.OverrideUpdateBlocked && blocked)
                {
                    updating.Add(screen);
                }
                else if(!blocked)
                {
                    updating.Add(screen);
                }
            }
            /*
            for (int i = GameScreens.Count - 1; i >= 0; i--)
                if (GameScreens[i].BlocksUpdate)
                {
                    if (i > 0)
                        for (int j = i - 1; j >= 0; j--)
                            if (!GameScreens[j].OverrideUpdateBlocked)
                                updating.Remove(GameScreens[j]);
                    break;
                }
            */
            foreach (GameScreen screen in updating)
                if (screen.Initialized)
                    screen.Update();

            updating.Clear();

            foreach (GameScreen screen in GameScreens)
                updating.Add(screen);
            for (int i = GameScreens.Count - 1; i >= 0; i--)
                if (GameScreens[i].BlocksInput)
                {
                    if (i > 0)
                        for (int j = i - 1; j >= 0; j--)
                            if (!GameScreens[j].OverrideInputBlocked)
                                updating.Remove(GameScreens[j]);
                    break;
                }
            foreach (GameScreen screen in GameScreens)
                if (!screen.InputDisabled)
                    screen.IsInputAllowed = updating.Contains(screen);
                else
                    screen.IsInputAllowed = false;

        }
        /* 
         * public static void Draw
         * args GameTime
         * 
         * Engine calls draw on all GameScreens, which then call draw on all components
         * 
         */
        public static void Draw(GameTime gametime)
        {
            Engine.GameTime = gametime;
            List<GameScreen> drawing = new List<GameScreen>();

            GraphicsDevice.Clear(Color.Black);

            foreach (GameScreen screen in GameScreens)
                if (screen.Visible)
                    drawing.Add(screen);

            for (int i = GameScreens.Count - 1; i >= 0; i--)
                if (GameScreens[i].BlocksDraw)
                {
                    if (i > 0)
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (!GameScreens[j].OverrideDrawBlocked)
                                drawing.Remove(GameScreens[j]);
                        }

                    break;
                }
            foreach (GameScreen screen in drawing)
                if (screen.Initialized)
                    screen.Draw();
        }
    }
}
