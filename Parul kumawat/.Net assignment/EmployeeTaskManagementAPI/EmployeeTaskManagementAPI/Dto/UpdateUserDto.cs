namespace EmployeeTaskManagementAPI.Dto
{
    public class UpdateUserDto
    {
        public string Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; }= string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
    }
}
