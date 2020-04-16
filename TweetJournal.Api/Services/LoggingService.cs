using System;

namespace TweetJournalApi.Services
{
    interface LoggingService
    {
        void Log(string value);
        void Debug(string value);
        void Exception(Exception value);
    }
}
