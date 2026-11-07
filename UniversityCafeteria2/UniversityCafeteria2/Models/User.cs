using System;
using System.Collections.Generic;

namespace UniversityCafeteria2.Models;

public partial class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;
}
