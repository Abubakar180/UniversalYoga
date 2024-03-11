using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalYoga.Services.Interface
{
    public interface IToast
    {
        Task Show(string message);
    }
}
