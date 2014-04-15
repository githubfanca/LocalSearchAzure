using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalEntitySearch.ResourceAccess.Model
{
    [Serializable]
    public class SearchResult<T> where T : class
    {
        public ulong SearchResultCnt
        {
            get;
            set;
        }

        public double DurationInSecond
        {
            get;
            set;
        }

        public IList<T> SearchResultList
        {
            get;
            set;
        }


    }
}