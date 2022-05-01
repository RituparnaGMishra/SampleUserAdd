using System.Data;
using System.Data.SqlClient;
using SampleUserAdd.Models;

namespace SampleUserAdd.Services
{
    public interface IDbService
    {
        void AddUser(UserEntityModel user);
        int IsExistingEmail(string emailId);
    }
    public class DbService : IDbService
    {
        private readonly SqlConnection _dbConnection;
        private readonly string _tableName;
        private readonly IConfiguration _configuration;
        public DbService(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration["ConnectionString"];
            _dbConnection = new SqlConnection(connectionString);
            _tableName = _configuration["TableName"];


        }
        public void AddUser(UserEntityModel user)
        {
            try
            {

                SqlCommand cmd = new SqlCommand("spAddUser", _dbConnection);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Password", user.Password);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Id", user.Id);
                cmd.Parameters.AddWithValue("@CreatedOn", user.CreatedOn);
                _dbConnection.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Exception while inserting data: {ex}");
            }
            finally
            {
                _dbConnection.Close();

            }

        }


        public int IsExistingEmail(string emailId)
        {
            int userCount = 0;
            try
            {

                var getTableQuery = $"SELECT Count(*) FROM {_tableName} WHERE EMAIL = '{emailId}'";
                SqlCommand cmd = new(getTableQuery, _dbConnection);

                _dbConnection.Open();
                var data = cmd.ExecuteScalar();
                if (data != null)
                {
                    userCount = (int)data;
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Exception while retrieving data: {ex}");
            }
            finally
            {
                _dbConnection.Close();
            }
            return userCount;
        }
    }
}
