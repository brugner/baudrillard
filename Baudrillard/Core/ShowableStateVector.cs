using Baudrillard.Attributes;
using System;

namespace Baudrillard.Core
{
    public class ShowableStateVector
    {
        [Display("Reloj", int.MinValue)]
        public TimeSpan Clock { get; set; }
    }
}
