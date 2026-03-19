using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Domain;
using SqlKata.Compilers;
using SqlKata;

namespace Infraestructure
{
    public class UserRepository: IUserRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly Compiler compiler;

        public UserRepository(string connection)
        {
            dbConnection = new SqliteConnection(connection);
            compiler = new SqliteCompiler();
        }

        public async Task CrearUsuario(User user)
        {
        var query = new Query("users").AsInsert(new
        {
        email = user.Email,
        password = user.Password,
        username = user.Username,
        age = user.Age,
        gender = user.Gender
        });

        var consulta = compiler.Compile(query);

        await dbConnection.ExecuteAsync(consulta.Sql, consulta.NamedBindings);
        }

        public async Task<User?> ObtenerUsuarioPorEmail(string email)
        {
            var query = new Query("users").Where("email", email);
            var consulta = compiler.Compile(query);

            return await dbConnection.QueryFirstOrDefaultAsync<User?>(consulta.Sql, consulta.NamedBindings);
        }
    }
}