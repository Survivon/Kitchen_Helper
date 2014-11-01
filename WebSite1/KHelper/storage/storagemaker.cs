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
        //funktion which add new product to storage or change it, if it is.
        public void addnewproduct(string nameofproduct, int count, string title)
        {            
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Дмитрий\Documents\GitHub\Ask_service\Stesnyashki\Stesnyashki\bin\StesnyashkiDB\StesnyashkiDB\StesnyashkiDB.dbmdl;Integrated Security=True");//подключение к БД
            conn.Open();
            SqlDataAdapter sq = new SqlDataAdapter("Select name_product from Storage where name_product like'%"+nameofproduct+"%'",conn);
            DataTable dt = new DataTable();
            sq.Fill(dt);
            if (dt.Columns[0][0] == nameofproduct)
            {
                SqlCommand sqc = new SqlCommand("Update Storage Set count=@count where name_product=@name", conn);
                sqc.Parameters.AddWithValue("@count", count);
                sqc.Parameters.AddWithValue("@name", nameofproduct);
                try
                {
                    sqc.ExecuteNonQuery();
                    conn.Close();
                    return;
                }
                catch (Exception e)
                {
                    string s = e.Message;
                }
            }
            else 
            {
                SqlDataAdapter sqa = new SqlDataAdapter("Select Max(id_u) from Storage",conn);
                DataTable dt1 = new DataTable();
                sqa.Fill(dt1);
                int id = dt1.Columns[0][0];
                SqlCommand sqc = new SqlCommand("Insert into Storage(id_u,name_product,count,title) values(@id,@name,@count,@title)");
                sqc.Parameters.AddWithValue("@id",id+1);
                sqc.Parameters.AddWithValue("@name",nameofproduct);
                sqc.Parameters.AddWithValue("@count",count);
                sqc.Parameters.AddWithValue("@title",title);
                try
                {
                    sqc.ExecuteNonQuery();
                    conn.Close();
                    return;
                }
                catch (Exception e) 
                {
                    string s = e.Message;
                }

            }
        } 

        //return list of product from storage
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
    }
}