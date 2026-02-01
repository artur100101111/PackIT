using System.Runtime.Serialization;

namespace PackIT.Domain.Locations
{
    public enum LocationTypeEnum {

        [EnumMember(Value ="factory")]
        Factory=1,

        [EnumMember(Value = "area")]
        Area =2,

        [EnumMember(Value = "line")]
        Line =3,

        [EnumMember(Value = "warehouse")]
        Warehouse =4 
    }
}
