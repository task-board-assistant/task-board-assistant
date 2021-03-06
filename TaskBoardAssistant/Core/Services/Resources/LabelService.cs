﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskBoardAssistant.Core.Models;
using TaskBoardAssistant.Core.Models.Resources;

namespace TaskBoardAssistant.Core.Services.Resources
{
    public abstract class LabelService : ResourceService
    {
        public async Task<ITaskLabel> GetLabel(string boardName, string labelName)
        {
            var boardService = Factory.BoardService;
            var board = await boardService.GetByName(boardName);
            return GetLabel((ITaskBoard) board, labelName);
        }

        public ITaskLabel GetLabel(ITaskBoard board, string labelName)
        {
            throw new NotImplementedException();
        }

        public override bool SatisfiesFilter(ITaskResource resource, TaskBoardResourceFilter filter)
        {
            return filter.Name.IsNullOrEqualsIgnoreCase(resource.Name);
        }

        public override Task<IEnumerable<ITaskResource>> PerformAction(IEnumerable<ITaskResource> resources, BaseAction action)
        {
            throw new NotImplementedException();
        }

    }
}
