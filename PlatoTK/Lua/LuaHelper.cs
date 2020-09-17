using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using StardewValley;

namespace PlatoTK.Lua
{

    internal class LuaHelper : InnerHelper, ILuaHelper
    {
        public LuaHelper(IPlatoHelper helper)
            : base(helper)
        {

        }

        private Dictionary<string,object> GetDefaultObjects()
        {
            var dict = new Dictionary<string, object>();

            dict.Add("Game1", Game1.game1);
            dict.Add("Utility", new Utility());

            return dict;
        }

        public void CallLua(string code, Dictionary<string, object> objects = null, bool addDefaults = true)
        {
            LoadLuaCode(code, objects, addDefaults);
        }

        public MoonSharp.Interpreter.Script LoadLuaCode(string code, Dictionary<string, object> objects = null, bool addDefaults = true)
        {
            MoonSharp.Interpreter.UserData.DefaultAccessMode = MoonSharp.Interpreter.InteropAccessMode.Reflection;
            MoonSharp.Interpreter.Script lua = new MoonSharp.Interpreter.Script();
            objects = objects ?? new Dictionary<string, object>();

            foreach (var obj in objects)
                lua.Globals[obj.Key] = obj.Value;

            if (addDefaults)
                foreach (var obj in GetDefaultObjects())
                    lua.Globals[obj.Key] = obj.Value;

            lua.DoString(code);
            return lua;
        }

        public T CallLua<T>(string code, Dictionary<string, object> objects = null, bool addDefaults = true)
        {
                var lua = LoadLuaCode("resultValue = " + code,objects,addDefaults);
                return (T) lua.Globals["resultValue"];
        }
    }
}
