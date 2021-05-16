create table movie_countries
(
    id       integer generated always as identity
        constraint movie_countries_pkey
            primary key,
    movie_id integer not null,
    country_id  integer not null
);

INSERT INTO movie_countries(movie_id, country_id) VALUES (4, 2);
INSERT INTO movie_countries(movie_id, country_id) VALUES (5, 2);
INSERT INTO movie_countries(movie_id, country_id) VALUES (6, 1);
INSERT INTO movie_countries(movie_id, country_id) VALUES (7, 2);