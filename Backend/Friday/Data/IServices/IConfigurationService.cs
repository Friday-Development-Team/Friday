using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Models;

namespace Friday.Data.IServices {
    public interface IConfigurationService {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Configuration GetConfig();
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        /// <summary>
        /// Sets the Configuration options
        /// </summary>
        void SetConfig(Configuration con);

    }
}
