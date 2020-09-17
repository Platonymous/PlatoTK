using System;

namespace PlatoTK.Patching
{
    public interface ITileAction
    {
        string[] Trigger { get; }

        Action<ITileActionTrigger> Handler { get; }
    }
}
