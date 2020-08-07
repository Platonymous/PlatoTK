using StardewValley;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PlatoTK.Harmony
{
    public class MethodPatches
    {
        internal static bool ForwardMethodVoid(object __instance, MethodInfo __originalMethod, params object[] args)
        {
            object instance = __instance;
            Type iType = __instance.GetType();
            if (HarmonyHelper.TracedTypes.FirstOrDefault(t => t.FromType == iType.GetType() && t.TargetForAllInstances != null) is TypeForwarding allForward
                && allForward.TargetForAllInstances.GetType()
                .GetMethod(__originalMethod.Name, __originalMethod
                .GetParameters()?.Select(p => p.ParameterType)?.ToArray() ?? new Type[0]) is MethodInfo targetMethod)
            {
                if (allForward.TargetForAllInstances is ILinked linkedTarget)
                    linkedTarget.Link = new Link(__instance, linkedTarget, allForward.Helper);


                targetMethod.Invoke(allForward.TargetForAllInstances, args);

                return false;
            }
            else if (HarmonyHelper.TracedObjects.FirstOrDefault(t => t.Original == instance) is TracedObject registred
                && registred.Target.GetType().GetMethod(__originalMethod.Name, __originalMethod.GetParameters()?.Select(p => p.ParameterType)?.ToArray() ?? new Type[0]) is MethodInfo targetMethod2)
            {
                if (registred.Target is ILinked linkedTarget)
                    linkedTarget.Link = new Link(__instance, linkedTarget, registred.Helper);

                targetMethod2.Invoke(registred.Target, args);

                return false;
            }

            return true;
        }

        internal static bool ForwardMethod(ref object __result, object __instance, MethodInfo __originalMethod, params object[] args)
        {
            object instance = __instance;
            Type iType = __instance.GetType();
            if (HarmonyHelper.TracedTypes.FirstOrDefault(t => t.FromType == iType.GetType() && t.TargetForAllInstances != null) is TypeForwarding allForward
                && allForward.TargetForAllInstances.GetType()
                .GetMethod(__originalMethod.Name, __originalMethod
                .GetParameters()?.Select(p => p.ParameterType)?.ToArray() ?? new Type[0]) is MethodInfo targetMethod)
            {
                if (allForward.TargetForAllInstances is ILinked linkedTarget)
                    linkedTarget.Link = new Link(__instance, linkedTarget, allForward.Helper);

                    __result = targetMethod.Invoke(allForward.TargetForAllInstances, args);

                return false;
            }
            else if (HarmonyHelper.TracedObjects.FirstOrDefault(t => t.Original == instance) is TracedObject registred
                && registred.Target.GetType().GetMethod(__originalMethod.Name, __originalMethod.GetParameters()?.Select(p => p.ParameterType)?.ToArray() ?? new Type[0]) is MethodInfo targetMethod2)
            {
                if (registred.Target is ILinked linkedTarget)
                    linkedTarget.Link = new Link(__instance, linkedTarget, registred.Helper);

                __result = targetMethod2.Invoke(registred.Target, args);

                return false;
            }

            return true;
        }

        //----------
        
        internal static bool ForwardMethodPatch(ref object __result, object __instance, MethodInfo __originalMethod)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod);
        }

        internal static bool ForwardMethodPatch<T0>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0);
        }
        internal static bool ForwardMethodPatch<T0,T1>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1);
        }

        internal static bool ForwardMethodPatch<T0, T1, T2>(ref object __result, object __instance, MethodInfo __originalMethod,
            T1 __0,
            T1 __1,
            T2 __2)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2);
        }

        internal static bool ForwardMethodPatch<T0, T1, T2, T3>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3);
        }

        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4);
        }

        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4, T5>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4, __5);
        }

        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4, T5, T6>(ref object __result, object __instance, MethodInfo __originalMethod,
           T0 __0,
           T1 __1,
           T2 __2,
           T3 __3,
           T4 __4,
           T5 __5,
           T6 __6)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6);
        }

        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4, T5, T6, T7>(ref object __result, object __instance, MethodInfo __originalMethod,
           T0 __0,
           T1 __1,
           T2 __2,
           T3 __3,
           T4 __4,
           T5 __5,
           T6 __6,
           T7 __7)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7);
        }
        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4, T5, T6, T7, T8>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8);
        }
        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8,
            T9 __9)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9);
        }
        internal static bool ForwardMethodPatch<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ref object __result, object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8,
            T9 __9,
            T10 __10)
        {
            return ForwardMethod(ref __result, __instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10);
        }


        //---------

        internal static bool ForwardMethodPatchVoid(object __instance, MethodInfo __originalMethod)
        {
            return ForwardMethodVoid(__instance, __originalMethod);
        }

        internal static bool ForwardMethodPatchVoid<T0>(object __instance, MethodInfo __originalMethod,
            T0 __0)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0);
        }

        public static bool ForwardMethodPatchVoid<T0, T1>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4>(object __instance, MethodInfo __originalMethod, 
            T0 __0, 
            T1 __1, 
            T2 __2, 
            T3 __3, 
            T4 __4)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6, T7>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6, T7, T8>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8,
            T9 __9)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8,
            T9 __9,
            T10 __10)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8,
            T9 __9,
            T10 __10,
            T11 __11)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10, __11);
        }

        internal static bool ForwardMethodPatchVoid<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(object __instance, MethodInfo __originalMethod,
            T0 __0,
            T1 __1,
            T2 __2,
            T3 __3,
            T4 __4,
            T5 __5,
            T6 __6,
            T7 __7,
            T8 __8,
            T9 __9,
            T10 __10,
            T11 __11,
            T12 __12)
        {
            return ForwardMethodVoid(__instance, __originalMethod, __0, __1, __2, __3, __4, __5, __6, __7, __8, __9, __10, __11, __12);
        }

    }
}
