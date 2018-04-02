﻿using OneDriveProject.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriveProject.Setup
{
    public class UserInteraction : IUserInteraction
    {
        public void Setup(IGraphController controller)
        {
            Console.WriteLine("Welcome to OneDrive program. Do you want to sign out first? y/n");
            var consoleAction = Console.ReadLine();

            if (consoleAction.ToLower() == "y")
            {
                controller.SignOut();
            }

            controller.SignIn();
        }
    }
}
