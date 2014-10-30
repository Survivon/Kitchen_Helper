using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace KHelper.registration
{
    public class registration
    {
        public string regist(string email, string password, string confpassword) 
        { 
        List<string> loginlist = new List<string>();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Дмитрий\Documents\GitHub\Ask_service\Stesnyashki\Stesnyashki\bin\StesnyashkiDB\StesnyashkiDB\StesnyashkiDB.dbmdl;Integrated Security=True");//подключение к БД
            conn.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter sqlcont = new SqlDataAdapter();
            sqlcont = new SqlDataAdapter("Select email From User Where email Like '%" + email + "%'", conn);//поменять название поля и таблицы в БД
            sqlcont.Fill(dt);
            loginlist = dt.AsEnumerable().Select(r => r.Field<string>("email")).ToList();
            sqlcont = new SqlDataAdapter("Select Max(idu) From User", conn);//поменять название поля и таблицы в БД
            sqlcont.Fill(dt);
            int maxid = dt.AsEnumerable().Select(r => r.Field<int>("email")).ToList()[0];
            foreach (var i in loginlist) 
            {
                if (i == email)
                    return "Пользователь с таким логином уже зарегистророван";
            }
            if (password == confpassword)
            {
                SqlCommand sq1 = new SqlCommand("Insert into Regist(id,login,password) values(@id,@email,@password)", conn);//поменять название поля и таблицы в БД
                sq1.Parameters.AddWithValue("@idu", 1);
                sq1.Parameters.AddWithValue("@email", email);
                sq1.Parameters.AddWithValue("@password", password);
                try
                {
                    sq1.ExecuteNonQuery();
                    conn.Close();
                    return "Регистрация проведена успешно!";
                }
                catch (Exception e)
                {
                    return e.Message;
                }
            }
            else return "Пароли не совпадают!";           
        }
    }
}
