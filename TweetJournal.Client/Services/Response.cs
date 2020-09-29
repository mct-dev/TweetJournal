namespace TweetJournal.Client.Services
{
    public class Response<T>
    {
        public bool IsError { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }
    }
}
