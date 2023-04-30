#nullable disable
namespace SequentialFileNamesSample.Modals;

public partial class Person
{
    public int PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDate { get; set; }
    public TimeOnly TimeOnly { get; set; }
    public override string ToString() => $"{FirstName} {LastName} {BirthDate} {TimeOnly}";

}