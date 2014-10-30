using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KHelper.Models;
using System.Data;
using System.Data.SqlClient;

namespace KHelper.storage
{
    public class storagemaker
    {


        public void addnewproduct() 
        {
            
        }

        public List<Product> iteminstorage(int userid) 
        {
            List<Product> list = new List<Product>();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Дмитрий\Documents\GitHub\Ask_service\Stesnyashki\Stesnyashki\bin\StesnyashkiDB\StesnyashkiDB\StesnyashkiDB.dbmdl;Integrated Security=True");//подключение к БД
            conn.Open();
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter sqlcont = new SqlDataAdapter();
                sqlcont = new SqlDataAdapter("Select * From Storage Where id_u =" + userid, conn);//поменять название поля и таблицы в БД
                sqlcont.Fill(dt);
            }
            catch(Exception e)
            {
                conn.Close();
                return null;
            }
            //List<string> name = dt.AsEnumerable().Select(r => r.Field<string>("name_product")).ToList();
            SqlDataAdapter sq = new SqlDataAdapter("Select Count(id_u) From Storage Where id_u =" + userid, conn);
            DataTable d = new DataTable();
            sq.Fill(d);
            int count = d.Columns[0][0];
            for (int i = 0; i < count; i++) 
            {
                Product p = new Product {productname= Convert.ToString( dt.Rows[i][1]), count= Convert.ToInt32(dt.Rows[i][2]), title= Convert.ToString(dt.Rows[i][3])};
                list.Add(p);
            }
            conn.Close();
                return list;
        }

        public bool change(int idu,string nameofproduct,int count) 
        {


            return true;
        }
    }
}