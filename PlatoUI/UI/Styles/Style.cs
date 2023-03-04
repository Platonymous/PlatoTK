﻿using PlatoUI.UI.Components;

namespace PlatoUI.UI.Styles
{
    public class Style : IStyle
    {
        protected IPlatoUIHelper Helper { get; set; }

        public virtual int Priority { get; set; } = -1;

        public virtual string Option { get;  } = "";

        protected bool IsActive { get; set; } = true;

        public Style(IPlatoUIHelper helper, string option = "")
        {
            Option = option;
            Helper = helper;
        }

        public virtual string[] PropertyNames { get; } = new string[0];

        public virtual bool ShouldApply(IComponent component)
        {
            IsActive = CheckOption(component);
            return IsActive;
        }

        public virtual void Apply(IComponent component)
        {
        }

        public virtual void Dispose()
        {

        }

        public virtual void Parse(string property, string value, IComponent component)
        {

        }

        protected virtual bool CheckOption(IComponent component)
        {
            if (string.IsNullOrEmpty(Option))
                return true;

            if (Option.ToLower() == "hover")
                return component.IsMouseOver();
            else if (Option.ToLower() == "selected")
                return component.IsSelected;

            return component.HasTag(Option);
        }

        public virtual void Update(IComponent component)
        {
            bool should = CheckOption(component);
            if (IsActive != should)
                component.Recompose();
        }

        public virtual IStyle New(IPlatoUIHelper helper, string option = "")
        {
            return new Style(helper, option);
        }
    }
}
