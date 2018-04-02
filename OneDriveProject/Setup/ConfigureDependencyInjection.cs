using Ninject;
using OneDriveProject.Logic.Controllers;
using OneDriveProject.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriveProject.Setup
{
    public class ConfigureDependencyInjection
    {
        private ConfigureDependencyInjection()
        {

        }

       private static readonly ConfigureDependencyInjection current;

        public static ConfigureDependencyInjection Get()
        {
            if(current == null)
            {
                return new ConfigureDependencyInjection();
            } else
            {
                return current;
            }

            //return current ?? new ConfigureDependencyInjection();
        }

        public IKernel InitializeDependencyInjection()
        {
            IKernel kernel = new StandardKernel();

            kernel.Bind<IGraphController>().To<GraphController>();

            kernel.Bind<IUserInteraction>().To<UserInteraction>();

            return kernel;
        }
    }
}
