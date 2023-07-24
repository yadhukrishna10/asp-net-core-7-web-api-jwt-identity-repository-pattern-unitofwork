using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace  HireMeNowWebApi.Exceptions
{
    [Serializable]
    public class UserAlreadyExistException:Exception
    {
        public UserAlreadyExistException()
        {
            
        }
        public UserAlreadyExistException(string email)
             : base("User Already Exist with this email "+email)
        {

        }

		public UserAlreadyExistException(string message, Exception inner)
	 : base(message, inner) { }
	}
}
