namespace Football.Core.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IPlayerModel
    {
        string FirstName { get; }

        string LastName { get; }

        string Team { get; }
    }
}
