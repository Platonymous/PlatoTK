using Harmony;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatoTK.Content;
using PlatoTK.Events;
using PlatoTK.Lua;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using xTile.Display;

namespace PlatoTK
{
    public class PlatoTKMod : Mod
    {
        public override void Entry(IModHelper helper)
        {
            var plato = helper.GetPlatoHelper();
            PlatoHelper.EventsInternal = new PlatoEventsHelper(plato);
            PlatoHelper.ConditionsProvider.Add(new LuaConditionsProvider(plato));

            var lua = helper.GetPlatoHelper().Lua;
            helper.ConsoleCommands.Add("L#", "Execute Lua script with PlatoTK", (s, p) =>
              {
                  try
                  {
                      if (p.Length == 0)
                          Monitor.Log("No lua script provided.", LogLevel.Warn);
                      else
                      {
                          bool returnValue = p[0] == "log";
                          string code = string.Join(" ", returnValue ? p.Skip(1) : p);
                          Monitor.Log("Running Lua: " + code, LogLevel.Trace);

                          if (!returnValue)
                              lua.CallLua(code);
                          else
                          {
                              var result = lua.CallLua<object>(code);

                              if (result is IEnumerable objs && objs.Cast<object>() is IEnumerable<object> results)
                                  Monitor.Log($"Results ({results.Count()}): {string.Join(",", objs.Cast<object>().Select(o => o.ToString()))}", LogLevel.Info);
                              else
                                  Monitor.Log("Result: " + (result?.ToString() ?? "null"), LogLevel.Info);
                          }

                          Monitor.Log("OK", LogLevel.Info);
                      }
                  }
                  catch (Exception e)
                  {
                      Monitor.Log(e.ToString(), LogLevel.Error);
                  }
              });

             

            //Spritefont test
            //helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
            //helper.Events.Input.ButtonPressed += Input_ButtonPressed; 
        }

       

    }
}
