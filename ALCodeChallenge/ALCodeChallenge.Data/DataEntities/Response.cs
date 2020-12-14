using ALCodeChallenge.Data.Interfaces;
using System.Collections.Generic;


namespace ALCodeChallenge.Data.DataEntities
{
    public class Response<T> : IResponse<T> where T : class
    {
        public IEnumerable<T> items { get; set; }

        public bool has_more { get; set; }

        public int quota_max { get; set; }

        public int quota_remaining { get; set; }
    }
}
