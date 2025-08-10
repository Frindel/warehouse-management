using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WarehouseManagement.Api.Controllers;

public class BaseController : ControllerBase
{
    private IMediator _mediator = null!;
    
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
}