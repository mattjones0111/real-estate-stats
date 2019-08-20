namespace FundaApiClient.Model
{
    public class Document
    {
        public long AccountStatus { get; set; }
        public bool EmailNotConfirmed { get; set; }
        public bool ValidationFailed { get; set; }
        public object ValidationReport { get; set; }
        public long Website { get; set; }
        public Metadata Metadata { get; set; }
        public Property[] Objects { get; set; }
        public Paging Paging { get; set; }
        public long TotaalAantalObjecten { get; set; }
    }

    public class Metadata
    {
        public string ObjectType { get; set; }
        public string Omschrijving { get; set; }
        public string Titel { get; set; }
    }

    public class Property
    {
        public long MakelaarId { get; set; }
        public string MakelaarNaam { get; set; }
    }

    public class Paging
    {
        public long AantalPaginas { get; set; }
        public long HuidigePagina { get; set; }
        public string VolgendeUrl { get; set; }
        public object VorigeUrl { get; set; }
    }
}
