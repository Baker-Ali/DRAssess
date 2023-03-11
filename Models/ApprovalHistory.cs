namespace DRAssessment.Models
{
    public class ApprovalHistory
    {
        public int ApprovalHistoryId { get; set; }
        public int VacationRequestId { get; set; }
        public virtual VacationRequest VacationRequest { get; set; }
        public int HeadApproverId { get; set; }
        public int ApproverId { get; set; }
        public virtual Employee  HeadApprover { get; set; }
        public virtual Operation Approver { get; set; }
        public DateTime HeadApprovalDateTime { get; set; }
        public DateTime ApprovalDateTime { get; set; }
        public string HeadApprovalStatus { get; set; }
        public string ApprovalStatus { get; set; }
    }
}
