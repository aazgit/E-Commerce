using UniversalIntegrationMiddleware.Models;

namespace UniversalIntegrationMiddleware.ViewModels
{
    public class DashboardViewModel
    {
        public int ActiveConnectorsCount { get; set; }
        public int ActiveFlowsCount { get; set; }
        public int TotalJobsToday { get; set; }
        public int FailedJobsCount { get; set; }
        public List<JobRun> RecentJobs { get; set; } = new List<JobRun>();
        public List<ChannelConnection> RecentConnectors { get; set; } = new List<ChannelConnection>();
    }
}
