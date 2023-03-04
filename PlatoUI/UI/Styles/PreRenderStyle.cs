﻿using PlatoUI.UI.Components;

namespace PlatoUI.UI.Styles
{
    public class PreRenderStyle : Style
    {
        public bool PreRender { get; set; } = false;

        public bool CacheRender { get; set; } = false;
        public override string[] PropertyNames => new string[] { "PreRender", "CacheRender" };

        public PreRenderStyle(IPlatoUIHelper helper, string option = "")
            : base(helper, option)
        {

        }

        public override void Apply(IComponent component)
        {
            component.PreRender = PreRender;
            component.CacheRender = CacheRender;
        }


        public override IStyle New(IPlatoUIHelper helper, string option = "")
        {
            return new PreRenderStyle(helper, option);
        }

        public override void Parse(string property, string value, IComponent component)
        {
            switch (property.ToLower())
            {
                case "prerender": PreRender = bool.TryParse(value, out bool prerender) ? prerender : false; break;
                case "cacherender": CacheRender = bool.TryParse(value, out bool cacherender) ? cacherender : false; break;
            }
            base.Apply(component);
        }
    }
}
