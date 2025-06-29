-- Criar banco
CREATE DATABASE IF NOT EXISTS sistema_consumo;
USE sistema_consumo;

-- Tabela cliente
CREATE TABLE cliente (
    id_cliente INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    cpf VARCHAR(14) NOT NULL,
    email VARCHAR(100)
);

-- Tabela cartao
CREATE TABLE cartao (
    id_cartao INT AUTO_INCREMENT PRIMARY KEY,
    id_cliente INT NOT NULL,
    saldo DECIMAL(10,2) DEFAULT 0,
    status VARCHAR(20) DEFAULT 'ativo',
    FOREIGN KEY (id_cliente) REFERENCES cliente(id_cliente)
);

-- Tabela funcionario
CREATE TABLE funcionario (
    id_funcionario INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL
);

-- Tabela produto
CREATE TABLE produto (
    id_produto INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    preco DECIMAL(10,2) NOT NULL
);

-- Tabela pedido
CREATE TABLE pedido (
    id_pedido INT AUTO_INCREMENT PRIMARY KEY,
    id_cartao INT NOT NULL,
    id_funcionario INT NOT NULL,
    data_pedido DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_cartao) REFERENCES cartao(id_cartao),
    FOREIGN KEY (id_funcionario) REFERENCES funcionario(id_funcionario)
);

-- Tabela associativa pedido_produto
CREATE TABLE pedido_produto (
    id_pedido INT,
    id_produto INT,
    quantidade INT DEFAULT 1,
    PRIMARY KEY (id_pedido, id_produto),
    FOREIGN KEY (id_pedido) REFERENCES pedido(id_pedido),
    FOREIGN KEY (id_produto) REFERENCES produto(id_produto)
);

-- Inserção de dados de exemplo
INSERT INTO cliente (nome, cpf, email) VALUES
('João da Silva', '123.456.789-00', 'joao@email.com'),
('Maria Oliveira', '987.654.321-00', 'maria@email.com');

INSERT INTO cartao (id_cliente, saldo, status) VALUES
(1, 100.00, 'ativo'),
(2, 50.00, 'ativo');

INSERT INTO funcionario (nome) VALUES
('Lucas Mendes'),
('Fernanda Costa');

INSERT INTO produto (nome, preco) VALUES
('Cerveja', 8.00),
('Batata Frita', 12.00),
('Hambúrguer', 18.00);