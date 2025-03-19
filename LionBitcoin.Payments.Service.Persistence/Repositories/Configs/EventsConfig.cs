using System.ComponentModel.DataAnnotations;

namespace LionBitcoin.Payments.Service.Persistence.Repositories.Configs;

public class EventsConfig
{
    /// <summary>
    /// The number of message retries.(it will try to publish failed message 'FailedRetryCount' times).
    /// the retry will stop when the threshold is reached.
    /// </summary>
    [Required]
    public int FailedRetryCount { get; set; }

    /// <summary>
    /// Failed messages polling delay time in seconds
    /// </summary>
    [Required]
    public int FailedRetryIntervalInSeconds { get; set; }

    /// <summary>
    /// Sent or received succeed message after time span of due, then the message will be deleted at due time.
    /// Time unit is in seconds
    /// </summary>
    [Required]
    public int SucceedMessageExpiredAfterInSeconds { get; set; }

    /// <summary>
    /// Sent or received failed message after time span of due, then the message will be deleted at due time.
    /// Time unit is in seconds
    /// </summary>
    [Required]
    public int FailedMessageExpiredAfterInSeconds { get; set; }
}