namespace MISA.Web01.Api.Model
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }
        public string EmployeeCode { get; set; }
        public string FullName { get; set; }
        public int? Gender { get; set; }
        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public DateTime? DateOfBirth { get; set;}

        public string? IdentityNumber { get; set; }
        public string? IdentityPlace { get; set; }

        public DateTime? IdentityDate { get; set; }

        public string? TaxCode { get; set; }
        public string? Salary { get; set; }

        public int? WorkStatus { get; set; }

        public Guid? PositionId { get; set; }
        public Guid? DeparmentId { get; set; }

        public DateTime? JoinDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    
        public string? ModifiedBy { get; set;}
    }

}
