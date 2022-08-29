using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using TruYumWebAPI.Model;

namespace TruYumWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CustomAuthFilter]
    [CustomExceptionFilter]
    [Authorize(Roles =UserRoles.Admin)]
    public class AdminController : ControllerBase
    {

        public static SqlConnection con;
        public static SqlCommand cmd;

        MenuItemOperation menuItemoper = new MenuItemOperation();

        /**public List<MenuItem> GetData()
        {
            con = getcon();
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

            return model;


        }**/
        private static SqlConnection getcon()
        {
            con = new SqlConnection(@"Data Source=BYODHSQ5DX2\MSSQLSERVERNEW;Initial Catalog=db;Integrated Security=true");
            con.Open();
            return con;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetAlldata()
        {
            return Ok(menuItemoper.GetData());

        }

        [HttpPut]
        public IActionResult Edit(int userStoryid , [FromBody] MenuItem menu)
        {
            con = getcon();
            cmd = new SqlCommand("update dbo.User_Story set UserStoryName= @UserStoryName, StoryOwner= @StoryOwner , StoryPoints =@StoryPoints,StoryDescription =@StoryDescription where UserStoryid= @userStoryid" , con);  
            cmd.Parameters.AddWithValue("@UserStoryName", menu.UserStoryName);
            cmd.Parameters.AddWithValue("@StoryOwner", menu.StoryOwner);
            cmd.Parameters.AddWithValue("@StoryPoints", menu.StoryPoints);
            cmd.Parameters.AddWithValue("@StoryDescription", menu.StoryDescription);
            cmd.Parameters.AddWithValue("@userStoryid", userStoryid);
            cmd.ExecuteReader();
            con.Close();
            return Ok();
            
        } 
    }
}
