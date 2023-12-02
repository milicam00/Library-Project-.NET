using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.BuildingBlocks.Infrastructure.IInternalCommandsMapper
{
    public interface IInternalCommandsMapper
    {
        string GetName(Type type);

        Type GetType(string name);
    }
}
