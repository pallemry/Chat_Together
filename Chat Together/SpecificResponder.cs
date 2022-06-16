using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Together
{
    public abstract class SpecificResponder
    {
        public Responder Parent { get; set; }
        public SpecificResponder(Responder parent)
        {
            Parent = parent;
        }
    }
}
