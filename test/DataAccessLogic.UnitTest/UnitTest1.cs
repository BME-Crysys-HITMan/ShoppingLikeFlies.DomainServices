using Microsoft.EntityFrameworkCore;
using ShoppingLikeFiles.DataAccessLogic.Context;
using ShoppingLikeFiles.DataAccessLogic.Entities;

namespace DataAccessLogic.UnitTest;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<ShoppingLikeFliesDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        ShoppingLikeFliesDbContext context = new ShoppingLikeFliesDbContext(options);

        context.Database.EnsureCreated();

        var time = DateTime.UtcNow;

        var response = context.Caff!.Add(new ShoppingLikeFiles.DataAccessLogic.Entities.Caff
        {
            Caption = "Hello",
            Comments = new(),
            CreationDateTime = new DateTime(2020, 1, 1, 1, 1, 0, DateTimeKind.Utc),
            Creator = "Feri",
            FilePath = "path/to/geci",
            ThumbnailPath = "faszom/ba/mar",
            Tags = "feri;bela;geci",
            Created = time,
            Updated = time
        });
        context.SaveChanges();

        var id = response.Entity.Id;

        var actual = context.Caff.First(x => x.Caption == "Hello");
        Assert.Equal(id, actual.Id);
        Assert.Equal("feri;bela;geci", actual.Tags);
    }

    [Fact]
    public async Task Test2()
    {
        var context = GetContext();
        await context.Database.EnsureCreatedAsync();

        var first = GetRandomCaff();
        var firstId = await context.Caff!.AddAsync(first);
        context.SaveChanges();
        var second = GetRandomCaff();
        var secondId = await context.Caff!.AddAsync(second);
        context.SaveChanges();

        Assert.Equal(2, context.Caff.Count());

        var expected1 = await context.Caff.FindAsync(firstId.Entity.Id);
        Assert.NotNull(expected1);
        Assert.Equal(first.Caption, expected1.Caption);

        var expected2 = await context.Caff.FindAsync(secondId.Entity.Id);
        Assert.NotNull(expected2);
        Assert.Equal(second.Caption, expected2.Caption);
    }

    [Fact]
    public async Task Test4()
    {
        var context = GetContext();

        var caff = GetRandomCaff();

        var e = await context.Caff.AddAsync(caff);
        context.SaveChanges();

        var item = await context.Caff.FindAsync(e.Entity.Id);

        Assert.NotNull(item);
        var time = DateTime.UtcNow;
        item.Comments.Add(new Comment { Text = "Fasza", UserId = Guid.NewGuid(), Created = time, Updated = time });
        await context.SaveChangesAsync();

        Assert.NotEmpty(context.Comment);
        Assert.Equal(1, context.Comment.Count());

        var com = await context.Comment.FirstAsync();

        Assert.Equal(item.Tags, com.Caff.Tags);
    }


    private ShoppingLikeFliesDbContext GetContext()
    {
        var dbName = Guid.NewGuid().ToString();
        var options = new DbContextOptionsBuilder<ShoppingLikeFliesDbContext>()
            .UseInMemoryDatabase(dbName)
            .Options;

        return new ShoppingLikeFliesDbContext(options);
    }

    private Caff GetRandomCaff()
    {
        var r = new Random();
        var time = DateTime.UtcNow;
        var ct = new DateTime(r.Next(1980, 2022), r.Next(1, 13), r.Next(1, 32), r.Next(25), r.Next(61), 0, DateTimeKind.Utc);
        var caff = new Caff()
        {
            Caption = Guid.NewGuid().ToString(),
            Comments = new(),
            CreationDateTime = ct,
            Creator = Guid.NewGuid().ToString(),
            FilePath = Guid.NewGuid().ToString(),
            Tags = Guid.NewGuid().ToString().Replace('-', ';'),
            ThumbnailPath= Guid.NewGuid().ToString(),
            Created=time,
            Updated=time,
        };

        return caff;
    }
}