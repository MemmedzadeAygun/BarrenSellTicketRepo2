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

namespace BarrenSellTicket.Application.Features.Command.Others.UpdateCommand
{
    public class UpdateOrganizerImageCommandHandler : IRequestHandler<UpdateOrganizerİmageCommand>
    {
        private readonly IUnitOfWork _unitOfwork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public UpdateOrganizerImageCommandHandler(IUnitOfWork unitOfwork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfwork = unitOfwork;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task Handle(UpdateOrganizerİmageCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                throw new ArgumentException("No image file provided.");
            }

            var organizer = await _unitOfwork.OrganizerDetailRepository.GetByIdAsync(request.Id);
            if (organizer == null)
            {
                throw new ArgumentException("Organizer not found");
            }

            if (organizer.ImageId.HasValue)
            {
                int imageId = organizer.ImageId.Value;
                var oldImage = await _unitOfwork.ImageRepository.GetByIdAsync(imageId);
                if (oldImage != null)
                {
                    await DeleteImageAsync(oldImage.ImageUrl);
                    _unitOfwork.ImageRepository.Remove(oldImage);
                }
            }

            var filePath = await SaveImageAsync(request.ImageFile);

            var newImage = new Image
            {
                ImageUrl = filePath
            };

            await _unitOfwork.ImageRepository.AddAsync(newImage);

            organizer.ImageId = newImage.Id;
            _unitOfwork.OrganizerDetailRepository.Update(organizer);
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

            return fileWithPath;

        }

        private async Task DeleteImageAsync(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                throw new ArgumentException("Image URL is null or empty.");
            }

            if (File.Exists(imageUrl))
            {
                File.Delete(imageUrl);
            }
        }
    }
}
