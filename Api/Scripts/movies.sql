create table movies
(
    id                integer generated always as identity
        constraint movies_pkey
            primary key,
    name              varchar(250)   not null,
    original_name     varchar(250),
    description       varchar(10000) not null,
    construction_year integer        not null,
    total_minute      integer        not null,
    poster_url        varchar(250),
    vision_entry_date date,
    created_on        date           not null,
    is_active         bool           not null
);


INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Geleceğe Dönüş', 'Back to the Future', 'Kolej öğrencisi Marty hayatından sıkılmıştır. Ailesiyle ve öğretmenleriyle anlaşamayan Marty, sadece kız arkadaşı Jennifer ve aykırı bilim adamı Brown'' in yanında kendine güvenini ve heyecanını geri kazanmaktadır. Dr. Brown, kendisinden nükleer silah yapmasını isteyen gizli bir örgütten para alarak, zaman makinesini icat etmek için çalışmalara başlar. Amacına ulaşan Dr. Brown, teröristler tarafından öldürülür. Olaya şahit olan Marty, kaçmaya çalışırken kendini zaman yolculuğunda bulur. Ancak, geri dönüş konusunda ciddi bi engel vardır. 88 mil/sa hıza ulaşmak zorundadır.', 1985, 116, 'back_to_the_future.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Esaretin Bedeli', 'The Shawshank Redemption', 'Karısını öldürmediği halde hapse düşen bir muhasebecinin dramı hapishaneye sonradan düşen bir gençle farklılık göstermeye başlar. Okuma yazma bilmeyen bu gence ders verdiği sırada gencin anlattığı bir hikaye ile hapisten kaçma fikirleri depreşir kafasında ve mükemmel planını uygulamak için çalışmalara başlar.', 1994, 136, 'the_shawshank_redemption.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Eşkıya', null, '35 yıl önce Cudi dağlarında bir grup eşkıya jandarma tarafından yakalanır. 35 yıl içinde eşkıyaların hepsi ya hastalıktan ya da hesaplaşmalardan ötürü can vermiştir. Biri dışında; Baran... Baran 35 yıl sonra hapisten çıkınca ilk işi köyüne dönmek olur. Ama doğduğu topraklar şimdi baraj suları altındadır. Geçmişin izlerini sürmeye başlayan Eşkıya, yıllardır bilmediği bir gerçeği öğrenir. Hapse düşmesine en yakın arkadaşının ihaneti neden olmuştur. Bu arkadaş Eşkıya’nın çocukluk aşkını, Keje’yi satın alarak İstanbul’a kaçmıştır. Eşkıya ne İstanbul’u ne de arkadaşının adresini bilmemektedir. Trende Beyoğlu’nun arka sokaklarında büyümüş, pavyon, kumarhane, uyuşturucu muhabbetinin içinde yaşayan Cumali adlı genç bir adamla tanışır...', 1996, 122, 'eskiya.jpg', null, now(), false);
INSERT INTO movies(name, original_name, description, construction_year, total_minute, poster_url, vision_entry_date, created_on, is_active)
values ('Yalancı Yalancı', 'Liar Liar', 'Gerçeği söyleyerek kendinizi daha mı özgür hissedersiniz yoksa başınıza olmadık dertler mi açılır? Dahi komedyen Jim Carrey ile Çatlak Profesör''ün Yapımcı-Yönetmeni başlangıcında sonuna kadar size durmaksızın sınırsız bir kahkaha tufanı yaşatacak! Fletcher Reede (Carrey) konuşma ustası bir avukat ve aynı zamanda da doğuştan bir yalancıdır. Oğlu Max (Justin Cooper) 5. yaş gününde doğum günü pastasını üflerken bir dilek tutar ve babasının 1 gün için hiç yalan söylememesini diler. Max''ın dileğinin mucizevi bir şekilde gerçekleşmesiyle Fletcher''in en değerli varlığı olan konuşma yeteneği bir anda en büyük kabusu haline gelir. Fletcher bir yandan kendini yeni durumuna adapte etmeye çalışırken bir yandan da oğlu ve eski karısı Audrey''nin (Maura Tierney) Boston''a taşınmasını engellemeye çalışır.', 1997, 83, 'liar_liar.jpg', null, now(), false);