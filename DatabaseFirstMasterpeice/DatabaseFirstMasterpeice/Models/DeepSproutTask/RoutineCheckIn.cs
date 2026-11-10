using System;
using System.Collections.Generic;

namespace DatabaseFirstMasterpeice.Models.DeepSproutTask;

public partial class RoutineCheckIn
{
    public int Id { get; set; }

    public int? RoutineId { get; set; }

    public bool? IsDone { get; set; }

    public virtual OfflineRoutine? Routine { get; set; }
}
