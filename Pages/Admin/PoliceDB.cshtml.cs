using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nalish.Pages.Admin
{
    public class IndexModel : PageModel
    {
        // sob users the information store krbe
        public List<PoliceInfo> listPolice = new List<PoliceInfo>();
        public void OnGet()
        {
            try
            {
                // taking database connection link
                string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); //opening the connection
                    string sql = "SELECT * FROM PoliceInfo"; // sql query for select data from database

                    // creating sql command to execude sql query
                    using (SqlCommand command = new SqlCommand(sql, connection)) 
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) // defining commnad for read data
                        {
                            while(reader.Read()) //reading info from database
                            {
                                PoliceInfo policeInfo = new PoliceInfo();
                                policeInfo.id = "" + reader.GetInt32(0);
                                policeInfo.name = reader.GetString(1);
                                policeInfo.email = reader.GetString(2);
                                policeInfo.phone = reader.GetString(3);
                                policeInfo.rank = reader.GetString(4);
                                policeInfo.area = reader.GetString(5);

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
        public string id;
        public string name;
        public string email;
        public string phone;
        public string rank;
        public string area;
    }
}
