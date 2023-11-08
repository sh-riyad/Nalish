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
            // reading data from the form
            policeInfo.name = Request.Form["name"];
            policeInfo.email = Request.Form["email"];
            policeInfo.phone = Request.Form["phone"];
            policeInfo.rank = Request.Form["rank"];
            policeInfo.area = Request.Form["area"];

            // showing error message in any input field stay empty
            if (policeInfo.name.Length == 0 || policeInfo.email.Length == 0 ||
                policeInfo.phone.Length == 0 || policeInfo.rank.Length == 0
                || policeInfo.area.Length == 0)
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

                    // sql query
                    string sql = "INSERT INTO PoliceInfo" +
                        "(name,email,phone,rank,area) Values " +
                        "(@name,@email,@phone,@rank,@area);";

                    //create sql command for executing sql query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", policeInfo.name);
                        command.Parameters.AddWithValue("@email", policeInfo.email);
                        command.Parameters.AddWithValue("@phone", policeInfo.phone);
                        command.Parameters.AddWithValue("@rank", policeInfo.rank);
                        command.Parameters.AddWithValue("@area", policeInfo.area);

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
            policeInfo.email = "";
            policeInfo.phone = "";
            policeInfo.rank = "";
            policeInfo.area = "";
            successMessage = "New Police information Added Correctly";

            Response.Redirect("/Admin/PoliceDB");

        }
    }
}
