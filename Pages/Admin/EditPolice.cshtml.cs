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
        public void OnGet()
        {
            string id = Request.Query["id"];

            try
            {
                // defining sql connection path
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";

                // creating sql connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // sql query for finding with with id
                    string sql = "SELECT * FROM PoliceInfo WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                policeInfo.id = "" + reader.GetInt32(0);
                                policeInfo.name = reader.GetString(1);
                                policeInfo.email = reader.GetString(2);
                                policeInfo.phone = reader.GetString(3);
                                policeInfo.rank = reader.GetString(4);
                                policeInfo.area = reader.GetString(5);

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
            policeInfo.id = Request.Form["id"];
            policeInfo.name = Request.Form["name"];
            policeInfo.email = Request.Form["email"];
            policeInfo.phone = Request.Form["phone"];
            policeInfo.rank = Request.Form["rank"];
            policeInfo.area = Request.Form["area"];

            if (policeInfo.name.Length == 0 || policeInfo.email.Length == 0 ||
                policeInfo.phone.Length != 11)
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
                        "SET name = @name,email=@email,phone=@phone,rank=@rank,area=@area WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {

                        command.Parameters.AddWithValue("@name", policeInfo.name);
                        command.Parameters.AddWithValue("@email", policeInfo.email);
                        command.Parameters.AddWithValue("@phone", policeInfo.phone);
                        command.Parameters.AddWithValue("@rank", policeInfo.rank);
                        command.Parameters.AddWithValue("@area", policeInfo.area);
                        command.Parameters.AddWithValue("@id", policeInfo.id);

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
