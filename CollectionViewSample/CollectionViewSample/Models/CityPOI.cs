namespace CollectionViewSample.Models
{
    public class CityPOI
    {
        //General Fields
        public string Type { get; set; }
        public string NID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Phone { get; set; }
        public string LinkText { get; set; }
        public string LinkURL { get; set; }
        public double Latitidue { get; set; }
        public double Longitude { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        //Address Fields
        public string Organization { get; set; }
        public string AddressLineOne { get; set; }
        public string AddressLineTwo { get; set; }
        public string Locality { get; set; }
        public string AdministrativeArea { get; set; }
        public string PostalCode { get; set; }
    }
}
