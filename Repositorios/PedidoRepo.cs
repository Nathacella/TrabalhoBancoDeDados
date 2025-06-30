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

    // 1. Obter preço do produto
    var cmdPreco = new MySqlCommand("SELECT preco FROM produto WHERE id_produto = @id_produto", conexao);
    cmdPreco.Parameters.AddWithValue("@id_produto", idProduto);
    decimal precoProduto = Convert.ToDecimal(cmdPreco.ExecuteScalar());

    decimal total = precoProduto * quantidade;

    // 2. Obter o cartão associado ao pedido
    var cmdCartao = new MySqlCommand("SELECT id_cartao FROM pedido WHERE id_pedido = @id_pedido", conexao);
    cmdCartao.Parameters.AddWithValue("@id_pedido", idPedido);
    int idCartao = Convert.ToInt32(cmdCartao.ExecuteScalar());

    // 3. Verificar saldo do cartão
    var cmdSaldo = new MySqlCommand("SELECT saldo FROM cartao WHERE id_cartao = @id_cartao", conexao);
    cmdSaldo.Parameters.AddWithValue("@id_cartao", idCartao);
    decimal saldoAtual = Convert.ToDecimal(cmdSaldo.ExecuteScalar());

    if (saldoAtual < total)
    {
        Console.WriteLine("Saldo insuficiente para realizar a compra.");
        return;
    }

    // 4. Registrar o produto no pedido
    var cmdInsert = new MySqlCommand("INSERT INTO pedido_produto (id_pedido, id_produto, quantidade) VALUES (@id_pedido, @id_produto, @quantidade)", conexao);
    cmdInsert.Parameters.AddWithValue("@id_pedido", idPedido);
    cmdInsert.Parameters.AddWithValue("@id_produto", idProduto);
    cmdInsert.Parameters.AddWithValue("@quantidade", quantidade);
    cmdInsert.ExecuteNonQuery();

    // 5. Atualizar saldo do cartão
    var cmdUpdateSaldo = new MySqlCommand("UPDATE cartao SET saldo = saldo - @total WHERE id_cartao = @id_cartao", conexao);
    cmdUpdateSaldo.Parameters.AddWithValue("@total", total);
    cmdUpdateSaldo.Parameters.AddWithValue("@id_cartao", idCartao);
    cmdUpdateSaldo.ExecuteNonQuery();

    Console.WriteLine($"Produto adicionado ao pedido. Valor de R$ {total:0.00} descontado do cartão (novo saldo: R$ {saldoAtual - total:0.00}).");
}

    public void Listar()
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();

        var cmd = new MySqlCommand(@"
        SELECT 
            p.id_pedido,
            p.id_cartao,
            p.id_funcionario,
            p.data_pedido,
            pr.nome AS nome_produto,
            pp.quantidade
        FROM pedido p
        JOIN pedido_produto pp ON p.id_pedido = pp.id_pedido
        JOIN produto pr ON pp.id_produto = pr.id_produto
        ORDER BY p.id_pedido, pr.nome", conexao);

        using var reader = cmd.ExecuteReader();

        int ultimoPedido = -1;
        while (reader.Read())
        {
            int idPedido = Convert.ToInt32(reader["id_pedido"]);

            // Cabeçalho do pedido
            if (idPedido != ultimoPedido)
            {
                Console.WriteLine($"\nPedido ID: {idPedido} | Cartão: {reader["id_cartao"]} | Funcionário: {reader["id_funcionario"]} | Data: {reader["data_pedido"]}");
                Console.WriteLine("Produtos:");
                ultimoPedido = idPedido;
            }

            // Produto associado ao pedido
            Console.WriteLine($" - {reader["nome_produto"]} x{reader["quantidade"]}");
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