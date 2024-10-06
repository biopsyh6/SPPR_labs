using Microsoft.EntityFrameworkCore;
using WEB_253504_Kolesnikov.Domain.Entities;

namespace WEB_253504_Kolesnikov.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            //await context.Database.MigrateAsync();

            //delete data
            //context.Movies.RemoveRange(context.Movies);
            //context.Genres.RemoveRange(context.Genres);
            //await context.Database.ExecuteSqlRawAsync("DELETE FROM movies;");
            //await context.Database.ExecuteSqlRawAsync("DELETE FROM genres;");
            //await context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name = 'movies';");
            //await context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name = 'genres';");
            //await context.SaveChangesAsync();



            var genres = new List<Genre>
                {
                    new Genre { Name="Боевик", NormalizedName="action" },
                    new Genre { Name="Ужасы", NormalizedName="horror" },
                    new Genre { Name="Комедия", NormalizedName="comedy" },
                    new Genre { Name="Приключения", NormalizedName="adventure" },
                    new Genre { Name="Драма", NormalizedName="drama" },
                    new Genre { Name="Криминальный", NormalizedName="crime" },
                    new Genre { Name="Фэнтези", NormalizedName="fantasy" },
                    new Genre { Name="Романтический", NormalizedName="romance" },
                    new Genre { Name="Триллер", NormalizedName="thriller" },
                    new Genre { Name="Анимационный", NormalizedName="animation" },
                    new Genre { Name="Семейный", NormalizedName="family" },
                    new Genre { Name="Документальный", NormalizedName="documentary" },
                    new Genre { Name="Мюзикл", NormalizedName="musical" },
                    new Genre { Name="Научная фантастика", NormalizedName="sci-fi" },
                    new Genre { Name="Вестерн", NormalizedName="western" }
                };
            await context.Genres.AddRangeAsync(genres);
            await context.SaveChangesAsync();


            var url = app.Configuration.GetSection("ImagesUrl").Value;
            var movies = new List<Movie>
                {
                    new Movie { Title = "Назад в будущее", Description = "Подросток Марти с помощью машины времени, сооружённой его " +
                "другом-профессором доком Брауном, попадает из 80-х в далекие 50-е. Там он встречается со своими будущими родителями, " +
                "ещё подростками, и другом-профессором, совсем молодым.", Genre =  genres.First(g => g.NormalizedName == "sci-fi"),
                        Duration = 116, ImagePath = url + "Back_to_the_Future.jpg" },
                    new Movie { Title = "Бронкская история", Description = "Король бронкской мафии Сонни убил человека. Просто так. Из-за места на парковке. " +
                "Среди бела дня вытащил пистолет и несколько раз выстрелил. Полиция узнала, что свидетелем преступления был 9-летний Калоджеро, " +
                "и допросила мальчика. Но Калоджеро не выдал Сонни. Так началась их дружба. И когда Калоджеро вырос, он не просто стал свидетелем " +
                "других кровавых деяний своего босса, но и сам оказался на краю пропасти.", Genre = genres.First(g => g.NormalizedName == "crime"),
                        Duration = 121, ImagePath = url + "bronx_tale.jpg" },
                    new Movie { Title = "Зеленая миля", Description = "Пол Эджкомб — начальник блока смертников в тюрьме «Холодная гора», каждый из узников которого однажды проходит «зеленую милю» " +
                "по пути к месту казни. Пол повидал много заключённых и надзирателей за время работы. Однако гигант Джон Коффи, " +
                "обвинённый в страшном преступлении, стал одним из самых необычных обитателей блока.", Genre = genres.First(g => g.NormalizedName == "drama"),
                        Duration = 189, ImagePath = url + "green_mile.jpg" },
                    new Movie { Title = "Гарри Поттер и философский камень", Description = "Жизнь десятилетнего Гарри Поттера нельзя назвать сладкой: родители умерли, едва ему исполнился год, а от дяди и тёти, " +
                "взявших сироту на воспитание, достаются лишь тычки да подзатыльники. Но в одиннадцатый день рождения Гарри всё меняется. Странный гость, неожиданно появившийся на пороге, приносит письмо, из которого " +
                "мальчик узнаёт, что на самом деле он - волшебник и зачислен в школу магии под названием Хогвартс. А уже через пару недель Гарри будет мчаться в поезде Хогвартс-экспресс навстречу новой жизни, где его " +
                "ждут невероятные приключения, верные друзья и самое главное — ключ к разгадке тайны смерти его родителей.", Genre = genres.First(g => g.NormalizedName == "fantasy"),
                        Duration = 152, ImagePath = url + "Harry_Potter.jpg" },
                    new Movie { Title = "Мадагаскар", Description = "Четверо изнеженных животных из Центрального зоопарка в Нью-Йорке - лев Алекс, зебра Марти, жираф Мелман и гиппопотамиха Глория - решаются на побег." +
                " Оказавшись после кораблекрушения на экзотическом острове Мадагаскар, населенном лемурами и поедающими их фоссами, путешественники с ужасом понимают, что с городскими привычками им придется распрощаться.\r\n\r\nЗдесь нет людей, " +
                "любимых клеток, вкусной кормежки. Начинается шоу «Последний герой» только с животными - нашим друзьям нужно выжить в непривычной среде обитания, где на каждом шагу подстерегает опасность...",
                    Genre = genres.First(g => g.NormalizedName == "animation"), Duration = 86, ImagePath = url + "madagascar.jpg" },
                    new Movie { Title = "Скуби-Ду", Description = "Два года спустя после того, как Тайная корпорация распалась из-за возникших противоречий Скуби-Ду и его сообразительных приятелей, раскрывших не одно " +
                "преступление, Фреда, Дафну, Шэгги и Велму, поодиночке вызывают на Зловещий остров, чтобы они начали расследование серии паранормальных явлений, происходящих в Спринг-Брейк.\r\n\r\nОпасаясь, что его невероятно" +
                " популярный курорт на самом деле может быть населен привидениями, владелец Зловещего острова Эмиль Мондавариус пытается воссоединить легендарных детективов, чтобы те разрешили загадку, пока сверхъестественная" +
                " тайна не распугала всех отдыхающих студентов.\r\n\r\nСкуби-Ду и его команде придется преодолеть личные разногласия и по-новому взглянуть на мнимых вампиров и нереальных привидений. И все это, чтобы распутать" +
                " дело, спасти самих себя, а возможно... и весь мир...", Genre = genres.First(g => g.NormalizedName == "comedy"),
                        Duration = 89, ImagePath = url + "Scooby-Doo_poster.jpg" },
                    new Movie { Title = "Синистер", Description = "Автор детективов с семьёй переезжает в небольшой городок и селится в доме, где год назад развернулась леденящая душу трагедия — были убиты все жильцы. Писатель" +
                " случайно находит видеозаписи, которые являются ключом к тайне преступления. Но ничто не дается даром: в доме начинают происходить жуткие вещи, и теперь под угрозой оказывается жизнь его близких.",
                        Genre = genres.First(g => g.NormalizedName == "horror"),
                        Duration = 110, ImagePath = url + "sinister.jpg" },
                    new Movie { Title = "Человек-паук", Description = "Питер Паркер – обыкновенный школьник. Однажды он отправился с классом на экскурсию, где его кусает странный паук-мутант. Через время парень почувствовал в себе нечеловеческую силу и ловкость в движении," +
                " а главное – умение лазать по стенам и метать стальную паутину. Свои способности он направляет на защиту слабых. Так Питер становится настоящим супергероем по имени Человек-паук, который помогает людям и борется с преступностью. Но там, где есть супергерой," +
                " рано или поздно всегда объявляется и суперзлодей...", Genre = genres.First(g => g.NormalizedName == "adventure"),
                        Duration = 121, ImagePath = url + "Spider-Man_(2002_film)_poster.jpg" },
                    new Movie { Title = "Такси", Description = "Таксист Даниэль помешан на быстрой езде. Как ураган проносится он по извилистым улицам Марселя на мощном ревущем «Пежо», пугая прохожих и приводя в ужас пассажиров. Начинающий следователь Эмильен вынуждает его" +
                " помогать в поимке банды грабителей банков, каждый раз ускользающих от полиции на неуловимых «Мерседесах».", Genre = genres.First(g => g.NormalizedName == "action"),
                        Duration = 89, ImagePath = url + "taxi.jpg" },
                    new Movie { Title = "Звонок", Description = "Молодая репортер расследует загадочное суеверие о смертоносной видеокассете. В доме того, кто посмотрит её, сначала раздается странный телефонный звонок, а позже человек умирает. После того как такой же смертью" +
                " умирает её племянница, женщина решает взяться за расследование и сама смотрит злополучную кассету. Чуть позже в её квартире раздаётся звонок. Теперь у журналистки есть только семь дней, чтобы докопаться до истоков страшного проклятия.",
                        Genre = genres.First(g => g.NormalizedName == "horror"),
                        Duration = 96, ImagePath = url + "The_ring_poster2.jpg" },
                    new Movie { Title = "Том и Джерри", Description = "Кайла, сотрудница престижного отеля, где обитает мышонок Джерри, рискующий нарушить ход дорогой свадьбы, нанимает уличного кота Тома, чтобы разобраться с наглым грызуном. Но решить эту проблему не так-то просто.",
                        Genre = genres.First(g => g.NormalizedName == "family"), Duration = 101, ImagePath = url + "Tom_&_Jerry_(Official_2021_Film_Poster).png" }
                };

            await context.Movies.AddRangeAsync(movies);
            await context.SaveChangesAsync();


        }
    }
}
