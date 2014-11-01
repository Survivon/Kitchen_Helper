using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using KHelper.Models;

namespace KHelper.recipe
{
    public class Recipemaker
    {
        public List<Recipe> easylistofrecipe()
        {
            List<Recipe> list = new List<string>();
            SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Дмитрий\Documents\GitHub\Ask_service\Stesnyashki\Stesnyashki\bin\StesnyashkiDB\StesnyashkiDB\StesnyashkiDB.dbmdl;Integrated Security=True");//подключение к БД
            conn.Open();
            SqlDataAdapter sqa = new SqlDataAdapter("Select name_recipe, photo, info from Recipe",conn );
            DataTable dt = new DataTable();
            sqa.Fill(dt);

        }
    }
}