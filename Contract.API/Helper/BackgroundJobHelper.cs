using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Contract.API.Helper
{
    public static class BackgroundJobHelper
    {
        #region Background Job

        public static void EnqueueBackgroundJob(Expression<Action> methodCall)
        {
            BackgroundJob.Enqueue(methodCall);
        }

        #endregion

        #region Delay Job

        public static void ScheduleBackgroundJob(Expression<Action> methodCall, DateTimeOffset enqueueAt)
        {
            BackgroundJob.Schedule(methodCall, enqueueAt);
        }

        #endregion

        #region Recurring Job

        public static void ClearRecuringJob(string jobJd)
        {
            RecurringJob.RemoveIfExists(jobJd);
        }

        public static void RecurringMinutely(Expression<Action> methodCall, int minute = 1)
        {
            RecurringJobHelper(methodCall, Cron.MinuteInterval(minute));
        }

        public static void RecurringJobDaily(Expression<Action> methodCall, int hour = 0, int minute = 0)
        {
            RecurringJobHelper(methodCall, Cron.Daily(hour, minute));
        }

        public static void RecurringJobDayInterval(Expression<Action> methodCall, int day = 1)
        {
            RecurringJobHelper(methodCall, Cron.DayInterval(day));
        }

        public static void RecurringJobMonthly(Expression<Action> methodCall, int day = 1, int hour = 0, int minute = 0)
        {
            RecurringJobHelper(methodCall, Cron.Monthly(day, hour, minute));
        }

        public static void RecurringJobMonthInterval(Expression<Action> methodCall, int month)
        {
            RecurringJobHelper(methodCall, Cron.MonthInterval(month));
        }

        public static void RecurringJobHelper(Expression<Action> methodCall, string cronExpression)
        {
            RecurringJob.AddOrUpdate(methodCall, cronExpression);
        }

        #endregion
    }
}