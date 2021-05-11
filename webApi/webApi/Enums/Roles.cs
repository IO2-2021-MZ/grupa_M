
using System.Runtime.Serialization;


namespace webApi.Enums
{
    public enum Role
    {
        [EnumMember(Value = "Restaurer")]
        Restaurer,
        [EnumMember(Value = "Admin")]
        Admin,
        [EnumMember(Value = "Customer")]
        Customer
    }
}
