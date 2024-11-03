public class Rental
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal TotalCost { get; set; }

    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    public int VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
}
