using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    //Mediator'ı her controller'da enjekte etmek istemiyorum, bu yüzden baseController class'ında enjekte ediyorum.
    //_mediator null ise git servisten IMediator'u getir diyorum. Null değilse tanımlı _mediator'u gönderiyoruz.
    //protected yapma sebebim sadece bu class'ı inherit edenler ulaşabilsin diye.
}
