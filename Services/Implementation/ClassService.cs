using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversalYoga.Helpers;
using UniversalYoga.Models;
using UniversalYoga.Services.Interface;

namespace UniversalYoga.Services.Implementation
{
    public class ClassService: IClasses
    {
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });

        public async Task<List<YogaClass>> GetAllClassesAsync()
        {
            return (await firebase.Child("classes").OnceAsync<YogaClass>()).Select(f => new YogaClass
            {
                Id = f.Object.Id,
                CourseId = f.Object.CourseId,
                TeacherName = f.Object.TeacherName,
                Date = f.Object.Date,
                Description = f.Object.Description,
                Key = f.Key,
            }).ToList();
        }
    }
}
