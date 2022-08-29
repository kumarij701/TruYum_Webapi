using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TruYumWebAPI.Model;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Data;

namespace TruYumWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        public static SqlCommand cmd;
        public static SqlConnection con;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        private static SqlConnection getcon()
        {
            con = new SqlConnection(@"Data Source=BYODHSQ5DX2\MSSQLSERVERNEW;Initial Catalog=db;Integrated Security=true");
            con.Open();
            return con;
        }


        [HttpGet]
        public IActionResult GetAllData(Single userid)
        {
            con = getcon();
            cmd = new SqlCommand("SELECT * FROM dbo.UserPass Where Userid=@userid", con);
            cmd.Parameters.AddWithValue("@userid", userid);

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            if ((ds.Tables[0].Rows.Count == 0))
                return NotFound("User does not Exist!");

            IActionResult response = Unauthorized();


            var tokenString = GenerateJSONWebToken(userid);
            response = Ok(new { token = tokenString });

            return response;
        }

        private string GenerateJSONWebToken(Single userid)
        {

            string role = null;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            if (userid == 1)
            {
                role = "Admin";
            }
            if (userid != 1 && userid != -1) { 
          
                role = "Customer";
            }
            var claims = new[] {
               new Claim(ClaimTypes.Role, role),
               //new Claim(ClaimTypes.Role, "Customer"),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
              };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
             claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

        



      /**  private String  GenerateToken(string username, string password)
    { 
       private readonly string tokenKey;
       public static SqlCommand cmd;
       public static SqlConnection con;


     

            User user = new User();
            con = getcon();
            SqlCommand cmd = new SqlCommand("select * from dbo.UserPass ");
            SqlDataReader dr = cmd.ExecuteReader();

            List<User> model = new List<User>();
            while (dr.Read())
            {
                User details = new User();

                details.Id = Convert.ToInt32(dr["Id"]);
                details.UserName = dr["UserName"].ToString();
               
                details.Password = dr["Password"].ToString();
                details.Userid = Convert.ToSingle(dr["Userid"]);
                model.Add(details);
            }
            con.Close();
       

            if (!model.Any(u => u.UserName == username && u.Password == password))
            {
                return null;
            }
            if(userid ==1 )
           {  
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, UserRoles.Admin)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
      if (userid != -1 && userid )
     {
    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(tokenKey);
    var tokenDescriptor = new SecurityTokenDescriptor
    {
        Subject = new ClaimsIdentity(new Claim[]
        {
                    new Claim(ClaimTypes.Name, UserRoles.Admin)
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature)
    };
    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
}


    }}
      
private static SqlConnection getcon()
    {
        con = new SqlConnection(@"Data Source=BYODHSQ5DX2\MSSQLSERVERNEW;Initial Catalog=db;Integrated Security=true");
        con.Open();
        return con;
    }

}

}**/
