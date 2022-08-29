using Microsoft.Data.SqlClient;
using TruYumWebAPI.Model;

namespace TruYumWebAPI.Model
{
    public class MenuItemOperation
    {
        public static SqlConnection con;
        public static SqlCommand cmd;

        public  List<MenuItem> GetData() { 
            con = getcon();
            cmd = new SqlCommand("select * from dbo.User_Story", con);
            // cmd.Connection = con;
            SqlDataReader dr = cmd.ExecuteReader();

            List<MenuItem> model = new List<MenuItem>();
            while (dr.Read())
            {
                MenuItem details = new MenuItem();
                details.UserStoryName = dr["UserStoryName"].ToString();
                details.UserStoryId = Convert.ToInt32(dr["UserStoryId"]);
                details.StoryPoints = Convert.ToInt32(dr["StoryPoints"]);
                details.StoryOwner = dr["StoryOwner"].ToString();
                details.StoryDescription = dr["StoryDescription"].ToString();
                model.Add(details);
            }
            con.Close();
            return model;


        }


        private static SqlConnection getcon()
        {
            con = new SqlConnection(@"Data Source=BYODHSQ5DX2\MSSQLSERVERNEW;Initial Catalog=db;Integrated Security=true");
            con.Open();
            return con;
        }
    }
}
