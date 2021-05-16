create table countries
(
    id         integer generated always as identity
        constraint countries_pkey
            primary key,
    name       varchar(250) not null,
    created_on date         not null
);

INSERT INTO countries(name, created_on) VALUES ('Türkiye', now());
INSERT INTO countries(name, created_on) VALUES ('ABD', now());
INSERT INTO countries(name, created_on) VALUES ('Fransa', now());
INSERT INTO countries(name, created_on) VALUES ('Almanya', now());