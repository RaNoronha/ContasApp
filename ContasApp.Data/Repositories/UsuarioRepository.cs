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
    public class UsuarioRepository
    {
        public void Cadastrar(Usuario usuario)
        {
            var query = @"INSERT INTO USUARIO(
                ID,
                NOME,
                EMAIL,
                SENHA,
                DATAHORACRIACAO)
            VALUES(
                @Id,
                @Nome,
                @Email,
                CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                @DataHoraCriacao)
            ";

            using (var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, usuario);
            }
        }

        public void Atualizar(Usuario usuario)
        {
            var query = @"
                UPDATE USUARIO SET
                    NOME = @Nome,
                    EMAIL = @Email,
                WHERE
                    ID = @Id
            ";

            using (var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, usuario);
            }
        }

        public void Atualizar(Guid id, string senha)
        {
            var query = @"
                UPDATE USUARIO SET
                    SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2)
                WHERE ID = @Id";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, new { @Id = id, @Senha = senha });
            }
        }

        public void Deletar(Usuario usuario)
        {
            var query = @"
                DELETE FROM USUARIO 
                WHERE ID = @Id
            ";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                conexao.Execute(query, usuario);
            }
        }

        public Usuario? PesquisaId(Guid id)
        {
            var query = @"
                SELECT * FROM USUARIO WHERE ID = @Id";

            using (var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                return conexao.Query<Usuario>(query, new {@Id = id}).FirstOrDefault();
            }
        }

        public Usuario? PesquisaEmail(string email)
        {
            var query = @"
                SELECT * FROM USUARIO WHERE EMAIL = @Email";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                return conexao.Query<Usuario>(query, new {@Email = email}).FirstOrDefault();
            }
        }

        public Usuario? PesquisaEmaileSenha(string email, string senha)
        {
            var query = @"
                SELECT * FROM USUARIO WHERE EMAIL = @Email AND SENHA = CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2)
            ";

            using(var conexao = new SqlConnection(SqlServerSettings.ConnectionSql()))
            {
                return conexao.Query<Usuario>(query, new {@Email = email, @Senha = senha}).FirstOrDefault();
            }
        }
    }
}





