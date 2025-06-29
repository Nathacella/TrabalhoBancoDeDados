using MySql.Data.MySqlClient;
using System;

public class PedidoRepo
{
    public int Criar(int idCartao, int idFuncionario)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("INSERT INTO pedido (id_cartao, id_funcionario) VALUES (@id_cartao, @id_funcionario)", conexao);
        cmd.Parameters.AddWithValue("@id_cartao", idCartao);
        cmd.Parameters.AddWithValue("@id_funcionario", idFuncionario);
        cmd.ExecuteNonQuery();
        return (int)cmd.LastInsertedId;
    }

    public void AdicionarProduto(int idPedido, int idProduto, int quantidade)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("INSERT INTO pedido_produto (id_pedido, id_produto, quantidade) VALUES (@id_pedido, @id_produto, @quantidade)", conexao);
        cmd.Parameters.AddWithValue("@id_pedido", idPedido);
        cmd.Parameters.AddWithValue("@id_produto", idProduto);
        cmd.Parameters.AddWithValue("@quantidade", quantidade);
        cmd.ExecuteNonQuery();
    }

    public void Listar()
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("SELECT * FROM pedido", conexao);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Pedido ID: {reader["id_pedido"]} | Cartão: {reader["id_cartao"]} | Funcionário: {reader["id_funcionario"]} | Data: {reader["data_pedido"]}");
        }
    }

    public void Deletar(int idPedido)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd1 = new MySqlCommand("DELETE FROM pedido_produto WHERE id_pedido=@id", conexao);
        cmd1.Parameters.AddWithValue("@id", idPedido);
        cmd1.ExecuteNonQuery();

        var cmd2 = new MySqlCommand("DELETE FROM pedido WHERE id_pedido=@id", conexao);
        cmd2.Parameters.AddWithValue("@id", idPedido);
        cmd2.ExecuteNonQuery();
    }
}