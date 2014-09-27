using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperList.Model
{
    public class Item
    {
        public DateTime CreateDate { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }

        public String HeatTicket { get; set; }
        public String ItemDescription { get; set; }

    }
}
