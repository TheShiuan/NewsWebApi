﻿namespace NewsWebAPI.Models;

public partial class Employees
{
    public Guid EmployeeId { get; set; }

    public string Name { get; set; }
    public string Account { get; set; }

    public string Password { get; set; }

    public Guid JobId { get; set; }
}
