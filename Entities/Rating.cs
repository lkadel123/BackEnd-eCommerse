namespace WebApplication2.Entities
{
    public class Rating
    {
        public int RatingID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int SalesId { get; set; }
        public int RatingValue { get; set; }
        public string? Comment { get; set; }
        public DateTime RatingDate { get; set; } = DateTime.Now;

        public virtual Users User { get; set; }
        public virtual ProductInformation productInformation { get; set; }
        public virtual Sales Sale { get; set; }
    }
}
