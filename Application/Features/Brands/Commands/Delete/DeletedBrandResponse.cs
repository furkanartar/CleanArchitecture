using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.Delete;

public class DeletedBrandResponse
{
    // delete sonucu hangi verirleri döneceğimizi belirtiyoruz
    public Guid Id { get; set; }
}
