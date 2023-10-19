namespace ItemManagment.Models
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { get; set; }
        DateTime DeletedAt { get; set; }
    }
}
