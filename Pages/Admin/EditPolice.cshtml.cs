using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace Nalish.Pages.Admin
{
    public class EditModel : PageModel
    {

        public PoliceInfo policeInfo = new PoliceInfo();
        public string errorMessage = "";
        public string successMessage = "";
        public void OnGet()
        {
            string policeid = Request.Query["policeid"];

            try
            {
                // defining sql connection path
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";

                // creating sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // sql query for finding with with id
                    string sql = "SELECT * FROM PoliceInfo WHERE policeid=@policeid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@policeid", policeid);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                policeInfo.policeid = "" + reader.GetInt32(0);
                                policeInfo.name = reader.GetString(1);
                                policeInfo.username = reader.GetString(2);
                                policeInfo.email = reader.GetString(3);
                                policeInfo.phone = reader.GetString(4);
                                policeInfo.password = reader.GetString(5);
                                policeInfo.gender = reader.GetString(6);
                                policeInfo.position = reader.GetString(7);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
        }
        public void OnPost()
        {
            policeInfo.policeid = Request.Form["policeid"];
            policeInfo.name = Request.Form["name"];
            policeInfo.username = Request.Form["username"];
            policeInfo.email = Request.Form["email"];
            policeInfo.phone = Request.Form["phone"];
            policeInfo.password = Request.Form["password"];
            policeInfo.gender = Request.Form["gender"];
            policeInfo.position = Request.Form["position"];

            if (policeInfo.name.Length == 0 || policeInfo.email.Length == 0 ||
                policeInfo.phone.Length == 0)
            {
                errorMessage = "All the fields are required";
                return;
            }
            try
            {
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE PoliceInfo " +
                        "SET name = @name, username=@username,email=@email,phone=@phone,password=@password,gender=@gender,position=@position WHERE policeid=@policeid";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@name", policeInfo.name);
                        command.Parameters.AddWithValue("@username", policeInfo.username);
                        command.Parameters.AddWithValue("@email", policeInfo.email);
                        command.Parameters.AddWithValue("@phone", policeInfo.phone);
                        command.Parameters.AddWithValue("@password", policeInfo.password);
                        command.Parameters.AddWithValue("@gender", policeInfo.gender);
                        command.Parameters.AddWithValue("@position", policeInfo.position);
                        command.Parameters.AddWithValue("@policeid", policeInfo.policeid);

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/Admin/PoliceDB");
        }
    }
}
