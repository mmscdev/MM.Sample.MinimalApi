namespace MM.MinimalAPI.Contracts
{
    public class PackageCreated
    {
        public int PackageId { get; set; }
        public string Code { get; set; }
        public DateTimeOffset Timestamp { get; init; }
    }
}