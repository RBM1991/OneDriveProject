using Microsoft.Identity.Client;
using OneDriveProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDriveProject.Logic.Interfaces
{
    public interface IGraphController
    {
        void SignIn();

        void SignOut();

        Task<OneDriveUser> GetHttpContentWithToken(string url, string token);
    }
}
