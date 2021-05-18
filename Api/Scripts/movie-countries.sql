create table movie_countries
(
    id       integer generated always as identity
        constraint movie_countries_pkey
            primary key,
    movie_id integer not null,
    country_id  integer not null
);