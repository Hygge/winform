using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsModel.Dto
{
    public class OrderPayMoneyDTO
    {
        public int tid { get; set; }
        public int memberInfoId { get; set; }
        public decimal money { get; set; }
        public int oid { get; set; }
        public decimal discount { get; set; }
        public decimal balance { get; set; }
        public int isBal { get; set; }
    }
}
