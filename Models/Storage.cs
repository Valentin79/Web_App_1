namespace Web_API.Models
{
    public class Storage: BaseModel
    {
        public virtual List<Product> Products { get; set; } = new List<Product>();
        //public Product Product { get; set; } = null;
        //public List<ProductStorage> ProductStorages { get; set; } = new List<ProductStorage>();
    }
}
