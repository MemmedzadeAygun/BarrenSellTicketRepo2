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
    public class UpdateEventImageCommandHandler : IRequestHandler<UpdateEventImageCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public UpdateEventImageCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task Handle(UpdateEventImageCommand request, CancellationToken cancellationToken)
        {
            if (request.ImageFile == null || request.ImageFile.Length == 0)
            {
                throw new ArgumentException("No image file provided.");
            }

            var events = await _unitOfWork.EventRepository.GetByIdAsync(request.Id);
            if (events is null)
            {
                throw new SellTicketException("Event not found");
            }

            if (events.ImageId.HasValue)
            {
                int imageId = events.ImageId.Value;
                var oldImage = await _unitOfWork.ImageRepository.GetByIdAsync(imageId);
                if (oldImage!=null)
                {
                    await DeleteImage(oldImage.ImageUrl);
                    _unitOfWork.ImageRepository.Remove(oldImage);
                }
            }

            var filePath = await SaveImage(request.ImageFile);
            var newImage = new Image
            {
                ImageUrl = filePath
            };

            await _unitOfWork.ImageRepository.AddAsync(newImage);
            events.ImageId = newImage.Id;
            _unitOfWork.EventRepository.Update(events);
            //await _unitOfWork.Commit();
        }


        private async Task<string> SaveImage(IFormFile imageFile)
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

        private async Task DeleteImage(string imageUrl)
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
