using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccServerAdmin.Domain.AccConfig;

namespace AccServerAdmin.Domain
{
    public class GlobalEntryList : IKeyedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid ServerId { get; set; }

        public string Name { get; set; }

        public List<Entry> Entries { get; set; }

        public static EntryList CreateDefault()
        {
            return new EntryList
            {
                Entries = new List<Entry>()
            };
        }
    }
}
