using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nalish.Pages.Admin
{
    public class CreateModel : PageModel
    {
        public PoliceInfo policeInfo = new PoliceInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }


        public void OnPost()
        {
            // Reading data from the form
            policeInfo.name = Request.Form["name"];
            policeInfo.username = Request.Form["username"];
            policeInfo.email = Request.Form["email"];
            policeInfo.phone = Request.Form["phone"];
            policeInfo.password = Request.Form["password"];
            policeInfo.confirm_password = Request.Form["confirm_password"];
            policeInfo.gender = Request.Form["gender"];
            policeInfo.position = Request.Form["position"];

            // showing error message in any input field stay empty
            if (policeInfo.name.Length == 0 || policeInfo.email.Length == 0 ||
                policeInfo.phone.Length == 0 || policeInfo.gender.Length == 0
                || policeInfo.position.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }

            // saving the new client data into database
            try
            {
                // defining database path
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //opening connection
                    connection.Open();
                    // Console.WriteLine("Database connection opened successfully.");

                    // sql query
                    string sql = "INSERT INTO PoliceInfo" +
                        "(name,username,email,phone,password,gender,position) Values " +
                        "(@name,@username,@email,@phone,@password,@gender,@position);";
                    // Console.WriteLine($"SQL Query: {sql}");

                    //create sql command for executing sql query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", policeInfo.name);
                        command.Parameters.AddWithValue("@username", policeInfo.username);
                        command.Parameters.AddWithValue("@email", policeInfo.email);
                        command.Parameters.AddWithValue("@phone", policeInfo.phone);
                        command.Parameters.AddWithValue("@password", policeInfo.password);
                        command.Parameters.AddWithValue("@gender", policeInfo.gender);
                        command.Parameters.AddWithValue("@position", policeInfo.position);

                        // executing sql query
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            policeInfo.name = "";
            policeInfo.username = "";
            policeInfo.email = "";
            policeInfo.phone = "";
            policeInfo.password = "";
            policeInfo.confirm_password = "";
            policeInfo.gender = "";
            policeInfo.position = "";
            successMessage = "New Police information Added Correctly";

            Response.Redirect("/Admin/PoliceDB");

        }
    }
}
