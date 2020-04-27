using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TweetJournal.Access.Entries.Domain
{
    [ExcludeFromCodeCoverage]
    [Table("Entries")]
    public class Entry
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
