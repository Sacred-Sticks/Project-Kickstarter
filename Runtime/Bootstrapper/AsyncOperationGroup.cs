﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kickstarter.Bootstrapper
{
    public readonly struct AsyncOperationGroup
    {
        public List<AsyncOperation> Operations { get; }

        public float Progress => Operations.Count == 0 ? 0 : Operations.Average(o => o.progress);
        public bool IsDone => Operations.All(o => o.isDone);

        public AsyncOperationGroup(int initialCapacity)
        {
            Operations = new List<AsyncOperation>(initialCapacity);
        }
    }
}

