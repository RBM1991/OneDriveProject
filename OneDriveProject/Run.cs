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
            //GraphController.SignOut();

            var kernel = ConfigureDependencyInjection.Get().InitializeDependencyInjection();

            var controller = kernel.Get<IGraphController>();

            var uI = kernel.Get<IUserInteraction>();

            uI.Setup(controller);

            Console.ReadLine();
        }

        //public static void Setup(IGraphController controller)
        //{
        //    Console.WriteLine("Welcome to OneDrive program. Do you want to sign out first? y/n");
        //    var consoleAction = Console.ReadLine();

        //    if(consoleAction.ToLower() == "y")
        //    {
        //        controller.SignOut();
        //    }

        //    controller.SignIn();
        //}
    }
}
