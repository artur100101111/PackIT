using PackIt.Shared.Abstractions.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackIt.Application.ItemTypes.Commands.DeleteItemType
{
    public record DeleteItemTypeCommand(long ItemTypeId) : ICommand;
}
