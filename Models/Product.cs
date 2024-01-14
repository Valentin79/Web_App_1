namespace Web_API.Models
{
    public class Product: BaseModel
    {
        public int Coast { get; set; }
        public int CategoryID { get; set; }
        public virtual Category? Category { get; set; }
        public virtual List<Storage> Storages { get; set; } = new List<Storage>();

       
    }
}
