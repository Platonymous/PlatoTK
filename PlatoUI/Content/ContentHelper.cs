namespace PlatoUI.Content
{
    internal class ContentHelper : InnerHelper, IContentHelper
    {
        public ITextureHelper Textures { get; }

        public ContentHelper(IPlatoUIHelper helper)
            : base(helper)
        {
            Textures = new TextureHelper(helper);
        }

    }
}
