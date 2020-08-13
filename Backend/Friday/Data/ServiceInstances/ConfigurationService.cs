using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friday.Data.IServices;
using Friday.Models;
using Microsoft.EntityFrameworkCore;

namespace Friday.Data.ServiceInstances {
    public class ConfigurationService : IConfigurationService {
        private readonly Context context;
        private readonly DbSet<Configuration> configSet;

        public ConfigurationService(Context context) {
            this.context = context;
            configSet = context.Configuration;
        }

        public Configuration GetConfig() {
            return configSet.SingleOrDefault();
        }

        public void SetConfig(Configuration con) {
            var config = configSet.Single();
            config.Copy(con);
            context.Configuration.Update(config);
            context.SaveChanges();
        }
    }
}
