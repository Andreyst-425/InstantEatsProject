using Microsoft.EntityFrameworkCore;
using System;

namespace InstantEatService.Tests.TestServices
{
    class TestsRepositoryService
    {
        public static InstantEatDbContext GetClearDataBase()
        {
            var options = new DbContextOptionsBuilder<InstantEatDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new InstantEatDbContext(options);
        }
    }
}
