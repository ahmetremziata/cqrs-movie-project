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
    "MovieConnection": "User ID=postgre;Password=postgre;Server=localhost;Port=5432;Database=postgres;Integrated Security=true;Pooling=true;"
  }
```
- elasticsearch in localhost:9200

After you initialize db, you can run queries in table_initialize.sql. So far movies tables will be initialized with default values

Then run application and test command and queries endpoints

