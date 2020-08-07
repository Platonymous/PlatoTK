using System;

namespace PlatoTK.Content
{
    internal abstract class AssetInjection
    {
        public readonly string AssetName;

        public readonly InjectionMethod Method;

        private readonly string Conditions;

        private readonly bool Subscribed;

        private readonly IPlatoHelper Helper;

        private bool MatchesConditions;

        private readonly IConditionsProvider ConditionsProvider;

        public bool HasConditions => !(string.IsNullOrEmpty(Conditions) || ConditionsProvider == null);

        public bool ConditionsMet
        {
            get
            {
                if (!HasConditions)
                    MatchesConditions = true;
                else if (!Subscribed)
                    MatchesConditions = ConditionsProvider.CheckConditions(Conditions, this);

                return MatchesConditions;
            }
        }

        private void ConditionsChanged(string conditions, bool newValue)
        {
            if (MatchesConditions != newValue)
            {
                MatchesConditions = newValue;
                Helper.ModHelper.Content.InvalidateCache(AssetName);
            }
        }

        public AssetInjection(
            IPlatoHelper helper,
            string assetName,
            InjectionMethod method,
            string conditions = "",
            IConditionsProvider provider = null)
        {
            Method = method;
            Helper = helper;
            AssetName = assetName;
            Conditions = conditions;
            ConditionsProvider = provider ?? new EventConditionsProvider();
            Subscribed = false;
            MatchesConditions = true;

            if (HasConditions)
            {
                Subscribed = provider.TrySubscribeToChange(Conditions, this, ConditionsChanged, out bool state);
                MatchesConditions = state;
            }
        }
    }

    internal abstract class AssetInjection<TAsset> : AssetInjection
    {
        public readonly TAsset Value;

        public Type GetAssetType()
        {
            return typeof(TAsset);
        }

        public AssetInjection(
            IPlatoHelper helper,
            string assetName,
            TAsset value,
            InjectionMethod method,
            string conditions = "",
            IConditionsProvider provider = null)
            : base(helper,assetName,method,conditions,provider)
        {
            Value = value;
        }
    }
}
