namespace TweetJournal.Contracts.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;

        public static class Entry
        {
            public const string GetAll = Base + "/entry";
            public const string Create = Base + "/entry";
            public const string GetOne = Base + "/entry/{entryId}";
            public const string Update = Base + "/entry/{entryId}";
            public const string Delete = Base + "/entry/{entryId}";
        }

        public static class Authentication
        {
            public const string Login = Base + "/authenticate";
        }
    }
}
