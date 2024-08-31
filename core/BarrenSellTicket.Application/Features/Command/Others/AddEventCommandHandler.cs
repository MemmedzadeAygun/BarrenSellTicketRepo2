using AutoMapper;
using BarrenSellTicket.Application.Extensions;
using BarrenSellTicket.Application.Interfaces;
using BarrenSellTicket.Domain.Entities.Events;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others
{
    public class AddEventCommandHandler : IRequestHandler<AddEventCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public AddEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task Handle(AddEventCommand request, CancellationToken cancellationToken)
        {
            int? imageId = null; 
            var events = _mapper.Map<Event>(request);
            if (request.ImageFile!=null)
            {
                var fullPath = await SaveImageAsync(request.ImageFile);
                var image = _mapper.Map<Image>(new Image { ImageUrl = fullPath, Description = request.Description });
                events.Image = image;

                imageId= image.Id;
            }

            var categories = await _unitOfWork.EventCategoryRepository.GetAllAsync();

            var categoryDictionary = categories.ToDictionary(c => c.Id, c => c.Name);
            if (!categoryDictionary.ContainsKey(request.EventCategoryId))
            {
                throw new SellTicketException("EventCategoryId not found");
            }

            var categoryName= categoryDictionary[request.EventCategoryId];

            if (request.BeginTime>request.EndTime)
            {
                throw new SellTicketException("BeginTime cannot be later than EndTime.");
            }

            var duration = (request.EndTime - request.BeginTime).TotalMinutes;
            events.Duration = (decimal)duration;

            var types = await _unitOfWork.EventTypeRepository.GetAllAsync();
            var typeDictionary = types.ToDictionary(x => x.Id, x => x.Name);

            if (!typeDictionary.ContainsKey(request.EventTypeId))
            {
                throw new SellTicketException("EventTypeId not found");
            }

            var typeName = typeDictionary[request.EventTypeId];
            

            var address = _mapper.Map<Address>(request);
            events.Address= address;


            await _unitOfWork.EventRepository.AddAsync(events);
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

            return fileWithPath;

        }
    }
}
