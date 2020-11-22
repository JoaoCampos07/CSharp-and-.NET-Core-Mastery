using MoshHamenadi.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoshHamenadi
{
    public class PhotoProcessor
    {
        public void Process(string path)
        {
            var photo = Photo.Load(path);
        }
    }
}
