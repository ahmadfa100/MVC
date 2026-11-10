using System;
using System.Collections.Generic;

namespace DatabaseFirstMasterpeice.Models.DeepSproutTask;

public partial class ConsentRecord
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public bool? IsAccepted { get; set; }

    public virtual Person? Person { get; set; }
}
