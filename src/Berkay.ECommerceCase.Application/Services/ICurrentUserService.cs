﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Berkay.ECommerceCase.Application.Services
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}
