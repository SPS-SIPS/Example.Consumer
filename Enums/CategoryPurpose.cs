namespace SIPS.Example.Consumer.Enums;

public sealed class CategoryPurpose : Enumeration<string>
{
    public static CategoryPurpose C2CCreditTransfer = new("C2CCRT", "C2C credit transfer");
    public static CategoryPurpose C2BCreditTransfer = new("C2BSQR", "C2B Request To Pay");
    public CategoryPurpose()
    {
    }
    public CategoryPurpose(string id, string name)
        : base(id, name)
    {
    }
}