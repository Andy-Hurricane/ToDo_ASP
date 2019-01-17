using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ToDo.Services.Zadania
{
    [DataContract]
    public enum SortFilter
    {
        [EnumMember]
        NULL,
        [EnumMember]
        ID,
        [EnumMember]
        START,
        [EnumMember]
        END,
        [EnumMember]
        STATUS,
        [EnumMember]
        PRIORITY,
        [EnumMember]
        TOPIC
    }
}