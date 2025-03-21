using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRestAPI.Domain.Entities
{
    public abstract class EntityBase : IDisposable
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }

        public EntityBase()
        {

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
