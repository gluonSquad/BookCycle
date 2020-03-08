using BookCycle.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace BookCycle.Data.Seeds
{
    public class BookSeed:IEntityTypeConfiguration<Book>
    {
        private readonly int[] _ids;

        public BookSeed(int[] ids)
        {
            _ids = ids;
        }
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book
                {
                    Id = 1,
                    AuthorId = _ids[0], 
                    Name = "Şeker Portakalı", 
                    BookPages = 260,
                    Isbn = "9750738609",
                    BookPublisher = "CAN YAYINLARI", ReleaseDate = DateTime.Parse(DateTime.Today.ToString()),
                    CategoryId=_ids[0],
                    BookImageFile =
                        "https://imageserver.kitapyurdu.com/select.php?imageid=1105506&width=180&isWatermarked=true",
                },
            new Book
                {
                    Id = 2,
                    AuthorId = _ids[1],
                    Name = "Posta Kutusundaki Mızıka",
                    BookPages = 206,
                    Isbn = "9756841357",
                    BookPublisher = "ŞULE YAYINLARI",
                    ReleaseDate = DateTime.Parse(DateTime.Today.ToString()),
                    CategoryId = _ids[1],
                    BookImageFile =
                        "https://imageserver.kitapyurdu.com/select.php?imageid=10289747&width=180&isWatermarked=true&pagecount=206"
            },
                new Book
                {
                    Id = 3,
                    AuthorId = _ids[2],
                    Name = "Otostopçunun Galaksi Rehberi",
                    BookPages = 248,
                    Isbn = "6051715124",
                    BookPublisher = "ALFA YAYINLARI",
                    ReleaseDate = DateTime.Parse(DateTime.Today.ToString()),
                    CategoryId = _ids[2],
                    BookImageFile =
                        "https://imageserver.kitapyurdu.com/select.php?imageid=10904955&width=180&isWatermarked=true&pagecount=248"
                },
                new Book
                {
                    Id = 4,
                    AuthorId = _ids[2],
                    Name = "Hayat, Evren ve Herşey",
                    BookPages = 248,
                    Isbn = "6051715100",
                    BookPublisher = "ALFA YAYINLARI",
                    ReleaseDate = DateTime.Parse(DateTime.Today.ToString()),
                    CategoryId = _ids[2],
                    BookImageFile =
                        "https://imageserver.kitapyurdu.com/select.php?imageid=10459513&width=180&isWatermarked=true&pagecount=248"
                },
                new Book
                {
                    Id = 5,
                    AuthorId = _ids[3],
                    Name = "Huzursuzluk",
                    BookPages = 160,
                    Isbn = "6050939828",
                    BookPublisher = "DOĞAN KİTAP",
                    ReleaseDate = DateTime.Parse(DateTime.Today.ToString()),
                    CategoryId = _ids[0],
                    BookImageFile =
                        "https://imageserver.kitapyurdu.com/select.php?imageid=2876841&width=180&isWatermarked=true&pagecount=160"
                }
               
            );

        }
    }
}
