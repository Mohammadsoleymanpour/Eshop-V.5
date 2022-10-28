using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Convertor;
using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.ViewModels.DynamicLinks;

namespace Application.Services
{
    public class DynamicLinkService : IDynamicLinkService
    {
        private IDynamicRepository _dynamicRepository;

        public DynamicLinkService(IDynamicRepository dynamicRepository)
        {
            _dynamicRepository = dynamicRepository;
        }

        //For Site
        public async Task<LinksForSiteViewModel> GetLinksForFooter()
        {
            var links = await _dynamicRepository.GetLinksByPosition(Domain.Models.Enums.PositionLinks.Footer);
            var res = new LinksForSiteViewModel()
            {
                Links = links,
                Position = Domain.Models.Enums.PositionLinks.Footer
            };
            return res;
        }

        public async Task<int> AddLink(AddLinkViewModel model)
        {
            var addLink = new DynamicLink()
            {
                CreatDate = DateTime.Now,
                Title = model.Title,
                ExpirationDate = model.ExpirationDate.ToGeorgianDateTime(),
                IsDelete = false,
                LinkUrl = model.LinkUrl,
                Position = model.Position,

            };
            return await _dynamicRepository.AddLink(addLink);

        }

        public async Task<EditLinkViewModel> GetViewModelLinkById(int id)
        {
            var res = await _dynamicRepository.GetLinkById(id);
            
            var viewModel = new EditLinkViewModel()
            {
                ExpirationDate = StaticTools.StaticTools.ToFarsi(res.ExpirationDate),
                LinkUrl = res.LinkUrl,
                Id = res.Id,
                Position = res.Position,
                Title = res.Title
            };

            return viewModel;
        }

        public async Task<DynamicLink> GetLinkById(int id)
        {
            return await _dynamicRepository.GetLinkById(id);
        }

        public async Task<bool> Updatelink(EditLinkViewModel model)
        {
            var link = await GetLinkById(model.Id);
            link.Position = model.Position;
           
            link.ExpirationDate = model.ExpirationDate.ToGeorgianDateTime();
            link.Title = model.Title;
            link.LinkUrl = model.LinkUrl;

            return await _dynamicRepository.Updatelink(link);

        }

        public async Task<bool> Deletelink(int id)
        {
            var link = await GetLinkById(id);
            if (link == null)
            {
                return false;

            }

            link.IsDelete = true;
            return await _dynamicRepository.Updatelink(link);

        }

        public async Task<LinksForAdminViewModel> GetLinksForAdmin(LinksForAdminViewModel filter)
        {
            return await _dynamicRepository.GetLinksForAdmin(filter);

        }

        public async Task<LinksForSiteViewModel> GetLinksForHeader()
        {
            var links = await _dynamicRepository.GetLinksByPosition(Domain.Models.Enums.PositionLinks.Header);
            var res = new LinksForSiteViewModel()
            {
                Links = links,
                Position = Domain.Models.Enums.PositionLinks.Header
            };
            return res;
        }
    }
}
