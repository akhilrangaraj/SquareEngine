using System.Collections.ObjectModel;

namespace SquareEngine
{
    /*
     * Class GameScreenCollection
     * 
     * look At componentCollection.s
     */
    public class GameScreenCollection : KeyedCollection<string, GameScreen>
    {
        protected override string GetKeyForItem(GameScreen item)
        {
            return item.Name;
        }

        protected override void RemoveItem(int index)
        {
            GameScreen screen = Items[index];

            if (Engine.DefaultScreen == screen)
                Engine.DefaultScreen = Engine.BackgroundScreen;

            base.RemoveItem(index);
        }
    }
}