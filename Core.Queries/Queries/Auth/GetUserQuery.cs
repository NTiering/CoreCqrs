using Core.Data.HttpClients;
using Core.Queries.Queries.ApiConsume;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Queries.Queries.Auth
{
    public class GetUserQuery : BaseQuery<GetUserResult> 
    { 
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

}
