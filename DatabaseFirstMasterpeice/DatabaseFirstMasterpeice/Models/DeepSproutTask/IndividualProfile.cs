using System;
using System.Collections.Generic;

namespace DatabaseFirstMasterpeice.Models.DeepSproutTask;

public partial class IndividualProfile
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string? Goal { get; set; }

    public virtual Person? Person { get; set; }
}
