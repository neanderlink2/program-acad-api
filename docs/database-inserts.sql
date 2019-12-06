INSERT INTO TB_LINGUAGEM_PROGRAMACAO(id, nome, descricao, num_tipo_compilador)
	VALUES (1, 'C#', 'C# é uma linguagem de programação, multiparadigma, de tipagem forte, desenvolvida pela Microsoft como parte da plataforma .NET.', 2), 
		   (2, 'Python', 'Python é uma linguagem de programação de alto nível,[4] interpretada, de script, imperativa, orientada a objetos, funcional, de tipagem dinâmica e forte.', 3), 
		   (3, 'C', 'C é uma linguagem de programação compilada de propósito geral, estruturada, imperativa, procedural e padronizada por Organização Internacional para Padronização (ISO).', 2), 
		   (4, 'Java', 'Java é uma linguagem de programação orientada a objetos desenvolvida na década de 90 por uma equipe de programadores chefiada por James Gosling, na empresa Sun Microsystems.', 3)

INSERT INTO TB_NIVEL_DIFICULDADE(id, nivel, descricao, pontos_a_receber)
	VALUES (1, 1, 'Muito fácil', 5),
		   (2, 2, 'Fácil', 15),
		   (3, 3, 'Médio', 40),
		   (4, 4, 'Difícil', 70),
		   (5, 5, 'Muito difícil', 150)
