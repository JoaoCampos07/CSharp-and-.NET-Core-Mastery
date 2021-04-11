namespace Delegates
{
    using Delegates.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class PhotoProcessor
    {
        /// <summary>
        /// Delegate that can hold a reference to a method or a group of methods that contain the below signature.
        /// </summary>
        /// <param name="photo"></param>
        //public delegate void PhotoFilterHandler(Photo photo);

        public void Process(string path, Action<Photo> filterHandler)
        {
            var photo = Photo.Load(path);

            // this code does not know what filter will be apply...
            filterHandler(photo);

            photo.Save();
        }
    }
}
