namespace PlatoTK.Harmony
{
    internal class TracedObject
    {
        internal readonly object Original;

        internal readonly object Target;

        internal readonly IPlatoHelper Helper;

        public TracedObject(object original, object target, IPlatoHelper helper)
        {
            Original = original;
            Target = target;
            Helper = helper;
        }
    }
}
