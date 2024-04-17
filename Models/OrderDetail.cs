namespace fruit_market_api.Models;

public record OrderDetail
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Count { get; set; }
    public float Price { get; set; }
  

}

