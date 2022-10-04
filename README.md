# Clima-Tempo 

Caso exista Erro de compilação excluir pasta bin do projeto e rebuild para criar uma nova bin na maquina de teste.

É necessário rodar os scripts na pasta scriptBanco para criar o banco e tabelas.
Precisa configurar a conectionString com o nome do servidor Sql, localizado na Web.congif linha 17, campo source=SeuServerName -- colocar nesse ponto seu local host
Quando a aplicação é iniciada pela primeira vez ela demora executar pois insere reistros referentes a todos o estados do brasil e cidades adicionando previsões aleatorias limitando se a 100 cidades para não demorar muito.
Quando carregar a pagina deverá demonstar as menores e maiores temperaturas de 3 cidades disponíveis no banco de dados ao clicar em uma cidade deverá demonstar a previsão dos proxímos 7 dias.
