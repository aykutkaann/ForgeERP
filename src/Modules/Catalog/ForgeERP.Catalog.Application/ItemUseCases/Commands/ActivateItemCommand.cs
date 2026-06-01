using ForgeERP.Catalog.Application.ItemUseCases.DTOs;
using ForgeERP.SharedKernel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForgeERP.Catalog.Application.ItemUseCases.Commands
{

    public record ActivateItemCommand(Guid Id) : IRequest<Result<ItemDto>>;
}
