using System;
using System.Collections.Generic;

namespace DatabaseFirstMasterpeice.Models.DeepSproutTask;

public partial class Person
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ConsentRecord> ConsentRecords { get; set; } = new List<ConsentRecord>();

    public virtual IndividualProfile? IndividualProfile { get; set; }

    public virtual ICollection<OfflineRoutine> OfflineRoutines { get; set; } = new List<OfflineRoutine>();
}
