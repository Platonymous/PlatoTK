using System.Collections.Generic;
using System.Reflection;

namespace PlatoUI.Reflection
{
    public interface IPrivateMethods
    {
        IEnumerable<MethodInfo> this[string name] { get; }

        T CallMethod<T>(string name, params object[] args);
        void CallMethod(string name, params object[] args);

    }
}
