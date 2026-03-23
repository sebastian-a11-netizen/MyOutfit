using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using Domain.Interfaces;
using Domain.Entities;
using SqlKata.Compilers;
using SqlKata;

namespace Infraestructure.Repositories
{
    public class ClothingRepository : IClothingRepository
    {
        private readonly IDbConnection dbConnection;
        private readonly Compiler compiler;

        public ClothingRepository(string connection)
        {
            dbConnection = new SqliteConnection(connection);
            compiler = new SqliteCompiler();
        }

        public async Task<int> AgregarPrenda(Clothing clothing)
        {
            var query = new Query("clothes").AsInsert(new
            {
                user_id = clothing.UserId,
                category = clothing.Category,
                color = clothing.Color,
                image_url = clothing.ImageUrl,
                season = clothing.Season,
                style = clothing.Style,
                name = clothing.Name
            });

            var consulta = compiler.Compile(query);

            await dbConnection.ExecuteAsync(consulta.Sql, consulta.NamedBindings);

            var id = await dbConnection.ExecuteScalarAsync<int>("SELECT last_insert_rowid();");

            return id;
        }

        public async Task EliminarPrenda(int id)
        {
            var query = new Query("clothes").Where("id", id).AsDelete();

            var consulta = compiler.Compile(query);

            await dbConnection.ExecuteAsync(consulta.Sql, consulta.NamedBindings);
        }

        public async Task<IEnumerable<Clothing>> ObtenerPrendasPorUsuario(int userId)
        {   
            var query = new Query("clothes")
            .Select(
            "id",
            "user_id as UserId",
            "category as Category",
            "color as Color",
            "image_url as ImageUrl",
            "season as Season",
            "style as Style",
            "name as Name",
            "created_at as CreatedAt"
            )
            .Where("user_id", userId);

            var consulta = compiler.Compile(query);

            return await dbConnection.QueryAsync<Clothing>(consulta.Sql, consulta.NamedBindings);
        }
    }
}