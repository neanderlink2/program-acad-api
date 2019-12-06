CREATE TABLE TB_LINGUAGEM_PROGRAMACAO
(
	id INT PRIMARY KEY,
	nome VARCHAR(20) NOT NULL,
	descricao VARCHAR(200) NOT NULL,
	num_tipo_compilador INT NOT NULL
)

CREATE TABLE TB_NIVEL_DIFICULDADE
(
	id INT PRIMARY KEY,
	nivel INT NOT NULL,
	descricao VARCHAR(50) NOT NULL,
	pontos_a_receber INT NOT NULL
)

CREATE TABLE TB_USUARIO
(
	id UNIQUEIDENTIFIER PRIMARY KEY,
	nome_completo VARCHAR(256) NOT NULL,
	nickname VARCHAR(32) NOT NULL UNIQUE,
	cpf VARCHAR(15) NOT NULL UNIQUE,
	cep VARCHAR(12) NULL,
	email VARCHAR(256) NOT NULL UNIQUE,
	sexo CHAR(1) NOT NULL,
	role VARCHAR(50) NOT NULL DEFAULT 'ESTUDANTE'
)

CREATE TABLE TB_TURMA
(
	id UNIQUEIDENTIFIER PRIMARY KEY,
	id_instrutor UNIQUEIDENTIFIER FOREIGN KEY REFERENCES TB_USUARIO(id),
	nome VARCHAR(75) NOT NULL,
	capacidade_alunos INT NOT NULL,
	url_imagem_turma VARCHAR(500) NOT NULL,
	data_criacao DATETIME NOT NULL DEFAULT GETDATE()
)

CREATE TABLE TB_ALGORITMO
(
	id UNIQUEIDENTIFIER PRIMARY KEY,
	id_nivel_dificuldade INT NOT NULL FOREIGN KEY REFERENCES TB_NIVEL_DIFICULDADE(id),
	id_turma UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_TURMA(id),
	titulo VARCHAR(100) NOT NULL,
	html_descricao VARCHAR(MAX) NOT NULL,
	data_criacao DATETIME NOT NULL	
)

CREATE TABLE TB_CASO_TESTE
(
	id UNIQUEIDENTIFIER PRIMARY KEY,
	entrada_esperada VARCHAR(100) NOT NULL,
	saida_esperada VARCHAR(100) NOT NULL,
	tempo_maximo_execucao INT NOT NULL,
	id_algoritmo UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_ALGORITMO(id)
)

CREATE TABLE TB_ALGORITMO_LINGUAGEM_DISPONIVEL
(
	id_algoritmo UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_ALGORITMO(id),
	id_linguagem_programacao INT NOT NULL FOREIGN KEY REFERENCES TB_LINGUAGEM_PROGRAMACAO(id),
	CONSTRAINT PK_ALGORITMO_LINGUAGEM PRIMARY KEY (id_algoritmo, id_linguagem_programacao)
)

CREATE TABLE TB_TESTE_PRIMEIRA_EXECUCAO
(
	id_caso_teste UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_CASO_TESTE(id),
	id_usuario UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_USUARIO(id),
	id_linguagem_programacao INT NOT NULL FOREIGN KEY REFERENCES TB_LINGUAGEM_PROGRAMACAO(id),
	sucesso BIT NOT NULL,
	tempo_execucao INT NOT NULL,
	CONSTRAINT PK_TESTE_EXECUCAO PRIMARY KEY (id_caso_teste, id_usuario)
) 

CREATE TABLE TB_USUARIO_RESOLVE_ALGORITMO
(
	id_usuario UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_USUARIO(id),
	id_algoritmo UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_ALGORITMO(id),
	data_conclusao DATETIME NOT NULL,
	id_linguagem_usada INT NOT NULL FOREIGN KEY REFERENCES TB_LINGUAGEM_PROGRAMACAO(id),
	CONSTRAINT PK_USUARIO_RESOLVE PRIMARY KEY(id_usuario, id_algoritmo)
)

CREATE TABLE TB_USUARIO_PERTENCE_TURMA
(
	id_usuario UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_USUARIO(id),
	id_turma UNIQUEIDENTIFIER NOT NULL FOREIGN KEY REFERENCES TB_TURMA(id),
	pontos_usuario INT NOT NULL,
	data_ingresso DATETIME NOT NULL,
	aceito BIT NOT NULL,
	CONSTRAINT PK_USUARIO_TURMA PRIMARY KEY (id_usuario, id_turma)
)
