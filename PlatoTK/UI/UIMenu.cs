using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PlatoTK.UI.Components;
using StardewValley;
using StardewValley.Menus;

namespace PlatoTK.UI
{
    class UIMenu : IClickableMenu, IUIMenu
    {
        public virtual IWrapper Menu { get; }

        protected readonly IPlatoHelper Helper;

        public UIMenu(IPlatoHelper helper, IWrapper menu)
            :  base(0,0,Game1.viewport.Width, Game1.viewport.Height, false)
        {
            Helper = helper;
            Menu = menu;
        }

        public override void draw(SpriteBatch b)
        {
            Menu.Draw(b);
            drawMouse(b);
        }

        public override void update(GameTime time)
        {
            Menu.Update(null);
        }
        public override void gameWindowSizeChanged(Rectangle oldBounds, Rectangle newBounds)
        {
            Menu.Repopulate();
            Menu.Recompose();
        }

        public override void draw(SpriteBatch b, int red = -1, int green = -1, int blue = -1)
        {
            this.draw(b);
        }

        public override void emergencyShutDown()
        {
            base.emergencyShutDown();
        }

    }
}
