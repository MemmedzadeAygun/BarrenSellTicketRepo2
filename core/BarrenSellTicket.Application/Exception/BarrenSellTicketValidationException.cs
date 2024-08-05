using FluentValidation.Results;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarrenSellTicket.Application.Exception
{
    public class BarrenSellTicketValidationException:ApplicationException
    {
        

        public List<ValidationFailure> ValidationFailures { get; set; }
        public BarrenSellTicketValidationException(List<ValidationFailure> validationFailures)
            : base("Validation Exception")
        {
            ValidationFailures = validationFailures;
        }

       
    }
}
