using BookStoreMVC.Areas.Identity.Data;
using BookStoreMVC.Data;
using BookStoreMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace MVCMovie.Models
{
    public class SeedData
    {
        public static async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<BookStoreMVCUser>>();
            IdentityResult roleResult;
            //Add Admin Role
            var roleAdminCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleAdminCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin")); }
            //Add User Role
            var roleUserCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleUserCheck) { roleResult = await RoleManager.CreateAsync(new IdentityRole("User")); }

            BookStoreMVCUser user = await UserManager.FindByEmailAsync("andrej@admin.com");
            if (user == null)
            {
                var User = new BookStoreMVCUser
                {
                    Email = "andrej@admin.com",
                    UserName = "andrej@admin.com"
                };
                string userPWD = "Admin123";
                IdentityResult chkUser = await UserManager.CreateAsync(User, userPWD);
                //Add default User to Role Admin      
                if (chkUser.Succeeded) { var result1 = await UserManager.AddToRoleAsync(User, "Admin"); }
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreMVCContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<BookStoreMVCContext>>()))
            {
                CreateUserRoles(serviceProvider).Wait();
                // Look for any book.
                if (context.Author.Any() || context.Book.Any() || context.bookGenres.Any() || context.Genre.Any() || context.Review.Any() || context.userBooks.Any())
                {
                    return;   // DB has been seeded
                }


                context.Author.AddRange(
              
                    new Author { FirstName = "Harper", LastName = "Lee", BirthDate = DateTime.Parse("1926-04-28"), Nationality = "American", Gender = "Female" },
                    new Author { FirstName = "George", LastName = "Orwell", BirthDate = DateTime.Parse("1903-06-25"), Nationality = "British", Gender = "Male" },
                    new Author { FirstName = "Joanne", LastName = "Rowling", BirthDate = DateTime.Parse("1965-07-31"), Nationality = "British", Gender = "Female" },
                    new Author { FirstName = "Toni", LastName = "Morrison", BirthDate = DateTime.Parse("1931-02-18"), Nationality = "American", Gender = "Female" },
                    new Author { FirstName = "Gabriel", LastName = "García Márquez", BirthDate = DateTime.Parse("1927-03-06"), Nationality = "Colombian", Gender = "Male" },
                    new Author { FirstName = "Jane", LastName = "Austen", BirthDate = DateTime.Parse("1775-12-16"), Nationality = "British", Gender = "Female" },
                    new Author { FirstName = "Fyodor", LastName = "Dostoevsky", BirthDate = DateTime.Parse("1821-11-11"), Nationality = "Russian", Gender = "Male" },
                    new Author { FirstName = "Virginia", LastName = "Woolf", BirthDate = DateTime.Parse("1882-01-25"), Nationality = "British", Gender = "Female" },
                    new Author { FirstName = "Mark", LastName = "Twain", BirthDate = DateTime.Parse("1835-11-30"), Nationality = "American", Gender = "Male" },
                    new Author { FirstName = "Agatha", LastName = "Christie", BirthDate = DateTime.Parse("1890-09-15"), Nationality = "British", Gender = "Female" }

                );
                context.SaveChanges();

                context.Book.AddRange(
                    new Book
                    {
                        Title = "To Kill a Mockingbird",
                        YearPublished = 1960,
                        NumPages = 281,
                        Description = "Harper Lee's \"To Kill a Mockingbird\" is a novel set in the American South during the 1930s, addressing themes of racial injustice and moral growth through the eyes of young Scout Finch.",
                        Publisher = "J.B. Lippincott & Co.",
                        DownloadUrl = "ToKillAMockingbird.pdf",
                        FrontPage = "ToKillAMockingbird.jpg",
                        AuthorId = 1
                    },

                    new Book
                    {
                        Title = "Nineteen Eighty-Four",
                        YearPublished = 1949,
                        NumPages = 328,
                        Description = "George Orwell's \"Nineteen Eighty-Four\" is a dystopian novel set in a totalitarian regime, exploring themes of surveillance, propaganda, and individual freedom.",
                        Publisher = "Secker & Warburg",
                        DownloadUrl = "NineteenEightyFour.pdf",
                        FrontPage = "NineteenEightyFour.jpg",
                        AuthorId = 2
                    },

                    new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        YearPublished = 1997,
                        NumPages = 223,
                        Description = "J.K. Rowling's \"Harry Potter and the Philosopher's Stone\" introduces readers to the magical world of Hogwarts School of Witchcraft and Wizardry, following the adventures of young Harry Potter.",
                        Publisher = "Bloomsbury",
                        DownloadUrl = "HarryPotterAndThePhilosophersStone.pdf",
                        FrontPage = "HarryPotterAndThePhilosophersStone.jpg",
                        AuthorId = 3
                    },

                    new Book
                    {
                        Title = "Beloved",
                        YearPublished = 1987,
                        NumPages = 321,
                        Description = "Toni Morrison's \"Beloved\" is a novel that explores the legacy of slavery in America, focusing on Sethe, an escaped slave, and the haunting presence of her deceased daughter.",
                        Publisher = "Alfred A. Knopf",
                        DownloadUrl = "Beloved.pdf",
                        FrontPage = "Beloved.jpg",
                        AuthorId = 4
                    },

                    new Book
                    {
                        Title = "One Hundred Years of Solitude",
                        YearPublished = 1967,
                        NumPages = 417,
                        Description = "Gabriel García Márquez's \"One Hundred Years of Solitude\" is a landmark novel of magical realism, chronicling the Buendía family's generations in the fictional town of Macondo.",
                        Publisher = "Editorial Sudamericana",
                        DownloadUrl = "OneHundredYearsOfSolitude.pdf",
                        FrontPage = "OneHundredYearsOfSolitude.jpg",
                        AuthorId = 5
                    },

                    new Book
                    {
                        Title = "Pride and Prejudice",
                        YearPublished = 1813,
                        NumPages = 279,
                        Description = "Jane Austen's \"Pride and Prejudice\" is a classic romance novel that follows the tumultuous relationship between Elizabeth Bennet and Mr. Darcy in Georgian England.",
                        Publisher = "T. Egerton, Whitehall",
                        DownloadUrl = "PrideAndPrejudice.pdf",
                        FrontPage = "PrideAndPrejudice.jpg",
                        AuthorId = 6
                    },

                    new Book
                    {
                        Title = "Crime and Punishment",
                        YearPublished = 1866,
                        NumPages = 551,
                        Description = "Fyodor Dostoevsky's \"Crime and Punishment\" is a psychological novel that delves into the mind of Rodion Raskolnikov, a young ex-law student who commits a murder and grapples with guilt.",
                        Publisher = "The Russian Messenger",
                        DownloadUrl = "CrimeAndPunishment.pdf",
                        FrontPage = "CrimeAndPunishment.jpg",
                        AuthorId = 7
                    },

                    new Book
                    {
                        Title = "Mrs. Dalloway",
                        YearPublished = 1925,
                        NumPages = 194,
                        Description = "Virginia Woolf's \"Mrs. Dalloway\" is a modernist novel that takes place over a single day in June, exploring the thoughts and experiences of Clarissa Dalloway as she prepares for a party.",
                        Publisher = "Hogarth Press",
                        DownloadUrl = "MrsDalloway.pdf",
                        FrontPage = "MrsDalloway.jpg",
                        AuthorId = 8
                    },

                    new Book
                    {
                        Title = "The Adventures of Huckleberry Finn",
                        YearPublished = 1884,
                        NumPages = 366,
                        Description = "Mark Twain's \"The Adventures of Huckleberry Finn\" follows the journey of Huck Finn and Jim, an escaped slave, as they travel down the Mississippi River and encounter various characters along the way.",
                        Publisher = "Chatto & Windus",
                        DownloadUrl = "TheAdventuresOfHuckleberryFinn.pdf",
                        FrontPage = "TheAdventuresOfHuckleberryFinn.jpg",
                        AuthorId = 9
                    },

                    new Book
                    {
                        Title = "Murder on the Orient Express",
                        YearPublished = 1934,
                        NumPages = 274,
                        Description = "Agatha Christie's \"Murder on the Orient Express\" is a classic detective novel featuring the Belgian detective Hercule Poirot, who investigates a murder that occurs aboard the luxurious train.",
                        Publisher = "Collins Crime Club",
                        DownloadUrl = "MurderOnTheOrientExpress.pdf",
                        FrontPage = "MurderOnTheOrientExpress.jpg",
                        AuthorId = 10
                    }

                );

                context.SaveChanges();

                context.Genre.AddRange(
                    new Genre
                    {
                        // Id = 1
                        GenreName = "Adventure"
                    },
                    new Genre
                    {
                        // Id = 2
                        GenreName = "Autobiography"
                    },
                    new Genre
                    {
                        // Id = 3
                        GenreName = "Goth"
                    },
                    new Genre
                    {
                        // Id = 4
                        GenreName = "Crime Fiction"
                    },
                    new Genre
                    {
                        // Id = 5
                        GenreName = "Comedy"
                    },
                    new Genre
                    {
                        // Id = 6
                        GenreName = "Dystopian"
                    },
                    new Genre
                    {
                        // Id = 7
                        GenreName = "Psychological Fiction"
                    },
                    new Genre
                    {
                        // Id = 8
                        GenreName = "Philosophy"
                    },
                    new Genre
                    {
                        // Id = 9
                        GenreName = "Horror"
                    },
                    new Genre
                    {
                        // Id = 10
                        GenreName = "Historical"
                    },
                    new Genre
                    {
                        // Id = 11
                        GenreName = "Romance Novel"
                    },
                    new Genre
                    {
                        // Id = 12
                        GenreName = "Fantasy"
                    },
                    new Genre
                    {
                        // Id = 13
                        GenreName = "News"
                    },
                    new Genre
                    {
                        // Id = 14
                        GenreName = "Novel"
                    },
                    new Genre
                    {
                        // Id = 15
                        GenreName = "Sci-Fi"
                    }
                );

                context.SaveChanges();

                context.bookGenres.AddRange(
                    new BookGenre
                    {
                        BookId = 1,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 1,
                        GenreId = 10
                    },
                    new BookGenre
                    {
                        BookId = 1,
                        GenreId = 15
                    },
                    new BookGenre
                    {
                        BookId = 2,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 2,
                        GenreId = 5
                    },
                    new BookGenre
                    {
                        BookId = 2,
                        GenreId = 15
                    },
                    new BookGenre
                    {
                        BookId = 3,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 3,
                        GenreId = 15
                    },
                    new BookGenre
                    {
                        BookId = 3,
                        GenreId = 6
                    },
                    new BookGenre
                    {
                        BookId = 4,
                        GenreId = 7
                    },
                    new BookGenre
                    {
                        BookId = 4,
                        GenreId = 8
                    },
                    new BookGenre
                    {
                        BookId = 4,
                        GenreId = 9
                    },
                    new BookGenre
                    {
                        BookId = 5,
                        GenreId = 12
                    },
                    new BookGenre
                    {
                        BookId = 5,
                        GenreId = 13
                    },
                    new BookGenre
                    {
                        BookId = 5,
                        GenreId = 14
                    },
                    new BookGenre
                    {
                        BookId = 6,
                        GenreId = 12
                    },
                    new BookGenre
                    {
                        BookId = 6,
                        GenreId = 13
                    },
                    new BookGenre
                    {
                        BookId = 7,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 7,
                        GenreId = 10
                    },
                    new BookGenre
                    {
                        BookId = 8,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 8,
                        GenreId = 10
                    },
                    new BookGenre
                    {
                        BookId = 8,
                        GenreId = 15
                    },
                    new BookGenre
                    {
                        BookId = 9,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 9,
                        GenreId = 10
                    },
                    new BookGenre
                    {
                        BookId = 10,
                        GenreId = 1
                    },
                    new BookGenre
                    {
                        BookId = 10,
                        GenreId = 2
                    },
                    new BookGenre
                    {
                        BookId = 10,
                        GenreId = 3
                    }
                );

                context.SaveChanges();

                context.Review.AddRange(
                    new Review
                    {
                        BookId = 1,
                        AppUser = "Bob",
                        Rating = 9,
                        Comment = "A riveting journey through time and space, this book keeps you hooked till the very last page."
                    },
                    new Review
                    {
                        BookId = 1,
                        AppUser = "Andrej",
                        Rating = 8,
                        Comment = "Super"
                    },
                    new Review
                    {
                        BookId = 1,
                        AppUser = "Louis",
                        Rating = 7,
                        Comment = "With its intricate plot twists and dynamic characters, this book is a rollercoaster of emotions."
                    },
                    new Review
                    {
                        BookId = 2,
                        AppUser = "John",
                        Rating = 10,
                        Comment = "An immersive experience that transports you to another world, leaving you breathless for more."
                    },
                    new Review
                    {
                        BookId = 2,
                        AppUser = "Stefanija",
                        Rating = 9,
                        Comment = "The author's writing style was engaging and kept me hooked from beginning to end"
                    },
                    new Review
                    {
                        BookId = 2,
                        AppUser = "Travis",
                        Rating = 9,
                        Comment = "Filled with suspense and intrigue, this book will keep you guessing until the final revelation."
                    },
                    new Review
                    {
                        BookId = 3,
                        AppUser = "Alice",
                        Rating = 8,
                        Comment = "While the ending wasn't quite what I was expecting, it was still satisfying and tied up all the loose ends"
                    },
                    new Review
                    {
                        BookId = 3,
                        AppUser = "Ana",
                        Rating = 7,
                        Comment = "I really enjoyed the unique perspective the author brought"
                    },
                    new Review
                    {
                        BookId = 4,
                        AppUser = "Bob",
                        Rating = 5,
                        Comment = "I didn't like the way this book ended. It felt rushed and unsatisfying"
                    },
                    new Review
                    {
                        BookId = 4,
                        AppUser = "Trey",
                        Rating = 7,
                        Comment = "I would highly recommend this book to anyone."
                    },
                    new Review
                    {
                        BookId = 4,
                        AppUser = "Stefan",
                        Rating = 4,
                        Comment = "This book was a disappointment. The plot was predictable and the characters felt flat."
                    },
                    new Review
                    {
                        BookId = 5,
                        AppUser = "Bob",
                        Rating = 9,
                        Comment = "The author's writing style was engaging and kept me hooked from beginning to end."
                    },
                    new Review
                    {
                        BookId = 5,
                        AppUser = "Bojan",
                        Rating = 4,
                        Comment = "This book was a disappointment. The plot was predictable and the characters felt flat."
                    },
                    new Review
                    {
                        BookId = 6,
                        AppUser = "Lionel",
                        Rating = 7,
                        Comment = "I found the characters to be relatable and their struggles felt very real."
                    },
                    new Review
                    {
                        BookId = 6,
                        AppUser = "Penny",
                        Rating = 8,
                        Comment = "The writing style was beautiful and the author really captured the essence."
                    },
                    new Review
                    {
                        BookId = 7,
                        AppUser = "Anne",
                        Rating = 9,
                        Comment = "The author's writing style was engaging and kept me hooked from beginning to end."
                    },
                    new Review
                    {
                        BookId = 7,
                        AppUser = "Penny",
                        Rating = 7,
                        Comment = "The world-building in this book was fantastic and really drew me in."
                    },
                    new Review
                    {
                        BookId = 8,
                        AppUser = "Emily",
                        Rating = 10,
                        Comment = "Overall, this was a fantastic read and I can't wait to see what this author comes up with next."
                    },
                    new Review
                    {
                        BookId = 8,
                        AppUser = "Stefan",
                        Rating = 10,
                        Comment = "I had a hard time getting into this book at first, but once I did, I really enjoyed it."
                    },
                    new Review
                    {
                        BookId = 9,
                        AppUser = "Alice",
                        Rating = 10,
                        Comment = "I didn't like the way this book ended. It felt rushed and unsatisfying."
                    },
                    new Review
                    {
                        BookId = 9,
                        AppUser = "Bob",
                        Rating = 8,
                        Comment = "The themes explored in this book were important and thought-provoking."
                    },
                    new Review
                    {
                        BookId = 10,
                        AppUser = "Mitre",
                        Rating = 8,
                        Comment = "This book was a real page-turner. It kept me guessing until the very end."
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
