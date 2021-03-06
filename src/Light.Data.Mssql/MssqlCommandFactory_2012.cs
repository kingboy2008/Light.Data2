﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Light.Data.Mssql
{
    class MssqlCommandFactory_2012 : MssqlCommandFactory_2008
    {
        public override CommandData CreateSelectBaseCommand(DataEntityMapping mapping, string customSelect, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)//, bool distinct)
        {
            if (region != null && region.Start > 0) {
                if (order == null) {
                    order = CreatePrimaryKeyOrderExpression(mapping);
                }
                if (order != null) {
                    CommandData commandData = base.CreateSelectBaseCommand(mapping, customSelect, query, order, null, state);
                    commandData.CommandText = string.Format("{0} offset {1} row fetch next {2} rows only", commandData.CommandText, region.Start, region.Size);
                    commandData.InnerPage = true;
                    return commandData;
                }
            }
            return base.CreateSelectBaseCommand(mapping, customSelect, query, order, region, state);
        }

        public override CommandData CreateSelectJoinTableBaseCommand(string customSelect, List<IJoinModel> modelList, QueryExpression query, OrderExpression order, Region region, CreateSqlState state)
        {
            if (region != null && region.Start > 0) {
                if (order != null) {
                    CommandData command = base.CreateSelectJoinTableBaseCommand(customSelect, modelList, query, order, null, state);
                    command.CommandText = string.Format("{0} offset {1} row fetch next {2} rows only", command.CommandText, region.Start, region.Size);
                    command.InnerPage = true;
                    return command;
                }
            }
            return base.CreateSelectJoinTableBaseCommand(customSelect, modelList, query, order, region, state);
        }

        public override CommandData CreateAggregateTableCommand(DataEntityMapping mapping, AggregateSelector selector, AggregateGroupBy groupBy, QueryExpression query, QueryExpression having, OrderExpression order, Region region, CreateSqlState state)
        {
            if (region != null && region.Start > 0) {
                if (order == null) {
                    order = CreateGroupByOrderExpression(groupBy);
                }
                if (order != null) {
                    CommandData command = base.CreateAggregateTableCommand(mapping, selector, groupBy, query, having, order, null, state);
                    command.CommandText = string.Format("{0} offset {1} row fetch next {2} rows only", command.CommandText, region.Start, region.Size);
                    command.InnerPage = true;
                    return command;
                }
            }
            return base.CreateAggregateTableCommand(mapping, selector, groupBy, query, having, order, region, state);
        }
    }
}
