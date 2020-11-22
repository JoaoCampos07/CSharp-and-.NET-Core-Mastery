using MoshHamenadi.Model;
using System;

namespace MoshHamenadi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var photo = new PhotoProcessor();
            photo.Process("somePath");
        }
    }
}
