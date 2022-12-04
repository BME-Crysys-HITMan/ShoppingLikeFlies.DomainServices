using Microsoft.EntityFrameworkCore;
using ShoppingLikeFiles.DataAccessLogic.Context;
using ShoppingLikeFiles.DataAccessLogic.Entities;
using ShoppingLikeFiles.DataAccessLogic.Repository;

namespace DataAccessLogic.UnitTest
{
    public class RepositoryTests
    {

        [Fact]
        public async Task Test1()
        {
            using var repo = GetContext();
            var expected = GetRandomCaff();
            var id = await repo.AddAsync(expected);

            var actual = await repo.GetAsync(id);
            Assert.NotNull(actual);
            Assert.Equal(expected.Tags, actual.Tags);
        }

        [Fact]
        public async Task test2()
        {
            var repo = GetRepo();

            var expected = GetRandomCaff();
            var id = await repo.AddAsync(expected);

            var comment = new Comment { Text = "test", UserId = Guid.NewGuid() };

            await repo.AddCommentAsync(id, comment);

            var actual = await repo.GetAsync(id);

            Assert.NotNull(actual);
            Assert.NotEmpty(actual.Comments);
            Assert.Single(actual.Comments);
        }


        private IGenericRepository<Caff> GetContext()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ShoppingLikeFliesDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            return new GenericRepository<Caff>(new ShoppingLikeFliesDbContext(options));
        }

        private CaffRepository GetRepo()
        {
            var dbName = Guid.NewGuid().ToString();
            var options = new DbContextOptionsBuilder<ShoppingLikeFliesDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;


            return new CaffRepository(new ShoppingLikeFliesDbContext(options));
        }

        private Caff GetRandomCaff()
        {
            var r = new Random();
            var time = DateTime.UtcNow;
            var ct = new DateTime(
                r.Next(1980, 2022),
                r.Next(1, 12),
                r.Next(1, 30),
                r.Next(24),
                r.Next(60),
                0,
                DateTimeKind.Utc);
            var caff = new Caff()
            {
                Caption = Guid.NewGuid().ToString(),
                Comments = new(),
                CreationDateTime = ct,
                Creator = Guid.NewGuid().ToString(),
                FilePath = Guid.NewGuid().ToString(),
                Tags = Guid.NewGuid().ToString().Replace('-', ';'),
                ThumbnailPath = Guid.NewGuid().ToString(),
                Created = time,
                Updated = time,
            };

            return caff;
        }
    }
}
