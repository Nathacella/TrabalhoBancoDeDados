using MySql.Data.MySqlClient;

public class Conexao
{
    private static string conexaoString = "server=localhost;user=root;password=nathi23117878;database=sistema_consumo";
    public static MySqlConnection ObterConexao()
    {
        return new MySqlConnection(conexaoString);
    }
}