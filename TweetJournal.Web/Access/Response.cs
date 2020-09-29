namespace TweetJournal.Web.Access
{
    internal class Response<T>
    {
        public bool IsError { get; set; }
        public string Error { get; set; }
        public T Result { get; set; }
    }
}
