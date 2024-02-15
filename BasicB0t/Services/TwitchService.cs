using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicB0t.Services
{
    public interface ITwitchService
    {
        void ConnectUserClient();
        void DisconnectUserClient();

    }
}
