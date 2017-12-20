using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GenreJson
{
    public class RakutenGenre
    {
        public class current
        {
            public int booksGenreId { get; set; }

            public string booksGenreName { get; set; }

            public int genreLevel { get; set; }
        }

        public List<child> children { get; set; }

        public class child
        {
            public int booksGenreId { get; set; }

            public string booksGenreName { get; set; }

            public int genreLevel { get; set; }
        }


        
    }
}
