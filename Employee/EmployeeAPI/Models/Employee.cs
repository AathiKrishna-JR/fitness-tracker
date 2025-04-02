namespace EmployeeAPI.Models
{
    public class Employee
    {
        public int? EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int JobRoleID { get; set; }
        public int JobTitleID { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? DepartureDate { get; set; }
        public string? JobRole { get; set; }
        public string? JobTitle { get; set; }

    }

    public class JobTitleDto
    {
        public int TitleID { get; set; }
        public string Title { get; set; }
    }

    public class JobRoleDto
    {
        public int RoleID { get; set; }
        public string Role { get; set; }
    }
}
