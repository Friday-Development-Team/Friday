using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{/// <summary>
/// Used to signal a NotFound in the code. Should result in a 404 Not Found to be returned in the controllers.
/// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Empty constructor. No message is needed.
        /// </summary>
        public NotFoundException() : base()
        {
        }
    }
}
