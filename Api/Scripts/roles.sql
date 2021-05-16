create table roles
(
    id         integer generated always as identity
        constraint roles_pkey
            primary key,
    name       varchar(250) not null,
    created_on date         not null
);

INSERT INTO roles(name, created_on) values ('Oyuncu', now());
INSERT INTO roles(name, created_on) values ('Yönetmen', now());
INSERT INTO roles(name, created_on) values ('Senaryo', now());
INSERT INTO roles(name, created_on) values ('Yapımcı', now());
INSERT INTO roles(name, created_on) values ('Görüntü Yönetmeni', now());
INSERT INTO roles(name, created_on) values ('Müzik', now());
INSERT INTO roles(name, created_on) values ('Eser', now());


