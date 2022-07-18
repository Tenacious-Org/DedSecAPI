using A5.Data.Repository;
using A5.Data.Repository.Interface;
using System.ComponentModel.DataAnnotations.Schema;

namespace A5.Models
{
    public class Status
    {
        public int Id {get; set;}
        public string ? StatusName {get;set;}
        public bool IsActive {get; set;}
    }
}