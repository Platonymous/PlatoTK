using StardewValley;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PlatoTK.Harmony
{
    internal class ConstructorPatches
    {
        internal static object locked = new object();

        internal static void HandleConstruction(object __instance, MethodInfo __originalMethod, params object[] args)
        {

               foreach (TypeForwarding observer in HarmonyHelper.LinkedConstructors.Where(o => o.FromType == __instance.GetType()))
                   if (observer.ToType.GetConstructor(__originalMethod.GetParameters().Select(p => p.ParameterType).ToArray()) is ConstructorInfo constructor)
                   {
                       observer.Helper.SetDelayedAction(1, () =>
                       {
                           bool canLink = true;
                           if (observer.ToType.GetMethod("TypeCanLinkWith", BindingFlags.Public | BindingFlags.Static) is MethodInfo canLinkMethod)
                               canLink = (bool)canLinkMethod.Invoke(null, new object[] { __instance });

                           if (canLink && constructor.Invoke(args) is object newObject)
                           {

                               if (!observer.Helper.Harmony.TryGetLink(__instance, out object priorLink) || priorLink.GetType() != newObject.GetType())
                               {
                                  if(observer.Helper.Harmony.LinkObjects(__instance, newObject) && newObject is IOnConstruction constructed)
                                       constructed.OnConstruction(observer.Helper, __instance);
                               }
                           }
                       });
                   }
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod)
        {
            HandleConstruction(__instance, __originalMethod);
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2,
                __3
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3,
           ref object __4
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2,
                __3,
                __4
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3,
           ref object __4,
           ref object __5
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2,
                __3,
                __4,
                __5
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3,
           ref object __4,
           ref object __5,
           ref object __6
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2,
                __3,
                __4,
                __5,
                __6
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3,
           ref object __4,
           ref object __5,
           ref object __6,
           ref object __7
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2,
                __3,
                __4,
                __5,
                __6,
                __7
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3,
           ref object __4,
           ref object __5,
           ref object __6,
           ref object __7,
           ref object __8
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0,
                __1,
                __2,
                __3,
                __4,
                __5,
                __6,
                __7,
                __8
                );
        }
        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
           ref object __0,
           ref object __1,
           ref object __2,
           ref object __3,
           ref object __4,
           ref object __5,
           ref object __6,
           ref object __7,
           ref object __8,
           ref object __9
           )
        {
            HandleConstruction(__instance, __originalMethod,
                __0, 
                __1, 
                __2, 
                __3, 
                __4,
                __5, 
                __6, 
                __7, 
                __8, 
                __9
                );
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
            ref object __0,
            ref object __1,
            ref object __2,
            ref object __3,
            ref object __4,
            ref object __5,
            ref object __6,
            ref object __7,
            ref object __8,
            ref object __9,
            ref object __10)
        {
            HandleConstruction(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10);
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
            ref object __0,
            ref object __1,
            ref object __2,
            ref object __3,
            ref object __4,
            ref object __5,
            ref object __6,
            ref object __7,
            ref object __8,
            ref object __9,
            ref object __10,
            ref object __11)
        {
            HandleConstruction(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10, __11);
        }

        internal static void ConstructorPatch(ref object __instance, MethodInfo __originalMethod,
            ref object __0,
            ref object __1,
            ref object __2,
            ref object __3,
            ref object __4,
            ref object __5,
            ref object __6,
            ref object __7,
            ref object __8,
            ref object __9,
            ref object __10,
            ref object __11,
            ref object __12)
        {
            HandleConstruction(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10, __11, __12);
        }
    }
}
