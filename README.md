# cqrs-movie-project
An api example for cqrs and event sourcing

## Dependencies
- Postgre (Datasource for command)
- Kafka (For event sourcing)
- ElasticSearch (Nosql for query)

## Usage
First of all you should standup below dependencies;
- kafka in localhost:9092 
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

Then run application and test command and queries endpoints

