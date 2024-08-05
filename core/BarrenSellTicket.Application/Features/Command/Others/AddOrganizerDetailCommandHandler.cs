using AutoMapper;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddOrganizerDetailCommandHandler : IRequestHandler<AddOrganizerDetailCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public AddOrganizerDetailCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task Handle(AddOrganizerDetailCommand request, CancellationToken cancellationToken)
        {
            int? imageId = null;
            var organizerDetail = _mapper.Map<OrganizerDetail>(request);
            if (request.ImageFile!=null)
            {    
               var imageUrl = await SaveImageAsync(request.ImageFile);

                //var image = new Image { ImageUrl = imageUrl };
                var image = _mapper.Map<Image>(new Image { ImageUrl = imageUrl });
                organizerDetail.ProfileImage = image;
               //await _unitOfWork.ImageRepository.AddAsync(image);


               imageId =image.Id;
                
            }

            //Create Address
            var address = _mapper.Map<Address>(request);
            //await _unitOfWork.AddressRepository.AddAsync(address);
         

           
            organizerDetail.Address = address;
          
           

            await _unitOfWork.OrganizerDetailRepository.AddAsync(organizerDetail);

            await _unitOfWork.Commit();
        }

        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile is null || imageFile.Length == 0)
            {
                throw new ArgumentException("Image file is not provided.");

            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Uploads");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(imageFile.FileName);
            var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            if (!allowedExtensions.Contains(ext))
            {
                throw new ArgumentException("Only .jpg, .png, .jpeg extensions are allowed.");
            }

            string uniqueString = Guid.NewGuid().ToString();
            var newFileName = uniqueString + ext;
            var fileWithPath = Path.Combine(path, newFileName);
            var stream = new FileStream(fileWithPath, FileMode.Create);
            await imageFile.CopyToAsync(stream);
            stream.Close();

            return newFileName;

        }
    }
}
