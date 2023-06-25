using domain.Model;
using NPOI.SS.UserModel;
using OmsDal.Dal;
using OmsModel.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmsBll.Bll
{
    public partial class OrderInfoBll
    {
        private OrderInfoDal _orderInfoDal = new OrderInfoDal();

        public int TakeDishByOrderId(int orderId, int dishId)
        {
            try
            {
                return _orderInfoDal.InsertOrderDetail(orderId, dishId);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public List<TakeOrderDishInfoDTO> TakeList(int orderId)
        {
            return _orderInfoDal.TakeList(orderId);           
        }


        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int TokeOrder(int tableId)
        {
            try
            {
                return _orderInfoDal.InsertOrderAndTableInfo(tableId);
            }
            catch (Exception ex)
            {
                return -1;
            }
       
        }

        public int GetOrderId(int tableId)
        { 
            return _orderInfoDal.GetOrderIdByTableId(tableId);          
        }
        public int UpdateCountByOId(int oid, int count)
        { 
            return _orderInfoDal.UpdateCountByOId(oid, count);          
        }

        /// <summary>
        /// 删除菜品，订单详情记录
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public int DeleteDetailById(int oid)
        {
            return _orderInfoDal.DeleteDetailById(oid);
        }

        /// <summary>
        /// 点完菜后下单
        /// </summary>
        /// <param name="oid"></param>
        /// <returns></returns>
        public int TakeOrderDetail(int orderId, decimal money)
        {
           return _orderInfoDal.SetOrderMomey(orderId, money);
        }

        public decimal GetTotalMoneyByOrderId(int orderId)
        {
           return _orderInfoDal.GetTotalMoneyByOrderId(orderId);
        }
        public OrderInfo GetOrderInfoByOId(int orderId)
        {
            return _orderInfoDal.SelectOneById(orderId);
        }

        //结账
        public int payOrder(OrderPayMoneyDTO o)
        {
            return _orderInfoDal.UpdateOrderInfoPay(o.tid, o.memberInfoId, o.money, o.oid, o.discount, o.balance, o.isBal);
        }


    }
}
