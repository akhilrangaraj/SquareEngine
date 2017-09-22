using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SquareEngine
{

    /*
     * Class IEContentManager
     * 
     * ContentManager class for SquareEngine
     * actually i stole this code from some tutorial, so i need to read 
     * up on this lol
     * 
     */
    public class IEContentManager : ContentManager
    {
        public IEContentManager(IServiceProvider service)
            : base(service) { }

        public bool PreserveAssets = true;
        List<IDisposable> disposable = new List<IDisposable>();
        Dictionary<string, object> loaded = new Dictionary<string, object>();

        public override T Load<T>(string assetName)
        {
            System.Console.Write(assetName);
            T r = this.ReadAsset<T>(assetName, RecordIDisposable);
            if (PreserveAssets && !loaded.ContainsKey(assetName))
                loaded.Add(assetName, r);

            return r;
        }

        void RecordIDisposable(IDisposable asset)
        {
            if (PreserveAssets)
                disposable.Add(asset);
        }

        public override void Unload()
        {
            foreach (IDisposable disp in disposable)
                disp.Dispose();

            loaded.Clear();
            disposable.Clear();
        }
        public void Unload(string assetName)
        {
            if (loaded.ContainsKey(assetName))
            {
                if (loaded[assetName] is IDisposable &&
                    disposable.Contains((IDisposable)loaded[assetName]))
                {
                    IDisposable obj = disposable[
                        disposable.IndexOf((IDisposable)loaded[assetName])];
                    obj.Dispose();
                    disposable.Remove(obj);
                }
                loaded.Remove(assetName);
            }
        }
    }
}

