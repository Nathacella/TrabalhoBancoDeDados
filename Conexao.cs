using MySql.Data.MySqlClient;

public class Conexao
{
    private static string conexaoString = "server=localhost;user=root;password=root;database=sistema_consumo";
    public static MySqlConnection ObterConexao()
    {
        return new MySqlConnection(conexaoString);
    }
}
