using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dog_Site
{
    public class ResultModel
    {
        public String Path { get;}
        public String Dog { get;}

        public ResultModel(String path, String dog)
        {
            Path = path;

            int start_index = dog.LastIndexOf('-') + 1;
            Dog = dog.Substring(start_index).Replace('_', ' ').ToUpper();
        }
    }
}
