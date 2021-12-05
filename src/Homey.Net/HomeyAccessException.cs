using System;

namespace Homey.Net
{
    public  class HomeyAccessException : Exception
    {
        public HomeyAccessException(string message)
        :base(message)
        {

        }

    }
}
