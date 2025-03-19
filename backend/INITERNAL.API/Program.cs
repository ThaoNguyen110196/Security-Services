
using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Aplication.Contracts;
using Domain.Entities.Entitie.Employee;
using Infrastruture.Data;
using Infrastruture.Helper;
using Infrastruture.Implementtations;
using Infrastruture.Services;
using INITERNAL.API.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Redislibrary.Services;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Infrastruture.Migrations;
using System.Transactions;
namespace INITERNAL.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TransactionManager.ImplicitDistributedTransactions = true;

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.CustomSchemaIds(type => type.FullName); // Use the full name of the type as the schema ID
            });
            // Configure the database context
            builder.Services.AddDbContext<AplicationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectDB") ??
                  throw new InvalidOperationException("Sorry, your connection string is not found"));
            });
            builder.Services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("YourDatabaseName"); // Thay thế bằng tên cơ sở dữ liệu MongoDB của bạn
            });

            // Cấu hình Redis
            /*var redisConnectionString = builder.Configuration.GetConnectionString("Redis");
            var redis = ConnectionMultiplexer.Connect(redisConnectionString);
            builder.Services.AddSingleton<IConnectionMultiplexer>(redis);
            builder.Services.AddScoped<ImageMetadataService, RedisService>();*/


            // Đăng ký dịch vụ AWS S3
            builder.Services.AddAWSService<IAmazonS3>();
            builder.Services.AddScoped<IS3Service, S3Service>();

            //Dăng ký  dịch vụ cloundinary
            builder.Services.AddScoped<ICloudinaryInterface, CloundinaryService>();


            // Cấu hình LogService
            builder.Services.AddScoped<ILogServiceEntity, LogService>();

            builder.Services.AddScoped<IGoogleDriveService, GoogleDriveService>();

            // Đăng ký mongoDb
            builder.Services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = builder.Configuration.GetConnectionString("MongoDb");
                return new MongoClient(connectionString);
            });

            builder.Services.AddCors();

            // Configure JWT settings
            builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));

            var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtSection:Key"]);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
         .AddJwtBearer(x =>
         {
             x.RequireHttpsMetadata = false;
             x.SaveToken = true;
             x.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(key),
                 ValidateIssuer = false,
                 ValidateAudience = false
             };
         });

            builder.Services.AddHttpContextAccessor();
            // Register repositories
            builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
            builder.Services.AddScoped<IGennericRepository<GeneralDepartment>, GenaralDeparmentRepasitory>();
            builder.Services.AddScoped<IGennericRepository<Departnent>, DeparmentRepository>();
            builder.Services.AddScoped<IGennericRepository<Branch>, BranchRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeReponsitory>();
            builder.Services.AddScoped<IGennericRepository<Manager>, managerRepository>();

            builder.Services.AddScoped<IGennericRepository<OverTime>, OvertimeRepository>();
            builder.Services.AddScoped<IGennericRepository<OvertimeType>, OvetimeTypeRepository>();

            builder.Services.AddScoped<IGennericRepository<Sanction>, SanctionRepository>();
            builder.Services.AddScoped<IGennericRepository<SanctionType>, SanctionTypeRepository>();

            builder.Services.AddScoped<IGennericRepository<Vacation>, VacationRepository>();
            builder.Services.AddScoped<IGennericRepository<VacationType>, VacationTypeRepository>();

            builder.Services.AddScoped<IGennericRepository<Contract>, ContractRepository>();
            builder.Services.AddScoped<IGennericRepository<Education>, EducationRepository>();

            builder.Services.AddScoped<IGennericRepository<Insurance>, InsuranceRepository>();
            builder.Services.AddScoped<IGennericRepository<Attendance>, AttendanceRepository>();

            builder.Services.AddScoped<IGennericRepository<WorkType>, WorkTypeRepository>();

            //country/ province/district
            builder.Services.AddScoped<IGennericRepository<Country>, CountryRepository>();
            builder.Services.AddScoped<IGennericRepository<Province>, ProvinceRepository>();
            builder.Services.AddScoped<IGennericRepository<District>, DistrictRepository>();

            //Service 
            // Repositories
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IServiceRequestRepository, ServiceRequestRepository>();
            builder.Services.AddScoped<IServiceRequestApprovalRepository, ServiceRequestApprovalRepository>();
            builder.Services.AddScoped<IServiceScheduleRepository, ServiceScheduleRepository>();
            builder.Services.AddScoped<ICashServiceRepository, CashServiceRepository>();
            builder.Services.AddScoped<ICashTransactionRepository, CashTransactionRepository>();
            builder.Services.AddScoped<IJobPositionRepository, JobPositionRepository>();
            builder.Services.AddScoped<ICandidateRepository, CandidateRepository>();
            builder.Services.AddScoped<IInterviewRepository, InterviewRepository>();
            builder.Services.AddScoped<ITrainingProgramRepository, TrainingProgramRepository>();
            builder.Services.AddScoped<ITrainingHistoryRepository, TrainingHistoryRepository>();
            builder.Services.AddScoped<IServiceRepository, ServiceRepositorys>();
            builder.Services.AddScoped<IEmployeeSupportRepository, SupportCustommerRepository>();


            builder.Services.AddScoped<IMailRepository, MailRepository>();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<IGennericRepository<Testimonial>, TestimonialRepository>();
            builder.Services.AddScoped<IGennericRepository<Position>, PositionRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline
            // Configure the HTTP request pipeline
            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                    //c.RoutePrefix = string.Empty; // Set Swagger UI at app's root
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); // Use the same endpoint for production or adjust if needed
                    //c.RoutePrefix = string.Empty; // Set Swagger UI at app's root
                });
            }

            app.UseRouting();
            app.UseCors(options => options
                .WithOrigins("http://localhost:3000", "http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}
