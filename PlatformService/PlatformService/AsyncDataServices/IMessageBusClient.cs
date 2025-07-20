using PlatformService.Repository.DTOs;

namespace PlatformService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        public void PublishNewPlatform(PlatformPublishedDTO platformPublishedDto);
    }
}
