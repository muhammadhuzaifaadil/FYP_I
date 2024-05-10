//using Core.Data.DataContext;
//using Core.Data.DTO;
//using Core.Data.Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Repository.IRepository;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;

//namespace FYP__.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        public static ApplicationUser user = new ApplicationUser();
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly RoleManager<IdentityRole> _roleManager;
//        private readonly IConfiguration configuration;
//        private readonly ApplicationDbContext context;
//        private readonly IUserRepository userRepository;
//        public UserController(UserManager<ApplicationUser> userManager,
//        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, ApplicationDbContext context, IUserRepository userRepository)
//        {
//            this.configuration = configuration;
//            this.context = context;
//            this.userRepository = userRepository;
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _roleManager = roleManager;
            
//        }
//        [HttpPost("register")]
//        public async Task<IActionResult> Register(UserDTO request)
//        {
//            var existingUser = await _userManager.FindByEmailAsync(request.Email);
//            if (existingUser != null)
//            {
//                return BadRequest("User already exists");
//            }

//            var user = new ApplicationUser
//            {
//                UserName = request.UserName,
//                Email = request.Email
//            };

//            var result = await _userManager.CreateAsync(user, request.Password);
//            if (result.Succeeded)
//            {
//                // Check if the "admin" role exists, and if not, create it
//                if (!_roleManager.RoleExistsAsync("admin").GetAwaiter().GetResult())
//                {
//                    await _roleManager.CreateAsync(new IdentityRole("admin"));
//                    await _roleManager.CreateAsync(new IdentityRole("user"));
//                }

//                // Assign the "admin" role to the user
//                await _userManager.AddToRoleAsync(user, "admin");

//                // Add claims based on roles
//                var adminClaims = new List<Claim>
//                {
//                    new Claim(ClaimTypes.Name, user.UserName),
//                    new Claim(ClaimTypes.Email, user.Email),
//                    // Add more claims as needed
//                };

//                var role = await _userManager.GetRolesAsync(user);
//                foreach (var r in role)
//                {
//                    adminClaims.Add(new Claim(ClaimTypes.Role, r));
//                }

//                // Create a token with claims
//                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Token").Value));
//                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//                var token = new JwtSecurityToken(
//                    claims: adminClaims,
//                    expires: DateTime.Now.AddDays(1),
//                    signingCredentials: creds
//                );

//                return Ok(new { Token = new JwtSecurityTokenHandler().WriteToken(token), Message = "User registered successfully" });
//            }

//            return BadRequest(result.Errors);
//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login(UserDTO request)
//        {
//            var user = await _userManager.FindByEmailAsync(request.Email);

//            if (user == null)
//            {
//                return BadRequest("User not Found");
//            }

//            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

//            if (!result.Succeeded)
//            {
//                return BadRequest("Wrong Password");
//            }

//            var roles = await _userManager.GetRolesAsync(user);
//            var claims = new List<Claim>
//        {
//            new Claim(ClaimTypes.Name, user.UserName),
//            new Claim(ClaimTypes.Email, user.Email),
//                // Adding roles as claims
//            new Claim(ClaimTypes.Role, roles.FirstOrDefault())
//        };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Token").Value));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

//            var token = new JwtSecurityToken(
//                claims: claims,
//                expires: DateTime.Now.AddDays(1),
//                signingCredentials: creds
//            );
//            string returntoken = CreateToken(user);
//            return Ok(new { user.UserName });
//        }
//        [HttpPost("forgot-password")]
//        public async Task<IActionResult> ForgotPassword(string email)
//        {
//            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
//            if (user == null)
//            {
//                return BadRequest("User not Found");
//            }
//            user.resetToken = CreateRandomToken();
//            //expire logic
//            await context.SaveChangesAsync();
//            return Ok(user.resetToken);
//        }
//        [HttpPost("reset-password")]
//        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordDTO)
//        {
//            var user = await context.Users.FirstOrDefaultAsync(u => u.resetToken == resetPasswordDTO.Token);
//            if (user == null)
//            {
//                return BadRequest("Token Incorrect");
//            }
//            CreatePasswordHash(resetPasswordDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
//            user.PasswordHash = passwordHash;
//            user.PasswordSalt = passwordSalt;
//            user.resetToken = null;
//            //expire logic
//            await context.SaveChangesAsync();
//            return Ok("Password Reset Successful");
//        }

//        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
//        {
//            using (var hmac = new HMACSHA512())
//            {
//                passwordSalt = hmac.Key;
//                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
//            }
//        }

//        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
//        {
//            using (var hmac = new HMACSHA512(storedSalt))
//            {
//                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

//                for (int i = 0; i < computedHash.Length; i++)
//                {
//                    if (computedHash[i] != storedHash[i])
//                    {
//                        return false;
//                    }
//                }

//                return true;
//            }
//        }
//        private string CreateRandomToken()
//        {
//            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
//        }
//        private string CreateToken(ApplicationUser user)
//        {
//            List<Claim> claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Role, "User"),
//               // new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
//            };
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Token").Value));
//            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
//            var token = new JwtSecurityToken(
//                claims: claims,
//                expires: DateTime.Now.AddDays(1),
//                signingCredentials: credentials
//            );
//            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
//            return jwt;
//        }

//    }
//}
