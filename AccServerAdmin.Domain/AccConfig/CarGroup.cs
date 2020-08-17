using System.ComponentModel;

namespace AccServerAdmin.Domain.AccConfig
{
    public enum CarGroup
    {
        [Description("FreeForAll")]
        FreeForAll = 0,

        [Description("GT3")]
        GT3 = 1,

        [Description("GT4")]
        GT4 = 2,

        [Description("Cup")]
        Cup = 3,

        [Description("ST")]
        ST = 4,

        
    }
}
