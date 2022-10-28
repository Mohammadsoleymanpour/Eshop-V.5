using Application.Interface;
using Domain.Interfaces;
using Domain.Models.Common;
using Domain.Models.Enums;
using Domain.ViewModels.SocialMedia;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SocialMediaService : ISocialMediaService
    {
        #region Injections

        private ISocialMediaRepository _socialMediaRepository;

        public SocialMediaService(ISocialMediaRepository socialMediaRepository)
        {
            _socialMediaRepository = socialMediaRepository;
        }

        #endregion


        public async Task<bool> EditSocialMediaLink(EditSocialMediaViewModel media)
        {
            var social = await _socialMediaRepository.GetMediaById(media.Id);
            social.PlatForm = media.Platform;
            social.Url = media.Url;

            try
            {
                await _socialMediaRepository.Update(social);
                return true;
            }
            catch
            {

                return false;
            }
        }

        public async Task<FilterSocialMediaForAdminViewModel> GetAllMediasForAdmin(FilterSocialMediaForAdminViewModel filter)
        {
            var query = _socialMediaRepository.GetAllSocialMediaForAdmin();

            #region Filter

            switch (filter.Platform)
            {
                case FilterPlatform.All:
                    break;
                case FilterPlatform.Twitter:
                    {
                        query = query.Where(s=>s.PlatForm == Domain.Models.Enums.SocialMediaPlatform.Twitter);
                        break;
                    }
                    
                case FilterPlatform.Telegram:
                    {
                        query = query.Where(s => s.PlatForm == Domain.Models.Enums.SocialMediaPlatform.Telegram);
                        break;
                    }
                case FilterPlatform.FaceBook:
                    {
                        query = query.Where(s => s.PlatForm == Domain.Models.Enums.SocialMediaPlatform.FaceBook);
                        break;
                    }
                case FilterPlatform.Instagram:
                    {
                        query = query.Where(s => s.PlatForm == Domain.Models.Enums.SocialMediaPlatform.Instagram);
                        break;
                    }

            }

            #endregion


            await filter.Paging(query);
            return filter;
        }

        public async Task<bool> DeleteSocial(int id)
        {
            return await _socialMediaRepository.Delete(id);
        }

        public async Task<int> AddSocialMedia(AddSocialMediaLinkViewModel model)
        {
            var media = new SocialMedia()
            {
                CreatDate = DateTime.Now,
                IsDelete = false,
                PlatForm = (Domain.Models.Enums.SocialMediaPlatform)model.PlatForm,
                Url = model.Url
            };
            return await _socialMediaRepository.AddSocialMedia(media);
        }

        public async Task<EditSocialMediaViewModel> GetMediaById(int id)
        {
            var model = await _socialMediaRepository.GetMediaById(id);
            var res = new EditSocialMediaViewModel() 
            {
                Id = model.Id,
                Platform = model.PlatForm,
                Url = model.Url
            };

            return res;
        }

        public async Task<bool> IsMediaAlreadyHasUrl(SocialMediaPlatform platform)
        {
            return await _socialMediaRepository.IsMediaAlreadyHasUrl(platform);
        }

        public Task<List<SocialMedia>> GeMediaForShow()
        {
            return _socialMediaRepository.GeMediaForShow();
        }
    }
}
