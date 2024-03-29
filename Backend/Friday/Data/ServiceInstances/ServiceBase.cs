﻿namespace Friday.Data.ServiceInstances
{
    /// <summary>
    /// Class used to allow for easier inheritence of documentation for services.
    /// </summary>
    public class ServiceBase
    {
        /// <summary>
        /// Link to DB.
        /// </summary>
        protected readonly Context context;
        /// <summary>
        /// Creates a new instance of this service. Should never be used directly.
        /// </summary>
        /// <param name="context">DB context</param>
        protected ServiceBase(Context context)
        {
            this.context = context;
        }


    }
}
