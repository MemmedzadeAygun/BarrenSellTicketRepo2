namespace BarrenSellTicket.WebApi
{
    public class ApiResponseModel<T>
    {
        public int StatusCode { get; set; }
        public IEnumerable<string?> Messages { get; set; }
        public T? Result { get; set; }
    }
}
