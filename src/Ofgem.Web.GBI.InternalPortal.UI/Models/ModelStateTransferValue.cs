namespace Ofgem.Web.GBI.InternalPortal.UI.Models
{
    public class ModelStateTransferValue
    {
        public string Key { get; set; } = string.Empty;
        public string? AttemptedValue { get; set; }
        public object? RawValue { get; set; }
        public ICollection<string>? ErrorMessages { get; set; } = new List<string>();
    }
}
