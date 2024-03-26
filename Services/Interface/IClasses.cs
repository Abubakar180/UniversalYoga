using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.Models;

namespace UniversalYoga.Services.Interface
{
    /*An Interface containg Methods for saving and fetching classes's info.*/
    public interface IClasses
    {
        Task<List<YogaClass>> GetAllClassesAsync();
    }
}
