using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CommPinboardAPI.Entities
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExternalId { get; set; } = Guid.NewGuid();
        [Column(TypeName="datetime2")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Column(TypeName="datetime2")]
        public DateTime DateUpdated { get; set;} = DateTime.Now;
        [Column(TypeName ="bit")]
        public bool IsDeleted { get; set; } = false;
    }
}