using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Net.NetworkInformation;



namespace Nalish.Pages.Login
{
    public class LogInModel : PageModel
    {


        public string module = "";
        private readonly string _connectionString;

        public LogInModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public string usernameError = "";
        public string passwordError = "";

        public void OnPost()
        {
            string inputUsername = Request.Form["username"];
            string inputPassword = Request.Form["password"];

            try
            {
                // Database connection string

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open(); // Open the connection

                    // SQL query to select username and password from AdminInfo table
                    string sql = "SELECT username, password FROM AdminInfo WHERE username=@username";

                    // Create SQL command to execute the query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        // Set parameters for the query
                        command.Parameters.AddWithValue("@username", inputUsername);

                        // Execute the query and read the results
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) // Check if there is a matching record
                            {
                                string storedUsername = reader.GetString(0);
                                string storedPassword = reader.GetString(1);

                                if (inputPassword == storedPassword)
                                {
                                    // Redirect to PoliceDB page if the username and password match

                                    ViewData["Module"] = "admin";
                                    Response.Redirect("/Admin/PoliceDB");
                                }
                                else
                                {
                                    // Display a message if the password is incorrect
                                    passwordError = "Incorrect password";
                                }
                            }
                            else
                            {
                                // Display a message if the username is not found
                                usernameError = "Username not found";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }

        public void OnGet()
        {
        }
    }
}
