﻿using DataAccess.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class FindMangaQuerie:IRequest<MangaDisplayModel>
    {
        public string Id { get; set; }
        public FindMangaQuerie(string id)
        {
            Id = id;
        }
    }
}