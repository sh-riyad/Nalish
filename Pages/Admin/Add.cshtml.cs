using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace Nalish.Pages.Admin
{
    public class AddModel : PageModel
    {
        public AddInfo addInfo = new AddInfo();

        public void OnGet()
        {
        }

        public void OnPost()
        {
            // Reading data from the form
            addInfo.name = Request.Form["name"];
            addInfo.username = Request.Form["username"];
            addInfo.email = Request.Form["email"];
            addInfo.phone = Request.Form["phone"];
            addInfo.password = Request.Form["password"];
            addInfo.conpassword = Request.Form["conpassword"];
            addInfo.gender = Request.Form["gender"];
            addInfo.position = Request.Form["position"];

            // Displaying data in the console
            Console.WriteLine(addInfo.name);
            Console.WriteLine(addInfo.username);
            Console.WriteLine(addInfo.email);
            Console.WriteLine(addInfo.phone);
            Console.WriteLine(addInfo.password);
            Console.WriteLine(addInfo.conpassword);
            Console.WriteLine(addInfo.gender);
            Console.WriteLine(addInfo.position);

            // Resetting form fields
            addInfo.name = "";
            addInfo.username = "";
            addInfo.email = "";
            addInfo.phone = "";
            addInfo.password = "";
            addInfo.conpassword = "";
            addInfo.gender = "";
            addInfo.position = "";
        }
        public class AddInfo
        {
            
            public string name;
            public string username;
            public string email;
            public string phone;
            public string password;
            public string conpassword;
            public string gender;
            public string position;
           
        }
    }
}
