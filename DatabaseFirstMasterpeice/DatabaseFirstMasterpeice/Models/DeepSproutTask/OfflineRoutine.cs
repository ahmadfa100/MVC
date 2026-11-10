using System;
using System.Collections.Generic;

namespace DatabaseFirstMasterpeice.Models.DeepSproutTask;

public partial class OfflineRoutine
{
    public int Id { get; set; }

    public int? PersonId { get; set; }

    public string? Title { get; set; }

    public virtual Person? Person { get; set; }

    public virtual ICollection<RoutineCheckIn> RoutineCheckIns { get; set; } = new List<RoutineCheckIn>();
}
