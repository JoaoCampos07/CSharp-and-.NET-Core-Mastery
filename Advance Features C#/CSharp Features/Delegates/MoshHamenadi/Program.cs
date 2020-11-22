using MoshHamenadi.Model;
using System;

namespace MoshHamenadi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var photoProcessor = new PhotoProcessor();

            var filters = new PhotoFilters();
            // create delegate
            Action<Photo> filterHandler = filters.ApplyBrightness;
            filterHandler += filters.ApplyContrast;
            filterHandler += RemoveRedEye;

            photoProcessor.Process("photo.png", filterHandler);
        }
        
        /// <summary>
        /// This function was not release with the Nuget or framework....
        /// But since is extensible, I, was user of the obj can create my own.
        /// </summary>
        /// <param name="photo"></param>
        public static void RemoveRedEye(Photo photo)
        {
            Console.WriteLine("Apply Remove Red eye.");
        }
    }
}
