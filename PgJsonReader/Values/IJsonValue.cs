using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PgJsonReader
{
    public interface IJsonValue
    {
        Json.Type Type { get; }
    }
}
