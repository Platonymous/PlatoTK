namespace PlatoTK.Harmony
{
    public interface ILinked
    {
        ILink Link {set;}
        bool CanLinkWith(object linkedObject);
        void OnLink(IPlatoHelper helper, object linkedObject);
        void OnUnLink(IPlatoHelper helper, object linkedObject);
    }

}
