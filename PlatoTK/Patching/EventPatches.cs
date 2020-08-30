using Harmony;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PlatoTK.Patching
{
    internal class EventPatches
    {
        const string PageKey = "@PlatoGotoPage_";

        internal static bool _patched = false;
        internal static bool _isTV = false;
        internal static int _responsesPerPage => (Constants.TargetPlatform == GamePlatform.Android) ? 3 : 8;

        internal static DialogueCall LastPaginatedDialogue;

        internal static void InitializePatch()
        {
            if (_patched)
                return;

            _patched = true;
            var questionRaised = AccessTools.DeclaredConstructor(typeof(DialogueBox),new Type[] { typeof(string), typeof(List<Response>), typeof(int) });
            
            List<Type> questionLocationTypes = new List<Type>() { 
                typeof(GameLocation), 
                typeof(BusStop), 
                typeof(Desert), 
                typeof(JojaMart)};


            var channelSelected = AccessTools.DeclaredMethod(typeof(TV), "selectChannel");
            var tvAction = AccessTools.DeclaredMethod(typeof(TV), "checkForAction");

            List<MethodInfo> questionAsked = new List<MethodInfo>(questionLocationTypes.Select(t => AccessTools.Method(t, "answerDialogue")));

            var harmony = HarmonyInstance.Create($"Plato.QuestionPatches");
            harmony.Patch(questionRaised, prefix: new HarmonyMethod(AccessTools.Method(typeof(EventPatches), nameof(DialogueBox))));
            
            foreach(var method in questionAsked)
                harmony.Patch(method, prefix: new HarmonyMethod(
                    AccessTools.DeclaredMethod(typeof(EventPatches), 
                    nameof(QuestionAsked),
                    null, new Type[] { method.DeclaringType })));

            harmony.Patch(channelSelected, prefix: new HarmonyMethod(AccessTools.Method(typeof(EventPatches), nameof(SelectChannel))));
            harmony.Patch(tvAction, prefix: new HarmonyMethod(AccessTools.Method(typeof(EventPatches), nameof(SetIsTv))));
            harmony.Patch(tvAction, postfix: new HarmonyMethod(AccessTools.Method(typeof(EventPatches), nameof(UnsetIsTV))));
        }

        internal static void SetIsTv()
        {
            _isTV = true;
        }

        internal static void UnsetIsTV()
        {
            _isTV = false;
        }

        internal static bool SelectChannel(TV __instance, string answer)
        {
            bool result = true;
            PlatoHelper.EventsInternal.HandleChannelSelection(answer, __instance, () => result = false);
            return result;
        }

        internal static void DialogueBox(ref string dialogue, ref List<Response> responses, int width)
        {
            LastPaginatedDialogue = null;

            int currentPage = 0;
            bool paginate = false;
            string question = dialogue;
            List<Response> choices = new List<Response>();
            choices.AddRange(responses);
            Response leave = choices.FirstOrDefault(c => c.responseKey == "(Leave)");
            if (choices.FirstOrDefault(c => c.responseKey.StartsWith(PageKey)) is Response next) {
                currentPage = int.Parse(next.responseKey.Split('_')[1]);
                choices.Remove(next);
                paginate = true;
            }

            if (leave != null)
                choices.Remove(leave);

            if(!paginate)
            PlatoHelper.EventsInternal.HandleQuestion(question, choices, (q) => question = q, (r) =>
             {
                 if (!choices.Contains(r))
                     choices.Add(r);
             }, (r) =>
             {
                 choices.Remove(r);
             }, _isTV, () => paginate = true);

            if (leave != null)
                choices.Add(leave);

            if (paginate)
            {
                int startIndex = currentPage * _responsesPerPage;
                int nextPage = currentPage + 1;
                if ((nextPage * _responsesPerPage) >= choices.Count)
                    nextPage = 0;

                List<Response> allChoices = new List<Response>();
                allChoices.AddRange(choices);

                choices = allChoices.GetRange(startIndex, Math.Min(_responsesPerPage, allChoices.Count - startIndex));

                if (currentPage != nextPage)
                {
                    var nextChoice = new Response(PageKey + nextPage, "...");
                    choices.Add(nextChoice);
                    allChoices.Add(nextChoice);
                    if (choices.Contains(leave))
                    {
                        choices.Remove(leave);
                        choices.Add(leave);
                    }
                }

                LastPaginatedDialogue = new DialogueCall(dialogue, allChoices, width);
            }

            dialogue = question;
            responses = choices;
        }

        internal static bool QuestionAsked<T>(T __instance, Response answer)
        {
            bool result = true;

            if (answer.responseKey.StartsWith(PageKey))
            {
                LastPaginatedDialogue.OpenDialogue();
                return false;
            }

            if (__instance is GameLocation location)
            {
                PlatoHelper.EventsInternal.HandleAnswer(answer, () => result = false, location.lastQuestionKey);

                if (!result)
                {
                    location.lastQuestionKey = null;
                    location.afterQuestion = null;
                }
            }

            return result;
        }
    }

    internal class DialogueCall
    {
        internal readonly string Dialogue;
        internal readonly List<Response> Responses;
        internal readonly int Width;

        public DialogueCall(string dialogue, List<Response> responses, int width)
        {
            Dialogue = dialogue;
            Responses = responses;
            Width = width;
        }

        public void OpenDialogue()
        {
            Game1.activeClickableMenu = new DialogueBox(Dialogue, Responses, Width);
        }
    }
}
