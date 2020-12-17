# LogManager
WebApi log services

Projeto back-end WebApi que disponibiliza endpoints para inclusão, atualização, deleção, obtenção e listagem de informações de arquivo de log.
Adicionalmente foi incluído também um endpoint para importar os dados de um arquivo .TXT.

A aplicação foi implementada utilizando injeção de dependência, swagger, auto mapper, repository e migrations para na camada de acesso a dados, herança e alguns dos princípios de SOLID.

Para compilação e publicação é necessário a execução do comando "update-database" no Package Manager Console apontando para o projeto LogManager.Data afim de criar banco de dados e suas respectivas tabelas.

Pode ser necessário ajustar a connection string LogManagerBD no arquivo appsettings.json.
