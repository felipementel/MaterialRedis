# RedisNET5

Comandos 
PM> Add-Migration Initial --p Aplicacao.Infra.DataAccess -c AplicacaoContext
PM> Update-Database --p Aplicacao.Infra.DataAccess -c AplicacaoContext
PM> Script-Migration -o .\Aplicacao\Initial.SQL
PM> Script-Migration -o .\Aplicacao\Initial2.SQL -idempotent
PM> Remove-Migration


========Montar ambiente Docker (server) ========
> docker ps -a | lista as imagens

> docker pull redis

> docker run --name redis-server-docker -p 6379:6379 redis 

========Montar ambiente Docker (cli) ========
> Open new Terminal

> docker exec -it redis-server-docker redis-cli

========Comandos Redis ========

> ping

## String --> https://redis.io/commands/append
# set e setex

> set <key> <value>
> set <key> <value> EX 10s
> get <key>
> append <key> <value>

> expire <key> <tempo>

> ttl <key>

# saber o endereço de memoria
> dump <key>

> mset <key> <value> <key> <value> | set em varias chaves e seus valores
> mget <key> <key> <key> ... | obtem os valores das chaves solicitadas

##namespace
> set <key>:<id> <value>

# substring
> getrange <key> <start> <stop>

#incrementar em caso da chave tiver um valor inteiro
> incr <key>

 # para incrementar um inteiro a partir de outro valor
 > incrby <key> <value>


## Listas --> https://redis.io/commands/rpush

(pilha e fila) ou lista
> rpush <key> v1 v2 v3 | adiciona os valores a direita
> lpush <key> v1 v2 v3 | adiciona os valores a esquerda

> pop | retira da fila pelo indice

>lrange <key> | tamnho da pilha

>lpop <key>
>rpop <key>

> llen

## hash --> https://redis.io/commands/hmset * a mais utilizada

> hset <key1> <field1> <value>
> hset <key1> <field2> <value>
> hkeys
> hgetall <key>


> hget Aplicacao-ClienteRedisRepository:1 data
> hget Aplicacao-BasketRepository:123 data
======

# deletar uma chave
> del <key>

# listar todas as chaves
> keys *

# listar chaves que contem um caracter
> keys *caracter* | * = infinito

# obter o tipo de uma chave
> type <key>


====
Os sets em Redis representam conjuntos de valores que não se repetem. Ao fazer uma adição de um elemento já existe em um set, o comando não faz nada com o set.

O comando de escrita em set é:

“SADD chave valor”: tenta adicionar o valor dado à chave dada.
A leitura pode ser feita com os comandos:

“SMEMBERS chave”: retorna todo o set.
“SISMEMBER chave valor”: retorna se o valor dado está ou não no set.
Ainda é possível remover um elemento com:

“SREM chave valor”: tenta remover o valor dado do set.

redis> SADD myset "Hello"
(integer) 1
redis> SADD myset "World"
(integer) 1
redis> SADD myset "World"
(integer) 0
redis> SMEMBERS myset
1) "World"
2) "Hello"
redis> 
====

#deletar todas as chaves
> flushall