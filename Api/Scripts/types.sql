create table types
(
    id         integer generated always as identity
        constraint types_pkey
            primary key,
    name       varchar(250) not null,
    created_on date         not null
);