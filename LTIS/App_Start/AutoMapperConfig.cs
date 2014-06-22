using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using LTIS.Lib.Repository;
using LTIS.Models;

namespace LTIS
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Contact, ContactModel>();
        }
    }
}