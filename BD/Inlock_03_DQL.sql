USE inlock_games_tarde;
GO

--Listar todos os usuários
SELECT * FROM USUARIO
--Listar todos os estúdios
SELECT * FROM ESTUDIO
--Listar todos os jogos
SELECT * FROM JOGO
--TipoUsuario
SELECT * FROM TIPOUSUARIO

--Listar todos os jogos e seus respectivos estúdios
SELECT idJogo,nomeJogo [Nome Jogo],descrição,dataLancamento [Data de Lançamento],valor,nomeEstudio[Nome Estudio] FROM JOGO J
INNER JOIN ESTUDIO E ON J.idEstudio = E.idEstudio
GO

--Buscar e trazer na lista todos os estúdios com os respectivos jogos. Obs.: Listar
--todos os estúdios mesmo que eles não contenham nenhum jogo de referência
SELECT E.idEstudio,nomeEstudio[Nome Estudio],nomeJogo [Nome Jogo],descrição,dataLancamento [Data de Lançamento],valor FROM ESTUDIO E
LEFT JOIN JOGO J ON E.idEstudio = J.idEstudio
GO

--Buscar um usuário por e-mail e senha (login)
SELECT * FROM USUARIO
WHERE email = 'cliente@cliente.com'
AND SENHA = 'cliente'
GO

--Buscar um jogo por idJogo
SELECT * FROM JOGO
WHERE idJogo = 2
GO

--Buscar um estúdio por idEstudio
SELECT * FROM ESTUDIO
WHERE idEstudio = 3
GO


SELECT	idJogo,
		nomeJogo, 
		descricao, 
		dataLancamento, 
		valor, 
		J.idEstudio, 
		nomeEstudio 
FROM JOGO J
LEFT JOIN ESTUDIO E
ON J.idEstudio = E.idEstudio
