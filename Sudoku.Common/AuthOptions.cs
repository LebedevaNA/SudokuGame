﻿using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Sudoku.Common
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
   
        public SymmetricSecurityKey GetSymmetricSecurityKey( )
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Secret));
        }
         
    }
}