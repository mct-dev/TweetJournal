using System;
using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Api.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string message): base(message)
        { }
    }
}