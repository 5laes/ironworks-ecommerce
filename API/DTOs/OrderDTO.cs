namespace API.DTOs
{
    public class OrderDTO
    {
        public string basketId { get; set; }
        public int DeliveryMethodId { get; set; }
        public AddressDTO ShipToAddress { get; set; }
    }
}