using Firebase.Database;
using Firebase.Database.Query;
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
    public class CourseService : ICourses
    {
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });

        public async Task<List<YogaCourse>> GetAllCoursesAsync()
        {
            return (await firebase.Child("courses").OnceAsync<YogaCourse>()).Select(f => new YogaCourse
            {
                Id = f.Object.Id,
                CourseName = f.Object.CourseName,
                DayOfWeek = f.Object.DayOfWeek,
                TimeOfCourse = f.Object.TimeOfCourse,
                Capacity = f.Object.Capacity,
                Duration = f.Object.Duration,
                PricePerClass = f.Object.PricePerClass,
                Description = f.Object.Description,
                TypeOfYoga = f.Object.TypeOfYoga,
                status = f.Object.status,
                Key = f.Key,
            }).ToList();
        }

        public async Task<bool> BookCourse(YogaCourse model)
        {
            try
            {
                await firebase.Child("booked").PostAsync(new YogaCourse()
                {
                    Id = model.Id,
                    CourseName = model.CourseName,
                    DayOfWeek = model.DayOfWeek,
                    TimeOfCourse = model.TimeOfCourse,
                    Capacity = model.Capacity,
                    Duration = model.Duration,
                    PricePerClass = model.PricePerClass,
                    Description = model.Description,
                    TypeOfYoga = model.TypeOfYoga,
                    BookedBy = model.BookedBy
                });
                return true;
            }
            catch(Exception ex)
            {
                return false;

            }
        }
        public async Task<List<YogaCourse>> GetBookedCoursesAsync()
        {
            return (await firebase.Child("booked").OnceAsync<YogaCourse>()).Select(f => new YogaCourse
            {
                Id = f.Object.Id,
                CourseName = f.Object.CourseName,
                DayOfWeek = f.Object.DayOfWeek,
                TimeOfCourse = f.Object.TimeOfCourse,
                Capacity = f.Object.Capacity,
                Duration = f.Object.Duration,
                PricePerClass = f.Object.PricePerClass,
                Description = f.Object.Description,
                TypeOfYoga = f.Object.TypeOfYoga,
                BookedBy = f.Object.BookedBy,
                Key = f.Key
            }).ToList();
        }

        //public async Task<bool> BookCourse(YogaCourse model)
        //{
        //    var result = await firebase.Child("booked").PostAsync(new YogaCourse()
        //    {
        //        Id = model.Id,
        //        CourseName = model.CourseName,
        //        DayOfWeek = model.DayOfWeek,
        //        TimeOfCourse = model.TimeOfCourse,
        //        Capacity = model.Capacity,
        //        Duration = model.Duration,
        //        PricePerClass = model.PricePerClass,
        //        Description = model.Description,
        //        TypeOfYoga = model.TypeOfYoga,
        //        BookedBy = model.BookedBy,
        //    });

        //    if (result.Object != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;

        //    }
        //}
    }
}
