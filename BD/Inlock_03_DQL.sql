USE inlock_games_tarde;
GO

--Listar todos os usu�rios
SELECT * FROM USUARIO
--Listar todos os est�dios
SELECT * FROM ESTUDIO
--Listar todos os jogos
SELECT * FROM JOGO
--TipoUsuario
SELECT * FROM TIPOUSUARIO

--Listar todos os jogos e seus respectivos est�dios
SELECT idJogo,nomeJogo [Nome Jogo],descri��o,dataLancamento [Data de Lan�amento],valor,nomeEstudio[Nome Estudio] FROM JOGO J
INNER JOIN ESTUDIO E ON J.idEstudio = E.idEstudio
GO

--Buscar e trazer na lista todos os est�dios com os respectivos jogos. Obs.: Listar
--todos os est�dios mesmo que eles n�o contenham nenhum jogo de refer�ncia
SELECT E.idEstudio,nomeEstudio[Nome Estudio],nomeJogo [Nome Jogo],descri��o,dataLancamento [Data de Lan�amento],valor FROM ESTUDIO E
LEFT JOIN JOGO J ON E.idEstudio = J.idEstudio
GO

--Buscar um usu�rio por e-mail e senha (login)
SELECT * FROM USUARIO
WHERE email = 'cliente@cliente.com'
AND SENHA = 'cliente'
GO

--Buscar um jogo por idJogo
SELECT * FROM JOGO
WHERE idJogo = 2
GO

--Buscar um est�dio por idEstudio
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
