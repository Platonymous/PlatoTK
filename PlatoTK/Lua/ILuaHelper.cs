using System.Collections.Generic;

namespace PlatoTK.Lua
{
    public interface ILuaHelper
    {
        void CallLua(string code, Dictionary<string, object> objects = null, bool addDefaults = true);

        T CallLua<T>(string code, Dictionary<string, object> objects = null, bool addDefaults = true);

        MoonSharp.Interpreter.Script LoadLuaCode(string code, Dictionary<string, object> objects = null, bool addDefaults = true);
    }
}