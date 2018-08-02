﻿using Fan.Blog.IntegrationTests.Base;
using Fan.Blog.Data;
using Fan.Blog.Helpers;
using Fan.Blog.Models;
using Fan.Blog.Services;
using Fan.Medias;
using Fan.Settings;
using Fan.Shortcodes;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;

namespace Fan.Blog.IntegrationTests.Base
{
    /// <summary>
    /// Blog integration test base.
    /// </summary>
    public class BlogServiceIntegrationTestBase : BlogIntegrationTestBase
    {
        protected BlogService _blogSvc;
        protected Mock<ISettingService> _settingSvcMock;
        protected Mock<IMediaService> _mediaSvcMock;
        protected ILoggerFactory _loggerFactory;

        public BlogServiceIntegrationTestBase()
        {
            // repos
            var catRepo = new SqlCategoryRepository(_db);
            var tagRepo = new SqlTagRepository(_db);
            var postRepo = new SqlPostRepository(_db);

            // SettingService mock
            _settingSvcMock = new Mock<ISettingService>();
            _settingSvcMock.Setup(svc => svc.GetSettingsAsync<CoreSettings>()).Returns(Task.FromResult(new CoreSettings()));
            _settingSvcMock.Setup(svc => svc.GetSettingsAsync<BlogSettings>()).Returns(Task.FromResult(new BlogSettings()));

            // MediaService mock
            _mediaSvcMock = new Mock<IMediaService>();

            // Cache
            var serviceProvider = new ServiceCollection().AddMemoryCache().AddLogging().BuildServiceProvider();
            var memCacheOptions = serviceProvider.GetService<IOptions<MemoryDistributedCacheOptions>>();
            var cache = new MemoryDistributedCache(memCacheOptions);

            // LoggerFactory
            _loggerFactory = serviceProvider.GetService<ILoggerFactory>();

            // Mapper
            var mapper = BlogUtil.Mapper;

            // Shortcode
            var shortcodeSvc = new Mock<IShortcodeService>();

            var loggerBlogSvc = _loggerFactory.CreateLogger<BlogService>();
            var mediatorMock = new Mock<IMediator>();

            _blogSvc = new BlogService(_settingSvcMock.Object, catRepo, postRepo, tagRepo, cache, 
                loggerBlogSvc, mapper, shortcodeSvc.Object, mediatorMock.Object);
        }
    }
}
