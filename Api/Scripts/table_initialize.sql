/* These script is about drop tables if you want refresh initial data
drop table movies;
drop table persons;
drop table roles;
drop table types;
drop table countries;
drop table movie_persons;
drop table movie_types;
drop table movie_countries;
*/

--Initialize movies table
create table movies
(
    id                integer generated always as identity
        constraint movies_pkey
            primary key,
    name              varchar(250)   not null,
    original_name     varchar(250),
    description       varchar(10000),
    construction_year integer        not null,
    total_minute      integer        not null,
    poster_url        varchar(250),
    vision_entry_date date,
    created_on        date           not null,
    is_active         boolean        not null,
    is_synchronized   boolean        not null
);

--Initialize persons table
create table persons
(
    id          integer generated always as identity
        constraint persons_pkey
            primary key,
    name        varchar(255) not null,
    real_name   varchar(255),
    biography   varchar(2000),
    avatar_url  varchar(255),
    birth_date  varchar(100),
    death_date  varchar(100),
    birth_place varchar(255),
    death_place varchar(255),
    created_on  date         not null
);

--Initialize roles table
create table roles
(
    id         integer generated always as identity
        constraint roles_pkey
            primary key,
    name       varchar(250) not null,
    created_on date         not null
);

--Initialize types table
create table types
(
    id         integer generated always as identity
        constraint types_pkey
            primary key,
    name       varchar(250) not null,
    created_on date         not null
);

--Initialize countries table
create table countries
(
    id         integer generated always as identity
        constraint countries_pkey
            primary key,
    name       varchar(250) not null,
    created_on date         not null
);

--Initialize movie_persons table
create table movie_persons
(
    id             integer generated always as identity
        constraint movie_persons_pkey
            primary key,
    movie_id       integer not null,
    person_id      integer not null,
    character_name varchar(255),
    role_id        integer not null
);

--Initialize movie_types table
create table movie_types
(
    id       integer generated always as identity
        constraint movie_types_pkey
            primary key,
    movie_id integer not null,
    type_id  integer not null
);

--Initialize movie_countries table
create table movie_countries
(
    id         integer generated always as identity
        constraint movie_countries_pkey
            primary key,
    movie_id   integer not null,
    country_id integer not null
);

--Insert movies
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('Gelece??e D??n????', 'Back to the Future', 'Kolej ????rencisi Marty hayat??ndan s??k??lm????t??r. Ailesiyle ve ????retmenleriyle anla??amayan Marty, sadece k??z arkada???? Jennifer ve ayk??r?? bilim adam?? Brown'' in yan??nda kendine g??venini ve heyecan??n?? geri kazanmaktad??r. Dr. Brown, kendisinden n??kleer silah yapmas??n?? isteyen gizli bir ??rg??tten para alarak, zaman makinesini icat etmek i??in ??al????malara ba??lar. Amac??na ula??an Dr. Brown, ter??ristler taraf??ndan ??ld??r??l??r. Olaya ??ahit olan Marty, ka??maya ??al??????rken kendini zaman yolculu??unda bulur. Ancak, geri d??n???? konusunda ciddi bi engel vard??r. 88 mil/sa h??za ula??mak zorundad??r.', 1985, 116, 'movies/back_to_the_future.jpg', null, now(), false, true);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('Gelece??e D??n???? 2', 'Back to the Future Part II',
        'Marty ve doktor bu kez 2015 y??l??na giderek gelecekte Marty'' nin ailesinin kar????laca???? problemleri ????zmeye ??al??????rlar. Ancak eve geri d??nd??klerinde birisinin zaman makinesini kurcalad??????n?? ve gelece??i kar????t??rd??????n?? farkederler. ??imdi yapmalar?? gereken tek ??ey 1955'' e geri d??n??p gelece??i kurtarmakt??r.', 1989, 108, 'movies/back_to_the_future_2.jpg', null, now(), false, true);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('Gelece??e D??n???? 3', 'Back to the Future Part III',
        'Hi?? g??r??lmemi?? bir ??im??ek patlamas??ndan sonra 1955 y??l??nda mahsur kalan Marty, doktoru vakitsiz bir sondan kurtarmak i??in 1885 y??l??na geri d??nmelidir. K??z??lderillilerin sald??r??s??ndan ve kasaba halk??n??n d????manca tutumundan sonra hayatta kalan Marty, doktoru bulur. Ancak doktorun ??ekici Clara Clayton'' ??n b??y??s??n??n etkisi alt??nda oldu??u d??????n??l??rse, onlar??n vah??i bat??dan uzakla??t??rmak ve gelece??e d??nd??rmek Marty'' ye ba??l??d??r.', 1990, 118, 'movies/back_to_the_future_3.jpg', null, now(), false, true);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('Y??z??klerin Efendisi:Y??z??k Karde??li??i', 'The Lord of the Rings: The Fellowship of the Ring', 'Bir grup kahraman??n d??nyalar??n?? ??eytani g????ler birli??inden kurtarma m??cadeleleri...
Hobbit???ler, c??celer ve halk, ??eytani g????le sava??mak i??in bir araya geliyorlar.
Elijah Wood, Ian McKellen, Liv Tyler, Cate Blanchett, Viggo Mortensen gibi bir??ok ??nl?? oyuncunun rol ald?????? filmi fantastik i??lerinden tan??d??????m??z Peter Jackson y??netiyor. ???The Lord of the Rings???, J.R.R. Tolkien???in d??nya ??ap??nda b??y??k ilgi g??ren roman serisinden sinemaya uyarlanm????.
Gelecek ??a??lar ??nce kaybolmu?? bir y??z??????n kaderinde gizlidir. Karanl??k g????ler y??z????e ula??mak i??in y??llarca ??abalam????lar ve hala da bu u??ra??lar?? devam etmektedir. Kader y??z?????? gen?? bir Hobbit olan Frodo???nun ellerine teslim eder. Y??z??k Frodo???ya miras kal??r ve olaylara davetiye b??ylece c??km???? olur.', 2001, 171, 'movies/lord_of_rings_1.jpg', null, now(), false, true);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('Esaretin Bedeli', 'The Shawshank Redemption', 'Kar??s??n?? ??ld??rmedi??i halde hapse d????en bir muhasebecinin dram?? hapishaneye sonradan d????en bir gen??le farkl??l??k g??stermeye ba??lar. Okuma yazma bilmeyen bu gence ders verdi??i s??rada gencin anlatt?????? bir hikaye ile hapisten ka??ma fikirleri depre??ir kafas??nda ve m??kemmel plan??n?? uygulamak i??in ??al????malara ba??lar.', 1994, 136, 'movies/the_shawshank_redemption.jpg', null, now(), false, true);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('E??k??ya', null, '35 y??l ??nce Cudi da??lar??nda bir grup e??k??ya jandarma taraf??ndan yakalan??r. 35 y??l i??inde e??k??yalar??n hepsi ya hastal??ktan ya da hesapla??malardan ??t??r?? can vermi??tir. Biri d??????nda; Baran... Baran 35 y??l sonra hapisten ????k??nca ilk i??i k??y??ne d??nmek olur. Ama do??du??u topraklar ??imdi baraj sular?? alt??ndad??r. Ge??mi??in izlerini s??rmeye ba??layan E??k??ya, y??llard??r bilmedi??i bir ger??e??i ????renir. Hapse d????mesine en yak??n arkada????n??n ihaneti neden olmu??tur. Bu arkada?? E??k??ya???n??n ??ocukluk a??k??n??, Keje???yi sat??n alarak ??stanbul???a ka??m????t??r. E??k??ya ne ??stanbul???u ne de arkada????n??n adresini bilmemektedir. Trende Beyo??lu???nun arka sokaklar??nda b??y??m????, pavyon, kumarhane, uyu??turucu muhabbetinin i??inde ya??ayan Cumali adl?? gen?? bir adamla tan??????r...', 1996, 122, 'movies/eskiya.jpg', null, now(), false, true);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active, is_synchronized)
values ('Yalanc?? Yalanc??', 'Liar Liar', 'Ger??e??i s??yleyerek kendinizi daha m?? ??zg??r hissedersiniz yoksa ba????n??za olmad??k dertler mi a????l??r? Dahi komedyen Jim Carrey ile ??atlak Profes??r''??n Yap??mc??-Y??netmeni ba??lang??c??nda sonuna kadar size durmaks??z??n s??n??rs??z bir kahkaha tufan?? ya??atacak! Fletcher Reede (Carrey) konu??ma ustas?? bir avukat ve ayn?? zamanda da do??u??tan bir yalanc??d??r. O??lu Max (Justin Cooper) 5. ya?? g??n??nde do??um g??n?? pastas??n?? ??flerken bir dilek tutar ve babas??n??n 1 g??n i??in hi?? yalan s??ylememesini diler. Max''??n dile??inin mucizevi bir ??ekilde ger??ekle??mesiyle Fletcher''in en de??erli varl?????? olan konu??ma yetene??i bir anda en b??y??k kabusu haline gelir. Fletcher bir yandan kendini yeni durumuna adapte etmeye ??al??????rken bir yandan da o??lu ve eski kar??s?? Audrey''nin (Maura Tierney) Boston''a ta????nmas??n?? engellemeye ??al??????r.', 1997, 83, 'movies/liar_liar.jpg', null, now(), false, true);

--Insert persons
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Michael J. Fox', 'Michael Andrew Fox',null, 'persons/micheal_j_fox.jpg','9 Haziran 1961',null,'Kanada',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Christopher Lloyd', 'Christopher Allen Lloyd',null, null,'22 Ekim 1938',null,'Stamford, CT',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Lea Thompson', 'Lea Katherine Thompson',null, null,'31 May??s 1961',null,'Rochester, MN',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Crispin Glover', 'Crispin Hellion Glover',null, null,'20 Nisan 1964',null,'New York',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Thomas F. Wilson', '',null, null,'15 Nisan 1959',null,'Philadeiphia',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Robert Zemeckis', 'Robert Lee Zemeckis',null, null,'14 May??s 1952',null,'Chicago, Illinois, ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Kathleen Kennedy', '',null, null,'5 Haziran 1953',null,'Berkeley',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Bob Gale', '',null, null,'25 May??s 1951',null,'Missouri/ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Alan Silvestri', 'Alan Anthony Silvestri',null, null,'26 Mart 1950',null,'New York',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Chuck Berry', 'Charles Edward Anderson Berry',null, null,'18 Ekim 1926',null,'Missouri, ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Dean Cundey', 'Dean Raymond Cundey',null, null,'12 Mart 1946',null,'Alhambra/California-ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Mary Steenburgen', 'Mary Nell Steenburgen',null, null,'8 ??ubat 1953',null,'Newport, Arkansas, ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Elisabeth Shue', 'Elisabeth Judson Shue',null, null,'6 Ekim 1963',null,'Wilmington/Delaware-ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Sammy Hagar', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Johnny Colla', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Claudia Wells', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Elijah Wood', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Viggo Mortensen', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Orlando Bloom', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Sean Bean', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Cate Blanchett', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Sean Astin', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Ian Holm', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Liv Tyler', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Hugo Weaving', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Ian McKellen', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('John Rhys-Davies', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Billy Boyd', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Dominic Monaghan	', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Christopher Lee', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Andy Serkis', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Peter Jackson', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Philippa Boyens', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Harvey Weinstein', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Howard Shore', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Andrew Lesnie', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('J.R.R. Tolkien', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Tim Robbins', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Morgan Freeman', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Bob Gunton', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('William Sadler', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Clancy Brown', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Gil Bellows', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Mark Rolston', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('James Whitmore', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Frank Darabont', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Niki Marvin', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Thomas Newman', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Roger Deakins', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Stephen King', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('??ener ??en', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Ye??im Salk??m', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('U??ur Y??cel', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Kamran Usluer', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Sermin H??rmergi??', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('??zkan U??ur', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Melih ??ardak', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Kemal ??nci', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('??mit ????rak', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('R??za S??nmez', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Yavuz Turgul', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Mine Varg??', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Erkan O??ur', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('A??k??n Arsunan', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('U??ur ????bak', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Jim Carrey', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Maura Tierney', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Justin Cooper', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Cary Elwes', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Jennifer Tilly', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Amanda Donohoe', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Jason Bernard', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Swoosie Kurtz', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Tom Shadyac', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Paul Guay', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('John Debney', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Russell Boyd', '',null, null,null,null,null,null, now());

--insert types
insert into types(name, created_on) values('Dram', now());
insert into types(name, created_on) values('Komedi', now());
insert into types(name, created_on) values('Macera', now());
insert into types(name, created_on) values('Sava??', now());
insert into types(name, created_on) values('Romantik-Komedi', now());
insert into types(name, created_on) values('Bilimkurgu', now());
insert into types(name, created_on) values('Biyografi', now());
insert into types(name, created_on) values('Fantastik', now());
insert into types(name, created_on) values('Duygusal', now());
insert into types(name, created_on) values('Gerilim', now());
insert into types(name, created_on) values('Polisiye', now());
insert into types(name, created_on) values('Tarihi', now());
insert into types(name, created_on) values('Aksiyon', now());

--insert countries
insert into countries(name, created_on) values('T??rkiye', now());
insert into countries(name, created_on) values('ABD', now());
insert into countries(name, created_on) values('Fransa', now());
insert into countries(name, created_on) values('G??ney Kore', now());
insert into countries(name, created_on) values('Almanya', now());


--insert roles
insert into roles(name, created_on) values('Oyuncu', now());
insert into roles(name, created_on) values('Y??netmen', now());
insert into roles(name, created_on) values('Senaryo', now());
insert into roles(name, created_on) values('Yap??mc??', now());
insert into roles(name, created_on) values('G??r??nt?? Y??netmeni', now());
insert into roles(name, created_on) values('M??zik', now());
insert into roles(name, created_on) values('Eser', now());

--insert movie_types
insert into movie_types(movie_id, type_id) values(1, 13);
insert into movie_types(movie_id, type_id) values(1, 6);
insert into movie_types(movie_id, type_id) values(1, 2);
insert into movie_types(movie_id, type_id) values(1, 3);
insert into movie_types(movie_id, type_id) values(2, 13);
insert into movie_types(movie_id, type_id) values(2, 6);
insert into movie_types(movie_id, type_id) values(2, 2);
insert into movie_types(movie_id, type_id) values(2, 3);
insert into movie_types(movie_id, type_id) values(3, 13);
insert into movie_types(movie_id, type_id) values(3, 6);
insert into movie_types(movie_id, type_id) values(3, 2);
insert into movie_types(movie_id, type_id) values(3, 3);
insert into movie_types(movie_id, type_id) values(4, 13);
insert into movie_types(movie_id, type_id) values(4, 8);
insert into movie_types(movie_id, type_id) values(4, 3);
insert into movie_types(movie_id, type_id) values(5, 1);
insert into movie_types(movie_id, type_id) values(5, 9);
insert into movie_types(movie_id, type_id) values(5, 12);
insert into movie_types(movie_id, type_id) values(7, 2);
insert into movie_types(movie_id, type_id) values(7, 8);
insert into movie_types(movie_id, type_id) values(6, 13);
insert into movie_types(movie_id, type_id) values(6, 1);
insert into movie_types(movie_id, type_id) values(6, 10);
insert into movie_types(movie_id, type_id) values(6, 11);

--insert movie_countries
insert into movie_countries(movie_id, country_id) values(1, 2);
insert into movie_countries(movie_id, country_id) values(2, 2);
insert into movie_countries(movie_id, country_id) values(3, 2);
insert into movie_countries(movie_id, country_id) values(4, 2);
insert into movie_countries(movie_id, country_id) values(5, 2);
insert into movie_countries(movie_id, country_id) values(6, 1);
insert into movie_countries(movie_id, country_id) values(7, 2);

--insert movie_persons
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 1, 'Marty McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 2, 'Dr. Emmett Brown', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 3, 'Lorraine Baines McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 4, 'George McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 5, 'Biff Tannen', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 6, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 6, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 7, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 8, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 9, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 10, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 11, null, 5);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(1, 16, 'Jennifer Parker', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 1, 'Marty McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 2, 'Dr. Emmett Brown', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 3, 'Lorraine Baines McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 4, 'George McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 5, 'Biff Tannen/Griff Tannen', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 13, 'Jennifer Parker', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 6, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 6, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 7, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 8, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 9, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 14, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(2, 11, null, 5);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 1, 'Marty McFly / Marty McFly Jr / Marlene McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 2, 'Dr. Emmett Brown', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 12, 'Clara Clayton', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 3, 'Maggie McFly', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 5, 'Biff Tannen/Griff Tannen', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 13, 'Jennifer Parker', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 6, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 6, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 7, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 8, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 9, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 15, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(3, 11, null, 5);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 17, 'Frodo', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 18, 'Aragorn', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 19, 'Legolas Greenleaf', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 20, 'Boromir', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 21, 'Galadriel', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 22, 'Sam Gamgee', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 23, 'Bilbo Baggins', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 24, 'Arwen', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 25, 'Lord Elrond', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 26, 'Gandalf', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 27, 'Gimli', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 28, 'Pippin', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 29, 'Meriadoc Brandybuck', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 30, 'Saruman (B??y??c??)', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 31, 'Gollum', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 32, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 33, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 32, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 34, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 32, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 35, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 36, null, 5);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(4, 37, null, 7);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 38, 'Andy Dufresne', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 39, 'Ellis Body ''Red'' Redding', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 40, 'Warden Norton', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 41, 'Heywood', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 42, 'Kaptan Hadley', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 43, 'Tommy', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 44, 'Bogs Diamond', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 45, 'Brooks Hatlen', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 46, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 46, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 47, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 48, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 49, null, 5);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(5, 50, null, 7);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 51, 'E??k??ya Baran', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 52, 'Emel', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 53, 'Cumali', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 54, 'Berfo/Mahmut ??aho??lu', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 55, 'Keje', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 56, 'Sedat', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 57, 'Demircan', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 58, 'Mustafa', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 59, 'Cimbom', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 60, 'Avarel', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 61, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 61, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 62, null, 4);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 63, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 64, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 65, null, 5);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 66, 'Fletcher Reede', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 67, 'Audrey Reede', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 68, 'Max Reede', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 69, 'Jerry', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 70, 'Samantha Cole', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 71, 'Miranda', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 72, 'Judge', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 73, 'Dana Appleton', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 74, null, 2);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 75, null, 3);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 76, null, 6);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(7, 77, null, 5);