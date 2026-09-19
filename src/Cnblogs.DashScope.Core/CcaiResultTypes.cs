namespace Cnblogs.DashScope.Core;

/// <summary>
/// Well-known <c>resultTypes</c> values for LingQue CCAI AnalyzeConversation.
/// </summary>
public static class CcaiResultTypes
{
    /// <summary>Conversation summary.</summary>
    public const string Summary = "summary";

    /// <summary>Title generation.</summary>
    public const string Title = "title";

    /// <summary>Field extraction.</summary>
    public const string Fields = "fields";

    /// <summary>Keyword extraction.</summary>
    public const string Keywords = "keywords";

    /// <summary>Service quality inspection.</summary>
    public const string ServiceInspection = "service_inspection";

    /// <summary>Question and solution.</summary>
    public const string QuestionSolution = "question_solution";

    /// <summary>Action items.</summary>
    public const string Actions = "actions";

    /// <summary>Satisfaction analysis.</summary>
    public const string Satisfaction = "satisfaction";

    /// <summary>Emotion detection.</summary>
    public const string EmotionDetection = "emotion_detection";

    /// <summary>QA extraction.</summary>
    public const string QuestionsAndAnswer = "questions_and_answer";

    /// <summary>User profile.</summary>
    public const string UserProfile = "user_profile";

    /// <summary>Category tagging.</summary>
    public const string CategoryTag = "category_tag";

    /// <summary>Custom prompt.</summary>
    public const string CustomPrompt = "custom_prompt";
}
