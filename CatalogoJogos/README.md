# Catálogo de Jogos – Web API .NET 10 com JWT e Scalar

Baseado no projeto do professor (ApplicationJwtScalar2TDSPW).

## Como rodar
Dentro da pasta que contém o `CatalogoJogos.csproj`:
```bash
dotnet restore
dotnet run
```
Abra: http://localhost:5062/scalar

No VS Code: abra esta pasta (File > Open Folder) e pressione **F5**.

## Como testar no Scalar
1. `POST /api/auth/login` com `{ "username": "Mauricio", "password": "123456" }`
2. Copie o valor de `token` da resposta.
3. Clique em **Authentication** (Bearer) e cole o token.
4. Teste as rotas de `/api/jogos`. Sem token elas retornam **401**.

## Endpoints
| Método | Rota | Retorno |
|---|---|---|
| POST | /api/auth/login | 200 (token) / 401 |
| POST | /api/jogos | 201 |
| GET | /api/jogos | 200 |
| GET | /api/jogos/{id} | 200 / 404 |
| PUT | /api/jogos/{id} | 200 / 404 |
| DELETE | /api/jogos/{id} | 204 / 404 |
| GET | /api/jogos/busca?plataforma=&precoMaximo= | 200 |
| GET | /api/jogos/relatorio-estoque | 200 |

## Estrutura
Controller > JogoService > IJogoRepository (em memória, Singleton).
