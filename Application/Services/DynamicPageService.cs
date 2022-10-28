using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.Models.Enums;
using Domain.ViewModels.DynamicPage;

namespace Application.Services
{
    public class DynamicPageService:IDynamicPageService
    {
        private IDynamicPageRepository _dynamicPageRepository;

        public DynamicPageService(IDynamicPageRepository dynamicPageRepository)
        {
            _dynamicPageRepository = dynamicPageRepository;
        }


       

        public Task<FilterDynamicPageViewModel> GetAllPagesForAdmin(FilterDynamicPageViewModel filter)
        {
            return _dynamicPageRepository.GetAllPagesForAdmin(filter);
        }
        public async Task<int> AddDynamicPage(DynamicPageViewModelAdmin model)
        {
            var addLink = new DynamicLink()
            {
                IsDelete = false,
                Title = model.Title,
                CreatDate = DateTime.Now,
                ExpirationDate = null,
                LinkUrl = model.UrlLink,
                Position = PositionLinks.Footer,

            };

            var linkId = await _dynamicPageRepository.AddDynamicLink(addLink);
            var addPage = new DynamicPage()
            {
                Content = model.Content,
                CreatDate = DateTime.Now,
                IsDelete = false,
                Title = model.Title,
                LinkId = linkId
                
            };
            var id= await _dynamicPageRepository.AddDynamicPage(addPage);

           
            return id;
        }

       

        public async Task<bool> DeleteDynamicPage(int id)
        {
            return await _dynamicPageRepository.DeleteDynamicPage(id);

        }

        public async Task<DynamicPage> GetPageByTitle(string title)
        {
            return await _dynamicPageRepository.GetPageByTitle(title);
        }

        public async Task<List<DynamicPage>> GetAllPagesForSite()
        {
            return await _dynamicPageRepository.GetAllPagesForSite();
        }
    }
}
