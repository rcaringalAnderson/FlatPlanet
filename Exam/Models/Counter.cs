using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Exam.Models
{
    public class Counter
    {
        [Key]
        public int Id { get; set; }
        public int counter { get; set; }

        [Timestamp]
        public Byte[] counterdate { get; set; }
    }

    public class CounterDBContext : DbContext
    {
        public DbSet<Counter> Counters { get; set; }
    }
}