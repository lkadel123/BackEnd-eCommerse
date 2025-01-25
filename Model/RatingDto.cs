namespace WebApplication2.Model
{
    public class RatingDto
    {
        public int RatingID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int SaleID { get; set; }
        public int RatingValue { get; set; }
        public string Comment { get; set; }
    }
}
