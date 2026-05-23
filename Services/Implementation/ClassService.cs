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
    /*ClassService is a class inharinting IClasses Interface containing its Methods.*/
    public class ClassService: IClasses
    {
        /*FirebaseClient containing DatabaseLink and DatabaseSecret for accessing firebase realtime database. 
         FirebaseDatabase.net plugin must be installed from nuget packages.*/
        FirebaseClient firebase = new FirebaseClient(FirebaseWebApi.DatabaseLink, new FirebaseOptions
        {
            AuthTokenAsyncFactory = () => Task.FromResult(FirebaseWebApi.DatabaseSecret)
        });
        /*Method implementation inherited from IClasses Interface.
         Task is a awaitable funtions. Meaning the can be run in background. They must have a return value*/
        public async Task<List<YogaClass>> GetAllClassesAsync()
        {
            /*Returning the List of YogaClass from realtime firebase DB by accessing the table name and also what properties it should return in model.*/
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
