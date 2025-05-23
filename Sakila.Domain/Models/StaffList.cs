﻿namespace Sakila.Domain.Models;

public class StaffList
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? ZipCode { get; set; }

    public string Phone { get; set; } = null!;

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public int Sid { get; set; }
}