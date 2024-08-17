using AutoMapper;
using BarrenSellTicket.Application.Dtos;
using BarrenSellTicket.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Queries
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var eventCategories = await _unitOfWork.EventCategoryRepository.GetAllAsync();

            if (eventCategories is null)
            {
                throw new NullReferenceException("category is null");
            }

            var categoriesDto = new List<CategoryDto>();

            foreach (var category in eventCategories)
            {
                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Name = category.Name
                };

                categoriesDto.Add(categoryDto);
            }

            return categoriesDto;
        }
    }
}
