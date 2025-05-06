using System;
using System.ComponentModel.DataAnnotations;

namespace DattingAppApi.Entities;

public class Connection
{
    public required string ConnectionId {get; set;}
    public required string UserName { get; set; } 
}
