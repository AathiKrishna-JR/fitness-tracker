using Microsoft.Data.SqlClient;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;



namespace EmployeeAPI.BuisnessLogic
{
    public class FitnessUserService : IFitnessUser
    {
        private readonly string _connectionString;

        public FitnessUserService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Insert Data into FitnessUsers
        public void AddFitnessUser(FitnessUser user)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = @"INSERT INTO A.AAFitnessUsers ( Email, PasswordHash, Birthdate, CreatedOn)
                                 VALUES ( @Email, @PasswordHash, @Birthdate, @CreatedOn)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    cmd.Parameters.AddWithValue("@Birthdate", user.Birthdate);
                    cmd.Parameters.AddWithValue("@CreatedOn", user.CreatedOn ?? (object)DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<FitnessUser> GetFitnessUsers()
        {
            var users = new List<FitnessUser>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Email, PasswordHash, Birthdate, CreatedOn FROM A.AAFitnessUsers";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new FitnessUser
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Email = reader["Email"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Birthdate = Convert.ToDateTime(reader["Birthdate"]),
                                CreatedOn = reader["CreatedOn"] == DBNull.Value
                                             ? null
                                             : Convert.ToDateTime(reader["CreatedOn"])
                            });
                        }
                    }
                }
            }

            return users;
        }
        public bool AuthenticateUser(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM A.AAFitnessUsers WHERE Email = @Email AND PasswordHash = @Password";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    connection.Open();
                    int userCount = (int)cmd.ExecuteScalar();

                    return userCount > 0;
                }
            }
        } 

        public List<Exercise> GetExercises()
        {
            var exercises = new List<Exercise>();

            using (var connection = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, Name, Duration, Calories FROM A.AExercises";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            exercises.Add(new Exercise
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Name = reader["Name"].ToString(),
                                Duration = Convert.ToInt32(reader["Duration"]),
                                Calories = Convert.ToInt32(reader["Calories"]),
                               
                            });
                        }
                    }
                }
            }

            return exercises;
        }

        public List<UserExercise> GetUserExercises(int fitnessUserId)
        {
            List<UserExercise> exercises = new List<UserExercise>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("A.GetUserExercises", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FitnessUserId", fitnessUserId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserExercise exercise = new UserExercise
                            {
                                Id = reader.GetInt32(0),
                                Date = reader.GetDateTime(1),
                                UserName = reader.GetString(2),
                                ExerciseName = reader.GetString(3),
                                Duration = reader.GetInt32(4),
                                CaloriesBurned = reader.GetInt32(5),
                                Status = reader.GetString(6)
                            };
                            exercises.Add(exercise);
                        }
                    }
                }
            }

            return exercises;
        }

    }
}
