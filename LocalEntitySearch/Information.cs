using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LocalEntitySearch
{
    [DataContract]
    public class Information
    {
        [DataMember]
        public string ServerId
        {
            get;
            set;
        }

        [DataMember]
        public string TableId
        {
            get;
            set;
        }

        [DataMember]
        public string Updated
        {
            get;
            set;
        }


    }
}