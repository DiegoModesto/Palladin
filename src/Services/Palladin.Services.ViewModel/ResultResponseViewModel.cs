namespace Palladin.Services.ViewModel
{
    public class ResultResponseViewModel<T> where T : class
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }
    }
}
