using Baudrillard.Attributes;
using System;

namespace Baudrillard.Simulator
{
    public class ShowableStateVector
    {
        [Display("Reloj", int.MinValue)]
        public TimeSpan Clock { get; set; }
    }
}
