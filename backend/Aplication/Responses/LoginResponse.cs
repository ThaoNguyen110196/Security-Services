﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Responses
{
   public record  LoginResponse(bool Flag, string Message= null!,string Token = null! ,string RefreshToken = null!);
    
}
