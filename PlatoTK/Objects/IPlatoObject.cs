using PlatoTK.Patching;
using PlatoTK.Network;
using System;

namespace PlatoTK.Objects
{
    public interface IPlatoObject : ILinked, ISyncedObject, IOnConstruction, IDisposable
    {

    }
}
