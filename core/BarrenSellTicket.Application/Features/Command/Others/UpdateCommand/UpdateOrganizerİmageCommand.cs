using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Features.Command.Others.UpdateCommand
{
    public class UpdateOrganizerİmageCommand:IRequest
    {
        public int Id { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
