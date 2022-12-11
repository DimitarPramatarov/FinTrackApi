namespace FinTrackApi.Services
{
    using FinTrackApi.Data.Models;
    using FinTrackApi.Infrastructure;
    using FinTrackApi.Models.RequestModels;
    using FinTrackApi.Models.ResponseModels;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;


        public IdentityService(UserManager<User> userManager,
            IOptions<AppSettings> appSettings)

        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }


        public string GenerateJwtToken(string userId, string username, string role, string secret)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
              {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role)

                }),

                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        public async Task<LoginResponseModel> Login(LoginModel requestModel)
        {
            var token = "";
            var user = await this.userManager.FindByNameAsync(requestModel.Username);

            if (user is null)
            {
                return null!;
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, requestModel.Password);

            if (!passwordValid)
            {
                return null!;
            }

            var userRole = await userManager.GetRolesAsync(user);
            
            var role = userRole.FirstOrDefault();
            role = "admin";
            token = GenerateJwtToken(
            user.Id,
            user.UserName,
            role,
            this.appSettings.Secret
            );

            var loginResponse = new LoginResponseModel
            {
                JwtToken = token,
                Username = user.UserName
            };

            return loginResponse;
        }

        public async Task<bool> Register(RegisterModel requestModel)
        {
            
            var user = new User
            {
                Email = requestModel.Email,
                UserName = requestModel.Username
            };

            var result = await this.userManager.CreateAsync(user, requestModel.Password);

            if (!result.Succeeded)
            {
                return false;
            }

            await this.userManager.AddToRoleAsync(user, "User");

            return true;
        }

    }
}
