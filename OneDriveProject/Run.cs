using System;
using OneDriveProject.Logic.Controllers;
using OneDriveProject.Logic;
using Ninject;
using OneDriveProject.Logic.Interfaces;
using OneDriveProject.Setup;

namespace OneDriveProject
{
    class Run
    {
        static void Main(string[] args)
        {
            var di = DependencyInjector.Get();

            var controller = di.GetDependency<IGraphController>();

            var uI = di.GetDependency<IUserInteraction>();

            uI.Setup(controller);

            Console.ReadLine();
        }
    }
}
