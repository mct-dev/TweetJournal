namespace TweetJournal.Contracts.V1
{
    public static class ApiRoutes
    {
        private const string Root = "api";
        private const string Version = "v1";
        private const string Base = Root + "/" + Version;

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
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
        }
    }
}
