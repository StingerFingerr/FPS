using Player.Interaction;

namespace Effects
{
    public class OutlineOnHover: HoverableObject
    {
        public Outline outline;

        protected override void OnHoverBegin() => 
            outline.enabled = true;

        protected override void OnHoverEnd() => 
            outline.enabled = false;
    }
}