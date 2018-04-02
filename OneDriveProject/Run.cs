using System;
using OneDriveProject.Logic.Controllers;
using OneDriveProject.Logic;

namespace OneDriveProject
{
    class Run
    {
        static void Main(string[] args)
        {
            GraphController.SignOut();
            GraphController.SignIn();
            Console.ReadLine();
        }
    }
}
