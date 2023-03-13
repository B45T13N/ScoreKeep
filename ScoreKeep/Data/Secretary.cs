﻿using HandAPI.Models.Interface;

namespace HandAPI.Models;

public class Secretary : Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}