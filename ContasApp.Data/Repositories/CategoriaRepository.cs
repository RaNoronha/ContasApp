using ContasApp.Data.Entities;
using ContasApp.Data.Settings;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class CategoriaRepository
    {
        public List<Categoria> TodasCategorias()
        {
            var query = @"
                SELECT * FROM CATEGORIA ORDER BY DESCRICAO";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                return conexao.Query<Categoria>(query).ToList();
            }
        }
    }
}
