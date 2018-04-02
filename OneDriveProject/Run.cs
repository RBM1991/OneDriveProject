using System;

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
