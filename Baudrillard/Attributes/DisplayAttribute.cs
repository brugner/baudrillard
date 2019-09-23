using System;

namespace Baudrillard.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayAttribute : Attribute
    {
        public string Title { get; }
        public int Order { get; }
        public bool Hidden { get; }

        public DisplayAttribute(string title = null, int order = 0)
        {
            Title = title;
            Order = order;
        }

        public DisplayAttribute(bool hidden)
        {
            Hidden = hidden;
        }
    }
}
