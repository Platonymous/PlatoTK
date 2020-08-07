using System;

namespace PlatoTK.Harmony
{
    internal class TypeObserver
    {
        public readonly string Id;

        public readonly Type Type;

        public readonly Delegate Observer;

        public TypeObserver(string id, Type type, Delegate observer)
        {
            Id = id;
            Type = type;
            Observer = observer;
        }
    }
}
