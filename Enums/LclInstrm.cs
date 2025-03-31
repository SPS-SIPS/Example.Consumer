namespace SIPS.Example.Consumer.Enums;
public sealed class LclInstrm : Enumeration<string>
{
    public static LclInstrm CreditTransferAdviceMode = new("CRTAM", "Credit Transfer Advice Mode");
    public static LclInstrm CreditTransferRegularMode = new("CRTRM", "Credit Transfer Regular Mode");
    public static LclInstrm P2P = new("P2P", "Person to Person QR");
    public static LclInstrm P2M = new("P2M", "Person to Merchant QR");

    public LclInstrm()
    {
    }
    public LclInstrm(string id, string name)
        : base(id, name)
    {
    }
}
