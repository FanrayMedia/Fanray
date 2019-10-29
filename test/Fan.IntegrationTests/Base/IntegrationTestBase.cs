﻿using Fan.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Fan.IntegrationTests.Base
{
    /// <summary>
    /// Base class for all integration tests, it provides the <see cref="FanDbContext"/> with 
    /// in-memory database.
    /// </summary>
    /// <remarks>
    /// When it comes to test with an in-memory database, there are two choices, the 
    /// EF Core In-Memory Database Provider (Microsoft.EntityFrameworkCore.InMemory)
    /// or the SQLite Database Provider (Microsoft.EntityFrameworkCore.Sqlite) with the SQLite 
    /// in-memory mode. However EF Core provider does not enforce any integrity like a relational 
    /// database, for example, the Meta table cannot have duplicate keys, it doesn't enforce that.
    /// 
    /// For more info https://docs.microsoft.com/en-us/ef/core/miscellaneous/testing/index
    /// </remarks>
    public class IntegrationTestBase : IDisposable
    {
        /// <summary>
        /// A <see cref="FanDbContext"/> built with Sqlite in-memory mode.
        /// </summary>
        protected readonly FanDbContext _db;
        protected readonly ILoggerFactory _loggerFactory;
        protected readonly MemoryDistributedCache _cache;

        public IntegrationTestBase()
        {
            var serviceProvider = new ServiceCollection().AddMemoryCache().AddLogging().BuildServiceProvider();
            _loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            var memCacheOptions = serviceProvider.GetService<IOptions<MemoryDistributedCacheOptions>>();
            _cache = new MemoryDistributedCache(memCacheOptions);

            _db = GetContextWithSqlite();
        }

        public void Dispose()
        {
            _db.Database.EnsureDeleted(); // important, otherwise SeedTestData is not erased
            _db.Dispose();
        }

        /// <summary>
        /// Returns <see cref="FanDbContext"/> with SQLite Database Provider in-memory mode.
        /// </summary>
        private FanDbContext GetContextWithSqlite()
        {
            var connection = new SqliteConnection() { ConnectionString = "Data Source=:memory:" };
            connection.Open();

            var builder = new DbContextOptionsBuilder<FanDbContext>();
            builder.UseSqlite(connection);

            var context = new FanDbContext(builder.Options, _loggerFactory);
            context.Database.EnsureCreated();

            return context;
        }

        /// <summary>
        /// Returns <see cref="CoreDbContext"/> with Entity Framework Core In-Memory Database.
        /// </summary>
        private FanDbContext GetContextWithEFCore()
        {
            var _options = new DbContextOptionsBuilder<FanDbContext>().UseInMemoryDatabase("FanInMemDb").Options;
            return new FanDbContext(_options, _loggerFactory);
        }
    }
}
