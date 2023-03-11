namespace DRAssessment.Models
{
    public class VacationRequest
    {
        public int VacationRequestId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string HeadApproval { get; set; } = "pending";
        public string Approval { get; set; } = "pending";
        public virtual ICollection<ApprovalHistory> ApprovalHistories { get; set; }
    }
}
