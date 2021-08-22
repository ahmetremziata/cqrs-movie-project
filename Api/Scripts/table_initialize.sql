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
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Geleceğe Dönüş', 'Back to the Future', 'Kolej öğrencisi Marty hayatından sıkılmıştır. Ailesiyle ve öğretmenleriyle anlaşamayan Marty, sadece kız arkadaşı Jennifer ve aykırı bilim adamı Brown'' in yanında kendine güvenini ve heyecanını geri kazanmaktadır. Dr. Brown, kendisinden nükleer silah yapmasını isteyen gizli bir örgütten para alarak, zaman makinesini icat etmek için çalışmalara başlar. Amacına ulaşan Dr. Brown, teröristler tarafından öldürülür. Olaya şahit olan Marty, kaçmaya çalışırken kendini zaman yolculuğunda bulur. Ancak, geri dönüş konusunda ciddi bi engel vardır. 88 mil/sa hıza ulaşmak zorundadır.', 1985, 116, 'movies/back_to_the_future.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Geleceğe Dönüş 2', 'Back to the Future Part II',
        'Marty ve doktor bu kez 2015 yılına giderek gelecekte Marty'' nin ailesinin karşılacağı problemleri çözmeye çalışırlar. Ancak eve geri döndüklerinde birisinin zaman makinesini kurcaladığını ve geleceği karıştırdığını farkederler. Şimdi yapmaları gereken tek şey 1955'' e geri dönüp geleceği kurtarmaktır.', 1989, 108, 'movies/back_to_the_future_2.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Geleceğe Dönüş 3', 'Back to the Future Part III',
        'Hiç görülmemiş bir şimşek patlamasından sonra 1955 yılında mahsur kalan Marty, doktoru vakitsiz bir sondan kurtarmak için 1885 yılına geri dönmelidir. Kızılderillilerin saldırısından ve kasaba halkının düşmanca tutumundan sonra hayatta kalan Marty, doktoru bulur. Ancak doktorun çekici Clara Clayton'' ın büyüsünün etkisi altında olduğu düşünülürse, onların vahşi batıdan uzaklaştırmak ve geleceğe döndürmek Marty'' ye bağlıdır.', 1990, 118, 'movies/back_to_the_future_3.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Yüzüklerin Efendisi:Yüzük Kardeşliği', 'The Lord of the Rings: The Fellowship of the Ring', 'Bir grup kahramanın dünyalarını şeytani güçler birliğinden kurtarma mücadeleleri...
Hobbit’ler, cüceler ve halk, şeytani güçle savaşmak için bir araya geliyorlar.
Elijah Wood, Ian McKellen, Liv Tyler, Cate Blanchett, Viggo Mortensen gibi birçok ünlü oyuncunun rol aldığı filmi fantastik işlerinden tanıdığımız Peter Jackson yönetiyor. “The Lord of the Rings”, J.R.R. Tolkien’in dünya çapında büyük ilgi gören roman serisinden sinemaya uyarlanmış.
Gelecek çağlar önce kaybolmuş bir yüzüğün kaderinde gizlidir. Karanlık güçler yüzüğe ulaşmak için yıllarca çabalamışlar ve hala da bu uğraşları devam etmektedir. Kader yüzüğü genç bir Hobbit olan Frodo’nun ellerine teslim eder. Yüzük Frodo’ya miras kalır ve olaylara davetiye böylece cıkmış olur.', 2001, 171, 'movies/lord_of_rings_1.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Esaretin Bedeli', 'The Shawshank Redemption', 'Karısını öldürmediği halde hapse düşen bir muhasebecinin dramı hapishaneye sonradan düşen bir gençle farklılık göstermeye başlar. Okuma yazma bilmeyen bu gence ders verdiği sırada gencin anlattığı bir hikaye ile hapisten kaçma fikirleri depreşir kafasında ve mükemmel planını uygulamak için çalışmalara başlar.', 1994, 136, 'movies/the_shawshank_redemption.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Eşkıya', null, '35 yıl önce Cudi dağlarında bir grup eşkıya jandarma tarafından yakalanır. 35 yıl içinde eşkıyaların hepsi ya hastalıktan ya da hesaplaşmalardan ötürü can vermiştir. Biri dışında; Baran... Baran 35 yıl sonra hapisten çıkınca ilk işi köyüne dönmek olur. Ama doğduğu topraklar şimdi baraj suları altındadır. Geçmişin izlerini sürmeye başlayan Eşkıya, yıllardır bilmediği bir gerçeği öğrenir. Hapse düşmesine en yakın arkadaşının ihaneti neden olmuştur. Bu arkadaş Eşkıya’nın çocukluk aşkını, Keje’yi satın alarak İstanbul’a kaçmıştır. Eşkıya ne İstanbul’u ne de arkadaşının adresini bilmemektedir. Trende Beyoğlu’nun arka sokaklarında büyümüş, pavyon, kumarhane, uyuşturucu muhabbetinin içinde yaşayan Cumali adlı genç bir adamla tanışır...', 1996, 122, 'movies/eskiya.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Yalancı Yalancı', 'Liar Liar', 'Gerçeği söyleyerek kendinizi daha mı özgür hissedersiniz yoksa başınıza olmadık dertler mi açılır? Dahi komedyen Jim Carrey ile Çatlak Profesör''ün Yapımcı-Yönetmeni başlangıcında sonuna kadar size durmaksızın sınırsız bir kahkaha tufanı yaşatacak! Fletcher Reede (Carrey) konuşma ustası bir avukat ve aynı zamanda da doğuştan bir yalancıdır. Oğlu Max (Justin Cooper) 5. yaş gününde doğum günü pastasını üflerken bir dilek tutar ve babasının 1 gün için hiç yalan söylememesini diler. Max''ın dileğinin mucizevi bir şekilde gerçekleşmesiyle Fletcher''in en değerli varlığı olan konuşma yeteneği bir anda en büyük kabusu haline gelir. Fletcher bir yandan kendini yeni durumuna adapte etmeye çalışırken bir yandan da oğlu ve eski karısı Audrey''nin (Maura Tierney) Boston''a taşınmasını engellemeye çalışır.', 1997, 83, 'movies/liar_liar.jpg', null, now(), false);

--Insert persons
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Michael J. Fox', 'Michael Andrew Fox',null, 'persons/micheal_j_fox.jpg','9 Haziran 1961',null,'Kanada',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Christopher Lloyd', 'Christopher Allen Lloyd',null, null,'22 Ekim 1938',null,'Stamford, CT',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Lea Thompson', 'Lea Katherine Thompson',null, null,'31 Mayıs 1961',null,'Rochester, MN',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Crispin Glover', 'Crispin Hellion Glover',null, null,'20 Nisan 1964',null,'New York',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Thomas F. Wilson', '',null, null,'15 Nisan 1959',null,'Philadeiphia',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Robert Zemeckis', 'Robert Lee Zemeckis',null, null,'14 Mayıs 1952',null,'Chicago, Illinois, ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Kathleen Kennedy', '',null, null,'5 Haziran 1953',null,'Berkeley',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Bob Gale', '',null, null,'25 Mayıs 1951',null,'Missouri/ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Alan Silvestri', 'Alan Anthony Silvestri',null, null,'26 Mart 1950',null,'New York',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Chuck Berry', 'Charles Edward Anderson Berry',null, null,'18 Ekim 1926',null,'Missouri, ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Dean Cundey', 'Dean Raymond Cundey',null, null,'12 Mart 1946',null,'Alhambra/California-ABD',null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Mary Steenburgen', 'Mary Nell Steenburgen',null, null,'8 Şubat 1953',null,'Newport, Arkansas, ABD',null, now());
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
('Şener Şen', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Yeşim Salkım', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Uğur Yücel', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Kamran Usluer', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Sermin Hürmergiç', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Özkan Uğur', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Melih Çardak', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Kemal İnci', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Ümit Çırak', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Rıza Sönmez', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Yavuz Turgul', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Mine Vargı', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Erkan Oğur', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Aşkın Arsunan', '',null, null,null,null,null,null, now());
insert into persons(name, real_name, biography, avatar_url, birth_date, death_date, birth_place, death_place, created_on) VALUES
('Uğur İçbak', '',null, null,null,null,null,null, now());
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
insert into types(name, created_on) values('Savaş', now());
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
insert into countries(name, created_on) values('Türkiye', now());
insert into countries(name, created_on) values('ABD', now());
insert into countries(name, created_on) values('Fransa', now());
insert into countries(name, created_on) values('Güney Kore', now());
insert into countries(name, created_on) values('Almanya', now());


--insert roles
insert into roles(name, created_on) values('Oyuncu', now());
insert into roles(name, created_on) values('Yönetmen', now());
insert into roles(name, created_on) values('Senaryo', now());
insert into roles(name, created_on) values('Yapımcı', now());
insert into roles(name, created_on) values('Görüntü Yönetmeni', now());
insert into roles(name, created_on) values('Müzik', now());
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
(4, 30, 'Saruman (Büyücü)', 1);
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
(6, 51, 'Eşkıya Baran', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 52, 'Emel', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 53, 'Cumali', 1);
insert into movie_persons(movie_id, person_id, character_name, role_id) VALUES
(6, 54, 'Berfo/Mahmut Şahoğlu', 1);
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