
namespace Common
{
    public class ResultMessage<T>
    {
        public bool Success { set; get; }
        public string Message { set; get; }
        public T Enything { set; get; }
    }
    public class ResultMessage
    {
        public bool Success { set; get; }
        public string Message { set; get; }
    }
}
