using Microsoft.Xna.Framework.Graphics;
using PlatoTK.UI.Components;
using PlatoTK.UI.Styles;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace PlatoTK.UI
{
    internal class UIHelper : InnerHelper, IUIHelper
    {
        private static HashSet<IStyle> StylesLoaded { get; } = new HashSet<IStyle>();
        private static HashSet<IComponent> ComponentsLoaded { get; } = new HashSet<IComponent>();

        public UIHelper(IPlatoHelper platoHelper)
            : base(platoHelper)
        {
            RegisterStyle(new BackgroundStyle(platoHelper));
            RegisterStyle(new PreRenderStyle(platoHelper));
            RegisterStyle(new BoundsStyle(platoHelper));
            RegisterStyle(new AlignementStyle(platoHelper));
            RegisterStyle(new GridStyle(platoHelper));
            RegisterStyle(new OnClickStyle(platoHelper));
            RegisterStyle(new ColorStyle(platoHelper));

            RegisterComponent(new Component(platoHelper));
            RegisterComponent(new Wrapper(platoHelper));
            RegisterComponent(new TextComponent(platoHelper));
        }

        public IWrapper LoadFromFile(string layoutPath, string id = "")
        {
            IWrapper wrapper = new Wrapper(Helper);
            wrapper.ParseAttribute("Id", id);
            string xml = File.ReadAllText(layoutPath);
            XDocument doc = XDocument.Parse(xml);
            foreach(var element in doc.Elements())
                ParseElements(wrapper, element);
            return wrapper;
        }

        public IClickableMenu OpenMenu(IWrapper wrapper)
        {
            return new UIMenu(Helper, wrapper);
        }

        private IComponent ParseElements(IComponent parent, XElement element)
        {
            if (Helper.UI.TryGetComponent(element.Name.LocalName, out IComponent component))
            {
                parent.AddChild(component);
                foreach (var attr in element.Attributes())
                    foreach(var attrEntry in attr.Value.Split(','))
                    component.ParseAttribute(attr.Name.LocalName, attrEntry.Trim());

                foreach (var child in element.Elements())
                    ParseElements(component, child);
            }

            return parent;
        }

        public void RegisterStyle(IStyle style)
        {
            if (!StylesLoaded.Contains(style))
                StylesLoaded.Add(style);
        }

        public void RegisterComponent(IComponent component)
        {
            if (!ComponentsLoaded.Contains(component))
                ComponentsLoaded.Add(component);
        }

        public bool TryGetStyle(string propertyName, out IStyle style, string option = "")
        {
            if (StylesLoaded.FirstOrDefault(s => s.PropertyNames.Any(p => p.ToLower() == propertyName.ToLower())) is IStyle loaded)
            {
                style = loaded.New(Helper, option);
                return true;
            }

            style = null;
            return false;
        }

        public bool TryGetComponent(string componentName, out IComponent component)
        {
            if (ComponentsLoaded.FirstOrDefault(s => s.ComponentName == componentName) is IComponent loaded)
            {
                component = loaded.New(Helper);
                return true;
            }

            component = null;
            return false;
        }

        
    }
}
