using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalEntitySearch.ResourceAccess.Model
{
    public class Entity
    {
        public string Identifiers
        {
            get;
            set;
        }

        public string MasterId
        {
            get;
            set;
        }

        public string ExternalId
        {
            get;
            set;
        }

        public string OdpTitle
        {
            get;
            set;
        }

        public string OdpDescription
        {
            get;
            set;
        }

        public string FeedsMulti8
        {
            get;
            set;
        }

        public string FeedsMulti9
        {
            get;
            set;
        }

        public string RowNumber
        {
            get;
            set;
        }
    }
}