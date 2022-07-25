namespace Blaggo.Blackbox
{
    public class PayloadOptions
    {
        public string? Id { get; set; }
        public string? Ids { get; set; }

        public int? Status { get; set; }
        public int? Page { get; set; }
        public int? PerPage { get; set; }
    }
}