using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordsManagementClient.Model
{
    public class Record
    {
        public int Id { get; set; }
        public string Performer { get; set; } = null!;
        public string Title { get; set; } = null!;
        public double Price { get; set; }
        public int StockCount { get; set; } = 0;
    }
}
