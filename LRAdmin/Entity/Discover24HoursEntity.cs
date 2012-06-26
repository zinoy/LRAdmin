using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LRAdmin.Entity
{
    public class Discover24HoursEntity
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Weibo { get; set; }
        public string Place { get; set; }
        public int HighScore { get; set; }
        public DateTime Time { get; set; }
    }
}
