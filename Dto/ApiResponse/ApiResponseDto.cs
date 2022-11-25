namespace backend.Dto.ApiResponse
{
    public class ApiResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public Object Data { get; set; } = new List<Object>();
    }
}
