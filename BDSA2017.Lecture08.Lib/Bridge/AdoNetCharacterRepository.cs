using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BDSA2017.Lecture08.Lib.Bridge
{
    public class AdoNetCharacterRepository : ICharacterRepository
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;

        public AdoNetCharacterRepository(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        public async Task<int> CreateAsync(Character character)
        {
            using (var command = _connection.CreateCommand())
            {
                var query = @"INSERT Characters (GivenName, Surname, Species, Origin, Year) 
                    VALUES (@GivenName, @Surname, @Species, @Origin, @Age); 
                    SELECT SCOPE_IDENTITY()";

                command.CommandText = query;
                command.Parameters.AddWithValue("@GivenName", character.GivenName);
                command.Parameters.AddWithValue("@Surname", character.Surname);
                command.Parameters.AddWithValue("@Species", character.Species as object ?? DBNull.Value);
                command.Parameters.AddWithValue("@Origin", character.Origin as object ?? DBNull.Value);
                command.Parameters.AddWithValue("@Year", character.Year as object ?? DBNull.Value);

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                var id = Convert.ToInt32(await command.ExecuteScalarAsync());

                character.Id = id;

                return id;
            }
        }

        public async Task<Character> FindAsync(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                var query = "SELECT Id, GivenName, Surname, Species, Origin, Year FROM Characters WHERE Id = @Id";

                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", id);

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        return new Character
                        {
                            Id = (int)reader["Id"],
                            GivenName = reader["GivenName"] as string,
                            Surname = reader["Surname"] as string,
                            Species = reader["Species"] as string,
                            Origin = reader["Origin"] as string,
                            Year = reader["Year"] as int?
                        };
                    }
                }

                return null;
            }
        }

        public async Task<IEnumerable<Character>> ReadAsync()
        {
            using (var command = _connection.CreateCommand())
            {
                var query = "SELECT Id, GivenName, Surname, Species, Origin, Year FROM Characters";

                command.CommandText = query;

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                var characters = new List<Character>();

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (reader.Read())
                    {
                        characters.Add(new Character
                        {
                            Id = (int)reader["Id"],
                            GivenName = reader["GivenName"] as string,
                            Surname = reader["Surname"] as string,
                            Species = reader["Species"] as string,
                            Origin = reader["Origin"] as string,
                            Year = reader["Year"] as int?
                        });
                    }
                }

                return characters;
            }
        }

        public async Task<bool> UpdateAsync(Character character)
        {
            using (var command = _connection.CreateCommand())
            {
                var query = @"UPDATE Characters SET 
                    GivenName = @GivenName, 
                    Surname = @Surname,
                    Species = @Species,
                    Origin = @Origin,
                    Year = @Year,
                    WHERE Id = @Id";

                command.CommandText = query;

                command.Parameters.AddWithValue("@Id", character.Id);
                command.Parameters.AddWithValue("@GivenName", character.GivenName);
                command.Parameters.AddWithValue("@Surname", character.Surname);
                command.Parameters.AddWithValue("@Species", character.Species as object ?? DBNull.Value);
                command.Parameters.AddWithValue("@Origin", character.Origin as object ?? DBNull.Value);
                command.Parameters.AddWithValue("@Year", character.Year as object ?? DBNull.Value);

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (var command = _connection.CreateCommand())
            {
                var query = "DELETE Characters WHERE Id = @Id";

                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", id);

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }

                return await command.ExecuteNonQueryAsync() > 0;
            }
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}