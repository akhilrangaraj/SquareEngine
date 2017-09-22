using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace SquareEngine
{
    /* 
     * Class ComponentCollection
     * Defines a collection of components
     * This is used as a list type thing, so the gamescreen can iterate through all components
     * and call draw.
     */
    public class ComponentCollection : Collection<Component>
    {
        GameScreen owner;
        public ComponentCollection(GameScreen Owner)
        {
            owner = Owner;
        }

        protected override void InsertItem(int index, Component item)
        {
            if (item.Parent != null && item.Parent != owner)
                item.Parent.Components.Remove(item);
            item.Parent = owner;
            base.InsertItem(index, item);
        }

        protected override void RemoveItem(int index)
        {
            Items[index].Parent = null;
            base.RemoveItem(index);
        }
    }
}