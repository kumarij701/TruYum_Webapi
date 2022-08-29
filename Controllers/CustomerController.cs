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
    [Authorize(Roles = UserRoles.Customer)]
    public class CustomerController : ControllerBase
    {
        public static SqlConnection con;
        public static SqlCommand cmd;

        MenuItemOperation menuItemoper = new MenuItemOperation();


        [HttpGet("api/GetAlldata")]
        public IActionResult GetAlldata()
        {
            return Ok(menuItemoper.GetData());

        }

        [HttpGet("api/TotalPrice")]
        public IActionResult TotalPrice(int userid)
        {
            con = getcon();
            // cmd.Connection = con;

            cmd = new SqlCommand("select  SUM(Price) AS TotalPrice, Userid from  dbo.Cart where Userid=@userid GROUP BY Userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);
            SqlDataReader dr= cmd.ExecuteReader();

            List<Cart> model = new List<Cart>();
            while (dr.Read())
            {
                Cart details = new Cart();
              //  details.Item = dr["Item"].ToString();
               // details.CartId = Convert.ToInt32(dr["CartId"]);
               // details.Price = Convert.ToInt32(dr["Price"]);
                details.Userid = Convert.ToInt32(dr["Userid"]);
                details.TotalPrice = Convert.ToInt32(dr["TotalPrice"]);
                model.Add(details);
            }

            con.Close();
            return Ok(model);
        }

        private static SqlConnection getcon()
        {
            con = new SqlConnection(@"Data Source=BYODHSQ5DX2\MSSQLSERVERNEW;Initial Catalog=db;Integrated Security=true");
            con.Open();
            return con;
        }



       
        [HttpPost]
        public IActionResult CreateCart([FromBody] Cart cart)
        {
            con = getcon();
            cmd = new SqlCommand("INSERT INTO dbo.Cart (CartId , Item ,Category, Price ,Userid ) Values (@cartid, @item, @category, @price, @userid)", con);
            cmd.Parameters.AddWithValue("@cartid", cart.CartId);
            cmd.Parameters.AddWithValue("@item", cart.Item);
            cmd.Parameters.AddWithValue("@category", cart.Category);
            cmd.Parameters.AddWithValue("@price", cart.Price);
            cmd.Parameters.AddWithValue("@userid", cart.Userid);
            cmd.ExecuteReader();
            con.Close();
            return Ok();
        }
         [HttpDelete]
        public IActionResult DeleteItem(int menuItemid)
        {
            con = getcon();
            cmd = new SqlCommand("DELETE FROM dbo.Cart WHERE CartId= @MenuItemid", con);
            cmd.Parameters.AddWithValue("@MenuItemid", menuItemid);
            cmd.ExecuteReader();
            con.Close();
            return Ok();
        }
    }

}
