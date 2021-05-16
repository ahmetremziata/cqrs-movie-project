create table persons
(
    id                integer generated always as identity
        constraint persons_pkey
            primary key,
    name            varchar(255)   not null,
    surname         varchar(255)   not null,
    biography       varchar(2000)  null,
    avatar_url      varchar(255)   null,
    birthdate        date          null,
    deathdate        date          null,
    birthcity        varchar(255)  null,
    deathcity        varchar(255)  null
);