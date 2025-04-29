namespace CarInsuranceSales.Models;

public class DocumentData
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string SerialNumber { get; set; }

    public override string ToString()
    {
        return $"{FirstName} {LastName} {SerialNumber}";
    }
}
