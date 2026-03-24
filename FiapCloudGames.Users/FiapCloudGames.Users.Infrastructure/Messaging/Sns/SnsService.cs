using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using FiapCloudGames.Users.Domain.Messaging;
using FiapCloudGames.Users.Domain.Messaging.Sns;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FiapCloudGames.Users.Infrastructure.Messaging.Sns;

public sealed class SnsService(
    IAmazonSimpleNotificationService sns,
    IConfiguration configuration) : ISnsService
{
    private readonly string _arn = configuration["Aws:Sns:UserCreatedTopicArn"]!;

    public async Task PublishUserCreatedAsync(UserCreatedEvent userEvent, CancellationToken cancellationToken)
    {
        var message = new PublishRequest
        {
            TopicArn = _arn,
            Message = JsonSerializer.Serialize(userEvent)
        };

        await sns.PublishAsync(message, cancellationToken);
    }
}