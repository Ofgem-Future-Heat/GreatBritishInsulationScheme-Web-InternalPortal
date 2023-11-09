namespace Ofgem.GBI.InternalPortal.Service.Models.Links
{
    public class Feedback : Link
    {
        public Feedback(string href, string @class = "") : base(href, @class: @class)
        {
        }

        public override string Render()
        {
            return $"<a href = \"{Href}\" class=\"{Class}\">feedback</a>";
        }
    }
}
