namespace EmployeeTaskManagementAPI.GlobalException
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ExceptionMessage { get; set; }= string.Empty;
    }
}
