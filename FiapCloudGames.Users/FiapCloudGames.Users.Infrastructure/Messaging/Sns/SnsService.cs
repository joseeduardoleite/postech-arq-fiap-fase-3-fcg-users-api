using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using FiapCloudGames.Users.Domain.Messaging;
using FiapCloudGames.Users.Domain.Messaging.Sns;
using System.Text.Json;

namespace FiapCloudGames.Users.Infrastructure.Messaging.Sns;

public sealed class SnsService(IAmazonSimpleNotificationService sns) : ISnsService
{
    private readonly string _arn = "arn:aws:sns:us-east-1:252625575525:fcg-fase-3-user-created-topic";

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