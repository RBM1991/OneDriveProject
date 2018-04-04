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
    public class DependencyInjector
    {
        private DependencyInjector()
        {
            InitializeDependencyInjection();
        }

        private static readonly DependencyInjector current;
        private static IKernel kernel;

        public static DependencyInjector Get()
        {
            return current ?? new DependencyInjector();
        }

        //Binds classes to interfaces
        public void InitializeDependencyInjection()
        {
            kernel = new StandardKernel();

            kernel.Bind<IGraphController>().To<GraphController>();

            kernel.Bind<IUserInteraction>().To<UserInteraction>();
        }


        public T GetDependency<T>()
        {
            return kernel.Get<T>();
        }
    }
}
