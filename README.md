# cqrs-movie-api
This project provides a simple movie api using cqrs and event sourcing. With this api, you can create new movie and new actor, update a movie or actor and delete movie or actor and moreover. It is written with c# programming language and .net5 technology.

## Dependencies
- Postgre (Datasource for command or query)
- Kafka (For event sourcing)
- ElasticSearch (Nosql for query)

## Usage
First of all you should standup below dependencies;
- kafka in localhost:9092 (Download local or run with docker)
- postgre with following configs (Download local or run with docker)
```yml
  "ConnectionStrings": {
    "MovieConnection": "User ID=postgres;Server=localhost;Port=5432;Database=movie;Integrated Security=true;Pooling=true;"
  }
```
- elasticsearch in localhost:9200 (Download local or run with docker)


## ElasticSearch
To get data from elastic search first you run elastic on local. These link show you how you can run elastic on locak
https://opensource.com/article/19/7/installing-elasticsearch-macos
Note: You can also load kibana on your local to operate elastic processes
For details; https://codingexplained.com/dev-ops/mac/installing-kibana-for-elasticsearch-on-os-x


Finally run application and test command and queries endpoints

