using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.DTOS.Employee.DTOs;
using Domain.Entities.Entitie.Service;
using Microsoft.AspNetCore.Http;

namespace Aplication.DTOS.Service.DTO
{
    public class ServiceDto
    {
        public class CustomerDto
        {
            public int? CustomerID { get; set; }
            public string? CustomerName { get; set; }
            public string? Email { get; set; }
            public string? Image { get; set; }
            public string? Phone { get; set; }
        }


    public class ServiceRequestDto
    {

        public int ServiceRequestID { get; set; }
            public string ServiceName { get; set; }
        public int CustomerID { get; set; }
        public string ServiceType { get; set; }
        public string RequestDetails { get; set; }
        public string Status { get; set; }
        public CustomerDto Customer { get; set; }
    }

       


        public class ServiceRequestApprovalDto
        {
            public int ApprovalID { get; set; }
            public int ServiceRequestID { get; set; }
            public string ApprovalStatus { get; set; }
            public DateTime ApprovalDate { get; set; }
            public ServiceRequestDto ServiceRequest { get; set; }
        }

        public class ServiceScheduleDto
        {
            public int? ScheduleID { get; set; }
            public int? ServiceRequestID { get; set; }
            public int? EmployeeID { get; set; }
            public DateTime? ScheduledDate { get; set; }
            public string? Location { get; set; }
            public string? Name { get; set; }
            public ServiceRequestDto? ServiceRequest { get; set; }
        }

        public class CashServiceDto
        {
            public int? CashServiceID { get; set; }
            public string? ServiceType { get; set; }
            public string? Name { get; set; }
            public string? Scope { get; set; }
            public decimal? Price { get; set; }
            public string? Conditions { get; set; }
        }

        public class CashTransactionDto
        {
            public int? TransactionID { get; set; }
            public int? CashServiceID { get; set; }
            public decimal? Amount { get; set; }
            public DateTime? TransactionDate { get; set; }
            public string? Status { get; set; }
            public int? EmployeeID { get; set; }
            public CashServiceDto? CashService { get; set; }
        }

        public class JobPositionDto
        {
            public int? PositionID { get; set; }
            public string? PositionName { get; set; }
            public string? JobRequirements { get; set; }
            public string? JobDescription { get; set; }
            public DateTime? ApplicationDeadline { get; set; }
            public string? Status { get; set; }

        }

        public class CandidateDto
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Phone { get; set; }
            public string? cvFile { get; set; }
            public string? Status { get; set; }

            public List<Interview>? Interviews { get; set; }
        }

        public class InterviewDto
        {
            public int? InterviewID { get; set; }
            public string? Name { get; set; }
            public int? CandidateID { get; set; }
            public DateTime? InterviewDate { get; set; }
            public string? InterviewLocation { get; set; }
            public string? InterviewResult { get; set; }

            public string? Comments { get; set; }
            public CandidateDto? Candidate { get; set; }
        }

        public class TrainingProgramDto
        {
            public int? ProgramID { get; set; }
            public string? ProgramName { get; set; }
            public string? Description { get; set; }
            public string? Objectives { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }
            public string? Instructor { get; set; }
        }

        public class TrainingHistoryDto
        {
            public int? TrainingID { get; set; }
            public int? ProgramID { get; set; }
            public int? EmployeeID { get; set; }
            public string? CompletionStatus { get; set; }
            public string? Name { get; set; }
            public DateTime? CompletionDate { get; set; }
            public TrainingProgramDto? TrainingProgram { get; set; }
        }
        public class ServiceDtos
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal? Price { get; set; }
            public string? Status { get; set; }
            public string? Image { get; set; }
            public string Description { get; set; }
        }
        public class EmployeeSupportDto
        {
            public int Id { get; set; }
            public int? CustomerId { get; set; }
            public int? EmployeeId { get; set; }

            public int? CashServiceId { get; set; }
            public int? ServiceId { get; set; }
        }
    }
}
