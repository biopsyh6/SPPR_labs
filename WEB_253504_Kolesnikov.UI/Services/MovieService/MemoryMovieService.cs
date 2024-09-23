using Microsoft.AspNetCore.Mvc;
using WEB_253504_Kolesnikov.Domain.Entities;
using WEB_253504_Kolesnikov.Domain.Models;
using WEB_253504_Kolesnikov.UI.Services.GenreService;

namespace WEB_253504_Kolesnikov.UI.Services.MovieService
{
    public class MemoryMovieService : IMovieService
    {
        private readonly IConfiguration _configuration;
        List<Movie> _movies;
        List<Genre> _genres;

        public MemoryMovieService([FromServices]IConfiguration configuration, IGenreService genreService)
        {
            _configuration = configuration;
            _genres = genreService.GetGenreListAsync().Result.Data;
            SetupData();
        }

        // Инициализация списков
        private void SetupData()
        {
            _movies = new List<Movie>
            {
                new Movie {Id = 1, Title = "Назад в будущее", Description = "Подросток Марти с помощью машины времени, сооружённой его " +
                "другом-профессором доком Брауном, попадает из 80-х в далекие 50-е. Там он встречается со своими будущими родителями, " +
                "ещё подростками, и другом-профессором, совсем молодым.", Genre = _genres.Find(g => g.NormalizedName!.Equals("sci-fi")), 
                    Duration = 116, ImagePath = "images/Back_to_the_Future.jpg"},
                new Movie {Id = 2, Title = "Бронкская история", Description = "Король бронкской мафии Сонни убил человека. Просто так. Из-за места на парковке. " +
                "Среди бела дня вытащил пистолет и несколько раз выстрелил. Полиция узнала, что свидетелем преступления был 9-летний Калоджеро, " +
                "и допросила мальчика. Но Калоджеро не выдал Сонни. Так началась их дружба. И когда Калоджеро вырос, он не просто стал свидетелем " +
                "других кровавых деяний своего босса, но и сам оказался на краю пропасти.", Genre = _genres.Find(g => g.NormalizedName!.Equals("crime")), 
                    Duration = 121, ImagePath = "images/bronx_tale.jpg"},
                new Movie {Id = 3, Title = "Зеленая миля", Description = "Пол Эджкомб — начальник блока смертников в тюрьме «Холодная гора», каждый из узников которого однажды проходит «зеленую милю» " +
                "по пути к месту казни. Пол повидал много заключённых и надзирателей за время работы. Однако гигант Джон Коффи, " +
                "обвинённый в страшном преступлении, стал одним из самых необычных обитателей блока." , Genre = _genres.Find(g => g.NormalizedName!.Equals("drama")),
                 Duration = 189, ImagePath = "images/green_mile.jpg"},
                new Movie {Id = 4, Title = "Гарри Поттер и философский камень", Description = "Жизнь десятилетнего Гарри Поттера нельзя назвать сладкой: родители умерли, едва ему исполнился год, а от дяди и тёти, " +
                "взявших сироту на воспитание, достаются лишь тычки да подзатыльники. Но в одиннадцатый день рождения Гарри всё меняется. Странный гость, неожиданно появившийся на пороге, приносит письмо, из которого " +
                "мальчик узнаёт, что на самом деле он - волшебник и зачислен в школу магии под названием Хогвартс. А уже через пару недель Гарри будет мчаться в поезде Хогвартс-экспресс навстречу новой жизни, где его " +
                "ждут невероятные приключения, верные друзья и самое главное — ключ к разгадке тайны смерти его родителей.", Genre = _genres.Find(g => g.NormalizedName!.Equals("fantasy")),
                 Duration = 152, ImagePath = "images/Harry_Potter.jpg"},
                new Movie {Id = 5, Title = "Мадагаскар", Description = "Четверо изнеженных животных из Центрального зоопарка в Нью-Йорке - лев Алекс, зебра Марти, жираф Мелман и гиппопотамиха Глория - решаются на побег." +
                " Оказавшись после кораблекрушения на экзотическом острове Мадагаскар, населенном лемурами и поедающими их фоссами, путешественники с ужасом понимают, что с городскими привычками им придется распрощаться.\r\n\r\nЗдесь нет людей, " +
                "любимых клеток, вкусной кормежки. Начинается шоу «Последний герой» только с животными - нашим друзьям нужно выжить в непривычной среде обитания, где на каждом шагу подстерегает опасность...", Genre = _genres.Find(g => g.NormalizedName!.Equals("animation")),
                 Duration = 86, ImagePath = "images/madagascar.jpg"},
                new Movie {Id = 6, Title = "Скуби-Ду", Description = "Два года спустя после того, как Тайная корпорация распалась из-за возникших противоречий Скуби-Ду и его сообразительных приятелей, раскрывших не одно " +
                "преступление, Фреда, Дафну, Шэгги и Велму, поодиночке вызывают на Зловещий остров, чтобы они начали расследование серии паранормальных явлений, происходящих в Спринг-Брейк.\r\n\r\nОпасаясь, что его невероятно" +
                " популярный курорт на самом деле может быть населен привидениями, владелец Зловещего острова Эмиль Мондавариус пытается воссоединить легендарных детективов, чтобы те разрешили загадку, пока сверхъестественная" +
                " тайна не распугала всех отдыхающих студентов.\r\n\r\nСкуби-Ду и его команде придется преодолеть личные разногласия и по-новому взглянуть на мнимых вампиров и нереальных привидений. И все это, чтобы распутать" +
                " дело, спасти самих себя, а возможно... и весь мир...", Genre = _genres.Find(g => g.NormalizedName!.Equals("comedy")),
                 Duration = 89, ImagePath = "images/Scooby-Doo_poster.jpg"},
                new Movie {Id = 7, Title = "Синистер", Description = "Автор детективов с семьёй переезжает в небольшой городок и селится в доме, где год назад развернулась леденящая душу трагедия — были убиты все жильцы. Писатель" +
                " случайно находит видеозаписи, которые являются ключом к тайне преступления. Но ничто не дается даром: в доме начинают происходить жуткие вещи, и теперь под угрозой оказывается жизнь его близких." , Genre = _genres.Find(g => g.NormalizedName!.Equals("horror")),
                Duration = 110, ImagePath = "images/sinister.jpg"},
                new Movie {Id = 8, Title = "Человек-паук", Description = "Питер Паркер – обыкновенный школьник. Однажды он отправился с классом на экскурсию, где его кусает странный паук-мутант. Через время парень почувствовал в себе нечеловеческую силу и ловкость в движении," +
                " а главное – умение лазать по стенам и метать стальную паутину. Свои способности он направляет на защиту слабых. Так Питер становится настоящим супергероем по имени Человек-паук, который помогает людям и борется с преступностью. Но там, где есть супергерой," +
                " рано или поздно всегда объявляется и суперзлодей...", Genre = _genres.Find(g => g.NormalizedName!.Equals("adventure")),
                Duration = 121, ImagePath = "images/Spider-Man_(2002_film)_poster.jpg"},
                new Movie {Id = 9, Title = "Такси", Description = "Таксист Даниэль помешан на быстрой езде. Как ураган проносится он по извилистым улицам Марселя на мощном ревущем «Пежо», пугая прохожих и приводя в ужас пассажиров. Начинающий следователь Эмильен вынуждает его" +
                " помогать в поимке банды грабителей банков, каждый раз ускользающих от полиции на неуловимых «Мерседесах».", Genre = _genres.Find(g => g.NormalizedName!.Equals("action")),
                Duration = 89, ImagePath = "images/taxi.jpg"},
                new Movie {Id = 10, Title = "Звонок", Description = "Молодая репортер расследует загадочное суеверие о смертоносной видеокассете. В доме того, кто посмотрит её, сначала раздается странный телефонный звонок, а позже человек умирает. После того как такой же смертью" +
                " умирает её племянница, женщина решает взяться за расследование и сама смотрит злополучную кассету. Чуть позже в её квартире раздаётся звонок. Теперь у журналистки есть только семь дней, чтобы докопаться до истоков страшного проклятия." , Genre = _genres.Find(g => g.NormalizedName!.Equals("horror")),
                 Duration = 96, ImagePath = "images/The_ring_poster2.jpg"},
                new Movie {Id = 11, Title = "Том и Джерри", Description = "Кайла, сотрудница престижного отеля, где обитает мышонок Джерри, рискующий нарушить ход дорогой свадьбы, нанимает уличного кота Тома, чтобы разобраться с наглым грызуном. Но решить эту проблему не так-то просто.",
                 Genre = _genres.Find(g => g.NormalizedName!.Equals("family")), Duration = 101, ImagePath = "images/Tom_&_Jerry_(Official_2021_Film_Poster).png"}
            };
        }
        public Task<ResponseData<Movie>> CreateMovieAsync(Movie product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Movie>> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ProductListModel<Movie>>> GetMovieListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var itemsPerPage = Convert.ToInt32(_configuration.GetRequiredSection("ItemsPerPage").Value);
            //var filteredMovies = string.IsNullOrEmpty(categoryNormalizedName) 
            //    ? _movies
            //    : _movies.Where(m => m.Genre?.NormalizedName == categoryNormalizedName).ToList();
            var filteredMovies = _movies.
                Where(m => categoryNormalizedName == "all" ||
                m.Genre.NormalizedName.Equals(categoryNormalizedName)).ToList();
            var totalMovies = filteredMovies.Count;
            var totalPages = (int)Math.Ceiling(totalMovies / (double)itemsPerPage);
            var moviesOnPage = filteredMovies
                .Skip((pageNo - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToList();
            var productListModel = new ProductListModel<Movie>
            {
                Items = moviesOnPage,
                CurrentPage = pageNo,
                TotalPages = totalPages,
            };
            var result = ResponseData<ProductListModel<Movie>>.Success(productListModel);
            return Task.FromResult(result);
        }

        public Task UpdateMovieAsync(int id, Movie product, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
