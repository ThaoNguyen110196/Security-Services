

using Domain.Entities.Entitie.Account;
using Domain.Entities.Entitie.Employee;
using Domain.Entities.Entitie.Service;
using Microsoft.EntityFrameworkCore;



namespace Infrastruture.Data
{
    public class AplicationContext : DbContext
    {
        public AplicationContext(DbContextOptions<AplicationContext> options)
      : base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Departnent> Departners { get; set; }

        //contry/  tow/city
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }


        public DbSet<AplictionUser> AplictionUsers { get; set; }
        public DbSet<GeneralDepartment> GeneralDepartment { get; set; }
        public DbSet<Manager> Managers { get; set; }


        public DbSet<RefreshTokenInfo> RefreshTokens { get; set; }


        //vacation/ Sanction /orvertime

        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<VacationType> VacastionTypes { get; set; }
        public DbSet<OvertimeType> OvertimeTypes { get; set; }

        public DbSet<OverTime> OverTime { get; set; }
        public DbSet<Sanction> Sanctions { get; set; }

        public DbSet<SanctionType> SanctionTypes { get; set; }

        public DbSet<Education> Educations { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<WorkType> WorkTypes { get; set; }

        // services interface
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        public DbSet<ServiceRequestApproval> ServiceRequestApprovals { get; set; }
        public DbSet<ServiceSchedule> ServiceSchedules { get; set; }
        public DbSet<CashService> CashServices { get; set; }
        public DbSet<CashTransaction> CashTransactions { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Interview> Interviews { get; set; }
        public DbSet<TrainingProgram> TrainingPrograms { get; set; }
        public DbSet<TrainingHistory> TrainingHistories { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<DeletionLog> DeletionLog { get; set; }
        public DbSet<EmployeeSupport> EmployeeSupports { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ giữa EmployeeSupport và Employee
            modelBuilder.Entity<EmployeeSupport>()
                .HasOne(es => es.Employee)
                .WithMany(e => e.EmployeeSupports)
                .HasForeignKey(es => es.EmployeeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Cấu hình cascade delete cho mối quan hệ giữa EmployeeSupport và Customer
            modelBuilder.Entity<EmployeeSupport>()
                .HasOne(es => es.Customer)
                .WithMany(c => c.EmployeeSupports)
                .HasForeignKey(es => es.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cấu hình cascade delete cho mối quan hệ giữa ServiceRequest và Customer
            modelBuilder.Entity<ServiceRequest>()
                .HasOne(sr => sr.Customer)
                .WithMany(c => c.ServiceRequests)
                .HasForeignKey(sr => sr.CustomerID)
                .OnDelete(DeleteBehavior.Cascade);


            // Cấu hình mối quan hệ giữa Customer và Testimonial
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Testimonials)
                .WithOne(t => t.Customer)
                .HasForeignKey(t => t.CustomerId);

            // Cấu hình mối quan hệ giữa Manager và Employee
            modelBuilder.Entity<Manager>()
                .HasMany(m => m.Employees)
                .WithOne(e => e.Manager)
                .HasForeignKey(e => e.ManagerId);

            // Cấu hình mối quan hệ giữa Employee và Manager
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager) // Một nhân viên có một người quản lý
                .WithMany(m => m.Employees) // Một người quản lý có thể có nhiều nhân viên
                .HasForeignKey(e => e.ManagerId) // Khóa ngoại trong bảng Employee
                .OnDelete(DeleteBehavior.SetNull); // Đặt giá trị null khi xóa

            // Cấu hình mối quan hệ giữa Manager và Employee (nếu Manager cũng là Employee)
            modelBuilder.Entity<Manager>()
                .HasOne<Employee>() // Nếu Manager cũng là Employee
                .WithMany() // Không có thuộc tính điều hướng từ Employee đến Manager
                .HasForeignKey(m => m.EmployeeId) // Khóa ngoại trong bảng Manager
                .OnDelete(DeleteBehavior.SetNull); // Đặt giá trị null khi xóa

            // Cấu hình quan hệ một-nhiều giữa CashService và CashTransaction
            modelBuilder.Entity<CashTransaction>()
                .HasOne(ct => ct.CashService)
                .WithMany(cs => cs.CashTransactions)
                .HasForeignKey(ct => ct.CashServiceID)
                .OnDelete(DeleteBehavior.Restrict); // Hoặc DeleteBehavior.Cascade tùy theo yêu cầu của bạn
            modelBuilder.Entity<CashService>()
               .HasMany(cs => cs.CashTransactions)
               .WithOne(ct => ct.CashService)
               .HasForeignKey(ct => ct.CashServiceID);
        }

    }
}