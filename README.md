# cqrs-movie-api
An api example for cqrs and event sourcing

## Dependencies
- Postgre (Datasource for command)
- Kafka (For event sourcing)
- ElasticSearch (Nosql for query)

## Usage
First of all you should standup below dependencies;
- kafka in localhost:9092 (Download local or run with docker)
- postgre with following configs
```yml
  "ConnectionStrings": {
    "MovieConnection": "User ID=postgres;Password=movie;Server=localhost;Port=5432;Database=movie;Integrated Security=true;Pooling=true;"
  }
```
- elasticsearch in localhost:9200

## Docker
You can open dependencies with docker

It is an example for running postgres with docker
Create Docker container with Postgres database:
```yml
  docker create --name postgres-movie -e POSTGRES_PASSWORD=movie -p 5432:5432 postgres:11.5-alpine
```

Start container:
```yml
  docker start postgres-movie
```

Stop container:
```yml
  docker stop postgres-movie
```

Note: This stores the data inside the container - when you delete the container, the data is deleted as well.

Connect to PSQL prompt from docker: 
```yml
  docker exec -it postgres-movie psql -U postgres
```

Create the Database with psql command:
```yml
psql> create database movie;
```

After you initialize db, you can run queries in table_initialize.sql. So far movies tables will be initialized with default values

## ElasticSearch
To get data from elastic search first you run elastic on local. These link show you how you can run elastic on locak
https://opensource.com/article/19/7/installing-elasticsearch-macos
Note: You can also load kibana on your local to operate elastic processes
For details; https://codingexplained.com/dev-ops/mac/installing-kibana-for-elasticsearch-on-os-x


Finally run application and test command and queries endpoints

