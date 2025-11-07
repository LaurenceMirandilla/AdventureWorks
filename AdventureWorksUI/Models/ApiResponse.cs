public class ApiResponse<T>
{
    public int Total { get; set; }
    public List<T> Data { get; set; } = new List<T>();
}