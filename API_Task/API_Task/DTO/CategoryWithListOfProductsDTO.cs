namespace API_Task.DTO
{
    public class CategoryWithListOfProductsDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductDTO>? Products { get; set; } 
    }
    public class ProductDTO
    {
        public string Name { get; set; }
    }
}
