using domain.Model;
using OmsDal.Utils;
using OmsModel.Domain;
using OmsModel.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OmsDal.Dal
{
    public partial class OrderInfoDal
    {

        public OrderInfo SelectOneById(int orderId)
        {
            string sql = "select * from orderInfo where oId=@oid";

            DataTable dt = SqliteHelper.ExecuteDataTable(sql, new SQLiteParameter("@oid", orderId));
            if (dt.Rows.Count > 0)
            {
                OrderInfo o = new OrderInfo();
                o.OId = Convert.ToInt32(dt.Rows[0]["oid"]);
                o.IsPay = Convert.ToBoolean(dt.Rows[0]["isPay"]);
                if (dt.Rows[0]["odate"] == DBNull.Value)
                {
                    o.ODate = null;
                }
                else
                {
                    o.ODate = (DateTime?)dt.Rows[0]["odate"];
                }
                if (dt.Rows[0]["tableId"] == DBNull.Value)
                {
                    o.TableId = null;
                }
                else
                {
                    o.TableId = Convert.ToInt32(dt.Rows[0]["tableId"]);
                }
                if (dt.Rows[0]["memberId"] == DBNull.Value)
                {
                    o.MemberId = null;
                }
                else
                {
                    o.MemberId = (int)dt.Rows[0]["memberId"];
                }

                if (dt.Rows[0]["oMoney"] == DBNull.Value)
                {
                    o.OMoney = null;
                }
                else
                {
                    o.OMoney = (decimal)dt.Rows[0]["oMoney"];
                }
                if (dt.Rows[0]["discount"] == DBNull.Value)
                {
                    o.Discount = null;
                }
                else
                {
                    o.Discount = (decimal)dt.Rows[0]["discount"];
                }



                return o;
            }
            return null;
        }

        /// <summary>
        /// 插入桌子下的单
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int InsertOrderAndTableInfo(int tableId)
        {
            //插入订单数据
            //更新餐桌状态
            //写在一起执行，只需要和数据库交互一次
            //下订单
            string sql = "insert into orderinfo(odate,ispay,tableId) values(datetime('now', 'localtime'),0,@tid);" +
                //更新餐桌状态
                "update tableinfo set tIsFree=0 where tid=@tid;" +
                //获取最新的订单编号
                "select oid from orderinfo where tableId=@tid order by oid desc limit 0,1";
            SQLiteParameter p = new SQLiteParameter("@tid", tableId);
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, p));
        }

        /// <summary>
        /// 获取已下单的菜列表
        /// </summary>
        /// <returns></returns>
        public List<TakeOrderDishInfoDTO> TakeList(int orderId)
        {
            string sql = "SELECT o.*, d.* FROM OrderDetailInfo o INNER JOIN DishInfo d on o.DishId = d.DId" +
               " WHERE o.OrderId=@orderId ";

            DataTable dt = SqliteHelper.ExecuteDataTable(sql, new SQLiteParameter("@orderId", orderId));
            List<TakeOrderDishInfoDTO > list = new List<TakeOrderDishInfoDTO>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new TakeOrderDishInfoDTO
                {
                    OId = Convert.ToInt32(dr["OId"]),
                    OrderId = Convert.ToInt32(dr["OrderId"]),
                    DishId = Convert.ToInt32(dr["DishId"]),
                    Count = Convert.ToInt32(dr["Count"]),
                    DTitle = Convert.ToString(dr["DTitle"]),
                    DPrice = Convert.ToDecimal(dr["DPrice"]),
                    DTypeId = Convert.ToInt32(dr["DTypeId"]),
                    DChar = Convert.ToString(dr["DChar"]),
                });

            }

            return list;
        }

        /// <summary>
        /// 查询当前桌位未结账订单
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int GetOrderIdByTableId(int tableId)
        {
            string sql = "select oid from orderinfo where tableId=@tableid and ispay=0";
            SQLiteParameter p = new SQLiteParameter("@tableId", tableId);
            return Convert.ToInt32(SqliteHelper.ExecuteScalar(sql, p));
        }

        public int InsertOrderDetail(int orderId, int dishId)
        {
            // 当前菜品已点则加数量，没有点则插入一条记录
            string sql = "SELECT Count(*) as count1 FROM OrderDetailInfo WHERE OrderId=@oid AND DishId=@did ";
            SQLiteParameter[] ps =
            {           
                new SQLiteParameter("@oid", orderId), new SQLiteParameter("@did", dishId)
            };
            object o = SqliteHelper.ExecuteScalar(sql, ps);
            if (o != null && Convert.ToInt32(o) > 0 )
            {
                //这个订单已经点过这个菜，让数量加1
                sql = "update orderDetailInfo set count=count+1 where orderId=@oid and dishId=@did";
            }
            else
            {
                //当前订单还没有点这个菜，加入这个菜
                sql = "insert into orderDetailInfo(orderid,dishId,count) values(@oid,@did,1)";
            }
            return SqliteHelper.ExecuteNoQuery(sql, ps);


        }

        /// <summary>
        /// 根据订单详情id更新菜品数量
        /// </summary>
        /// <param name="oid">订单详情id</param>
        /// <param name="count">菜品数量</param>
        /// <returns></returns>
        public int UpdateCountByOId(int oid, int count)
        {
            string sql = "update orderDetailInfo set count=@count where oid=@oid";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@count", count),
                new SQLiteParameter("@oid", oid)
            };
            return SqliteHelper.ExecuteNoQuery(sql, ps);
        }

        /// <summary>
        /// 获取订单金额
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public decimal GetTotalMoneyByOrderId(int orderid)
        {
            string sql = @"	select sum(oti.count*di.dprice) 
	            from orderdetailinfo as oti
	            inner join dishinfo as di
	            on oti.dishid=di.did
	            where oti.orderid=@orderid";
            SQLiteParameter p = new SQLiteParameter("@orderid", orderid);

            object obj = SqliteHelper.ExecuteScalar(sql, p);
            if (obj == DBNull.Value)
            {
                return 0;
            }
            return Convert.ToDecimal(obj);
        }

        public int SetOrderMomey(int orderid, decimal money)
        {
            string sql = "update orderinfo set omoney=@money where oid=@oid";
            SQLiteParameter[] ps =
            {
                new SQLiteParameter("@money", money),
                new SQLiteParameter("@oid", orderid)
            };
            return SqliteHelper.ExecuteNoQuery(sql, ps);
        }

        public int DeleteDetailById(int oid)
        {
            string sql = "delete from orderDetailInfo where oid=@oid";
            SQLiteParameter p = new SQLiteParameter("@oid", oid);
            return SqliteHelper.ExecuteNoQuery(sql, p);
        }

        /// <summary>
        /// 支付后修改状态
        /// </summary>
        /// <param name="tid">桌位id</param>
        /// <param name="memberInfoId">会员id</param>
        /// <param name="money">订单金额</param>
        /// <param name="oid">订单id</param>
        /// <param name="discount">会员折扣</param>
        /// <param name="balance">会员余额</param>
        /// <param name="isBal">是否使用余额 1使用 0未使用</param>
        public int UpdateOrderInfoPay(int tid, int memberInfoId, decimal money, int oid, decimal discount, decimal balance, int isBal)
        {
            int result = 0;         
    
            using (SQLiteConnection sqlConn = new SQLiteConnection(ConfigurationManager.ConnectionStrings["sqliteUrl"].ConnectionString.ToString()))
            {          
                // 打开连接
                sqlConn.Open();
                SQLiteTransaction transaction = sqlConn.BeginTransaction();     
                //创建command对象
                SQLiteCommand cmd = new SQLiteCommand();
                //将命令对象启用事务
                cmd.Transaction = transaction;              
                SQLiteParameter[] ps;

                // 会员
                if (memberInfoId > 0)
                {       
                    // 使用余额
                    if (isBal == 1)
                    {
                        string updateMemberInfo = "update memberInfo set MMoney=@balance where MId=@mid ";
                        // 更新会员余额
                        cmd.CommandText = updateMemberInfo;
                        cmd.Parameters.AddRange(new SQLiteParameter[2] {
                            new SQLiteParameter("mid", memberInfoId),
                            new SQLiteParameter("balance", balance)
                        });
                        result += cmd.ExecuteNonQuery();
                    }
                    string updateSqlOrderInfo2 = "update orderInfo set MemberId=@memberId, OMoney=@money , IsPay=1, Discount=@discount where OId=@oid";
                    // 更新订单状态
                    cmd.CommandText = updateSqlOrderInfo2;
                    cmd.Parameters.AddRange(new SQLiteParameter[4] {
                        new SQLiteParameter("memberId", memberInfoId),
                        new SQLiteParameter("money", money),
                        new SQLiteParameter("discount", discount),                        
                        new SQLiteParameter("oid", oid)
                    });
                    result += cmd.ExecuteNonQuery(); 
                }
                else
                {
                    // 不是会员    更新订单表，保存金额、支付状态，更新桌位状态未空闲
                    string updateSqlOrderInfo = "update orderInfo set OMoney=@money , IsPay=1 where OId=@oid";
                    ps = new SQLiteParameter[]
                    {
                        new SQLiteParameter("@oid", oid), new SQLiteParameter("@money", money)
                    };
                    cmd.CommandText = updateSqlOrderInfo;
                    cmd.Parameters.AddRange(ps);
                    result += cmd.ExecuteNonQuery();
                }
                // 更新桌位状态
                string updateSqlTable = "update tableInfo set TIsFree=1 where TId=@tid";
                ps = new SQLiteParameter[]
                {
                        new SQLiteParameter("@tid", tid)
                };
                cmd.CommandText = updateSqlTable;
                cmd.Parameters.AddRange(ps);
                result += cmd.ExecuteNonQuery();

                
                transaction.Commit();
                sqlConn.Close();

            }
            return result;

        }


    }
}
