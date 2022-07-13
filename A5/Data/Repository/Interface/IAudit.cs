namespace A5.Data.Repository.Interface
{
    public interface IAudit
    {
        int AddedBy {get; set;}
        DateTime AddedOn{get; set;}
        int ? UpdatedBy {get; set;}
        DateTime ? UpdatedOn {get;set;}

    }
}