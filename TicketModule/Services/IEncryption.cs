using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketModule.Services
{
    public interface IEncryption
    {
        public string EncryptString(string Message);
    }
}
