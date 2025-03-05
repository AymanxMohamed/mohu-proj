namespace MOHU.Integration.WebApi.Features.Hootsuite.Webhooks.ConversationResolved;


public class ConversationResolvedPayloadEvent
{
    public Conversation Conversation { get; set; }
    public Medium Medium { get; set; }
    public Channel Channel { get; set; }
    public Agent Agent { get; set; }
    public ContactProfile ContactProfile { get; set; }
    public string StatusUpdatedReason { get; set; }
    public string StatusUpdatedComment { get; set; }
    public List<List<Message>> Messages { get; set; }
    public List<ContactAttribute> ContactAttributes { get; set; }
    public List<Note> Notes { get; set; }
    public List<Topic> Topics { get; set; }
}

public class Conversation
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string PreviousStatus { get; set; }
    public string CurrentStatus { get; set; }
}

public class Medium
{
    public string Id { get; set; }
}

public class Channel
{
    public string Id { get; set; }
    public string Name { get; set; }
}

public class Agent
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}

public class ContactProfile
{
    public string Id { get; set; }
    public string MediumContactProfileId { get; set; }
    public string PrimaryIdentifier { get; set; }
    public string SecondaryIdentifier { get; set; }
    public string PictureUrl { get; set; }
}

public class Message
{
    public string Id { get; set; }
    public string Direction { get; set; }
    public string Text { get; set; }
}

public class ContactAttribute
{
    public string Attribute { get; set; }
    public string Value { get; set; }
    public string Source { get; set; }
}

public class Note
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public string CreationUser { get; set; }
    public DateTime CreationTimestamp { get; set; }
}

public class Topic
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}