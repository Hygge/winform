using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsModel.Dto
{
    public class TakeOrderDishInfoDTO
    {

        /// <summary>
        /// 订单详情id
        /// </summary>
        public int OId { get; set; }
        /// <summary>
        /// 订单id
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public int DishId { get; set; }
        public int Count { get; set; }
        public string DTitle { get; set; }
        /// <summary>
        /// 菜单分类id
        /// </summary>
        public int DTypeId { get; set; }
        public decimal DPrice { get; set; }
        /// <summary>
        /// 菜单拼音
        /// </summary>
        public string DChar { get; set; }
      



    }
}
