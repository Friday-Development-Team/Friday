using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Friday.Models
{
    /// <summary>
    /// Contains the different roles. Couples to String to a fault-free way to reference these.
    /// </summary>
    public static class Role
    {
        public const string Admin = "Admin";
        public const string Catering = "Catering";
        public const string Kitchen = "Kitchen";
        public const string User = "User";
    }
}
