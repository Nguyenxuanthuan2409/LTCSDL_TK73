using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;

    public class CategoriesRep : GenericRep<NorthwindContext, Categories>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override Categories Read(int id)
        {
            var res = All.FirstOrDefault(p => p.CategoryId == id);
            return res;
        }


        /// <summary>
        /// Remove and not restore
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Number of affect</returns>
        public int Remove(int id)
        {
            var m = base.All.First(i => i.CategoryId == id);
            m = base.Delete(m); //TODO
            return m.CategoryId;
        }

        #endregion

        

        /// <summary>
        /// Initialize
        /// </summary>
        /// 
        public object getCustOrderHist(string cusId)       // ADO.DOT NET
        {
            List<object> res = new List<object>();      // res la result , ket qua
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "CustOrderHist";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", cusId);
                da.SelectCommand = cmd;
                da.Fill(ds);        // do du lieu vao
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)  // Kiem tra co du lieu hay khong
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            ProductName = row["ProductName"],
                            Total = row["Total"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        public object GetCustOrdersDetail(int ordId)       // ADO.DOT NET
        {
            List<object> res = new List<object>();      // res la result , ket qua
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "CustOrdersDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderID", ordId);
                da.SelectCommand = cmd;
                da.Fill(ds);        // do du lieu vao
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)  // Kiem tra co du lieu hay khong
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            ProductName = row["ProductName"],
                            UnitPrice = row["UnitPrice"],
                            Quantity = row["Quantity"],
                            Discount = row["Discount"],
                            ExtendedPrice = row["ExtendedPrice"]

                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        public object GetCustOrdersOrders(string ordId)       // ADO.DOT NET
        {
            List<object> res = new List<object>();      // res la result , ket qua
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "CustOrdersOrders";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerID", ordId);
                da.SelectCommand = cmd;
                da.Fill(ds);        // do du lieu vao
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)  // Kiem tra co du lieu hay khong
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            OrderID = row["OrderID"],
                            OrderDate = row["OrderDate"],
                            RequiredDate = row["RequiredDate"],
                            ShippedDate = row["ShippedDate"],


                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        public object getCustOrderHist_LinQ(string cusId)       // ADO.DOT NET LIN Q
        {
            var res = Context.Products
                .Join(Context.OrderDetails, a => a.ProductId, b => b.ProductId, (a, b) => new
                {
                    a.ProductId,    // Lay cac Trường trong Stored
                    a.ProductName,
                    b.Quantity,
                    b.OrderId
                })
                .Join(Context.Orders, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.ProductId,
                    a.ProductName,
                    a.Quantity,
                    a.OrderId,
                    b.CustomerId
                }).Where(x=>x.CustomerId == cusId)
                .ToList();

            var data = res.GroupBy(x => x.ProductName).Select(x => new
            {
                ProductName = x.First().ProductName,    // các trường select trong Stored
                Total = x.Sum(p => p.Quantity)
            });

            return data;    // Return kiểu dữ liệu trả về
            
        }

        public object getCustOrdersDetail_LinQ(int ordId)       // ADO.DOT NET LIN Q
        {
            var res = Context.Products
                .Join(Context.OrderDetails, a => a.ProductId, b => b.ProductId, (a, b) => new       // Ket ProductId
                {
                    a.ProductId,    // Lay cac Trường trong Stored
                    a.ProductName,
                    b.UnitPrice,
                    b.Quantity,
                    b.Discount,
                    ExtendedPrice = b.Quantity * (1 - (decimal)b.Discount) * b.UnitPrice,   // Ep sang kieu decimal
                    b.OrderId
                }).Where(x => x.OrderId == ordId)       // Ket OrderID
                .ToList();

            // Co Group By thi co dong duoi
            //var data = res.Select(x => new
            //{
            //    ProductName = x.ProductName,    // các trường select trong Stored
            //    UnitPrice =  x.UnitPrice,
            //    Quantity = x.Quantity,
            //    Discount = x.Discount,
            //    x.ExtendedPrice

            //});
            //return Data;

            return res;    // Return kiểu dữ liệu trả về

        
        }

        public object getEmployeeRevenue(DateTime date)       // ADO.DOT NET
        {
            List<object> res = new List<object>();      // res la result , ket qua
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "DoanhThuTatCaNhanVienTrongNgay";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@date", date);
                da.SelectCommand = cmd;
                da.Fill(ds);        // do du lieu vao
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)  // Kiem tra co du lieu hay khong
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            EmployeeID = row["EmployeeID"],
                            LastName = row["LastName"],
                            FirstName = row["FirstName"],
                            Revenue = row["Revenue"]
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

        public object getEmployeeRevenue_LinQ(DateTime date)       // ADO.DOT NET
        {
            var res = Context.Orders
                .Join(Context.OrderDetails, a => a.OrderId, b => b.OrderId, (a, b) => new
                {
                    a.OrderDate,
                    a.EmployeeId,
                    Revenue = b.Quantity * (1 - (decimal)b.Discount) * b.UnitPrice
                })
                .Join(Context.Employees, a => a.EmployeeId, b => b.EmployeeId, (a, b) => new
                {
                    a.OrderDate,
                    a.EmployeeId,
                    a.Revenue,
                    b.LastName,
                    b.FirstName
                })
                .Where(x => x.OrderDate.Value.Date == date.Date)
                .ToList();
            var data = res.GroupBy(x => x.EmployeeId).Select(x => new
            {
                x.First().EmployeeId,
                x.First().FirstName,
                x.First().LastName,
                Revenue = x.Sum(p => p.Revenue)
            });

            return data;
        }
            public object getEmployeeRevenueStartEnd(DateTime begindate, DateTime enddate)       // ADO.DOT NET
        {
            List<object> res = new List<object>();      // res la result , ket qua
            var cnn = (SqlConnection)Context.Database.GetDbConnection();
            if (cnn.State == ConnectionState.Closed)
                cnn.Open();

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();
                var cmd = cnn.CreateCommand();
                cmd.CommandText = "DoanhThuNhanVienCoNgayBatDauVaKetThuc";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@startdate", begindate);
                cmd.Parameters.AddWithValue("@endtdate", enddate);
                da.SelectCommand = cmd;
                da.Fill(ds);        // do du lieu vao
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)  // Kiem tra co du lieu hay khong
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        var x = new
                        {
                            EmployeeID = row["EmployeeID"],
                            LastName = row["LastName"],
                            FirstName = row["FirstName"],
                            Revenue = row["Revenue"],
                        };
                        res.Add(x);
                    }
                }

            }
            catch (Exception e)
            {
                res = null;
            }
            return res;
        }

    }

}

