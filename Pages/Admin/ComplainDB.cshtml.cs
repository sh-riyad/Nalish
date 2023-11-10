using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Nalish.Pages.Admin
{
    public class ComplainDBModel : PageModel
    {
        string errorMessage = "";
        // taking database connection link
        string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Nalish;Integrated Security=True";
        // sob users the information store krbe
        public List<ComplainInfo> listComplain = new List<ComplainInfo>();
        public List<PoliceInfo> listPolice = new List<PoliceInfo>();
        
        public void OnGet()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open(); //opening the connection
                    string sql = "SELECT * FROM ComplainInfo"; // sql query for select data from database

                    // creating sql command to execude sql query
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) // defining commnad for read data
                        {
                            while (reader.Read()) //reading info from database
                            {
                                ComplainInfo complainInfo = new ComplainInfo();
                                complainInfo.complainID = "" + reader.GetInt32(0);
                                complainInfo.complainType = reader.GetString(1);
                                complainInfo.complainDetails = reader.GetString(2);
                                complainInfo.complainantEmail = reader.GetString(3);
                                complainInfo.incidentDate = reader.GetDateTime(4).ToString("dd-MM-yyyy");
                                if (!reader.IsDBNull(5))
                                {
                                    complainInfo.policeName = reader.GetString(5);
                                }
                                else
                                {
                                    complainInfo.policeName = null;
                                }


                                listComplain.Add(complainInfo); // add every police info into list
                            }
                        }
                    }
                    string sql_police = "SELECT policeid, name FROM PoliceInfo";
                    using (SqlCommand command = new SqlCommand(sql_police, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader()) // defining commnad for read data
                        {
                            while (reader.Read()) //reading info from database
                            {
                                PoliceInfo policeInfo = new PoliceInfo();
                                policeInfo.policeid = "" + reader.GetInt32(0);
                                policeInfo.name = reader.GetString(1);
                                

                                listPolice.Add(policeInfo); // add every police info into list
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
        public void onPost()
        {
            string selectedValue = Request.Form["id_name"];
            string complainID = "";
            string policeName = "";
             
            if (!string.IsNullOrEmpty(selectedValue))
            {
                // Split the selected value to get complainID and policeName
                string[] values = selectedValue.Split('-');
                complainID = values[0];
                policeName = values[1];
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql_assign = "UPDATE ComplainInfo " +
                        "SET policeName = @policeName WHERE complainID=@complainID";
                    using (SqlCommand command = new SqlCommand(sql_assign, connection))
                    {

                        command.Parameters.AddWithValue("@policeName", policeName);
                        command.Parameters.AddWithValue("@complainID", complainID);

                        command.ExecuteNonQuery();
                    }

                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Admin/ComplainDB");

        }
    }


    // UsersInfo class user er information store krbe. single user er jnno
    public class ComplainInfo
    {
        public string complainID;
        public string complainType;
        public string complainDetails;
        public string complainantEmail;
        public string incidentDate;
        public string policeName;
        
    }
    
}
