using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nalish.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly string _connectionString;

        public IndexModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // sob users the information store krbe
        public List<PoliceInfo> listPolice = new List<PoliceInfo>();
   
        public int count = 0;
        public void OnGet()
        {
            try
            {
                // taking database connection link
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open(); //opening the connection
                    string sql = "SELECT policeid,name,email,phone,gender,position FROM PoliceInfo"; // sql query for select data from database

                    // creating sql command to execude sql query
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) // defining commnad for read data
                        {
                            while(reader.Read()) //reading info from database
                            {
                                PoliceInfo policeInfo = new PoliceInfo();
                                policeInfo.policeid = "" + reader.GetInt32(0);
                                policeInfo.name = reader.GetString(1);
                                policeInfo.email = reader.GetString(2);
                                policeInfo.phone = reader.GetString(3);
                                policeInfo.gender = reader.GetString(4);
                                policeInfo.position = reader.GetString(5);

                                listPolice.Add(policeInfo); // add every police info into list
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
        }
    }
    
    
    // UsersInfo class user er information store krbe. single user er jnno
    public class PoliceInfo
    {
        public string policeid;
        public string name;
        public string username;
        public string email;
        public string phone;
        public string password;
        public string confirm_password;
        public string gender;
        public string position;
    }
}
