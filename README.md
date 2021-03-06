# cqrs-movie-api
This project provides a simple movie api using cqrs and event sourcing. With this api, you can create new movie and new actor, update a movie or actor and delete movie or actor and moreover. It is written with c# programming language and .net5 technology.

## Dependencies
- Postgre (Datasource for command or query)
- Kafka (For event sourcing)
- ElasticSearch (Nosql for query)

## Installation
Postgresql:
To install postgres on your local, i advice https://postgresapp.com/ link, it downloads packages for postgre installation and stand up server for postgres

Elastic:
To get data from elastic search first you run elastic on local. These link show you how you can run elastic on locak
https://opensource.com/article/19/7/installing-elasticsearch-macos

Note: You can also load kibana on your local to operate elastic processes
For details; https://codingexplained.com/dev-ops/mac/installing-kibana-for-elasticsearch-on-os-x

## Usage
First of all you should standup below dependencies;
- kafka in localhost:9092 (Download local or run with docker)
- postgre with following configs (Download local or run with docker)
```yml
  "ConnectionStrings": {
    "MovieConnection": "User ID=postgres;Password=postgres;Server=localhost;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;"
  }
```
- elasticsearch in localhost:9200 (Download local or run with docker)

After dependencies are active, then for sample data you can run table_initialize.sql file on local postgres database.

## Run on local
You can run up api with your favorite ide (i advice jetbrains) after dependencies has stand up.
After running project, you can try with http://localhost:8080/index.html swagger link or you can try postman or insomnia



