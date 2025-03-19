




using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Aplication.Contracts;
using Aplication.DTOS;
using Aplication.Responses;
using BCrypt.Net;
using Domain.Entities.Entitie.Account;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Infrastruture.Helper;
using Infrastruture.Migrations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


using Microsoft.AspNetCore.Authentication;
using Azure;
using Microsoft.AspNetCore.Http;
using Aplication.DTOS.Employee.DTOs;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;

namespace Infrastruture.Implementtations
{
    public class UserAccountRepository(IOptions<JwtSection> config, AplicationContext context, IHttpContextAccessor httpContextAccessor) : IUserAccount
    {

        public async Task<GeneralReponse> CreateAsync(Register user)
        {

            if (user is null) return new GeneralReponse(false, "Model is empty");

            var employee = await FindEmployeeById(user.EmployeeId);
            if (employee is null) return new GeneralReponse(false, "Not found employee");

            var checkUser = await FindUserByEmail(user.Email!);

            var existingUser = await FindUserByEmployeeId(user.EmployeeId);

            if (checkUser != null || existingUser != null) return new GeneralReponse(false, "User registered already");

            var applicationUser = await AddToDatabase(new AplictionUser()
            {
                FullName = user.Fullname,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
                EmployeeId = employee.Id
            });

            if (applicationUser is null) new GeneralReponse(false, "Failed to register user");

            return new GeneralReponse(true, "Acount created");

        }
        public async Task<LoginResponse> SingInAsync(Login user)
        {
            if (user is null) return new LoginResponse(false, "model is empty");
            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser is null) return new LoginResponse(false, "User not found");

            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
                return new LoginResponse(false, "Email password not valid");

            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == applicationUser.EmployeeId);
            if (employee is null) return new LoginResponse(false, "Employee not found");


            string jwtToken = GenerateToken(applicationUser, employee);
            string refeshToken = GenerateRefeshToken();

            //save the refresh token to the RefreshTokenInfo

            var findUser = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.UserId == applicationUser.Id);
            if (findUser is not null)
            {
                findUser.Token = refeshToken;
                await context.SaveChangesAsync();

            }
            else
            {
                await AddToDatabase(new RefreshTokenInfo() { Token = refeshToken, UserId = applicationUser.Id });
            }

            httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", jwtToken, new CookieOptions
            {
                HttpOnly = true, // Chỉ có thể truy cập qua HTTP
                Secure = true,   // Chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Lax, // Cho phép gửi trong các yêu cầu điều hướng
                Expires = DateTime.UtcNow.AddDays(1) // Token hết hạn sau 1 ngày
            });

            return new LoginResponse(true, "Sucessfully", jwtToken, refeshToken);

        }

        private string GenerateToken(AplictionUser user, Employee employee)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var credentiaqls = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.FullName!),
        new Claim(ClaimTypes.Email, user.Email!),
        new Claim("EmployeeId", user.EmployeeId.ToString()),
        new Claim("Image", employee.Photo)
    };

            if (employee.IsDirector)
            {
                claims.Add(new Claim("Role", "Director"));
            }
            else if (employee.IsHeadOfDepartment)
            {
                claims.Add(new Claim("Role", "HeadOfDepartment"));
            }
            else
            {
                claims.Add(new Claim("Role", "Employee"));
            }

            var token = new JwtSecurityToken(
                issuer: config.Value.Issuer,
                audience: config.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentiaqls

                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private static string GenerateRefeshToken() =>
        Convert.ToBase64String(System.Security.Cryptography.RandomNumberGenerator.GetBytes(64));

        /// <summary>
        /// kiểm tra check employee
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<AplictionUser> FindUserByEmail(string email) =>
        await context.AplictionUsers.FirstOrDefaultAsync(user => user.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<Employee> FindEmployeeById(int id) =>
        await context.Employees.FirstOrDefaultAsync(_ => _.Id == id);
        private async Task<AplictionUser> FindUserByEmployeeId(int employeeId) =>
       await context.AplictionUsers.FirstOrDefaultAsync(_ => _.EmployeeId == employeeId);
        private async Task<T> AddToDatabase<T>(T model)
        {
            var resul = context.Add(model!);
            await context.SaveChangesAsync();
            return (T)resul.Entity;

        }

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token is null) return new LoginResponse(false, "Model is empty");
            var findToken = await context.RefreshTokens.FirstOrDefaultAsync(regresh => regresh.Token!.Equals(token.Token));
            if (findToken is null) return new LoginResponse(false, "Refresh token is required");

            //get user detailss 
            var user = await context.AplictionUsers.FirstOrDefaultAsync(user => user.Id == findToken.Id);
            if (user is null) return new LoginResponse(false, "Refresg token could not be gennerated user not found ");

            //get employee
            var employee = await context.Employees.FirstOrDefaultAsync(e => e.Id == user.EmployeeId);
            if (employee is null) return new LoginResponse(false, "Employee not found");

            string jwtToken = GenerateToken(user, employee);
            string refeshToken = GenerateRefeshToken();
            var updateRedreshToken = await context.RefreshTokens.FirstOrDefaultAsync(_ => _.UserId == user.Id);
            if (updateRedreshToken is null) return new LoginResponse(false, "refresh token not be generated user has not sing in");

            updateRedreshToken.Token = refeshToken;
            await context.SaveChangesAsync();
            return new LoginResponse(true, "Token refresh successfully", jwtToken, refeshToken);
        }
    }
}
