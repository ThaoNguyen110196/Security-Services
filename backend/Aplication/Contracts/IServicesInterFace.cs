
using Aplication.Responses;
using Domain.Entities.Entitie.Employee;
using Domain.Entities.Entitie.Service;


namespace Aplication.Contracts
{
    public interface IServicesInterFace
    {
       
    }
    public interface IServiceRepository
    {

        Task<Service> GetByIdAsync(int id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task<GeneralReponse> AddAsync(Service service);
        Task<GeneralReponse> UpdateAsync(Service service);
        Task<GeneralReponse> DeleteAsync(int id);
    }
    
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Customer> AddCustomerAsync(Customer customer);
        Task<GeneralReponse> UpdateCustomerAsync(Customer customer);
        Task<GeneralReponse> DeleteCustomerAsync(int id);
    }


    public interface IServiceRequestRepository
    {
        Task<IEnumerable<ServiceRequest>> GetAllServiceRequestsAsync();
        Task<ServiceRequest> GetServiceRequestByIdAsync(int id);
        Task<IEnumerable<ServiceRequest>> GetServiceRequestsByCustomerIdAsync(int id);

        Task<GeneralReponse> AddServiceRequestAsync(ServiceRequest request);
        Task<GeneralReponse> UpdateServiceRequestAsync(ServiceRequest request);
        Task<GeneralReponse> DeleteServiceRequestAsync(int id);
    }

    public interface IServiceRequestApprovalRepository
    {
        Task<IEnumerable<ServiceRequestApproval>> GetAllServiceRequestApprovalsAsync();
        Task<ServiceRequestApproval> GetServiceRequestApprovalByIdAsync(int id);
        Task AddServiceRequestApprovalAsync(ServiceRequestApproval approval);
        Task UpdateServiceRequestApprovalAsync(ServiceRequestApproval approval);
        Task DeleteServiceRequestApprovalAsync(int id);
    }

    public interface IServiceScheduleRepository
    {
        Task<IEnumerable<ServiceSchedule>> GetAllServiceSchedulesAsync();
        Task<ServiceSchedule> GetServiceScheduleByIdAsync(int id);
        Task<GeneralReponse> AddServiceScheduleAsync(ServiceSchedule schedule);
        Task<GeneralReponse> UpdateServiceScheduleAsync(ServiceSchedule schedule);
        Task<GeneralReponse> DeleteServiceScheduleAsync(int id);
    }

    public interface ICashServiceRepository
    {
        Task<IEnumerable<CashService>> GetAllCashServicesAsync();
        Task<CashService> GetCashServiceByIdAsync(int id);
        Task<GeneralReponse> AddCashServiceAsync(CashService service);
        Task<GeneralReponse> UpdateCashServiceAsync(CashService service);
        Task<GeneralReponse> DeleteCashServiceAsync(int id);
    }

    public interface ICashTransactionRepository
    {
        Task<IEnumerable<CashTransaction>> GetAllCashTransactionsAsync();
        Task<CashTransaction> GetCashTransactionByIdAsync(int id);
        Task<GeneralReponse> AddCashTransactionAsync(CashTransaction transaction);
        Task<GeneralReponse> UpdateCashTransactionAsync(CashTransaction transaction);
        Task<GeneralReponse> DeleteCashTransactionAsync(int id);
        Task<IEnumerable<CashTransaction>> GetByCashServiceIdAsync(int cashServiceId);
    }

    public interface IJobPositionRepository
    {
        Task<IEnumerable<JobPosition>> GetAllJobPositionsAsync();
        Task<JobPosition> GetJobPositionByIdAsync(int id);
        Task<GeneralReponse> AddJobPositionAsync(JobPosition position);
        Task<GeneralReponse> UpdateJobPositionAsync(JobPosition position);
        Task<GeneralReponse> DeleteJobPositionAsync(int id);
    }

    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllCandidatesAsync();
        Task<Candidate> GetCandidateByIdAsync(int id);
        Task<GeneralReponse> AddCandidateAsync(Candidate candidate);
        Task<GeneralReponse> UpdateCandidateAsync(Candidate candidate);
        Task<GeneralReponse> DeleteCandidateAsync(int id);
    }

    public interface IInterviewRepository
    {
        Task<IEnumerable<Interview>> GetAllInterviewsAsync();
        Task<Interview> GetInterviewByIdAsync(int id);
        Task<Interview> GetInterviewByCandidateIdAsync(int id);

        Task<GeneralReponse> AddInterviewAsync(Interview interview);
        Task<GeneralReponse> UpdateInterviewAsync(Interview interview);
        Task<GeneralReponse> DeleteInterviewAsync(int id);
    }

    public interface ITrainingProgramRepository
    {
        Task<IEnumerable<TrainingProgram>> GetAllTrainingProgramsAsync();
        Task<TrainingProgram> GetTrainingProgramByIdAsync(int id);
        Task<GeneralReponse> AddTrainingProgramAsync(TrainingProgram program);
      
        Task<GeneralReponse> DeleteTrainingProgramAsync(int id);
        Task<GeneralReponse> UpdateTrainingProgramAsync(TrainingProgram program);
    }

    public interface ITrainingHistoryRepository
    {
        Task<IEnumerable<TrainingHistory>> GetAllTrainingHistoriesAsync();
        Task<TrainingHistory> GetTrainingHistoryByIdAsync(int id);
        Task<GeneralReponse> AddTrainingHistoryAsync(TrainingHistory history);
        Task<GeneralReponse> UpdateTrainingHistoryAsync(TrainingHistory history);
        Task<GeneralReponse> DeleteTrainingHistoryAsync(int id);
    }
}
