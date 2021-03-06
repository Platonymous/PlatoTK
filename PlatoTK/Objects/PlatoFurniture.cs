﻿using PlatoTK.Patching;
using PlatoTK.Network;

namespace PlatoTK.Objects
{
    public abstract class PlatoFurniture<TLink> : StardewValley.Objects.Furniture, IPlatoObject
        where TLink : class
    {
        protected TLink Base => Link.GetAs<TLink>();

        public ILink Link { protected get; set; }

        protected IPlatoHelper Helper { get; private set; }

        public virtual bool CanLinkWith(object linkedObject)
        {
            if (linkedObject is StardewValley.Item item)
                return PlatoObject<TLink>.CanLinkWith(item, this);

            return false;
        }

        public virtual void OnConstruction(IPlatoHelper helper, object linkedObject)
        {
            Helper = helper;
        }

        public virtual void OnLink(IPlatoHelper helper, object linkedObject)
        {
           
        }
        public virtual void OnUnLink(IPlatoHelper helper, object linkedObject)
        {
           
        }

        public virtual void Dispose()
        {
            Link.Unlink();
        }
    }
}
