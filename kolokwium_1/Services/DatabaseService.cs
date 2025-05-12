namespace kolokwium_1.Services;

public class Database : IDatabaseService
{
    // Implement methods from IDatabaseService
    
    /* Database stuff
     * 
     * Connection to the database
     * using connection = new SqlConnection(connection_string);
     *
     * Open connection
     * connection.Open();
     *
     * Create SQL command
     * using var command = new SqlCommand();
     *
     * Required properties
     * Connection to the database
     * command.Connection = connection;
     *
     * SQL command with parameter(s) - parameter is marked with @
     * command.CommandText = "SELECT * FROM users WHERE user_id = @user_id";
     * command.Parameters.AddWithValue("user_id", 1234);
     *
     * Result of command
     * var data_reader = command.ExecuteReader();
     *
     * Get data from data reader
     * string username = data_reader["username"];
     * int age = data_reader["age"];
     * 
     * To open transaction
     * var transaction = connection.BeginTransaction();
     *
     * To close transaction
     * transaction.Rollback();
     *
     * To commit transaction
     * transaction.Commit();
     */
}

