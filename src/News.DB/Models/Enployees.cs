﻿namespace NewsWebAPI.Models;

public partial class Enployees
{
    public Guid EmployeesId { get; set; }

    public string Name { get; set; }

    public Guid JobId { get; set; }

    public string Account { get; set; }

    public string Password { get; set; }
}
