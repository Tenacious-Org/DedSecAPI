using A5.Data.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace A5.Models
{
    public class Status : IEntityBase
    {
        public int Id {get; set;}
        public string StatusName {get;set;}
        public bool IsActive {get; set;}
    }
}