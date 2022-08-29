using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TruYumWebAPI.Model;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace TruYumWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        public static SqlCommand cmd;
        public static SqlConnection con;
        private static SqlConnection getcon()
        {
            con = new SqlConnection(@"Data Source=BYODHSQ5DX2\MSSQLSERVERNEW;Initial Catalog=db;Integrated Security=true");
            con.Open();
            return con;
        }


        [HttpPost("SignUp")]
        public IActionResult SavingData([FromBody] User User)
        {
            con = getcon();
            cmd = new SqlCommand("Insert into dbo.UserPass (Id, UserName ,Password , Userid) Values (@Id, @UserName, @Password, @Userid)", con);
            cmd.Parameters.AddWithValue("@Id", User.Id);
            cmd.Parameters.AddWithValue("@Password", User.Password);
            cmd.Parameters.AddWithValue("@Userid", User.Userid);
            cmd.Parameters.AddWithValue("@UserName", User.UserName);

            cmd.ExecuteReader();
            con.Close();
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult GetData(int Userid, [FromBody] User user)
        {

               con = getcon();
            
                SqlCommand cmd = new SqlCommand("select * from dbo.UserPass where Userid = @Userid and Password = @Password", con);
                cmd.Parameters.AddWithValue("@Userid", Userid);
                cmd.Parameters.AddWithValue("@Password", user.Password);

           
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

            if (loginSuccessful)
            {
                return Ok("True!");
            }
            else
            {
                return NotFound("FalseSubmission");
            }

        }
    }
}


        /**private readonly string tokenKey;
        public  void AuthenticationManager(string tokenKey)
        {
            this.tokenKey = tokenKey;
        }


        

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] User model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        public string Authenticate(string username, string password)
        {
            if (!admins.Any(u => u.Key == username && u.Value == password))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }



        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> Register([FromBody] SignUp model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            cmd = new SqlCommand("Insert into dbo.UserPass (UserName,Password) Values (@model.Username, @model.Password)", con);
            cmd.ExecuteReader();

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }



        /**[HttpPost]
        [Route("signup-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] SignUp model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            if (!await _roleManager.RoleExistsAsync(UserRoles.Anon))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Anon));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Anon)) 
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Anon);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }**/
        /**
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

       
        

        [AllowAnonymous]
        [HttpPost("AuthenicateUser")]
        public IActionResult AuthenticateUser([FromBody] User user)
        {
            var token = manager.Authenticate(user.UserName, user.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

        /**[HttpGet]
        public string Get()
        {
            return "Hello";
        }
        **/
    
