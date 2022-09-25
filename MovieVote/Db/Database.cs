using JetBrains.Annotations;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MovieVote.Api.Discord.Models;
using MovieVote.Api.Tmdb.Models;

namespace MovieVote.Db;

public static class Database
{
    private static readonly DatabaseContext Ctx = new();
    
    
}