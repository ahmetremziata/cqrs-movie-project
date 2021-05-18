create table movie_persons
(
    id        integer generated always as identity
        constraint movie_persons_pkey
            primary key,
    movie_id  integer not null,
    person_id integer not null,
    character_name varchar(255) null,
    role_id   integer not null
);