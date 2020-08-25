using PlatoTK.Reflection;
using System;

namespace PlatoTK.Patching
{
    public interface ILink
    {
        IPrivateFields PrivateFields { get; }
        IPrivateFields PrivateProperties { get; }
        IPrivateMethods PrivateMethods { get; }
        T GetAs<T>() where T : class;
        void Unlink();
        void Relink();

        TReturn CallUnlinked<TLink, TReturn>(Func<TLink, TReturn> call) where TLink : class;

        void CallUnlinked<TLink>(Action<TLink> call) where TLink : class;
    }
}
