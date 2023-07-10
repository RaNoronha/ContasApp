using ContasApp.Data.Entities;
using ContasApp.Data.Settings;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContasApp.Data.Repositories
{
    public class ContaRepository
    {
        public void Cadastrar(Conta conta)
        {
            var query = @"
                INSERT INTO CONTA(ID, NOME, DATA, VALOR, TIPO, OBSERVACOES, CATEGORIAID, USUARIOID)
                VALUES(@Id, @Nome, @Data, @Valor, @Tipo, @Observacoes, @CategoriaId, @UsuarioId)";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, conta);
            }
        }

        public void Alterar(Conta conta)
        {
            var query = @"
                UPDATE CONTA 
                SET 
                    NOME = @Nome,
                    DATA = @Data,
                    VALOR = @Valor,
                    TIPO = @Tipo,
                    OBSERVACOES = @Observacoes,
                    CATEGORIAID = @CategoriaId
                WHERE
                    ID = @Id";

            using (var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, conta);
            }
        }

        public void Apagar(Conta conta)
        {
            var query = @"
                DELETE FROM CONTA WHERE ID = @Id";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, conta);
            }
        }

        public List<Conta> PesquisarDataUsuario(DateTime dtinicio, DateTime dtfim, Guid id)
        {
            var query = @"
                SELECT * FROM CONTA WHERE
                    USUARIOID = @UsuarioId
                AND DATA BETWEEN @dtinicio AND @dtfim
                ORDER BY DATA DESC";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                return conexao.Query<Conta>(query, new { @UsuarioId = id, @dtinicio = dtinicio, @dtfim = dtfim }).ToList();
            }
        }

        public Conta? PesquisaId(Guid id)
        {
            var query = @"
                SELECT * FROM CONTA WHERE ID = @Id";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                return conexao.Query<Conta>(query, new { @Id = id }).FirstOrDefault();
            }
        }
    }
}
