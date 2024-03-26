using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalYoga.Services.Interface
{
    /*An Interface containg Show Method for showing a message.*/
    public interface IToast
    {
        Task Show(string message);
    }
}
