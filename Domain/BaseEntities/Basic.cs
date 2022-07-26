using System;

namespace Domain.BaseEntities
{
    public class Basic
    {
        public long Id { set; get; }
        public bool IsRemoved { set; get; }
        public DateTime InsertTime { set; get; }
        public DateTime? LastUpdate { set; get; }
    }
}
