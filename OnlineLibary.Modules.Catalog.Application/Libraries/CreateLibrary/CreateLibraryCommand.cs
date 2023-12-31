﻿using OnlineLibary.Modules.Catalog.Application.Contracts;
using OnlineLibrary.BuildingBlocks.Domain;
using System.Globalization;

namespace OnlineLibary.Modules.Catalog.Application.Libraries.CreateLibrary
{
    public class CreateLibraryCommand : CommandBase<Result>
    {
        public CreateLibraryCommand(

            string libraryName,
            bool isActive,
            string username)
        {

            LibraryName = libraryName;
            IsActive = isActive;
            Username = username;
        }


        public string LibraryName { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
    }
}
