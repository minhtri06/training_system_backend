namespace backend.Dto.ApiResponse
{
    public class ApiResponseDto
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = "";
        public Object Data { get; set; } = new List<Object>();
    }
}
