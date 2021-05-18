create table movie_types
(
    id       integer generated always as identity
        constraint movie_types_pkey
            primary key,
    movie_id integer not null,
    type_id  integer not null
);