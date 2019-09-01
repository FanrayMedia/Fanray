using Fan.Blog.Enums;
using Fan.Blog.Helpers;
using Fan.Blog.Models.View;
using Fan.Blog.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fan.WebApp.Manage.Admin
{
    public class PagesModel : PageModel
    {
        private readonly IPageService pageService;

        public PagesModel(IPageService pageService)
        {
            this.pageService = pageService;
        }

        public string PagesJson { get; set; }
        public int ParentId { get; set; }

        /// <summary>
        /// Displays either a list of parents or a parent with its child pages.
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public async Task OnGetAsync(int parentId)
        {
            var pageVMs = await GetPageVMsAsync(parentId);
            PagesJson = JsonConvert.SerializeObject(pageVMs);
            ParentId = parentId;
        }

        public async Task OnDeleteAsync(int pageId)
        {
            await pageService.DeleteAsync(pageId);
        }

        /// <summary>
        /// Returns a list of <see cref="PageAdminVM"/>, when <paramref name="parentId"/> is present 
        /// the returned list contains the parent and its children, otherwise a list of parents are
        /// returned.
        /// </summary>
        /// <param name="parentId"></param>
        /// <remarks>
        /// When a page or its parent is draft, its PageLink is null.
        /// </remarks>
        private async Task<List<PageAdminVM>> GetPageVMsAsync(int parentId)
        {
            var pageVMs = new List<PageAdminVM>();
            IList<Blog.Models.Page> pages;
            bool isChild = false;
            Fan.Blog.Models.Page parent = null;

            if (parentId <= 0)
            {
                pages = await pageService.GetParentsAsync(withChildren: true);
            }
            else
            {
                parent = await pageService.GetAsync(parentId);
                pages = parent.Children;
                isChild = true;

                pageVMs.Add(new PageAdminVM
                {
                    Id = parent.Id,
                    Title = parent.Title,
                    Date = parent.CreatedOn.ToString("yyyy-MM-dd"),
                    Author = parent.User.DisplayName,
                    EditLink = BlogRoutes.GetPageEditLink(parent.Id),
                    IsDraft = parent.Status == EPostStatus.Draft,
                    PageLink = parent.Status == EPostStatus.Draft ? null :
                            $"{Request.Scheme}://{Request.Host}" + BlogRoutes.GetPageRelativeLink(parent.Slug),
                    ChildCount = parent.Children.Count,
                });
            }

            foreach (var page in pages)
            {
                var pageLink = parent != null && parent.Status == EPostStatus.Published?
                        $"{Request.Scheme}://{Request.Host}" + BlogRoutes.GetPageRelativeLink(parent.Slug, page.Slug) :
                        $"{Request.Scheme}://{Request.Host}" + BlogRoutes.GetPageRelativeLink(page.Slug);

                pageLink = page.Status == EPostStatus.Draft || (parent != null && parent.Status == EPostStatus.Draft) ? null : pageLink;

                pageVMs.Add(new PageAdminVM
                {
                    Id = page.Id,
                    Title = page.Title,
                    Date = page.CreatedOn.ToString("yyyy-MM-dd"),
                    Author = page.User.DisplayName,
                    ChildrenLink = !isChild && page.Children.Count > 0 ? $"{Request.Path}/{page.Id}" : "",
                    EditLink = BlogRoutes.GetPageEditLink(page.Id),
                    PageLink = pageLink,
                    IsChild = isChild,
                    IsDraft = page.Status == EPostStatus.Draft,
                    ChildCount = isChild ? 0 : page.Children.Count,
                });
            }

            return pageVMs;
        }     
    }
}