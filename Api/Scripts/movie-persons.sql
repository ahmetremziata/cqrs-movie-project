create table movie_types
(
    id       integer generated always as identity
        constraint movie_types_pkey
            primary key,
    movie_id integer not null,
    type_id  integer not null
);

INSERT INTO movie_types(movie_id, type_id) VALUES (4, 2);
INSERT INTO movie_types(movie_id, type_id) VALUES (4, 3);
INSERT INTO movie_types(movie_id, type_id) VALUES (4, 6);
INSERT INTO movie_types(movie_id, type_id) VALUES (5, 1);
INSERT INTO movie_types(movie_id, type_id) VALUES (5, 15);
INSERT INTO movie_types(movie_id, type_id) VALUES (5, 16);
INSERT INTO movie_types(movie_id, type_id) VALUES (6, 1);
INSERT INTO movie_types(movie_id, type_id) VALUES (6, 9);
INSERT INTO movie_types(movie_id, type_id) VALUES (7, 2);