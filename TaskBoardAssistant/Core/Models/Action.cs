﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskBoardAssistant.Core.Models
{
    public class BaseAction
    {
        public ResourceAction Type { get; set; }
        public Dictionary<string, string> Params { get; set; }
        public override bool Equals(Object o)
        {
            var other = (BaseAction)o;
            return Type.Equals(other.Type) &&
                ((Params == null && other.Params == null) || Params.SequenceEqual(other.Params));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public enum ResourceAction
    {
        AddCard,
        Move,
        Archive,
        Delete,
        Label,
        Sort,
        Rename,
        Notify
    }
}
