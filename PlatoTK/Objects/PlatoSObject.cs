using PlatoTK.Harmony;
using PlatoTK.Network;

namespace PlatoTK.Objects
{
    public abstract class PlatoSObject<TLink> : StardewValley.Object, IPlatoObject
        where TLink : class
    {
        protected TLink Base => Link.GetAs<TLink>();

        public ISyncedData Data { get; set; }

        public ILink Link { protected get; set; }

        protected IPlatoHelper Helper { get; private set; }

        protected bool Listening { get; set; } = false;
        
        public abstract bool CanLinkWith(object linkedObject);

        public virtual void OnConstruction(IPlatoHelper helper, object linkedObject)
        {
            Helper = helper;

        }

        public abstract Netcode.NetString GetDataLink(object linkedObject);

        public virtual void OnLink(IPlatoHelper helper, object linkedObject)
        {
            if (Data == null)
                Data = new SyncedData(GetDataLink(linkedObject));

            if (!Listening)
            {
                helper.ModHelper.Events.GameLoop.Saving += GameLoop_Saving;
                Listening = true;
            }

            Data.StartListener();
        }

        protected virtual void GameLoop_Saving(object sender, StardewModdingAPI.Events.SavingEventArgs e)
        {
            Data.Update();
        }

        public virtual void OnUnLink(IPlatoHelper helper, object linkedObject)
        {
            Data.Update();

            if (Listening)
            {
                helper.ModHelper.Events.GameLoop.Saving -= GameLoop_Saving;
                Listening = false;
            }

            Data.StopListener();
        }

        public virtual void Dispose()
        {
            Link.Unlink();
        }
    }
}
