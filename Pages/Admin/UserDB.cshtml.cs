using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nalish.Pages.Admin
{
    public class UserDBModel : PageModel
    {
        private readonly string _connectionString;

        public UserDBModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        // sob users the information store krbe
        public List<UserInfo> listUser = new List<UserInfo>();
        public void OnGet()
        {
            try
            {
                // taking database connection link

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open(); //opening the connection
                    string sql = "SELECT userid,name,email,phone,gender FROM UserInfo"; // sql query for select data from database

                    // creating sql command to execude sql query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) // defining commnad for read data
                        {
                            while (reader.Read()) //reading info from database
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.userid = "" + reader.GetInt32(0);
                                userInfo.name = reader.GetString(1);
                                userInfo.email = reader.GetString(2);
                                userInfo.phone = reader.GetString(3);
                                userInfo.gender = reader.GetString(4);
                                

                                listUser.Add(userInfo); // add every police info into list
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
    }


    // UsersInfo class user er information store krbe. single user er jnno
    public class UserInfo
    {
        public string userid;
        public string name;
        public string username;
        public string email;
        public string phone;
        public string password;
        public string gender;
    }
}
