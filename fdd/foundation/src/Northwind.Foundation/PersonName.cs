using JetBrains.Annotations;

namespace Northwind.Foundation;

[PublicAPI]
public class PersonName : IComparable<PersonName>, IPii
{
    public PersonName(string givenName, string familyName)
    {
        GivenName = givenName;
        FamilyName = familyName;
    }
    
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string? MiddleName { get; set; }
    public string? Suffix { get; set; }

    public override string ToString()
    {
        return string.Join(',', $"{GivenName} {FamilyName}", Suffix);
    }

    public int CompareTo(PersonName? other)
    {
        if (ReferenceEquals(this, other)) return 0;
        if (ReferenceEquals(null, other)) return 1;
        var familyNameComparison = string.Compare(FamilyName, other.FamilyName, StringComparison.CurrentCultureIgnoreCase);
        if (familyNameComparison != 0) return familyNameComparison;        
        var givenNameComparison = string.Compare(GivenName, other.GivenName, StringComparison.CurrentCultureIgnoreCase);
        if (givenNameComparison != 0) return givenNameComparison;
        var middleNameComparison = string.Compare(MiddleName, other.MiddleName, StringComparison.CurrentCultureIgnoreCase);
        if (middleNameComparison != 0) return middleNameComparison;
        return string.Compare(Suffix, other.Suffix, StringComparison.CurrentCultureIgnoreCase);
    }
}