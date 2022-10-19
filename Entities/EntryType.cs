using System.Runtime.Serialization;

namespace TransactionEntry.Entities
{
    public enum EntryType
    {
        [EnumMember(Value = "CREDIT")]
        Credit=0,
        [EnumMember(Value = "DEBIT")]
        Debit=1
    }
}