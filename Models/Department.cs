namespace DRAssessment.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public int HeadId { get; set; }
        public virtual Employee Head { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }

    }
}
